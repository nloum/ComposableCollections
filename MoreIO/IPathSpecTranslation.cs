using System.Collections.Generic;

namespace MoreIO
{
    public interface IPathSpecTranslation : IEnumerable<CalculatedPathSpecTranslation>
    {
        IIoService IoService { get; }
        PathSpec Source { get; }
        PathSpec Destination { get; }
    }
}
