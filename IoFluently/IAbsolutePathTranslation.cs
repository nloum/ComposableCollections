using System.Collections.Generic;

namespace IoFluently
{
    public interface IAbsolutePathTranslation : IEnumerable<CalculatedAbsolutePathTranslation>
    {
        IAbsolutePathTranslation Invert();
        IIoService IoService { get; }
        IFileOrFolderOrMissingPath Source { get; }
        IFileOrFolderOrMissingPath Destination { get; }
    }
}