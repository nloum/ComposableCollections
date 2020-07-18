using System.Collections.Immutable;

namespace IoFluently.Examples.RemoveDuplicates
{
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
}