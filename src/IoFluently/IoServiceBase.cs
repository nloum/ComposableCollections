using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
    public interface IFormatService
    {
        
    }
    
    public abstract class IoServiceBase : IIoService
    {
        private readonly IMultiTypeDictionary<IFormatService> _formatServices = new MultiTypeDictionary<IFormatService>();

        public T GetIoService<T>() where T : IFormatService
        {
            if (!_formatServices.TryGetValue(out T result))
            {
                throw new InvalidOperationException($"No {typeof(T)} registered with this IoService");
            }

            return result;
        }

        public bool AddFormatService<T>(Func<T> value, out T result) where T : IFormatService
        {
            return _formatServices.TryAdd<T>(value, out result, out var _);
        }

        #region Environmental stuff

        /// <inheritdoc />
        public abstract bool CanEmptyDirectoriesExist { get; }

        /// <inheritdoc />
        public string DefaultDirectorySeparator { get; }
        /// <inheritdoc />
        public bool IsCaseSensitiveByDefault { get; }
        /// <inheritdoc />
        public virtual Folder DefaultRelativePathBase { get; protected set; }
        
        /// <summary>
        /// The default buffer size for this <see cref="IIoService"/>, used in calls where the buffer size is an optional
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

        /// <inheritdoc />
        public virtual void SetDefaultRelativePathBase(Folder defaultRelativePathBase)
        {
            DefaultRelativePathBase = defaultRelativePathBase;
        }

        /// <inheritdoc />
        public virtual void UnsetDefaultRelativePathBase()
        {
            throw new NotImplementedException();
        }

        public abstract IObservableReadOnlySet<Folder> Roots { get; }

        /// <inheritdoc />
        public abstract void UpdateRoots();

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
        
        /// <inheritdoc />
        public abstract Folder GetTemporaryFolder();

        #endregion
        #region Creating
        
        /// <inheritdoc />
        public abstract Folder CreateFolder(MissingPath path, bool createRecursively = false);

        #endregion
        #region Deleting
        
        /// <inheritdoc />
        public abstract AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false);

        /// <inheritdoc />
        public abstract AbsolutePath DeleteFile(AbsolutePath path);

        /// <inheritdoc />
        public virtual AbsolutePath Delete(AbsolutePath path, bool recursiveDeleteIfFolder = true)
        {
            if (path.IoService.Type(path) == IoFluently.PathType.File) return path.IoService.DeleteFile(path);

            if (path.IoService.Type(path) == IoFluently.PathType.Folder) return path.IoService.DeleteFolder(path, recursiveDeleteIfFolder);

            return path;
        }

        /// <inheritdoc />
        public abstract Task<AbsolutePath> DeleteFolderAsync(AbsolutePath path, CancellationToken cancellationToken,
            bool recursive = false);

        /// <inheritdoc />
        public abstract Task<AbsolutePath> DeleteFileAsync(AbsolutePath path, CancellationToken cancellationToken);

        /// <inheritdoc />
        public Task<AbsolutePath> DeleteAsync(AbsolutePath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true)
        {
            if (path.IoService.Type(path) == IoFluently.PathType.File) return path.IoService.DeleteFileAsync(path, cancellationToken);

            if (path.IoService.Type(path) == IoFluently.PathType.Folder) return path.IoService.DeleteFolderAsync(path, cancellationToken, recursiveDeleteIfFolder);

            return Task.FromResult(path);
        }

        #endregion
        #region Ensuring is

        /// <inheritdoc />
        public async Task<AbsolutePath> EnsureIsFolderAsync(AbsolutePath path, CancellationToken cancellationToken, bool createRecursively = false)
        {
            switch (path.IoService.Type(path))
            {
                case PathType.Folder:
                    break;
                case PathType.File:
                    await DeleteAsync(path, cancellationToken, true);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    CreateFolder(path);
                    break;
                case PathType.MissingPath:
                    CreateFolder(path);
                    break;
            }

            return path;
        }

        /// <inheritdoc />
        public Task<AbsolutePath> EnsureIsEmptyFolderAsync(AbsolutePath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true, bool createRecursively = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsFolder(AbsolutePath path, bool createRecursively = false)
        {
            if (!IsFolder(path))
            {
                path.IoService.CreateFolder(path);
            }

            return path;
        }
        
        /// <inheritdoc />
        public AbsolutePath EnsureIsEmptyFolder(AbsolutePath path, bool recursiveDeleteIfFolder = true, bool createRecursively = false)
        {
            if (path.IoService.Exists(path))
            {
                path.IoService.Delete(path, recursiveDeleteIfFolder);
            }

            path.IoService.CreateFolder(path);

            return path;
        }

        #endregion
        #region Ensuring is not

        /// <inheritdoc />
        public async Task<AbsolutePath> EnsureIsNotFolderAsync(AbsolutePath path, CancellationToken cancellationToken, bool recursive = false)
        {
            if (path.IoService.IsFolder(path))
            {
                await path.IoService.DeleteFolderAsync(path, cancellationToken, recursive);
            }

            return path;
        }

        /// <inheritdoc />
        public async Task<AbsolutePath> EnsureIsNotFileAsync(AbsolutePath path, CancellationToken cancellationToken)
        {
            if (path.IoService.IsFile(path))
            {
                await path.IoService.DeleteFileAsync(path, cancellationToken);
            }

            return path;
        }

        /// <inheritdoc />
        public async Task<AbsolutePath> EnsureDoesNotExistAsync(AbsolutePath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true)
        {
            if (path.IoService.Exists(path))
            {
                await path.IoService.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsNotFile(AbsolutePath path)
        {
            if (IsFile(path))
            {
                path.IoService.DeleteFile(path);
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureDoesNotExist(AbsolutePath path, bool recursiveDeleteIfFolder = true)
        {
            if (path.IoService.Exists(path))
            {
                path.IoService.Delete(path, recursiveDeleteIfFolder);
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsNotFolder(AbsolutePath path, bool recursive = false)
        {
            if (IsFolder(path))
            {
                path.IoService.DeleteFolder(path, recursive);
            }

            return path;
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
        public bool IsFile(AbsolutePath absolutePath)
        {
            return absolutePath.IoService.Type(absolutePath) == IoFluently.PathType.File;
        }

        /// <inheritdoc />
        public bool IsFolder(AbsolutePath absolutePath)
        {
            return absolutePath.IoService.Type(absolutePath) == IoFluently.PathType.Folder;
        }
        
        /// <inheritdoc />
        public bool HasExtension(AbsolutePath path)
        {
            return path.Extension.HasValue;
        }

        /// <inheritdoc />
        public virtual bool CanBeSimplified(AbsolutePath path)
        {
            return path.Path.SkipWhile(str => str == "..").Any(str => str == "..");
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
        public virtual bool IsAncestorOf(AbsolutePath path, AbsolutePath possibleDescendant)
        {
            return IsDescendantOf(possibleDescendant, path);
        }

        /// <inheritdoc />
        public virtual bool IsDescendantOf(AbsolutePath path, AbsolutePath possibleAncestor)
        {
            var possibleDescendantStr = Path.GetFullPath(path.ToString()).ToLower();
            var possibleAncestorStr = Path.GetFullPath(possibleAncestor.ToString()).ToLower();
            return possibleDescendantStr.StartsWith(possibleAncestorStr);
        }

        /// <inheritdoc />
        public virtual bool HasExtension(AbsolutePath path, string extension)
        {
            if (!extension.StartsWith(".")) {
                extension = "." + extension;
            }

            var actualExtension = Path.GetExtension(path.ToString());
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
        public abstract AbsolutePath Decrypt(AbsolutePath path);

        /// <inheritdoc />
        public abstract AbsolutePath Encrypt(AbsolutePath path);

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
        public IMaybe<AbsolutePath> TryParseAbsolutePath(string path, Folder optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var relativePath = TryParseRelativePath(path, flags);
            if (relativePath.HasValue)
            {
                return (optionallyRelativeTo / relativePath.Value).ToMaybe();
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "\\", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "\\", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "\\", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "\\", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "\\", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "/", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "/", this, components);
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
                    relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, "/", this, components);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment))
                flags = IsCaseSensitiveByDefault ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
            if (path == ".." || path == ".")
                relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{path});
            else
                relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{".", path});
            return true;
        }

        /// <inheritdoc />
        public virtual AbsolutePath ParsePathRelativeToDefault(string path)
        {
            return TryParseAbsolutePath(path, DefaultRelativePathBase).Value;
        }
        
        /// <inheritdoc />
        public virtual AbsolutePath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            AbsolutePath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        /// <inheritdoc />
        public virtual IMaybe<AbsolutePath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            AbsolutePath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                return Nothing<AbsolutePath>();
            return Something(pathSpec);
        }

        private bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec, out string error,
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

                    pathSpec = new AbsolutePath(false, "\\", this, components);
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
                    pathSpec = new AbsolutePath(false, "\\", this, components);
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
                    pathSpec = new AbsolutePath(false, "\\", this, components);
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
                    pathSpec = new AbsolutePath(false, "\\", this, components);
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
                    pathSpec = new AbsolutePath(false, "\\", this, components);
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
                    pathSpec = new AbsolutePath(true, "/", this, components);
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
                    pathSpec = new AbsolutePath(true, "/", this, components);
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
                    pathSpec = new AbsolutePath(true, "/", this, components);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            error = "Must be an absolute path";
            return false;
        }

        /// <inheritdoc />
        public AbsolutePath ParseAbsolutePath(string path, AbsolutePath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return TryParseAbsolutePath(path, optionallyRelativeTo, flags).Value;
        }

        #endregion
        #region Translation stuff
        
        #region Stuff that must be implemented

        /// <inheritdoc />
        public virtual async Task<IAbsolutePathTranslation> CopyFileAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite && translation.Destination.Exists)
            {
                await translation.Destination.IoService.DeleteAsync(translation.Destination, cancellationToken, true);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return translation;
            }
            
            var fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            using var sourceStream = translation.Source.IoService.TryOpen(translation.Source, FileMode.Open, FileAccess.Read,
                FileShare.Read, fileOptions, bufferSize).Value;
            using var destinationStream = translation.Destination.IoService.TryOpen(translation.Destination, FileMode.Open,
                FileAccess.Write, FileShare.None, fileOptions, bufferSize).Value;

            await sourceStream.CopyToAsync(destinationStream, GetBufferSizeOrDefaultInBytes(bufferSize), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            
            return translation;
        }

        /// <inheritdoc />
        public virtual async Task<IAbsolutePathTranslation> CopyFolderAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite && translation.Destination.Exists)
            {
                await translation.Destination.IoService.DeleteAsync(translation.Destination, cancellationToken, true);
            }

            translation.Destination.IoService.CreateFolder(translation.Destination);

            foreach (var child in translation)
            {
                // TODO - investigate if there's a more efficient way to do this.
                await CopyAsync(child, cancellationToken, bufferSize, overwrite);
            }

            return translation;
        }

        /// <inheritdoc />
        public virtual async Task<IAbsolutePathTranslation> MoveFileAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            await CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
            await translation.Source.IoService.DeleteAsync(translation.Source, cancellationToken, true);
            return translation;
        }

        /// <inheritdoc />
        public virtual async Task<IAbsolutePathTranslation> MoveFolderAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            await CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
            await translation.Source.IoService.DeleteAsync(translation.Source, cancellationToken, true);
            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite && translation.Destination.Exists)
            {
                translation.Destination.IoService.Delete(translation.Destination, true);
            }

            var fileOptions = FileOptions.SequentialScan;

            using var sourceStream = translation.Source.IoService.TryOpen(translation.Source, FileMode.Open, FileAccess.Read,
                FileShare.Read, fileOptions, bufferSize).Value;
            using var destinationStream = translation.Destination.IoService.TryOpen(translation.Destination, overwrite ? FileMode.Create : FileMode.CreateNew,
                FileAccess.Write, FileShare.None, fileOptions, bufferSize).Value;

            sourceStream.CopyTo(destinationStream, GetBufferSizeOrDefaultInBytes(bufferSize));
            
            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            if (overwrite && translation.Destination.Exists)
            {
                translation.Destination.IoService.Delete(translation.Destination, true);
            }

            translation.Destination.IoService.CreateFolder(translation.Destination);

            foreach (var child in translation)
            {
                Copy(child, bufferSize, overwrite);
            }

            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            CopyFile(translation, bufferSize, overwrite);
            translation.Source.IoService.Delete(translation.Source, true);
            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            CopyFolder(translation, bufferSize, overwrite);
            translation.Source.IoService.Delete(translation.Source, true);
            return translation;
        }

        #endregion

        /// <inheritdoc />
        public IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source,
            AbsolutePath destination)
        {
            return new CalculatedAbsolutePathTranslation(pathToBeCopied, source, destination, this);
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination)
        {
            return new AbsolutePathTranslation(source, destination, this);
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Copy(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Copy(Translate(pathToBeCopied, source, destination), bufferSize, overwrite);
        }
        
        /// <inheritdoc />
        public IAbsolutePathTranslation Copy(AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false) {
            return Copy(Translate(source, destination), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Move(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Move(Translate(pathToBeCopied, source, destination), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Move(AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Move(Translate(source, destination), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation RenameTo(AbsolutePath source, AbsolutePath target,
            Information? bufferSize = default, bool overwrite = false)
        {
            return Move(Translate(source, target), bufferSize, overwrite);
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.Type)
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
        public IAbsolutePathTranslation Move(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.Type)
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
        public Task<IAbsolutePathTranslation> CopyAsync(AbsolutePath pathToBeCopied, AbsolutePath source,
            AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return CopyAsync(Translate(pathToBeCopied, source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IAbsolutePathTranslation> CopyAsync(AbsolutePath source, AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return CopyAsync(Translate(source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IAbsolutePathTranslation> MoveAsync(AbsolutePath pathToBeCopied, AbsolutePath source,
            AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return MoveAsync(Translate(pathToBeCopied, source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IAbsolutePathTranslation> MoveAsync(AbsolutePath source, AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return MoveAsync(Translate(source, destination), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public Task<IAbsolutePathTranslation> RenameToAsync(AbsolutePath source, AbsolutePath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return MoveAsync(Translate(source, target), cancellationToken, bufferSize, overwrite);
        }

        /// <inheritdoc />
        public async Task<IAbsolutePathTranslation> CopyAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.Type)
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
        public async Task<IAbsolutePathTranslation> MoveAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            switch (translation.Source.Type)
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
        public AbsolutePath GenerateUniqueTemporaryPath(string extension = null)
        {
            var result = GetTemporaryFolder() / Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(extension))
            {
                result = result.WithExtension(extension);
            }

            return result;
        }

        /// <inheritdoc />
        public AbsolutePaths GlobFiles(Folder path, string pattern)
        {
            Func<AbsolutePath, IEnumerable<RelativePath>> patternFunc = absPath => absPath.Children(pattern).Select(x => new RelativePath(x.IsCaseSensitive, x.DirectorySeparator, x.IoService, new[]{x.Name}));
            return path / patternFunc;
        }

        /// <inheritdoc />
        public AbsolutePath Combine(Folder path, params string[] subsequentPathParts)
        {
            return TryDescendant(path.Path, subsequentPathParts).Value;
        }

        /// <inheritdoc />
        public AbsolutePath WithoutExtension(AbsolutePath path)
        {
            if (!HasExtension(path))
            {
                return path;
            }

            var newComponents = new List<string>();

            for (var i = 0; i < path.Path.Components.Count - 1; i++)
            {
                newComponents.Add(path.Path.Components[i]);
            }

            var name = path.Name;
            newComponents.Add(name.Substring(0, name.LastIndexOf('.')));
            
            return new AbsolutePath(path.IsCaseSensitive, path.DirectorySeparator, path.IoService, newComponents);
        }

        /// <inheritdoc />
        public IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, Func<string, string> differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension(path.Extension.ValueOrDefault ?? string.Empty));
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
            
            return new RelativePath(path.IsCaseSensitive, path.DirectorySeparator, path.IoService, result);
        }

        /// <inheritdoc />
        public virtual AbsolutePath Simplify(AbsolutePath path)
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
        public virtual IMaybe<AbsolutePath> TryParent(AbsolutePath path)
        {
            if (path.Path.Components.Count > 1)
            {
                return new AbsolutePath(path.IsCaseSensitive, path.DirectorySeparator, path.IoService,
                    path.Path.Components.Take(path.Path.Components.Count - 1)).ToMaybe();
            }
            else
            {
                return Nothing<AbsolutePath>(() => throw new InvalidOperationException($"The path {path} has only one component, so there is no parent"));
            }
        }
        
        /// <inheritdoc />
        public virtual RelativePath RelativeTo(AbsolutePath path, AbsolutePath relativeTo)
        {
            var simplified = Simplify(path);
            var pathStr = simplified.ToString();
            var common = path.IoService.TryCommonWith(path, relativeTo);

            if (!common.HasValue)
                throw new InvalidOperationException("No common ancestor");
            //return path;

            var sb = new StringBuilder();

            for (var i = 0; i < relativeTo.Path.Count - common.Value.Path.Count; i++)
            {
                sb.Append("..");
                sb.Append(path.DirectorySeparator);
            }

            var restOfRelativePath = pathStr.Substring(common.Value.ToString().Length);
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
        public virtual IMaybe<AbsolutePath> TryCommonWith(AbsolutePath path, AbsolutePath that)
        {
            var path1Str = path.ToString();
            var path2Str = that.ToString();

            if (!path.IsCaseSensitive || !that.IsCaseSensitive)
            {
                path1Str = path1Str.ToUpper();
                path2Str = path2Str.ToUpper();
            }

            var caseSensitive = path.IsCaseSensitive ||
                                that.IsCaseSensitive;
            var zippedComponents = path.Path.Components.SkipWhile(x => x == path.DirectorySeparator).Zip(that.Path.Components.SkipWhile(x => x == path.DirectorySeparator), (comp1, comp2) => 
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
        public virtual Folder Root(AbsolutePath path)
        {
            var ancestor = path;
            IMaybe<AbsolutePath> cachedParent;
            while ((cachedParent = ancestor.IoService.TryParent(ancestor)).HasValue)
                ancestor = cachedParent.Value;

            return ancestor;
        }

        /// <inheritdoc />
        public virtual IMaybe<AbsolutePath> TryGetCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            return TryParseAbsolutePath(path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\'));
        }

        /// <inheritdoc />
        public virtual IMaybe<Uri> TryGetCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            return MaybeCatch(() => new Uri(path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\'),
                UriKind.Relative));
        }

        /// <inheritdoc />
        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            return MaybeCatch(() =>
            {
                var commonAncestry = path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\');
                return new Tuple<Uri, Uri>(
                    new Uri(path1.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative),
                    new Uri(path2.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative));
            });
        }

        /// <inheritdoc />
        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            return MaybeCatch(() =>
            {
                var commonDescendants = path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\');
                return new Tuple<Uri, Uri>(
                    new Uri(
                        path1.ToString().Substring(0, path1.ToString().Length - commonDescendants.Length).Trim('\\')),
                    new Uri(
                        path2.ToString().Substring(0, path2.ToString().Length - commonDescendants.Length).Trim('\\')));
            });
        }

        /// <inheritdoc />
        public virtual IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, string differentExtension)
        {
            return TryParseAbsolutePath(Path.ChangeExtension(path.ToString(), differentExtension));
        }

        public IEnumerable<Folder> Ancestors(Folder path, bool includeItself)
        {
            if (includeItself)
            {
                yield return path;
            }

            foreach (var ancestor in Ancestors(path))
            {
                yield return ancestor;
            }
        }

        public IEnumerable<FileOrFolder> Ancestors(File path, bool includeItself)
        {
            if (includeItself)
            {
                yield return path.ExpectFileOrFolder();
            }

            foreach (var ancestor in Ancestors(path))
            {
                yield return ancestor.ExpectFileOrFolder();
            }
        }

        public IEnumerable<Folder> Ancestors(Folder path)
        {
            foreach (var ancestor in Ancestors(path.Path))
            {
                yield return ancestor.ExpectFolder();
            }
        }

        public IEnumerable<Folder> Ancestors(File path)
        {
            foreach (var ancestor in Ancestors(path.Path))
            {
                yield return ancestor.ExpectFolder();
            }
        }

        public IEnumerable<FolderOrMissingPath> Ancestors(MissingPath path)
        {
            foreach (var ancestor in Ancestors(path.Path))
            {
                yield return ancestor.ExpectFolderOrMissingPath();
            }
        }

        public IEnumerable<FolderOrMissingPath> Ancestors(MissingPath path, bool includeItself)
        {
            if (includeItself)
            {
                yield return path.ExpectFolderOrMissingPath();
            }

            foreach (var ancestor in Ancestors(path))
            {
                yield return ancestor;
            }
        }

        public IEnumerable<AbsolutePath> Ancestors(AbsolutePath path, bool includeItself)
        {
            if (includeItself)
            {
                yield return path;
            }

            foreach (var ancestor in Ancestors(path))
            {
                yield return ancestor;
            }
        }

        /// <inheritdoc />
        public virtual IEnumerable<AbsolutePath> Ancestors(AbsolutePath path)
        {
            while (true)
            {
                var maybePath = path.IoService.TryParent(path);
                if (maybePath.HasValue)
                {
                    yield return maybePath.Value;
                    path = maybePath.Value;
                }
                else
                {
                    break;
                }
            }
        }

        /// <inheritdoc />
        public virtual IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.IoService.TryDescendant(path, paths.Select(p => p.ToString()).ToArray());
        }

        /// <inheritdoc />
        public virtual IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params string[] paths)
        {
            var pathStr = path.ToString();
            // Make sure that pathStr is treated as a directory.
            if (!pathStr.EndsWith(path.DirectorySeparator))
                pathStr += path.DirectorySeparator;

            var result = path.Path.Concat(paths).ToArray();
            var combinedResult = Path.Combine(result);
            var pathResult = TryParseAbsolutePath(combinedResult);
            return pathResult;
        }
        
        /// <inheritdoc />
        public virtual IMaybe<Folder> TryAncestor(AbsolutePath path, int level)
        {
            var maybePath = path.ToMaybe();
            for (var i = 0; i < level; i++)
            {
                maybePath = maybePath.Select(p => p.IoService.TryParent(p)).SelectMany(x => x);
                if (!maybePath.HasValue)
                    return Nothing<Folder>(() => throw new InvalidOperationException($"The path {path} has no ancestor"));
            }

            return maybePath.Select(x => x.ExpectFolder());
        }

        /// <inheritdoc />
        public virtual IEnumerable<KeyValuePair<AbsolutePath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(
            IEnumerable<AbsolutePath> paths)
        {
            var alreadyProposedNames = new HashSet<string>();
            foreach (var path in paths)
            {
                var enumerator = ProposeSuccessivelyMoreSpecificNames(path).GetEnumerator();
                enumerator.MoveNext();
                while (alreadyProposedNames.Contains(enumerator.Current)) enumerator.MoveNext();

                alreadyProposedNames.Add(enumerator.Current);
                yield return new KeyValuePair<AbsolutePath, string>(path, enumerator.Current);
                enumerator.Dispose();
            }
        }

        private IEnumerable<string> ProposeSuccessivelyMoreSpecificNames(AbsolutePath path)
        {
            string filename = null;
            foreach (var parentPath in path.IoService.Ancestors(path))
            {
                if (filename == null)
                    filename = parentPath.Path.Name;
                else
                    filename = $"{parentPath.Path.Name}.{filename}";

                yield return filename;
            }
        }

        /// <inheritdoc />
        public abstract IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, string searchPattern = null, bool includeFolders = true,
            bool includeFiles = true);

        /// <inheritdoc />
        public virtual IEnumerable<AbsolutePath> EnumerateDescendants(AbsolutePath path, string searchPattern = null,
            bool includeFolders = true,
            bool includeFiles = true)
        {
            return path.TraverseTree(x =>
            {
                var children = EnumerateChildren(x);
                var childrenNames = children.Select(child => child.Name);
                return childrenNames;
            }, (AbsolutePath node, string name, out AbsolutePath child) =>
            {
                child = node / name;
                if (CanEmptyDirectoriesExist)
                {
                    var result = child.IoService.Exists(child);
                    return result;
                }

                return true;
            })
                .Skip(1)
                .Where(tt => tt.Type != TreeTraversalType.ExitBranch)
                .Select(tt => tt.Value);
        }

        #endregion
        #region File metadata
        
        /// <inheritdoc />
        public virtual bool Exists(AbsolutePath path)
        {
            var pathType = path.IoService.Type(path);
            if (pathType == IoFluently.PathType.Folder && !CanEmptyDirectoriesExist)
            {
                return false;
            }

            return pathType != IoFluently.PathType.MissingPath;
        }

        /// <inheritdoc />
        public abstract PathType Type(AbsolutePath path);

        /// <inheritdoc />
        public virtual IMaybe<bool> TryIsReadOnly(AbsolutePath path)
        {
            return TryAttributes(path).Select(attr => attr.HasFlag(FileAttributes.ReadOnly));
        }

        /// <inheritdoc />
        public abstract IMaybe<Information> TryFileSize(AbsolutePath path);

        /// <inheritdoc />
        public abstract IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes);

        /// <inheritdoc />
        public abstract IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath attributes);

        /// <inheritdoc />
        public abstract IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath attributes);

        /// <inheritdoc />
        public abstract IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath attributes);

        /// <inheritdoc />
        public IFileInfo GetFileInfo( string subpath ) => new AbsolutePathFileInfoAdapter(ParseAbsolutePath( subpath ));

        #endregion
        #region File reading
        
        /// <inheritdoc />
        public BufferEnumerator ReadBuffers(AbsolutePath path, FileShare fileShare = FileShare.None,
            Information? bufferSize = default, int paddingAtStart = 0, int paddingAtEnd = 0)
        {
            var stream = TryOpen(path, FileMode.Open, FileAccess.Read, fileShare, FileOptions.SequentialScan,
                bufferSize).Value;
            var result = new BufferEnumerator(stream, 0, GetBufferSizeOrDefaultInBytes(bufferSize), paddingAtStart, paddingAtEnd);
            return result;
        }

        #endregion
        #region File writing

        /// <inheritdoc />
        public virtual AbsolutePath WriteAllBytes(AbsolutePath path, byte[] bytes, bool createRecursively = false)
        {
            var maybeStream = TryOpen(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, FileOptions.WriteThrough, Information.FromBytes(bytes.Length), createRecursively);
            using (var stream = maybeStream.Value)
            {
                stream.Write(bytes, 0, Math.Max(bytes.Length, 1));
            }

            return path;
        }

        #endregion
        #region File open for reading or writing

        /// <inheritdoc />
        public abstract IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.None | FileOptions.SequentialScan,
            Information? bufferSize = default, bool createRecursively = false);

        #endregion
        #region LINQ-style APIs
        
        /// <inheritdoc />
        public virtual IObservable<Unit> ObserveChanges(AbsolutePath path)
        {
            return path.IoService.ObserveChanges(path, NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                       NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess |
                                       NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
        }

        /// <inheritdoc />
        public virtual IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters)
        {
            var parent = path.IoService.TryParent(path).Value;
            return parent.Children().ToLiveLinq().Where(x => x == path).AsObservable().SelectUnit();
        }

        /// <inheritdoc />
        public virtual IObservable<PathType> ObservePathType(AbsolutePath path)
        {
            var parent = path.IoService.TryParent(path);
            if (!parent.HasValue) return Observable.Return(path.IoService.Type(path));
            return parent.Value.Children(path.Name).ToLiveLinq().AsObservable().Select(_ => path.IoService.Type(path))
                .DistinctUntilChanged();
        }

        /// <inheritdoc />
        public abstract IObservable<AbsolutePath> Renamings(AbsolutePath path);

        public abstract IQueryable<AbsolutePath> Query();

        public abstract ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);
        #endregion
        #region IFileProvider implementation
        public IDirectoryContents GetDirectoryContents( string subpath ) => new AbsolutePathDirectoryContents( ParseAbsolutePath( subpath ) );

        public IChangeToken Watch( string filter )
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }
        #endregion

        protected IoServiceBase(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator)
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