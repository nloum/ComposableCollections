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
}