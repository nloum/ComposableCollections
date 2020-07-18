using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Serilog;

namespace IoFluently.Examples.RemoveDuplicates
{
    public class RemoveDuplicatesService : IRemoveDuplicatesService
    {
        private readonly ILogger _logger;

        public RemoveDuplicatesService(ILogger logger)
        {
            _logger = logger;
        }

        public DuplicateRemovalPlan FindDuplicates(IEnumerable<AbsolutePath> rootFolders)
        {
            var logPeriodically = new LogPeriodically(TimeSpan.FromSeconds(3));
            _logger.Information("Finding files that may be duplicates based on their size");

            var filesGroupedBySize = new Dictionary<long, List<AbsolutePath>>();
            var totalFileCount = 0;

            _logger.Information("Starting to iterate files");
            
            foreach (var rootFolder in rootFolders)
            {
                foreach (var path in rootFolder.Descendants())
                {
                    if (path.IsFolder())
                    {
                        continue;
                    }

                    var length = path.Length();
                    if (!filesGroupedBySize.ContainsKey(length))
                    {
                        filesGroupedBySize[length] = new List<AbsolutePath>();
                    }

                    if (logPeriodically.HasBeenLongEnough())
                    {
                        _logger.Information("Iterated {Count} files", totalFileCount);
                    }
                    
                    totalFileCount++;
                    filesGroupedBySize[length].Add(path);
                }
            }

            _logger.Information("Done iterating {Count} files", totalFileCount);

            var filesWithNoDuplicatesBySizeCount = 0;
            long filesWithNoDuplicatesBySizeSize = 0;
            
            foreach (var key in filesGroupedBySize.Keys.ToImmutableList())
            {
                if (filesGroupedBySize[key].Count == 1)
                {
                    filesWithNoDuplicatesBySizeCount++;
                    filesWithNoDuplicatesBySizeSize += key;
                    filesGroupedBySize.Remove(key);

                    if (logPeriodically.HasBeenLongEnough())
                    {
                        _logger.Information(
                            "Ignored {Count} files with a total size of {Size} bytes that definitely don't have duplicates because they have a unique size",
                            filesWithNoDuplicatesBySizeCount, filesWithNoDuplicatesBySizeSize);
                    }
                }
            }

            _logger.Information(
                "Done ignoring {Count} files with a total size of {Size} bytes that definitely don't have duplicates because they have a unique size",
                filesWithNoDuplicatesBySizeCount, filesWithNoDuplicatesBySizeSize);

            var filesGroupedByHash = new Dictionary<string, List<AbsolutePath>>();
            var totalHashedCount = 0;
            long totalHashedSize = 0;

            foreach (var possibleDuplicate in filesGroupedBySize.SelectMany(x => x.Value.Select(path => new { path, size = x.Key })))
            {
                var hash = possibleDuplicate.path.Md5().ToHexString();
                if (!filesGroupedByHash.ContainsKey(hash))
                {
                    filesGroupedByHash[hash] = new List<AbsolutePath>();
                }

                filesGroupedByHash[hash].Add(possibleDuplicate.path);
                totalHashedCount++;
                totalHashedSize += possibleDuplicate.size;
                
                if (logPeriodically.HasBeenLongEnough())
                {
                    _logger.Information(
                        "Hashed {Count} files for a total of {Size} bytes",
                        totalHashedCount, totalHashedSize);
                }
            }

            _logger.Information(
                "Done hashing {Count} files for a total of {Size} bytes",
                totalHashedCount, totalHashedSize);

            var result = new DuplicateRemovalPlan(filesGroupedByHash.ToImmutableDictionary(x => x.Key, x => new DuplicateFiles(x.Key, x.Value.ToImmutableDictionary(path => path, _ => DuplicateFileAction.Undecided))));
            
            _logger.Information("Done compiling results");
            
            return result;
        }

        public void Execute(DuplicateRemovalPlan plan)
        {
            foreach (var duplicates in plan.DuplicateFiles)
            {
                foreach (var duplicate in duplicates.Value.Paths)
                {
                    if (duplicate.Value == DuplicateFileAction.Delete)
                    {
                        duplicate.Key.DeleteFile();
                    }
                    _logger.Information("Executed plan {Action} for {Path}", duplicate.Value, duplicate.Key);
                }
            }
        }
    }
}