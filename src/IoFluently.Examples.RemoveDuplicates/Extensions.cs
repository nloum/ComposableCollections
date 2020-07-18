using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IoFluently.Examples.RemoveDuplicates
{
    public static class Extensions
    {
        public static byte[] Md5(this AbsolutePath path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = path.Open(FileMode.Open))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        public static string ToHexString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static IPathWithKnownFormatSync<DuplicateRemovalPlan, DuplicateRemovalPlan> AsDuplicateRemovalPlan(this AbsolutePath x)
        {
            return x.AsPathFormat(path =>
            {
                var result = new List<KeyValuePair<string, DuplicateFiles>>();
                
                string currentHash = null;
                var files = new List<KeyValuePair<AbsolutePath, DuplicateFileAction>>();
                foreach (var line in path.ReadLines())
                {
                    if (line.EndsWith(":"))
                    {
                        if (currentHash != null)
                        {
                            result.Add(new KeyValuePair<string, DuplicateFiles>(currentHash, new DuplicateFiles(currentHash, files.ToImmutableDictionary())));
                            files.Clear();
                        }
                        
                        currentHash = line.Trim(':');
                    }
                    else
                    {
                        var parts = line.Trim().Split(':');
                        files.Add(new KeyValuePair<AbsolutePath, DuplicateFileAction>(x.IoService.ParseAbsolutePath(parts[0].Trim()), Enum.Parse<DuplicateFileAction>(parts[1].Trim())));
                    }
                }
                result.Add(new KeyValuePair<string, DuplicateFiles>(currentHash, new DuplicateFiles(currentHash, files.ToImmutableDictionary())));
                
                return new DuplicateRemovalPlan(result.ToImmutableDictionary());
            }, (path, duplicateRemovalPlan) =>
            {
                using (var writer = path.OpenWriter())
                {
                    foreach (var item in duplicateRemovalPlan.DuplicateFiles)
                    {
                        writer.WriteLine($"{item.Key}:");
                        foreach (var duplicate in item.Value.Paths)
                        {
                            writer.WriteLine($"  {duplicate.Key}: {duplicate.Value}");
                        }
                    }
                }
            });
        }
    }
}