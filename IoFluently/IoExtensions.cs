using System;
using System.IO;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using UtilityDisposables;

namespace IoFluently
{
    public static partial class IoExtensions
    {
        public static string ConvertToString(this IFileOrFolderOrMissingPath path)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < path.Components.Count; i++)
            {
                sb.Append(path.Components[i]);
                if (path.Components[i] != path.DirectorySeparator && i + 1 != path.Components.Count &&
                    sb.ToString() != path.DirectorySeparator)
                    sb.Append(path.DirectorySeparator);
            }

            return sb.ToString();
        }
        
        public static MissingPath ExpectMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path is MissingPath missingPath)
            {
                return missingPath;
            }
            
            return path.Collapse(
                file => throw file.AssertExpectedType(PathType.MissingPath),
                folder => throw folder.AssertExpectedType(PathType.MissingPath),
                missingPath =>
                {
                    if (missingPath is MissingPath missingPathObj)
                    {
                        return missingPathObj;
                    }
                    
                    return new MissingPath(missingPath);
                });
        }

        public static AbsolutePath ExpectFileOrFolderOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path is AbsolutePath absolutePath)
            {
                return absolutePath;
            }
            
            return new AbsolutePath(path);
        }

        public static File ExpectFile(this IFileOrFolderOrMissingPath path)
        {
            if (path is File file)
            {
                return file;
            }
            
            return path.Collapse(
                file =>
                {
                    if (file is File fileObj)
                    {
                        return fileObj;
                    }

                    return new File(file);
                },
                folder => throw folder.AssertExpectedType(PathType.File),
                missingPath => throw missingPath.AssertExpectedType(PathType.File));
        }

        public static Folder ExpectFolder(this IFileOrFolderOrMissingPath path)
        {
            if (path is Folder folder)
            {
                return folder;
            }
            
            return path.Collapse(
                file => throw file.AssertExpectedType(PathType.Folder),
                folder =>
                {
                    if (folder is Folder folderObj)
                    {
                        return folderObj;
                    }

                    return new Folder(folder);
                },
                missingPath => throw missingPath.AssertExpectedType(PathType.Folder));
        }

        public static IFileOrFolder ExpectFileOrFolder(this IFileOrFolderOrMissingPath path)
        {
            return path.Collapse(file => (IFileOrFolder)file, folder => folder, missingPath => throw missingPath.AssertExpectedType(PathType.File, PathType.Folder));
        }

        public static IFileOrMissingPath ExpectFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return path.Collapse(
            file => (IFileOrMissingPath)file,
            folder => {
                if (!folder.IoService.CanEmptyDirectoriesExist)
                {
                    switch (folder.IoService.EmptyFolderMode)
                    {
                        case EmptyFolderMode.EmptyFilesAreFolders:
                            return new File(folder);
                        case EmptyFolderMode.AllNonExistentPathsAreFolders:
                            return new MissingPath(folder);
                    }
                }

                throw folder.AssertExpectedType(PathType.File, PathType.MissingPath);
            },
            missingPath => missingPath);
        }

        public static IFolderOrMissingPath ExpectFolderOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return path.Collapse(file => throw file.AssertExpectedType(PathType.Folder, PathType.MissingPath), folder => (IFolderOrMissingPath)folder, missingPath => missingPath);
        }
    
        /// <summary>
        /// Backs up the specified path and then when the IDisposable is disposed of, the backup file is restored, overwriting
        /// any changes that were made to the path. This is useful for making temporary changes to the path.
        /// </summary>
        /// <param name="path">The path that temporary changes will be made to.</param>
        /// <returns>An object that, when disposed of, undoes any changes made to the specified path.</returns>
        public static IDisposable TemporaryChanges(this AbsolutePath path)
        {
            var backupPath = path.IoService.TryWithExtension(path, x => x + ".backup").Value;
            var translation = path.Translate(backupPath);
            translation.IoService.Copy(translation, overwrite: true);

            return new AnonymousDisposable(() => translation.IoService.Move(translation.Invert(), overwrite: true));
        }

        /// <summary>
        /// If <see cref="mainPath"/> exists, then return <see cref="mainPath"/>. Otherwise, return <see cref="fallbackPath"/>.
        /// </summary>
        public static AbsolutePath FallbackTo(this AbsolutePath mainPath, AbsolutePath fallbackPath)
        {
            if (!mainPath.IoService.Exists(mainPath))
            {
                return fallbackPath;
            }

            return mainPath;
        }

        /// <summary>
        /// If <see cref="mainPath"/> doesn't exist, then creates it by copying from <see cref="fallbackPath"/>.
        /// </summary>
        public static AbsolutePath FallbackCopyFrom(this AbsolutePath mainPath, AbsolutePath fallbackPath)
        {
            if (!mainPath.IoService.Exists(mainPath))
            {
                fallbackPath.Copy(mainPath);
            }

            return mainPath;
        }
    
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
    }
}