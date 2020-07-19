using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Serilog;
using UnitsNet;
using UnitsNet.Units;

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
            var totalFileSize = Information.Zero;

            foreach (var rootFolder in rootFolders)
            {
                foreach (var path in rootFolder.Descendants())
                {
                    if (path.IsFolder())
                    {
                        continue;
                    }

                    var size = path.FileSize();
                    totalFileSize += size;
                    if (!filesGroupedBySize.ContainsKey(size))
                    {
                        filesGroupedBySize[size] = new List<AbsolutePath>();
                    }

                    if (logPeriodically.HasBeenLongEnough())
                    {
                        _logger.Information("Step {CurrentStep}: iterated {Size} in {Count:N0} files", 1, ConvertToOptimalUnit(totalFileSize), totalFileCount);
                    }
                    
                    totalFileCount++;
                    filesGroupedBySize[size].Add(path);
                }
            }

            _logger.Information("Step {CurrentStep}: done iterating {Size} in {Count:N0} files", 1, ConvertToOptimalUnit(totalFileSize), totalFileCount);

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
                            "Step {CurrentStep} {Percentage}%: ignored {Size} in {Count:N0} files that definitely don't have duplicates because they have a unique size",
                            2, percentage, ConvertToOptimalUnit(filesWithNoDuplicatesBySizeSize), filesWithNoDuplicatesBySizeCount);
                    }
                }
            }

            _logger.Information(
                "Step {CurrentStep} {Percentage}%: done ignoring {Size} in {Count:N0} files that definitely don't have duplicates because they have a unique size",
                2, 100, ConvertToOptimalUnit(filesWithNoDuplicatesBySizeSize), filesWithNoDuplicatesBySizeCount);

            var filesGroupedByHash = new Dictionary<string, List<Tuple<AbsolutePath, Information>>>();
            var totalHashedCount = 0;
            var totalHashedSize = Information.Zero;
            counter = 0;

            foreach (var possibleDuplicates in filesGroupedBySize)
            {
                counter++;
                foreach (var possibleDuplicate in possibleDuplicates.Value.Select(path => new {path, size = possibleDuplicates.Key}))
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
                        var percentage = Math.Round(counter / (double) filesGroupedBySize.Count * 100, 2);
                        _logger.Information(
                            "Step {CurrentStep} {Percentage}%: hashed {Size} in {Count:N0} files",
                            3, percentage, ConvertToOptimalUnit(totalHashedSize), totalHashedCount);
                    }
                }
            }

            _logger.Information(
                "Step {CurrentStep} {Percentage}%: done hashing {Size} in {Count:N0} files",
                3, 100, ConvertToOptimalUnit(totalHashedSize), totalHashedCount);
            
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
                            "Step {CurrentStep} {Percentage}%: ignored {Size} in {Count:N0} files that definitely don't have duplicates because they have a unique hash",
                            4, percentage, ConvertToOptimalUnit(filesWithNoDuplicatesByHashSize), filesWithNoDuplicatesByHashCount);
                    }
                }
            }
            
            _logger.Information(
                "Step {CurrentStep} {Percentage}%: done ignoring {Size} in {Count:N0} files that definitely don't have duplicates because they have a unique hash",
                4, 100, ConvertToOptimalUnit(filesWithNoDuplicatesByHashSize), filesWithNoDuplicatesByHashCount);

            var result = new DuplicateRemovalPlan(filesGroupedByHash.ToImmutableDictionary(x => x.Key, x => new DuplicateFiles(x.Key, x.Value.ToImmutableDictionary(path => path.Item1, _ => DuplicateFileAction.Undecided))));
            
            _logger.Information("Step {CurrentStep}: done compiling results", 5);
            
            return result;
        }

        private Information ConvertToOptimalUnit(Information info)
        {
            if (info > Information.FromTerabytes(1))
            {
                return info.ToUnit(InformationUnit.Terabyte);
            }
            
            if (info > Information.FromGigabytes(1))
            {
                return info.ToUnit(InformationUnit.Gigabyte);
            }

            if (info > Information.FromMegabytes(1))
            {
                return info.ToUnit(InformationUnit.Megabyte);
            }
            
            if (info > Information.FromKilobytes(1))
            {
                return info.ToUnit(InformationUnit.Kilobyte);
            }

            return info;
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