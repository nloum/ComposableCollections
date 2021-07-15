using System.Collections.Generic;

namespace IoFluently
{
    public interface IAbsolutePathTranslation : IEnumerable<CalculatedAbsolutePathTranslation>
    {
        IAbsolutePathTranslation Invert();
        IFileSystem FileSystem { get; }
        AbsolutePath Source { get; }
        AbsolutePath Destination { get; }
    }
}