using System.Collections.Generic;

namespace IoFluently.Examples.RemoveDuplicates
{
    public interface IRemoveDuplicatesService
    {
        DuplicateRemovalPlan FindDuplicates(IEnumerable<AbsolutePath> rootFolders);
        void Execute(DuplicateRemovalPlan plan);
    }
}