using System.Collections.Generic;

namespace IoFluently
{
    /// <summary>
    /// Represents lines of text being read from a text file.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// The lines of text from the text file
        /// </summary>
        public IEnumerable<string> Lines { get; }

        /// <summary>
        /// Constructs an object representing the contents of a text file
        /// </summary>
        /// <param name="lines">The lines to be read from the text file</param>
        public Text(IEnumerable<string> lines)
        {
            Lines = lines;
        }
    }
}