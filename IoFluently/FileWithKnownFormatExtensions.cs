using System.Collections.Generic;

namespace IoFluently
{
    public static class FileWithKnownFormatExtensions
    {
        public static FileWithKnownFormatSync<Text, Text> AsTextFile(this AbsolutePath path)
        {
            return new FileWithKnownFormatSync<Text, Text>(path, absPath =>
            {
                return new Text(absPath.ReadLines());
            }, (absPath, text) =>
            {
                absPath.WriteAllLines(text.Lines);
            });
        }

        public static Text AsText(this IEnumerable<string> lines)
        {
            return new Text(lines);
        }
    }
}