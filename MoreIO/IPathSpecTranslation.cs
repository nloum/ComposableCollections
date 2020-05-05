using System.Collections.Generic;

namespace MoreIO
{
    public interface IAbsolutePathTranslation : IEnumerable<CalculatedAbsolutePathTranslation>
    {
        IIoService IoService { get; }
        AbsolutePath Source { get; }
        AbsolutePath Destination { get; }
    }
}