using System.Collections.Generic;

namespace IoFluently
{
    public interface IAbsolutePathTranslation : IEnumerable<CalculatedAbsolutePathTranslation>
    {
        IAbsolutePathTranslation Invert();
        IIoService IoService { get; }
        AbsolutePath Source { get; }
        AbsolutePath Destination { get; }
    }
}