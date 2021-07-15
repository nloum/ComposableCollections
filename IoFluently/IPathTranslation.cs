using System.Collections.Generic;

namespace IoFluently
{
    public interface IPathTranslation : IEnumerable<CalculatedPathTranslation>
    {
        IPathTranslation Invert();
        IFileSystem FileSystem { get; }
        FileOrFolderOrMissingPath Source { get; }
        FileOrFolderOrMissingPath Destination { get; }
    }
}