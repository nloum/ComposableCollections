using System.Collections.Generic;

namespace MoreIO
{
    public interface IFileUriTranslation : IEnumerable<CalculatedFileUriTranslation>
    {
        PathSpec Source { get; }
        PathSpec Destination { get; }
    }
}
