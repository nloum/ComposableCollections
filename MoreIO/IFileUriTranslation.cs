using System.Collections.Generic;

namespace MoreIO
{
    public interface IFileUriTranslation : IEnumerable<CalculatedFileUriTranslation>
    {
        IIoService IoService { get; }
        PathSpec Source { get; }
        PathSpec Destination { get; }
    }
}
