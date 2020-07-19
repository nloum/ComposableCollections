using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Serilog;
using UnitsNet;

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

            var filesGroupedBySize = new Dictionary<Information, List<AbsolutePath>>();
            var totalFileCount = 0;

            foreach (var rootFolder in rootFolders)
            {
                foreach (var path in rootFolder.Descendants())
                {
                    if (path.IsFolder())
                    {
                        continue;
                    }

                    var length = path.FileSize();
                    if (!filesGroupedBySize.ContainsKey(length))
                    {
                        filesGroupedBySize[length] = new List<AbsolutePath>();
                    }

                    if (logPeriodically.HasBeenLongEnough())
                    {
                        _logger.Information("Step {CurrentStep}: iterated {Count} files", 1, totalFileCount);
                    }
                    
                    totalFileCount++;
                    filesGroupedBySize[length].Add(path);
                }
            }

            _logger.Information("Step {CurrentStep}: done iterating {Count} files", 1, totalFileCount);

            var filesWithNoDuplicatesBySizeCount = 0;
            var filesWithNoDuplicatesBySizeSize = Information.Zero;
            var counter = 0;
            
            foreach (var key in filesGroupedBySize.Keys.ToImmutableList())
            {
                counter++;
                if (filesGroupedBySize[key].Count == 1)
                {
                    filesWithNoDuplicatesBySizeCount++;
                    filesWithNoDuplicatesBySizeSize += key;
                    filesGroupedBySize.Remove(key);

                    if (logPeriodically.HasBeenLongEnough())
                    {
                        var percentage = Math.Round(counter / (double) filesGroupedBySize.Count * 100, 2);
                        _logger.Information(
                            "Step {CurrentStep} {Percentage}%: ignored {Count} files with a total size of {Size} that definitely don't have duplicates because they have a unique size",
                            2, percentage, filesWithNoDuplicatesBySizeCount, filesWithNoDuplicatesBySizeSize);
                    }
                }
            }

            _logger.Information(
                "Step {CurrentStep} {Percentage}%: done ignoring {Count} files with a total size of {Size} that definitely don't have duplicates because they have a unique size",
                2, 100, filesWithNoDuplicatesBySizeCount, filesWithNoDuplicatesBySizeSize);

            var filesGroupedByHash = new Dictionary<string, List<Tuple<AbsolutePath, Information>>>();
            var totalHashedCount = 0;
            var totalHashedSize = Information.Zero;

            foreach (var possibleDuplicate in filesGroupedBySize.SelectMany(x => x.Value.Select(path => new { path, size = x.Key })))
            {
                var hash = possibleDuplicate.path.Md5().ToHexString();
                if (!filesGroupedByHash.ContainsKey(hash))
                {
                    filesGroupedByHash[hash] = new List<Tuple<AbsolutePath, Information>>();
                }

                filesGroupedByHash[hash].Add(Tuple.Create(possibleDuplicate.path, possibleDuplicate.size));
                totalHashedCount++;
                totalHashedSize += possibleDuplicate.size;
                
                if (logPeriodically.HasBeenLongEnough())
                {
                    var percentage = Math.Round(filesGroupedByHash.Count / (double) filesGroupedBySize.Count * 100, 2);
                    _logger.Information(
                        "Step {CurrentStep} {Percentage}%: hashed {Count} files for a total of {Size}",
                        3, percentage, totalHashedCount, totalHashedSize);
                }
            }

            _logger.Information(
                "Step {CurrentStep} {Percentage}%: done hashing {Count} files for a total of {Size}",
                3, 100, totalHashedCount, totalHashedSize);
            
            var filesWithNoDuplicatesByHashCount = 0;
            var filesWithNoDuplicatesByHashSize = Information.Zero;
            counter = 0;
            
            foreach (var key in filesGroupedByHash.Keys.ToImmutableList())
            {
                counter++;
                if (filesGroupedByHash[key].Count == 1)
                {
                    filesWithNoDuplicatesByHashCount++;
                    filesWithNoDuplicatesByHashSize += filesGroupedByHash[key][0].Item2;
                    filesGroupedByHash.Remove(key);

                    if (logPeriodically.HasBeenLongEnough())
                    {
                        var percentage = Math.Round(counter / (double) filesGroupedByHash.Count * 100, 2);
                        _logger.Information(
                            "Step {CurrentStep} {Percentage}%: ignored {Count} files with a total size of {Size} that definitely don't have duplicates because they have a unique hash",
                            4, percentage, filesWithNoDuplicatesByHashCount, filesWithNoDuplicatesByHashSize);
                    }
                }
            }
            
            _logger.Information(
                "Step {CurrentStep} {Percentage}%: done ignoring {Count} files with a total size of {Size} that definitely don't have duplicates because they have a unique hash",
                4, 100, filesWithNoDuplicatesByHashCount, filesWithNoDuplicatesByHashSize);

            var result = new DuplicateRemovalPlan(filesGroupedByHash.ToImmutableDictionary(x => x.Key, x => new DuplicateFiles(x.Key, x.Value.ToImmutableDictionary(path => path.Item1, _ => DuplicateFileAction.Undecided))));
            
            _logger.Information("Step {CurrentStep}: done compiling results", 5);
            
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