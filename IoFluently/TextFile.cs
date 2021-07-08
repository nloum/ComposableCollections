using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleMonads;
using UnitsNet;

namespace IoFluently
{
    public static class TextFileExtensions
    {
        public static TextFile ExpectTextFile(this IFileOrFolderOrMissingPath path)
        {
            return new TextFile(path);
        }

        public static TextFileOrMissingPath ExpectTextFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path.IsFile)
            {
                return new TextFileOrMissingPath(new TextFile(path));
            }
            
            return new TextFileOrMissingPath(new MissingPath(path));
        }
        
        public static StreamWriter OpenWriter(this IFileOrMissingPath fileOrMissingPath, FileOptions fileOptions = FileOptions.WriteThrough, Encoding encoding = null, Information? bufferSize = default,
            bool createRecursively = false)
        {
            var stream = fileOrMissingPath.IoService.Open(fileOrMissingPath, FileMode.Create, FileAccess.Write, FileShare.None,
                fileOptions, bufferSize, createRecursively);
            if (encoding == null)
            {
                return new StreamWriter(stream);
            }
            else
            {
                return new StreamWriter(stream, encoding);
            }
        }

        public static TextFile WriteAllLines(this IFileOrMissingPath fileOrMissingPath, IEnumerable<string> lines, string newline = null, Encoding encoding = null, Information? bufferSize = default, bool createRecursively = false)
        {
            newline ??= Environment.NewLine;
            
            var stream = fileOrMissingPath.IoService.Open(fileOrMissingPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Write, FileOptions.WriteThrough, bufferSize, createRecursively);
            foreach (var line in lines)
            {
                var bytes = Encoding.Default.GetBytes(line + newline);
                stream.Write(bytes, 0, bytes.Length);
            }

            return new TextFile(fileOrMissingPath);
        }

        public static TextFile WriteAllText(this IFileOrMissingPath fileOrMissingPath, string text, Encoding encoding = null, bool createRecursively = false)
        {
            encoding ??= Encoding.Default;
            using var writer = OpenWriter(fileOrMissingPath, FileOptions.None, encoding, Information.FromBytes(Math.Max(encoding.GetByteCount(text), 1)), createRecursively);
            writer.Write(text);

            return new TextFile(fileOrMissingPath);
        }
    }
    
    public class TextFile : File
    {
        public TextFile(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public StreamWriter OpenWriter(FileOptions fileOptions = FileOptions.WriteThrough, Encoding encoding = null, Information? bufferSize = default,
            bool createRecursively = false)
        {
            return this.ExpectTextFileOrMissingPath()
                .OpenWriter(fileOptions, encoding, bufferSize, createRecursively);
        }

        public virtual TextFile WriteAllLines(IEnumerable<string> lines, string newline = null, Encoding encoding = null, Information? bufferSize = default, bool createRecursively = false)
        {
            return this.ExpectTextFileOrMissingPath()
                .WriteAllLines(lines, newline, encoding, bufferSize, createRecursively);
        }

        public TextFile WriteAllText(string text, Encoding encoding = null, bool createRecursively = false)
        {
            return this.ExpectTextFileOrMissingPath()
                .WriteAllText(text, encoding, createRecursively);
        }
        
        /// <inheritdoc />
        public StreamReader OpenReader(FileOptions fileOptions = FileOptions.SequentialScan, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, Information? bufferSize = default)
        {
            var stream = IoService.Open(this, FileMode.Open, FileAccess.Read, FileShare.Read,
                FileOptions.SequentialScan, bufferSize);
            if (encoding == null)
            {
                return new StreamReader(stream, detectEncodingFromByteOrderMarks);
            }

            return new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks);
        }

        /// <inheritdoc />
        public Encoding GetEncoding()
        {
            using(var stream = IoService.Open(this, FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.SequentialScan)) {
                using (var reader = new StreamReader(stream, detectEncodingFromByteOrderMarks: true))
                {
                    var line = reader.ReadLine();
                    return reader.CurrentEncoding;
                }
            }
        }

        /// <inheritdoc />
        public IMaybe<string> TryGetNewLine()
        {
            using var stream = IoService.Open(this, FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.SequentialScan);
            while (true)
            {
                var data = stream.ReadByte();
                if (data == -1)
                {
                    return Maybe<string>.Nothing();
                }

                if (data == '\r')
                {
                    return "\r\n".ToMaybe();
                }

                if (data == '\n')
                {
                    return "\n".ToMaybe();
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<Line> ReadLines(Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, Information? bufferSize = default, ulong startingByteOffset = 0)
        {
            if (detectEncodingFromByteOrderMarks)
            {
                encoding ??= GetEncoding();
            }
            
            if (encoding == null && !detectEncodingFromByteOrderMarks)
            {
                throw new ArgumentException(nameof(detectEncodingFromByteOrderMarks));
            }

            using var stream = IoService.Open(this, FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.SequentialScan, bufferSize);
            if (startingByteOffset > (ulong) stream.Length)
            {
                startingByteOffset = (ulong) stream.Length;
            }
            
            if (startingByteOffset != 0)
            {
                stream.Seek((long)startingByteOffset, SeekOrigin.Begin);
            }

            ulong lineNumber = 1;

            var lastLine = new List<Line>();
            
            foreach (var buffer in new StreamStringEnumerator(stream, startingByteOffset, (ulong) encoding.GetMaxCharCount((int)startingByteOffset), IoService.GetBufferSizeOrDefaultInBytes(bufferSize), encoding))
            {
                var innerEnumerator = new LineSplitEnumerator(buffer.Value,
                    buffer.ByteOffsetOfStart, buffer.CharOffsetOfStart,
                    lineNumber, encoding);

                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                lastLine.Add(innerEnumerator.Current);
                
                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                do
                {
                    if (lastLine.Count == 1)
                    {
                        yield return lastLine[0];
                        lastLine.Clear();
                    }
                    else
                    {
                        yield return lastLine.Combine();
                        lastLine.Clear();
                    }
                    lastLine.Add(innerEnumerator.Current);
                } while (innerEnumerator.MoveNext());
            }
            
            if (lastLine.Count == 1)
            {
                yield return lastLine[0];
                lastLine.Clear();
            }
            else if (lastLine.Count > 1)
            {
                yield return lastLine.Combine();
                lastLine.Clear();
            }
        }

        /// <inheritdoc />
        public IEnumerable<Line> ReadLinesBackwards(Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            FileShare fileShare = FileShare.Read, Information? bufferSize = default,
            ulong startingByteOffset = UInt64.MaxValue)
        {
            if (detectEncodingFromByteOrderMarks)
            {
                encoding ??= GetEncoding();
            }

            if (encoding == null && !detectEncodingFromByteOrderMarks)
            {
                throw new ArgumentException(nameof(detectEncodingFromByteOrderMarks));
            }
            
            using var stream = IoService.Open(this, FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.RandomAccess, bufferSize);
            if (startingByteOffset > (ulong) stream.Length)
            {
                startingByteOffset = (ulong) stream.Length;
            }
            
            if (startingByteOffset != 0)
            {
                stream.Seek((long)startingByteOffset, SeekOrigin.Begin);
            }

            ulong lineNumber = 1;

            var lastLine = new List<Line>();
            
            foreach (var buffer in new ReverseStreamStringEnumerator(stream, startingByteOffset, (ulong) encoding.GetMaxCharCount((int)startingByteOffset), IoService.GetBufferSizeOrDefaultInBytes(bufferSize), encoding))
            {
                var innerEnumerator = new ReverseLineSplitEnumerator(buffer.Value,
                    buffer.ByteOffsetOfStart, buffer.CharOffsetOfStart,
                    lineNumber, encoding);

                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                lastLine.Add(innerEnumerator.Current);
                
                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                do
                {
                    if (lastLine.Count == 1)
                    {
                        yield return lastLine[0];
                        lastLine.Clear();
                    }
                    else
                    {
                        yield return lastLine.Combine();
                        lastLine.Clear();
                    }
                    lastLine.Add(innerEnumerator.Current);
                } while (innerEnumerator.MoveNext());
            }
            
            if (lastLine.Count == 1)
            {
                yield return lastLine[0];
                lastLine.Clear();
            }
            else if (lastLine.Count > 1)
            {
                yield return lastLine.Combine();
                lastLine.Clear();
            }
        }
        
        /// <inheritdoc />
        // public virtual string ReadAllText(TextFile textFile, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
        //     Information? bufferSize = default)

        public string ReadAllText(FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            Information? bufferSize = default)
        {
            using var reader = OpenReader(FileOptions.SequentialScan, encoding, detectEncodingFromByteOrderMarks, bufferSize);
            return reader.ReadToEnd();
        }
    }

    // public class MissingTextFile : MissingPath
    // {
    //     public MissingTextFile(AbsolutePath Path) : base(Path)
    //     {
    //     }
    //
    //     public StreamWriter OpenWriter(FileOptions fileOptions = FileOptions.WriteThrough, Encoding encoding = null, Information? bufferSize = default,
    //         bool createRecursively = false)
    //     {
    //         return this.ExpectTextFileOrMissingPath()
    //             .OpenWriter(fileOptions, encoding, bufferSize, createRecursively);
    //     }
    //
    //     public virtual TextFile WriteAllLines(IEnumerable<string> lines, string newline = null, Encoding encoding = null, Information? bufferSize = default, bool createRecursively = false)
    //     {
    //         return this.ExpectTextFileOrMissingPath()
    //             .WriteAllLines(lines, newline, encoding, bufferSize, createRecursively);
    //     }
    //
    //     public TextFile WriteAllText(string text, Encoding encoding = null, bool createRecursively = false)
    //     {
    //         return this.ExpectTextFileOrMissingPath()
    //             .WriteAllText(text, encoding, createRecursively);
    //     }
    // }
}