using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoFluently
{
    internal static class InternalExtensions
    {
        /// <summary>
        ///     Remove all elements in the specified list that cause the predicate to return true.
        /// </summary>
        public static int RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var howManyHaveBeenRemoved = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    i--;
                    howManyHaveBeenRemoved++;
                }
            }
            return howManyHaveBeenRemoved;
        }        
        
        /// <summary>
        ///     Concatenates an array with item (the this parameter) as its only element, then ts.
        /// </summary>
        public static IEnumerable<T> ItemConcat<T>(this T item, IEnumerable<T> ts)
        {
            return new[] {item}.Concat(ts);
        }
        
        public static string GetCommonBeginning(this string str1, string str2)
        {
            int i;
            for (i = 0; i < Math.Min(str1.Length, str2.Length) && str1[i] == str2[i]; i++) ;
            if (i == str1.Length)
                return str1;
            return str1.Substring(0, i);
        }
        
        public static Line ToLine(this LineSplitEntry lineSplitEntry, Encoding encoding)
        {
            return new Line(
                new string(lineSplitEntry.Line),
                new string(lineSplitEntry.Separator),
                lineSplitEntry.ByteOffsetOfStart, lineSplitEntry.CharOffsetOfStart, lineSplitEntry.LineNumber,
                encoding);
        }

        public static Line Combine(this IReadOnlyList<Line> parts)
        {
            return new Line(
                String.Create(parts.Select(x => x.Value.Length).Sum(), parts,
                    (newString, lastLine) =>
                    {
                        var position = 0;
                        foreach (var part in lastLine)
                        {
                            part.Value.AsSpan().CopyTo(newString.Slice(position, part.Value.Length));
                        }
                    }),
                parts[parts.Count - 1].Separator,
                parts[0].ByteOffsetOfStart, parts[0].CharOffsetOfStart, parts[0].LineNumber,
                parts[0].Encoding);
        }
    }
}