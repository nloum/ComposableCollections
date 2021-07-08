using System;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;
using UtilityDisposables;

namespace IoFluently
{
    public static partial class IoExtensions
    {
        public static Stream Open(this MissingPath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.SequentialScan | FileOptions.Asynchronous,
            Nullable<Information> bufferSize = null, Boolean createRecursively = false)
        {
            return path.ExpectFileOrMissingPath()
                .Open(fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public static File CopyFrom(this FileOrMissingPath fileOrMissingPath, Stream sourceStream)
        {
            using var targetStream = fileOrMissingPath.Open(FileMode.Create, FileAccess.Write, FileShare.None,
                FileOptions.None, createRecursively: true);
            sourceStream.CopyTo(targetStream);
            return new File(fileOrMissingPath);
        }
        
        public static async Task<File> CopyFromAsync(this FileOrMissingPath fileOrMissingPath, Stream sourceStream,
            CancellationToken cancellationToken)
        {
            using var targetStream = fileOrMissingPath.Open(FileMode.Create, FileAccess.Write, FileShare.None,
                FileOptions.None, createRecursively: true);
            await sourceStream.CopyToAsync(targetStream, cancellationToken);
            return new File(fileOrMissingPath);
        }
        
        public static File CopyFrom(this MissingPath fileOrMissingPath, Stream sourceStream)
        {
            using var targetStream = fileOrMissingPath.Open(FileMode.Create, FileAccess.Write, FileShare.None,
                FileOptions.None, createRecursively: true);
            sourceStream.CopyTo(targetStream);
            return new File(fileOrMissingPath);
        }
        
        public static async Task<File> CopyFromAsync(this MissingPath fileOrMissingPath, Stream sourceStream,
            CancellationToken cancellationToken)
        {
            using var targetStream = fileOrMissingPath.Open(FileMode.Create, FileAccess.Write, FileShare.None,
                FileOptions.None, createRecursively: true);
            await sourceStream.CopyToAsync(targetStream, cancellationToken);
            return new File(fileOrMissingPath);
        }
    
        /// <summary>
        /// Backs up the specified path and then when the IDisposable is disposed of, the backup file is restored, overwriting
        /// any changes that were made to the path. This is useful for making temporary changes to the path.
        /// </summary>
        /// <param name="path">The path that temporary changes will be made to.</param>
        /// <returns>An object that, when disposed of, undoes any changes made to the specified path.</returns>
        public static IDisposable TemporaryChanges(this IFileOrFolderOrMissingPath path)
        {
            var backupPath = path.IoService.TryWithExtension(path, x => x + ".backup").Value;
            var translation = path.IoService.Translate(path, backupPath);
            translation.IoService.Copy(translation, overwrite: true);

            return new AnonymousDisposable(() => translation.IoService.Move(translation.Invert(), overwrite: true));
        }

        /// <summary>
        /// If <see cref="mainPath"/> exists, then return <see cref="mainPath"/>. Otherwise, return <see cref="fallbackPath"/>.
        /// </summary>
        public static THasAbsolutePath FallbackTo<THasAbsolutePath>(this THasAbsolutePath mainPath, THasAbsolutePath fallbackPath, bool copy = false)
            where THasAbsolutePath : IFileOrFolderOrMissingPath
        {
            if (!mainPath.IoService.Exists(mainPath))
            {
                if (copy)
                {
                    fallbackPath.IoService.Copy(fallbackPath, mainPath).Copy();
                }
                else
                {
                    return fallbackPath;
                }
            }

            return mainPath;
        }

        /// <summary>
        /// Don't use this method, instead use the / operator on AbsolutePath objects (and related objects) to write
        /// idiomatic code with IoFluently.
        /// </summary>
        [Obsolete("Use the / operator on AbsolutePath objects (and related objects) to write idiomatic code with IoFluently", true)]
        public static FileOrFolderOrMissingPath Combine(this FileOrFolderOrMissingPath path, string subpath)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Don't use this method, instead use the / operator on AbsolutePath objects (and related objects) to write
        /// idiomatic code with IoFluently.
        /// </summary>
        [Obsolete("Use the / operator on RelativePath objects (and related objects) to write idiomatic code with IoFluently", true)]
        public static FileOrFolderOrMissingPath Combine(this RelativePath path, string subpath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Don't use this method, instead use the / operator on AbsolutePath objects (and related objects) to write
        /// idiomatic code with IoFluently.
        /// </summary>
        [Obsolete("Use the / operator on AbsolutePath objects (and related objects) to write idiomatic code with IoFluently", true)]
        public static FileOrFolderOrMissingPath Combine(this IIoService path, string subpath)
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
    }
}