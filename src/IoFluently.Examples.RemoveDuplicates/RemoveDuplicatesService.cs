using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Autofac.Core;
using Serilog;

namespace IoFluently.Examples.RemoveDuplicates
{
    public interface IRemoveDuplicatesService
    {
        DuplicateRemovalPlan FindDuplicates(IEnumerable<AbsolutePath> rootFolders);
        void Execute(DuplicateRemovalPlan plan);
    }

    public class RemoveDuplicatesService : IRemoveDuplicatesService
    {
        private readonly ILogger _logger;

        public RemoveDuplicatesService(ILogger logger)
        {
            _logger = logger;
        }

        public DuplicateRemovalPlan FindDuplicates(IEnumerable<AbsolutePath> rootFolders)
        {
            var filesBySize = rootFolders.SelectMany(x => x.Descendants())
                .Where(x => x.IsFile())
                .GroupBy(x => x.Length())
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableHashSet());

            var possibleDuplicates = filesBySize.Where(x => x.Value.Count > 1);

            var duplicates = possibleDuplicates
                .OrderByDescending(x => x.Key)
                .SelectMany(x =>
                    x.Value.GroupBy(path => path.Md5().ToHexString())
                        .ToImmutableDictionary(kvp => kvp.Key, kvp =>
                        {
                            var actions = kvp.ToImmutableDictionary(x => x, _ => DuplicateFileAction.Undecided);
                            var wastedSpaceInBytes = x.Key * (actions.Count - 1);
                            return new DuplicateFiles(kvp.Key, actions);
                        }))
                .ToImmutableDictionary();
            
            return new DuplicateRemovalPlan(duplicates);
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

    public class DuplicateRemovalPlan
    {
        public DuplicateRemovalPlan(ImmutableDictionary<string, DuplicateFiles> duplicateFiles)
        {
            DuplicateFiles = duplicateFiles;
        }

        public ImmutableDictionary<string, DuplicateFiles> DuplicateFiles { get; }
    }

    public class DuplicateFiles
    {
        public DuplicateFiles(string mdh5Hash, ImmutableDictionary<AbsolutePath, DuplicateFileAction> paths)
        {
            Mdh5Hash = mdh5Hash;
            Paths = paths;
        }

        public string Mdh5Hash { get; }
        public ImmutableDictionary<AbsolutePath, DuplicateFileAction> Paths { get; }
    }

    public enum DuplicateFileAction
    {
        Undecided,
        Delete,
        Keep,
    }
}