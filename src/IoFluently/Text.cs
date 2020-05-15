using System.Collections.Generic;

namespace IoFluently
{
    public class Text
    {
        public IEnumerable<string> Lines { get; }

        public Text(IEnumerable<string> lines)
        {
            Lines = lines;
        }
    }
}