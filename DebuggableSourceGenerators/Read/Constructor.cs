using System.Collections.Generic;
using System.Linq;

namespace DebuggableSourceGenerators.Read
{
    public class Constructor
    {
        public IReadOnlyList<Parameter> Parameters { get; init; }
        public Visibility Visibility { get; init; }
        public override string ToString()
        {
            return $"{Visibility} constructor ({string.Join(", ", Parameters.Select(x => x.ToString()))});";
        }
    }
}