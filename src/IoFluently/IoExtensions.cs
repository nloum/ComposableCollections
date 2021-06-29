using System;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace IoFluently
{
    public static partial class IoExtensions
    {
        /// <summary>
        /// Don't use this method, instead use the / operator on AbsolutePath objects (and related objects) to write
        /// idiomatic code with IoFluently.
        /// </summary>
        [Obsolete("Use the / operator on AbsolutePath objects (and related objects) to write idiomatic code with IoFluently", true)]
        public static AbsolutePath Combine(this AbsolutePath path, string subpath)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Don't use this method, instead use the / operator on AbsolutePath objects (and related objects) to write
        /// idiomatic code with IoFluently.
        /// </summary>
        [Obsolete("Use the / operator on RelativePath objects (and related objects) to write idiomatic code with IoFluently", true)]
        public static AbsolutePath Combine(this RelativePath path, string subpath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Don't use this method, instead use the / operator on AbsolutePath objects (and related objects) to write
        /// idiomatic code with IoFluently.
        /// </summary>
        [Obsolete("Use the / operator on AbsolutePath objects (and related objects) to write idiomatic code with IoFluently", true)]
        public static AbsolutePath Combine(this IIoService path, string subpath)
        {
            throw new NotImplementedException();
        }

        public static IObservable<string> ObserveLines(this StreamReader reader)
        {
            return new StreamReaderObservableAdapter(reader, new[]{'\n'});
        }

        public static IObservable<string> Observe(this StreamReader reader, char[] terminators)
        {
            return new StreamReaderObservableAdapter(reader, terminators);
        }

        public static IObservable<string> ObserveBlocks(this StreamReader reader, int size)
        {
            return new StreamReaderObservableAdapter(reader, size);
        }
        
        public static IObservable<string> ToLines(this IObservable<char> characters, char splitCharacter = '\n')
        {
            return characters.Scan(new {StringBuilder = new StringBuilder(), BuiltString = (string) null},
                (state, ch) =>
                {
                    if (ch == splitCharacter)
                    {
                        return new {StringBuilder = new StringBuilder(), BuiltString = state.StringBuilder.ToString()};
                    }

                    state.StringBuilder.Append(ch);
                    return new {state.StringBuilder, BuiltString = (string) null};
                }).Where(state => state.BuiltString != null).Select(state => state.BuiltString);
        }

        /// <summary>
        /// Parses the path. If the path is a relative path, assumes that it is relative to <see cref="IoService.DefaultRelativePathBase"/>.
        /// </summary>
        public static AbsolutePath ParsePathRelativeToDefault(this IIoService ioService, string path)
        {
            return ioService.TryParseAbsolutePath(path, ioService.DefaultRelativePathBase).Value;
        }
    }
}