using System.Collections.Generic;

namespace IoFluently
{
    public interface IAbsolutePathTranslation : IEnumerable<CalculatedAbsolutePathTranslation>
    {
        IAbsolutePathTranslation Invert();
        IIoService IoService { get; }
        IAbsolutePath Source { get; }
        IAbsolutePath Destination { get; }
    }
}