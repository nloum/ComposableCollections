using System.Collections.Immutable;

namespace IoFluently.Examples.RemoveDuplicates
{
    public class DuplicateRemovalPlan
    {
        public DuplicateRemovalPlan(ImmutableDictionary<string, DuplicateFiles> duplicateFiles)
        {
            DuplicateFiles = duplicateFiles;
        }

        public ImmutableDictionary<string, DuplicateFiles> DuplicateFiles { get; }
    }
}