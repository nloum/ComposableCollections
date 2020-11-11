using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}