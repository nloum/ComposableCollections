using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ComposableCollections.Dictionary.Sources;
using LiveLinq;
using LiveLinq.Set;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using MoreCollections;
using SimpleMonads;
using TreeLinq;
using UnitsNet;
using static SimpleMonads.Utility;

namespace IoFluently
{
    public abstract class FileSystemBase : IFileSystem
    {
        #region Environmental stuff

        /// <inheritdoc />
        public abstract IObservableReadOnlySet<FolderPath> Roots { get; }

        /// <inheritdoc />
        public abstract void UpdateRoots();

        public abstract FolderPath DefaultRoot { get; }

        public abstract EmptyFolderMode EmptyFolderMode { get; }
        /// <inheritdoc />
        public abstract bool CanEmptyDirectoriesExist { get; }

        /// <inheritdoc />
        public string DefaultDirectorySeparator { get; }
        /// <inheritdoc />
        public bool IsCaseSensitiveByDefault { get; }
        /// <summary>
        /// The default buffer size for this <see cref="IFileSystem"/>, used in calls where the buffer size is an optional
        /// parameter.
        /// </summary>
        public Information DefaultBufferSize { get; set; } = Information.FromBytes(4096);

        public int GetBufferSizeOrDefaultInBytes(Information? bufferSize)
        {
            if (bufferSize != null)
            {
                return (int)Math.Round(bufferSize.Value.Bytes);
            }

            return (int)Math.Round(DefaultBufferSize.Bytes);
        }

        protected static string GetDefaultDirectorySeparatorForThisEnvironment()
        {
            var path = Path.Combine("a", "b");
            var result = path.Substring(1, path.Length - 2);
            return result;
        }

        protected static bool ShouldBeCaseSensitiveByDefault()
        {
            var file = Path.GetTempFileName();
            var caseSensitive = System.IO.File.Exists(file.ToLower()) && System.IO.File.Exists(file.ToUpper());
            System.IO.File.Delete(file);
            return caseSensitive;
        }
        
        #endregion
        #region Creating
        
        /// <inheritdoc />
        public abstract FolderPath CreateFolder(IMissingPath path,  bool createRecursively = true);

        #endregion
        #region Deleting
        
        /// <inheritdoc />
        public abstract MissingPath DeleteFolder(IFolderPath folderPath,  bool recursive = true);

        /// <inheritdoc />
        public abstract MissingPath DeleteFile(IFilePath path);

        /// <inheritdoc />
        public virtual MissingPath Delete(IFileOrFolderPath path, bool recursiveDeleteIfFolder = true)
        {
            return path.Collapse(file => DeleteFile(file), folder => DeleteFolder(folder, recursiveDeleteIfFolder));
        }

        /// <inheritdoc />
        public abstract Task<MissingPath> DeleteFolderAsync(IFolderPath folderPath, CancellationToken cancellationToken,
             bool recursive = true);

        /// <inheritdoc />
        public abstract Task<MissingPath> DeleteFileAsync(IFilePath path, CancellationToken cancellationToken);

        /// <inheritdoc />
        public Task<MissingPath> DeleteAsync(IFileOrFolderPath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true)
        {
            return path.Collapse(file => DeleteFileAsync(file, cancellationToken), folder => DeleteFolderAsync(folder, cancellationToken, recursiveDeleteIfFolder));
        }

        #endregion
        #region Ensuring is

        /// <inheritdoc />
        public async Task<FolderPath> EnsureIsFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,  bool createRecursively = true)
        {
            return path.Collapse(
                file => CreateFolder(DeleteFile(file)),
                folder => folder.ExpectFolder(),
                missingPath => CreateFolder(missingPath));
        }

        /// <inheritdoc />
        public Task<FolderPath> EnsureIsEmptyFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true,  bool createRecursively = true)
        {
            return path.Collapse(
                async file =>
                {
                    var missingPath = await DeleteFileAsync(file, cancellationToken);
                    return CreateFolder(missingPath);
                },
                async folder =>
                {
                    foreach (var child in EnumerateChildren(folder))
                    {
                        await EnsureDoesNotExistAsync(child , cancellationToken, true);
                    }

                    return folder.ExpectFolder();
                },
                missingPath => Task.FromResult(CreateFolder(missingPath)));
        }

        /// <inheritdoc />
        public FolderPath EnsureIsFolder(IFileOrFolderOrMissingPath path,  bool createRecursively = true)
        {
            return path.Collapse(
                file => CreateFolder(DeleteFile(file)),
                folder => folder.ExpectFolder(),
                missingPath => CreateFolder(missingPath));
        }
        
        /// <inheritdoc />
        public FolderPath EnsureIsEmptyFolder(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true,  bool createRecursively = true)
        {
            return path.Collapse(
                file => CreateFolder(DeleteFile(file)),
                folder =>
                {
                    foreach (var child in EnumerateChildren(folder))
                    {
                        EnsureDoesNotExist(child , true);
                    }

                    return folder.ExpectFolder();
                },
                missingPath => CreateFolder(missingPath));
        }

        #endregion
        #region Ensuring is not

        /// <inheritdoc />
        public Task<IFileOrMissingPath> EnsureIsNotFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,  bool recursive = true)
        {
            return path.Collapse(
                file => Task.FromResult((IFileOrMissingPath)file),
                async folder =>
                {
                    var missingPath = await DeleteFolderAsync(folder, cancellationToken, recursive);
                    return (IFileOrMissingPath)missingPath;
                },
                missingPath => Task.FromResult(missingPath.ExpectFileOrMissingPath()));
        }

        /// <inheritdoc />
        public Task<IFolderOrMissingPath> EnsureIsNotFileAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken)
        {
            return path.Collapse(
                async file =>
                {
                    var missingPath = await DeleteFileAsync(file, cancellationToken);
                    return missingPath.ExpectFolderOrMissingPath();
                },
                folder => Task.FromResult(folder.ExpectFolderOrMissingPath()),
                missingPath => Task.FromResult(missingPath.ExpectFolderOrMissingPath()));
        }

        /// <inheritdoc />
        public Task<MissingPath> EnsureDoesNotExistAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true)
        {
            return path.Collapse(
                file => DeleteFileAsync(file, cancellationToken),
                folder => DeleteFolderAsync(folder, cancellationToken, recursiveDeleteIfFolder),
                missingPath => Task.FromResult(missingPath.ExpectMissingPath()));
        }

        /// <inheritdoc />
        public IFolderOrMissingPath EnsureIsNotFile(IFileOrFolderOrMissingPath path)
        {
            return path.Collapse(
                file =>
                {
                    var missingPath = DeleteFile(file);
                    return missingPath.ExpectFolderOrMissingPath();
                },
                folder => folder.ExpectFolderOrMissingPath(),
                missingPath => missingPath.ExpectFolderOrMissingPath());
        }

        /// <inheritdoc />
        public MissingPath EnsureDoesNotExist(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true)
        {
            return path.Collapse(
                file => DeleteFile(file),
                folder => DeleteFolder(folder, recursiveDeleteIfFolder),
                missingPath => missingPath.ExpectMissingPath());
        }

        /// <inheritdoc />
        public IFileOrMissingPath EnsureIsNotFolder(IFileOrFolderOrMissingPath path,  bool recursive = true)
        {
            return path.Collapse(
                file => file.ExpectFileOrMissingPath(),
                folder =>
                {
                    var missingPath = DeleteFolder(folder, recursive);
                    return missingPath.ExpectFileOrMissingPath();
                },
                missingPath => missingPath.ExpectFileOrMissingPath());
        }


        #endregion
        #region Utilities
        
        /// <inheritdoc />
        public virtual bool MayCreateFile(FileMode fileMode)
        {
            return fileMode.HasFlag(FileMode.Append) || fileMode.HasFlag(FileMode.Create) ||
                   fileMode.HasFlag(FileMode.CreateNew) || fileMode.HasFlag(FileMode.OpenOrCreate);
        }

        /// <inheritdoc />
        public bool IsFile(IFileOrFolderOrMissingPath absolutePath)
        {
            return absolutePath.FileSystem.Type(absolutePath) == IoFluently.PathType.File;
        }

        /// <inheritdoc />
        public bool IsFolder(IFileOrFolderOrMissingPath absolutePath)
        {
            return absolutePath.FileSystem.Type(absolutePath) == IoFluently.PathType.Folder;
        }
        
        /// <inheritdoc />
        public bool HasExtension(IFileOrFolderOrMissingPath path)
        {
            return Extension(path) != null;
        }

        public string Name(IFileOrFolderOrMissingPath path)
        {
            return path.Components[^1];
        }

        public string? Extension(IFileOrFolderOrMissingPath path)
        {
            var name = Name(path);
            var index = name.LastIndexOf('.');
            if (index < 0)
            {
                return null;
            }

            return name.Substring(index);
        }

        /// <inheritdoc />
        public virtual bool CanBeSimplified(IFileOrFolderOrMissingPath path)
        {
            return path.Components.SkipWhile(str => str == "..").Any(str => str == "..");
        }

        /// <inheritdoc />
        public virtual bool IsImageUri(Uri uri)
        {
            if (uri == null)
                return false;
            var str = uri.ToString();
            if (!str.Contains("."))
                return false;
            var extension = str.Substring(str.LastIndexOf('.') + 1);
            return ImageFileExtensions.Any(curExtension => extension == curExtension);
        }

        /// <inheritdoc />
        public virtual bool IsVideoUri(Uri uri)
        {
            if (uri == null)
                return false;
            var str = uri.ToString();
            if (!str.Contains("."))
                return false;
            var extension = str.Substring(str.LastIndexOf('.') + 1);
            return VideoFileExtensions.Any(curExtension => extension == curExtension);
        }

        /// <inheritdoc />
        public virtual string StripQuotes(string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\""))
                return str.Substring(1, str.Length - 2);
            if (str.StartsWith("'") && str.EndsWith("'"))
                return str.Substring(1, str.Length - 2);
            return str;
        }


        /// <inheritdoc />
        public virtual Uri Child(Uri parent, Uri child)
        {
            var parentLocalPath = parent.ToString();
            if (!parentLocalPath.EndsWith(Path.DirectorySeparatorChar.ToString())
                && !parentLocalPath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                parentLocalPath += Path.DirectorySeparatorChar;
            return new Uri(parentLocalPath).MakeRelativeUri(child);
        }

        /// <summary>
        /// List of video file extensions
        /// </summary>
        public readonly List<string> VideoFileExtensions = new List<string>
        {
            "3gpp",
            "3g2",
            "3gp",
            "3gp2",
            "asf",
            "mov",
            "avi",
            "mts",
            "m2ts",
            "m2t",
            "m4v",
            "m3u",
            "asx",
            "mpe",
            "mpeg",
            "mpg",
            "m1v",
            "m2v",
            "mp2v",
            "mpv2",
            "mp4",
            "mp4v",
            "ts",
            "wm",
            "wpl",
            "wmx",
            "wmv",
            "wvx"
        };

        /// <summary>
        /// List of image file extensions
        /// </summary>
        public readonly List<string> ImageFileExtensions = new List<string>
        {
            "ani",
            "b3d",
            "bmp",
            "dib",
            "cam",
            "clp",
            "crw",
            "cr2",
            "cur",
            "dcm",
            "acr",
            "ima",
            "dcx",
            "dds",
            "djvu",
            "iw44",
            "dxf",
            "ecw",
            "emf",
            "eps",
            "ps",
            "exr",
            "fpx",
            "g3",
            "gif",
            "hdp",
            "jxr",
            "wdp",
            "icl",
            "ico",
            "iff",
            "lbm",
            "img",
            "jls",
            "jp2",
            "jpc",
            "j2k",
            "jpf",
            "jpg",
            "jpeg",
            "jpe",
            "jpm",
            "kdc",
            "mng",
            "jng",
            "pbm",
            "pcd",
            "pcx",
            "pgm",
            "png",
            "ppm",
            "psd",
            "psp",
            "ras",
            "sun",
            "raw",
            "rle",
            "sff",
            "sfw",
            "sgi",
            "rgb",
            "sid",
            "tga",
            "tif",
            "tiff",
            "wbmp",
            "webp",
            "wmf",
            "xbm",
            "xpm"
        };

        /// <inheritdoc />
        public virtual bool IsAncestorOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant)
        {
            return IsDescendantOf(possibleDescendant, path);
        }

        /// <inheritdoc />
        public virtual bool IsDescendantOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor)
        {
            var possibleDescendantStr = Path.GetFullPath(path.FullName).ToLower();
            var possibleAncestorStr = Path.GetFullPath(possibleAncestor.FullName).ToLower();
            return possibleDescendantStr.StartsWith(possibleAncestorStr);
        }

        /// <inheritdoc />
        public virtual bool HasExtension(IFileOrFolderOrMissingPath path, string extension)
        {
            if (!extension.StartsWith(".")) {
                extension = "." + extension;
            }

            var actualExtension = Path.GetExtension(path.FullName);
            if (actualExtension == extension)
                return true;
            if (actualExtension == null)
                return false;
            return actualExtension.Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public virtual string SurroundWithDoubleQuotesIfNecessary(string str)
        {
            if (str.Contains(" "))
            {
                if (!str.StartsWith("\""))
                    str = "\"" + str;
                if (!str.EndsWith("\""))
                    str = str + "\"";
            }

            return str;
        }

        /// <inheritdoc />
        public IOpenFilesTrackingService OpenFilesTrackingService { get; }
        
                /// <summary>
        ///     Returns a regex that filters files the same as the specified pattern.
        ///     From here: http://www.java2s.com/Code/CSharp/Regular-Expressions/Checksifnamematchespatternwithandwildcards.htm
        ///     Copyright:   Julijan ?ribar, 2004-2007
        ///     This software is provided 'as-is', without any express or implied
        ///     warranty.  In no event will the author(s) be held liable for any damages
        ///     arising from the use of this software.
        ///     Permission is granted to anyone to use this software for any purpose,
        ///     including commercial applications, and to alter it and redistribute it
        ///     freely, subject to the following restrictions:
        ///     1. The origin of this software must not be misrepresented; you must not
        ///     claim that you wrote the original software. If you use this software
        ///     in a product, an acknowledgment in the product documentation would be
        ///     appreciated but is not required.
        ///     2. Altered source versions must be plainly marked as such, and must not be
        ///     misrepresented as being the original software.
        ///     3. This notice may not be removed or altered from any source distribution.
        /// </summary>
        /// <param name="filename">
        ///     Name to match.
        /// </param>
        /// <param name="pattern">
        ///     Pattern to match to.
        /// </param>
        /// <returns>
        ///     <c>true</c> if name matches pattern, otherwise <c>false</c>.
        /// </returns>
        public virtual Regex FileNamePatternToRegex(string pattern)
        {
            // prepare the pattern to the form appropriate for Regex class
            var sb = new StringBuilder(pattern);
            // remove superflous occurences of  "?*" and "*?"
            while (sb.ToString().IndexOf("?*") != -1) sb.Replace("?*", "*");

            while (sb.ToString().IndexOf("*?") != -1) sb.Replace("*?", "*");

            // remove superflous occurences of asterisk '*'
            while (sb.ToString().IndexOf("**") != -1) sb.Replace("**", "*");

            // if only asterisk '*' is left, the mask is ".*"
            if (sb.ToString().Equals("*"))
            {
                pattern = ".*";
            }
            else
            {
                // replace '.' with "\."
                sb.Replace(".", "\\.");
                // replaces all occurrences of '*' with ".*" 
                sb.Replace("*", ".*");
                // replaces all occurrences of '?' with '.*' 
                sb.Replace("?", ".");
                // add "\b" to the beginning and end of the pattern
                sb.Insert(0, "\\b");
                sb.Append("\\b");
                pattern = sb.ToString();
            }

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex;
        }

        #endregion
        
        #region Parsing paths
        
        /// <inheritdoc />
        public virtual bool IsAbsoluteWindowsPath(string path)
        {
            return char.IsLetter(path[0]) && path[1] == ':';
        }

        /// <inheritdoc />
        public virtual bool IsAbsoluteUnixPath(string path)
        {
            return path.StartsWith("/");
        }

        /// <summary>
        ///     Checks for invalid relative paths, like C:\.. (Windows) or /.. (Unix)
        /// </summary>
        internal bool IsAncestorOfRoot(IReadOnlyList<string> pathComponents)
        {
            var numberOfComponentsToSkip = 0;
            var isRelative = !ComponentsAreAbsolute(pathComponents);
            for (var i = pathComponents.Count - 1; i >= 0; i--)
            {
                if (!isRelative && i == 0)
                {

                }
                else if (pathComponents[i] == ".")
                {
                    continue;
                }
                else if (pathComponents[i] == "..")
                {
                    numberOfComponentsToSkip++;
                }
                else if (numberOfComponentsToSkip > 0)
                {
                    numberOfComponentsToSkip--;
                }
                else
                {
                }
            }

            return numberOfComponentsToSkip > 0 && !isRelative;
        }

        /// <inheritdoc />
        public virtual bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            if (path.Count == 0)
            {
                return true;
            }

            if (path[0] == "/")
            {
                return true;
            }

            if (path[0].Length >= 2 && char.IsLetter(path[0][0]) && path[0][1] == ':')
            {
                return true;
            }

            if (path[0] == "\\")
            {
                return true;
            }
            
            return false;
        }

        /// <inheritdoc />
        public IMaybe<FileOrFolderOrMissingPath> TryParseAbsolutePath(string path, IFolderPath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var relativePath = TryParseRelativePath(path, flags);
            if (relativePath.HasValue)
            {
                return new FileOrFolderOrMissingPath(optionallyRelativeTo.Components.Concat(relativePath.Value.Path).ToImmutableList(),
                    optionallyRelativeTo.IsCaseSensitive, optionallyRelativeTo.DirectorySeparator,
                    optionallyRelativeTo.FileSystem).ToMaybe();
            }

            return TryParseAbsolutePath(path, flags);
        }

        /// <inheritdoc />
        public bool IsRelativePath(string path)
        {
            return TryParseRelativePath(path, CaseSensitivityMode.UseDefaultsForGivenPath).HasValue;
        }

        /// <inheritdoc />
        public bool IsAbsolutePath(string path)
        {
            return TryParseAbsolutePath(path, CaseSensitivityMode.UseDefaultsForGivenPath).HasValue;
        }

        /// <inheritdoc />
        public virtual RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            RelativePath pathSpec;
            if (!TryParseRelativePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        /// <inheritdoc />
        public virtual IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            RelativePath pathSpec;
            if (!TryParseRelativePath(path, out pathSpec, out error, flags))
                return Nothing<RelativePath>();
            return Something(pathSpec);
        }

        private bool TryParseRelativePath(string path, out RelativePath relativePath, out string error,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            if (flags == CaseSensitivityMode.UseDefaultsFromEnvironment && flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                throw new ArgumentException(
                    "Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags == CaseSensitivityMode.UseDefaultsFromEnvironment)
                flags = IsCaseSensitiveByDefault ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
            error = string.Empty;
            relativePath = null;
            
            var backslashIndex = path.IndexOf('\\');
            var isWindowsStyle = backslashIndex != -1;
            var slashIndex = path.IndexOf('/');
            var isUnixStyle = slashIndex != -1;

            if (isWindowsStyle && isUnixStyle)
            {
                if (backslashIndex < slashIndex)
                {
                    isUnixStyle = false;
                    path = path.Replace("/", "\\");
                }
                else
                {
                    isWindowsStyle = false;
                    path = path.Replace("\\", "/");
                }
            }

            if (isWindowsStyle)
            {
                if (flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                    flags = CaseSensitivityMode.CaseInsensitive;
                if (path.Length > 1 && path.EndsWith("\\"))
                    path = path.Substring(0, path.Length - 1);
                var colonIdx = path.LastIndexOf(':');
                if (colonIdx > -1 && (colonIdx != 1 || !char.IsLetter(path[0]) || path.Length > 2 && path[2] != '\\'))
                {
                    error = "A Windows path may not contain a : character, except as part of the drive specifier.";
                    return false;
                }

                var isRelative = IsAbsoluteWindowsPath(path);
                if (isRelative)
                {
                    var components = path.Split('\\').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "\\", this);
                }
                else if (path.StartsWith("."))
                {
                    var components = path.Split('\\').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "\\", this);
                }
                else if (path.StartsWith("\\\\"))
                {
                    var components = "\\\\".ItemConcat(path.Substring(2).Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "\\", this);
                }
                else if (path.StartsWith("\\"))
                {
                    var components = "\\".ItemConcat(path.Substring(1).Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "\\", this);
                }
                else
                {
                    var components = ".".ItemConcat(path.Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "\\", this);
                }

                return true;
            }

            if (isUnixStyle)
            {
                if (flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                    flags = CaseSensitivityMode.CaseSensitive;
                if (path.Length > 1 && path.EndsWith("/"))
                    path = path.Substring(0, path.Length - 1);
                if (path.Contains(":"))
                {
                    error = "A Unix path may not contain a : character.";
                    return false;
                }

                var isRelative = IsAbsoluteUnixPath(path);
                if (isRelative)
                {
                    var components = "/".ItemConcat(path.Substring(1).Split('/')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "/", this);
                }
                else if (path.StartsWith("."))
                {
                    var components = path.Split('/').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "/", this);
                }
                else
                {
                    var components = ".".ItemConcat(path.Split('/')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    
                    if (ComponentsAreAbsolute(components))
                    {
                        error = "Must not be an absolute path";
                        return false;
                    }
                    relativePath = new RelativePath(components, flags == CaseSensitivityMode.CaseSensitive, "/", this);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment))
                flags = IsCaseSensitiveByDefault ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
            if (path == ".." || path == ".")
                relativePath = new RelativePath(new[]{path}, flags == CaseSensitivityMode.CaseSensitive, GetDefaultDirectorySeparatorForThisEnvironment(), this);
            else
                relativePath = new RelativePath(new[]{".", path}, flags == CaseSensitivityMode.CaseSensitive, GetDefaultDirectorySeparatorForThisEnvironment(), this);
            return true;
        }

        /// <inheritdoc />
        public virtual FileOrFolderOrMissingPath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            FileOrFolderOrMissingPath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            FileOrFolderOrMissingPath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                return Nothing<FileOrFolderOrMissingPath>();
            return Something(pathSpec);
        }

        private bool TryParseAbsolutePath(string path, out FileOrFolderOrMissingPath pathSpec, out string error,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment) && flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                throw new ArgumentException(
                    "Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment))
                flags = IsCaseSensitiveByDefault ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
            error = string.Empty;
            pathSpec = null;
            var containsWindowsDriveSpecification = path.LastIndexOf(':') == 1;
            if (containsWindowsDriveSpecification && path.Contains("/"))
            {
                path = path.Replace("/", "\\");
            }

            var backslashIndex = path.IndexOf('\\');
            var containsColon = containsWindowsDriveSpecification;
            var isWindowsStyle = backslashIndex != -1 || containsColon;
            var slashIndex = path.IndexOf('/');
            var isUnixStyle = slashIndex != -1;

            if (isWindowsStyle && isUnixStyle)
            {
                if (containsColon || backslashIndex < slashIndex)
                {
                    isUnixStyle = false;
                    path = path.Replace("/", "\\");
                }
                else
                {
                    isWindowsStyle = false;
                    path = path.Replace("\\", "/");
                }
            }
            
            if (isWindowsStyle)
            {
                if (flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                    flags = CaseSensitivityMode.CaseInsensitive;
                if (path.Length > 1 && path.EndsWith("\\"))
                    path = path.Substring(0, path.Length - 1);
                var colonIdx = path.LastIndexOf(':');
                if (colonIdx > -1 && (colonIdx != 1 || !char.IsLetter(path[0]) || path.Length > 2 && path[2] != '\\'))
                {
                    error = "A Windows path may not contain a : character, except as part of the drive specifier.";
                    return false;
                }

                var isAbsolute = IsAbsoluteWindowsPath(path);
                if (isAbsolute)
                {
                    var components = path.Split('\\').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    pathSpec = new FileOrFolderOrMissingPath(components, false, "\\", this);
                }
                else if (path.StartsWith("."))
                {
                    var components = path.Split('\\').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, false, "\\", this);
                }
                else if (path.StartsWith("\\\\"))
                {
                    var components = "\\\\".ItemConcat(path.Substring(2).Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, false, "\\", this);
                }
                else if (path.StartsWith("\\"))
                {
                    var components = "\\".ItemConcat(path.Substring(1).Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, false, "\\", this);
                }
                else
                {
                    var components = ".".ItemConcat(path.Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, false, "\\", this);
                }

                return true;
            }

            if (isUnixStyle)
            {
                if (flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                    flags = CaseSensitivityMode.CaseSensitive;
                if (path.Length > 1 && path.EndsWith("/"))
                    path = path.Substring(0, path.Length - 1);
                // On Max OSX, paths may contain colons, so this is commented out
                // if (path.Contains(":"))
                // {
                //     error = "A Unix path may not contain a : character.";
                //     return false;
                // }

                var isAbsolute = IsAbsoluteUnixPath(path);
                if (isAbsolute)
                {
                    var components = "/".ItemConcat(path.Substring(1).Split('/')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Count == 2 && components[0] == "/" && components[1].Length == 0)
                    {
                        components.RemoveAt(1);
                    }
                    else if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, true, "/", this);
                }
                else if (path.StartsWith("."))
                {
                    var components = path.Split('/').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }

                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, true, "/", this);
                }
                else
                {
                    var components = ".".ItemConcat(path.Split('/')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(string.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }

                    if (IsAncestorOfRoot(components))
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    
                    if (!ComponentsAreAbsolute(components))
                    {
                        error = "Must be an absolute path";
                        return false;
                    }
                    pathSpec = new FileOrFolderOrMissingPath(components, true, "/", this);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            error = "Must be an absolute path";
            return false;
        }

        /// <inheritdoc />
        public FileOrFolderOrMissingPath ParseAbsolutePath(string path, IFolderPath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return TryParseAbsolutePath(path, optionallyRelativeTo, flags).Value;
        }

        #endregion
        #region Translation stuff
        
        #region Stuff that must be implemented

        /// <inheritdoc />
        public virtual async Task<IPathTranslation> CopyFileAsync(IPathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite)
            {
                await translation.Destination.Collapse(
                    file => DeleteFileAsync(file, cancellationToken),
                    folder => DeleteFolderAsync(folder, cancellationToken, true),
                    missingPath => Task.FromResult(missingPath.ExpectMissingPath()));
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return translation;
            }
            
            var fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            using var sourceStream = translation.Source.FileSystem.Open(translation.Source.ExpectFile(), FileMode.Open, FileAccess.Read,
                FileShare.Read, fileOptions, bufferSize);
            using var destinationStream = translation.Destination.FileSystem.Open(translation.Destination.ExpectFileOrMissingPath(), FileMode.Open,
                FileAccess.Write, FileShare.None, fileOptions, bufferSize);

            await sourceStream.CopyToAsync(destinationStream, GetBufferSizeOrDefaultInBytes(bufferSize), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            
            return translation;
        }

        /// <inheritdoc />
        public virtual async Task<IPathTranslation> CopyFolderAsync(IPathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite)
            {
                await translation.Destination.Collapse(
                    file => DeleteFileAsync(file, cancellationToken),
                    folder => DeleteFolderAsync(folder, cancellationToken, true),
                    missingPath => Task.FromResult(missingPath.ExpectMissingPath()));
            }
            
            translation.Destination.FileSystem.CreateFolder(translation.Destination.ExpectMissingPath());

            foreach (var child in translation)
            {
                // TODO - investigate if there's a more efficient way to do this.
                await CopyAsync(child, cancellationToken, bufferSize, overwrite);
            }

            return translation;
        }

        /// <inheritdoc />
        public virtual async Task<IPathTranslation> MoveFileAsync(IPathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            await CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
            await translation.Source.FileSystem.DeleteAsync(translation.Source.ExpectFileOrFolder(), cancellationToken, true);
            return translation;
        }

        /// <inheritdoc />
        public virtual async Task<IPathTranslation> MoveFolderAsync(IPathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            await CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
            await translation.Source.FileSystem.DeleteAsync(translation.Source.ExpectFileOrFolder(), cancellationToken, true);
            return translation;
        }

        /// <inheritdoc />
        public virtual IPathTranslation CopyFile(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite)
            {
                translation.Destination.Collapse(
                    file => DeleteFile(file),
                    folder => DeleteFolder(folder, true),
                    missingPath => missingPath);
            }

            var fileOptions = FileOptions.SequentialScan;

            using var sourceStream = translation.Source.FileSystem.Open(translation.Source.ExpectFile(), FileMode.Open, FileAccess.Read,
                FileShare.Read, fileOptions, bufferSize);
            using var destinationStream = translation.Destination.FileSystem.Open(translation.Destination.ExpectFileOrMissingPath(), overwrite ? FileMode.Create : FileMode.CreateNew,
                FileAccess.Write, FileShare.None, fileOptions, bufferSize);

            sourceStream.CopyTo(destinationStream, GetBufferSizeOrDefaultInBytes(bufferSize));
            
            return translation;
        }

        /// <inheritdoc />
        public virtual IPathTranslation CopyFolder(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite)
            {
                translation.Destination.Collapse(
                    file => DeleteFile(file),
                    folder => DeleteFolder(folder, true),
                    missingPath => missingPath);
            }

            translation.Destination.FileSystem.EnsureIsFolder(translation.Destination);

            foreach (var child in translation)
            {
                Copy(child, bufferSize, overwrite);
            }

            return translation;
        }

        /// <inheritdoc />
        public virtual IPathTranslation MoveFile(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            CopyFile(translation, bufferSize, overwrite);
            translation.Source.FileSystem.Delete(translation.Source.ExpectFileOrFolder(), true);
            return translation;
        }

        /// <inheritdoc />
        public virtual IPathTranslation MoveFolder(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            CopyFolder(translation, bufferSize, overwrite);
            translation.Source.FileSystem.Delete(translation.Source.ExpectFileOrFolder(), true);
            return translation;
        }

        #endregion

        /// <inheritdoc />
        public IPathTranslation Translate(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination)
        {
            return new CalculatedPathTranslation(pathToBeCopied, source, destination, this);
        }

        /// <inheritdoc />
        public IPathTranslation Translate(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination)
        {
            return new PathTranslation(source, destination, this);
        }

        /// <inheritdoc />
        public IPathTranslation Copy(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Copy(Translate(pathToBeCopied, source, destination), bufferSize, overwrite);
        }
        
        /// <inheritdoc />
        public IPathTranslation Copy(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false) {
            return Copy(Translate(source, destination), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IPathTranslation Move(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Move(Translate(pathToBeCopied, source, destination), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IPathTranslation Move(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Move(Translate(source, destination), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IPathTranslation RenameTo(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Move(Translate(source, target), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IPathTranslation Copy(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.FileSystem.Type(translation.Source))
            {
                case PathType.File:
                    CopyFile(translation, bufferSize, overwrite);
                    break;
                case PathType.Folder:
                    CopyFolder(translation, bufferSize, overwrite);
                    break;
                default:
                    throw new InvalidOperationException($"Cannot copy non-existent path {translation.Source}");
            }

            return translation;
        }

        /// <inheritdoc />
        public IPathTranslation Move(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.FileSystem.Type(translation.Source))
            {
                case PathType.File:
                    MoveFile(translation, bufferSize, overwrite);
                    break;
                case PathType.Folder:
                    MoveFolder(translation, bufferSize, overwrite);
                    break;
                default:
                    throw new InvalidOperationException($"Cannot move non-existent path {translation.Source}");
            }

            return translation;
        }

        /// <inheritdoc />
        public Task<IPathTranslation> CopyAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return CopyAsync(Translate(pathToBeCopied, source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IPathTranslation> CopyAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return CopyAsync(Translate(source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IPathTranslation> MoveAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return MoveAsync(Translate(pathToBeCopied, source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IPathTranslation> MoveAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return MoveAsync(Translate(source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IPathTranslation> RenameToAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return MoveAsync(Translate(source, target), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public async Task<IPathTranslation> CopyAsync(IPathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.FileSystem.Type(translation.Source))
            {
                case PathType.File:
                    await CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
                    break;
                case PathType.Folder:
                    await CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
                    break;
                default:
                    throw new InvalidOperationException($"Cannot copy non-existent path {translation.Source}");
            }

            return translation;
        }

        /// <inheritdoc />
        public async Task<IPathTranslation> MoveAsync(IPathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.FileSystem.Type(translation.Source))
            {
                case PathType.File:
                    await MoveFileAsync(translation, cancellationToken, bufferSize, overwrite);
                    break;
                case PathType.Folder:
                    await MoveFolderAsync(translation, cancellationToken, bufferSize, overwrite);
                    break;
                default:
                    throw new InvalidOperationException($"Cannot copy non-existent path {translation.Source}");
            }

            return translation;
        }

        #endregion
        #region Path building

        /// <inheritdoc />
        public FilesOrFoldersOrMissingPaths GlobFiles(IFolderPath folderPath, string pattern)
        {
            Func<FileOrFolderOrMissingPath, IEnumerable<RelativePath>> patternFunc = absPath => absPath.Collapse(
                file => Enumerable.Empty<RelativePath>(),
                folder => folder.FileSystem.EnumerateChildren(folder, pattern).Select(x => new RelativePath(new[]{Name(x)}, x.IsCaseSensitive, x.DirectorySeparator, x.FileSystem)),
                missingPath => Enumerable.Empty<RelativePath>());
            return folderPath.ExpectFolder() / patternFunc;
        }

        /// <inheritdoc />
        public FileOrFolderOrMissingPath Combine(IFolderPath folderPath, params string[] subsequentPathParts)
        {
            return TryDescendant(folderPath, subsequentPathParts).Value;
        }

        /// <inheritdoc />
        public FileOrFolderOrMissingPath WithoutExtension(IFileOrFolderOrMissingPath path)
        {
            if (!HasExtension(path))
            {
                return new FileOrFolderOrMissingPath(path);
            }

            var newComponents = new List<string>();

            for (var i = 0; i < path.Components.Count - 1; i++)
            {
                newComponents.Add(path.Components[i]);
            }

            var name = path.FileSystem.Name(path);
            newComponents.Add(name.Substring(0, name.LastIndexOf('.')));
            
            return new FileOrFolderOrMissingPath(newComponents, path.IsCaseSensitive, path.DirectorySeparator, path.FileSystem);
        }

        /// <inheritdoc />
        public IMaybe<FileOrFolderOrMissingPath> TryWithExtension(IFileOrFolderOrMissingPath path, Func<string, string> differentExtension)
        {
            return path.FileSystem.TryWithExtension(path, differentExtension(Extension(path) ?? ""));
        }

        /// <inheritdoc />
        public virtual RelativePath Simplify(RelativePath path)
        {
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            for (var i = path.Path.Count - 1; i >= 0; i--)
            {
                if (path.Path[i] == ".")
                    continue;
                if (path.Path[i] == "..")
                    numberOfComponentsToSkip++;
                else if (numberOfComponentsToSkip > 0)
                    numberOfComponentsToSkip--;
                else
                    result.Insert(0, path.Path[i]);
            }

            for (var i = 0; i < numberOfComponentsToSkip; i++) result.Insert(0, "..");

            if (result.Count == 0)
            {
                result.Add(".");
            }
            
            return new RelativePath(result, path.IsCaseSensitive, path.DirectorySeparator, path.FileSystem);
        }

        /// <inheritdoc />
        public virtual FileOrFolderOrMissingPath Simplify(IFileOrFolderOrMissingPath path)
        {
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            for (var i = path.Components.Count - 1; i >= 0; i--)
            {
                if (path.Components[i] == ".")
                    continue;
                if (path.Components[i] == "..")
                    numberOfComponentsToSkip++;
                else if (numberOfComponentsToSkip > 0)
                    numberOfComponentsToSkip--;
                else
                    result.Insert(0, path.Components[i]);
            }

            if (numberOfComponentsToSkip > 0)
                throw new ArgumentException(
                    "Error: the specified path points to an ancestor of the root, which means that the specified path is invalid");
            for (var i = 0; i < numberOfComponentsToSkip; i++) result.Insert(0, "..");

            var sb = new StringBuilder();
            for (var i = 0; i < result.Count; i++)
            {
                sb.Append(result[i]);
                if (result[i] != "\\" && result[i] != "/" && i != result.Count - 1)
                    sb.Append(path.DirectorySeparator);
            }

            var str = sb.ToString();
            if (str.Length == 0)
                str = ".";
            return TryParseAbsolutePath(str, path.IsCaseSensitive ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive).Value;
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryParent(IFileOrFolderOrMissingPath path)
        {
            if (path .Components.Count > 1)
            {
                return new FileOrFolderOrMissingPath(path .Components.Take(path .Components.Count - 1).ToImmutableList(),
                    path.IsCaseSensitive, path.DirectorySeparator, path.FileSystem).ToMaybe();
            }
            else
            {
                return Nothing<FileOrFolderOrMissingPath>(() => throw new InvalidOperationException($"The path {path} has only one component, so there is no parent"));
            }
        }

        /// <inheritdoc />
        public virtual RelativePath RelativeTo(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo)
        {
            var simplified = Simplify(path);
            var pathStr = simplified.FullName;
            var common = path.FileSystem.TryCommonWith(path, relativeTo);

            if (!common.HasValue)
                throw new InvalidOperationException("No common ancestor");
            //return path;

            var sb = new StringBuilder();

            for (var i = 0; i < relativeTo.Components.Count - common.Value.Components.Count; i++)
            {
                sb.Append("..");
                sb.Append(path.DirectorySeparator);
            }

            var restOfRelativePath = pathStr.Substring(common.Value.FullName.Length);
            while (restOfRelativePath.StartsWith(path.DirectorySeparator))
                restOfRelativePath = restOfRelativePath.Substring(path.DirectorySeparator.Length);

            sb.Append(restOfRelativePath);

            return TryParseRelativePath(sb.ToString()).Value;

            //if (pathStr.StartsWith(relativeToStr))
            //{
            //    var result = pathStr.Substring(relativeToStr.Length);
            //    if (result.StartsWith(path.DirectorySeparator))
            //        return ToAbsolutePath(result.Substring(path.DirectorySeparator.Length)).Value;
            //}
            //throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryCommonWith(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that)
        {
            var path1Str = path.FullName;
            var path2Str = that.FullName;

            if (!path.IsCaseSensitive || !that.IsCaseSensitive)
            {
                path1Str = path1Str.ToUpper();
                path2Str = path2Str.ToUpper();
            }

            var caseSensitive = path.IsCaseSensitive ||
                                that.IsCaseSensitive;
            var zippedComponents = path.Components.SkipWhile(x => x == path.DirectorySeparator).Zip(that.Components.SkipWhile(x => x == path.DirectorySeparator), (comp1, comp2) => 
                new
                {
                    equals = comp1.Equals(comp2, !caseSensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal),
                    component = comp1
                });

            if (path.DirectorySeparator == "/")
            {
                return TryParseAbsolutePath("/" + string.Join(path.DirectorySeparator, zippedComponents.TakeWhile(x => x.equals).Select(x => x.component)));
            }
            else
            {
                return TryParseAbsolutePath(string.Join(path.DirectorySeparator, zippedComponents.TakeWhile(x => x.equals).Select(x => x.component)));
            }
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryGetCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return TryParseAbsolutePath(path1.FullName.GetCommonBeginning(path2.FullName).Trim('\\'));
        }

        /// <inheritdoc />
        public virtual IMaybe<Uri> TryGetCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return MaybeCatch(() => new Uri(path1.FullName.GetCommonEnding(path2.FullName).Trim('\\'),
                UriKind.Relative));
        }

        /// <inheritdoc />
        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return MaybeCatch(() =>
            {
                var commonAncestry = path1.FullName.GetCommonBeginning(path2.FullName).Trim('\\');
                return new Tuple<Uri, Uri>(
                    new Uri(path1.FullName.Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative),
                    new Uri(path2.FullName.Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative));
            });
        }

        /// <inheritdoc />
        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return MaybeCatch(() =>
            {
                var commonDescendants = path1.FullName.GetCommonEnding(path2.FullName).Trim('\\');
                return new Tuple<Uri, Uri>(
                    new Uri(
                        path1.FullName.Substring(0, path1.FullName.Length - commonDescendants.Length).Trim('\\')),
                    new Uri(
                        path2.FullName.Substring(0, path2.FullName.Length - commonDescendants.Length).Trim('\\')));
            });
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryWithExtension(IFileOrFolderOrMissingPath path, string differentExtension)
        {
            return TryParseAbsolutePath(Path.ChangeExtension(path.FullName, differentExtension));
        }

        public IEnumerable<FolderPath> Ancestors(IFolderPath folderPath, bool includeItself)
        {
            if (includeItself)
            {
                yield return folderPath.ExpectFolder();
            }

            foreach (var ancestor in Ancestors(folderPath))
            {
                yield return ancestor;
            }
        }

        public IEnumerable<IFileOrFolderPath> Ancestors(IFilePath filePath, bool includeItself)
        {
            if (includeItself)
            {
                yield return filePath.ExpectFileOrFolder();
            }

            foreach (var ancestor in Ancestors(filePath))
            {
                yield return ancestor.ExpectFileOrFolder();
            }
        }

        public FolderPathAncestors Ancestors(IFolderPath folderPath)
        {
            return new(folderPath);
        }

        public FilePathAncestors Ancestors(IFilePath filePath)
        {
            return new(filePath);
        }

        public MissingPathAncestors Ancestors(IMissingPath missingPath)
        {
            return new(missingPath);
        }

        public IEnumerable<IFileOrFolderOrMissingPath> Ancestors(IMissingPath missingPath, bool includeItself)
        {
            if (includeItself)
            {
                yield return missingPath.ExpectFolderOrMissingPath();
            }

            foreach (var ancestor in Ancestors(missingPath))
            {
                yield return ancestor;
            }
        }

        public IEnumerable<FileOrFolderOrMissingPath> Ancestors(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath, bool includeItself)
        {
            if (includeItself)
            {
                yield return new FileOrFolderOrMissingPath(fileOrFolderOrMissingPath);
            }

            foreach (var ancestor in Ancestors(fileOrFolderOrMissingPath))
            {
                yield return ancestor;
            }
        }

        /// <inheritdoc />
        public virtual FileOrFolderOrMissingPathAncestors Ancestors(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return new(fileOrFolderOrMissingPath);
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryDescendant(IFileOrFolderOrMissingPath path, params IFileOrFolderOrMissingPath[] paths)
        {
            return path.FileSystem.TryDescendant(path, paths.Select(p => p.FullName).ToArray());
        }

        /// <inheritdoc />
        public virtual IMaybe<FileOrFolderOrMissingPath> TryDescendant(IFileOrFolderOrMissingPath path, params string[] paths)
        {
            var pathStr = path.FullName;
            // Make sure that pathStr is treated as a directory.
            if (!pathStr.EndsWith(path.DirectorySeparator))
                pathStr += path.DirectorySeparator;

            var result = path.Components.Concat(paths).ToArray();
            var combinedResult = Path.Combine(result);
            var pathResult = TryParseAbsolutePath(combinedResult);
            return pathResult;
        }
        
        /// <inheritdoc />
        public virtual IEnumerable<KeyValuePair<FileOrFolderOrMissingPath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(
            IEnumerable<FileOrFolderOrMissingPath> paths)
        {
            var alreadyProposedNames = new HashSet<string>();
            foreach (var path in paths)
            {
                var enumerator = ProposeSuccessivelyMoreSpecificNames(path).GetEnumerator();
                enumerator.MoveNext();
                while (alreadyProposedNames.Contains(enumerator.Current)) enumerator.MoveNext();

                alreadyProposedNames.Add(enumerator.Current);
                yield return new KeyValuePair<FileOrFolderOrMissingPath, string>(path, enumerator.Current);
                enumerator.Dispose();
            }
        }

        private IEnumerable<string> ProposeSuccessivelyMoreSpecificNames(IFileOrFolderOrMissingPath path)
        {
            string filename = null;
            foreach (var parentPath in path.FileSystem.Ancestors(path))
            {
                if (filename == null)
                    filename = parentPath.Components[^1];
                else
                    filename = $"{parentPath.Components[^1]}.{filename}";

                yield return filename;
            }
        }

        public ChildFilesOrFolders Children(IFolderPath folderPath, string searchPattern)
        {
            return new(folderPath, searchPattern);
        }

        public ChildFiles ChildFiles(IFolderPath folderPath, string searchPattern)
        {
            return new(folderPath, searchPattern);
        }

        public ChildFolders ChildFolders(IFolderPath folderPath, string searchPattern)
        {
            return new(folderPath, searchPattern);
        }

        public DescendantFilesOrFolders Descendants(IFolderPath folderPath, string searchPattern)
        {
            return new(folderPath, searchPattern);
        }

        public DescendantFolders DescendantFolders(IFolderPath folderPath, string searchPattern)
        {
            return new(folderPath, searchPattern);
        }

        public DescendantFiles DescendantFiles(IFolderPath folderPath, string searchPattern)
        {
            return new(folderPath, searchPattern);
        }

        public ChildFilesOrFolders Children(IFolderPath folderPath)
        {
            return new(folderPath);
        }

        public ChildFiles ChildFiles(IFolderPath folderPath)
        {
            return new(folderPath);
        }

        public ChildFolders ChildFolders(IFolderPath folderPath)
        {
            return new(folderPath);
        }

        public DescendantFilesOrFolders Descendants(IFolderPath folderPath)
        {
            return new(folderPath);
        }

        public DescendantFolders DescendantFolders(IFolderPath folderPath)
        {
            return new(folderPath);
        }

        public DescendantFiles DescendantFiles(IFolderPath folderPath)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<FilePath> EnumerateChildFiles(IFolderPath folderPath, string searchPattern = null)
        {
            return EnumerateChildren(folderPath, searchPattern, false, true).Select(x => new FilePath(x));
        }

        public virtual IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath folderPath, string searchPattern = null)
        {
            return EnumerateChildren(folderPath, searchPattern, true, false).Select(x => new FolderPath(x));
        }

        public virtual IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath folderPath, string searchPattern = null)
        {
            return EnumerateDescendants(folderPath, searchPattern, true, false).Select(x => new FolderPath(x));
        }

        public virtual IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath folderPath, string searchPattern = null)
        {
            return EnumerateDescendants(folderPath, searchPattern, false, true).Select(x => new FilePath(x));
        }

        /// <inheritdoc />
        public abstract IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath, string searchPattern = null, bool includeFolders = true,
            bool includeFiles = true);

        /// <inheritdoc />
        public virtual IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath, string searchPattern = null,
            bool includeFolders = true,
            bool includeFiles = true)
        {
            return folderPath.TraverseTree(x =>
            {
                var children = x.Collapse(file => Enumerable.Empty<IFileOrFolderPath>(), folder => EnumerateChildren(folder));
                var childrenNames = children.Select(child => child.Components[^1]);
                return childrenNames;
            }, (IFileOrFolderPath node, string name, out IFileOrFolderPath child) =>
            {
                child = (new FolderPath(node) / name)
                    .Collapse(file => (IFileOrFolderPath)file, folder => (IFileOrFolderPath)folder, missingPath => throw missingPath.AssertExpectedType(PathType.File, PathType.Folder));
                if (CanEmptyDirectoriesExist)
                {
                    var result = child .Exists;
                    return result;
                }

                return true;
            })
                .Skip(1)
                .Where(tt => tt.Type != TreeTraversalType.ExitBranch)
                .Select(tt => tt.Value);
        }

        public IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath)
        {
            return EnumerateChildren(folderPath, "*");
        }

        public IEnumerable<FilePath> EnumerateChildFiles(IFolderPath folderPath)
        {
            return EnumerateChildFiles(folderPath, "*");
        }

        public IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath folderPath)
        {
            return EnumerateChildFolders(folderPath, "*");
        }

        public IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath)
        {
            return EnumerateDescendants(folderPath, "*");
        }

        public IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath folderPath)
        {
            return EnumerateDescendantFolders(folderPath, "*");
        }

        public IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath folderPath)
        {
            return EnumerateDescendantFiles(folderPath, "*");
        }

        #endregion
        #region File metadata
        
        /// <inheritdoc />
        public virtual bool Exists(IFileOrFolderOrMissingPath path)
        {
            var pathType = path.FileSystem.Type(path);
            if (pathType == IoFluently.PathType.Folder && !CanEmptyDirectoriesExist)
            {
                return false;
            }

            return pathType != IoFluently.PathType.MissingPath;
        }

        /// <inheritdoc />
        public abstract PathType Type(IFileOrFolderOrMissingPath path);

        /// <inheritdoc />
        public virtual bool IsReadOnly(IFilePath filePath)
        {
            return GetAttributes(filePath).HasFlag(FileAttributes.ReadOnly);
        }

        /// <inheritdoc />
        public abstract Information FileSize(IFilePath filePath);

        /// <inheritdoc />
        public abstract FileAttributes GetAttributes(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath);

        /// <inheritdoc />
        public abstract void SetAttributes(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath,
            FileAttributes fileAttributes);

        /// <inheritdoc />
        public abstract DateTimeOffset CreationTime(IFilePath filePath);

        /// <inheritdoc />
        public abstract DateTimeOffset LastAccessTime(IFilePath filePath);

        /// <inheritdoc />
        public abstract DateTimeOffset LastWriteTime(IFilePath filePath);

        /// <inheritdoc />
        public IFileInfo GetFileInfo( string subpath ) => new FilePathFileInfoAdapter(new FilePath(ParseAbsolutePath( subpath )));

        #endregion
        #region File reading
        
        /// <inheritdoc />
        public BufferEnumerator ReadBuffers(IFilePath path, FileShare fileShare = FileShare.None,
            Information? bufferSize = default, int paddingAtStart = 0, int paddingAtEnd = 0)
        {
            var stream = Open(path, FileMode.Open, FileAccess.Read, fileShare, FileOptions.SequentialScan,
                bufferSize);
            var result = new BufferEnumerator(stream, 0, GetBufferSizeOrDefaultInBytes(bufferSize),
                paddingAtStart, paddingAtEnd);
            return result;
        }

        #endregion
        #region File writing

        /// <inheritdoc />
        public virtual FilePath WriteAllBytes(IFileOrMissingPath path, byte[] bytes,  bool createRecursively = true)
        {
            using var stream = Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, FileOptions.WriteThrough,
                Information.FromBytes(bytes.Length), createRecursively);
            stream.Write(bytes, 0, Math.Max(bytes.Length, 1));

            return new FilePath(path);
        }
        
        #endregion
        #region File open for reading or writing

        /// <inheritdoc />
        public abstract Stream Open(IFileOrMissingPath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.None | FileOptions.SequentialScan,
            Information? bufferSize = default,  bool createRecursively = true);

        #endregion
        #region LINQ-style APIs
        
        public abstract IQueryable<FileOrFolderOrMissingPath> Query();

        public abstract ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq(IFolderPath folderPath, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);
        #endregion
        #region IFileProvider implementation
        public IDirectoryContents GetDirectoryContents( string subpath ) =>
            new FolderContents( new FolderPath(ParseAbsolutePath( subpath )) );

        public IChangeToken Watch( string filter )
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }
        #endregion

        protected FileSystemBase(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator)
        {
            IsCaseSensitiveByDefault = isCaseSensitiveByDefault;
            OpenFilesTrackingService = openFilesTrackingService ?? throw new ArgumentNullException(nameof(openFilesTrackingService));
            DefaultDirectorySeparator = defaultDirectorySeparator ?? throw new ArgumentNullException(nameof(defaultDirectorySeparator));
        }
        
        #region Internal extension methods

        /// <inheritdoc />
        public virtual StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode)
        {
            if (caseSensitivityMode.HasFlag(CaseSensitivityMode.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        /// <inheritdoc />
        public virtual StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode, CaseSensitivityMode otherCaseSensitivityMode)
        {
            if (caseSensitivityMode.HasFlag(CaseSensitivityMode.CaseSensitive) && otherCaseSensitivityMode.HasFlag(CaseSensitivityMode.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        #endregion
    }
}