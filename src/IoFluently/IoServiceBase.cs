using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LiveLinq;
using LiveLinq.Set;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using MoreCollections;
using SimpleMonads;
using UnitsNet;
using static SimpleMonads.Utility;

namespace IoFluently
{
    public abstract class IoServiceBase : IIoService
    {
        protected readonly object Lock = new object();
        protected string DefaultDirectorySeparatorForThisEnvironment;

        public IOpenFilesTrackingService OpenFilesTrackingService { get; }

        public abstract AbsolutePath CurrentDirectory { get; }

        public abstract IObservableReadOnlySet<AbsolutePath> Roots { get; }

        public abstract IQueryable<AbsolutePath> Query();

        public abstract ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);

        public abstract IMaybe<StreamWriter> TryOpenWriter(AbsolutePath pathSpec);

        protected IoServiceBase(IOpenFilesTrackingService openFilesTrackingService)
        {
            OpenFilesTrackingService = openFilesTrackingService;
        }

        public IEnumerable<AbsolutePath> Ancestors(AbsolutePath path)
        {
            return Ancestors(path, false);
        }

        protected IoServiceBase(IOpenFilesTrackingService openFilesTrackingService, string newline)
        {
            OpenFilesTrackingService = openFilesTrackingService;
            _newline = newline;
        }
        
        public IFileInfo GetFileInfo( string subpath ) => new AbsolutePathFileInfoAdapter(ParseAbsolutePath( subpath ));

        public IDirectoryContents GetDirectoryContents( string subpath ) => new AbsolutePathDirectoryContents( ParseAbsolutePath( subpath ) );

        public IChangeToken Watch( string filter )
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public virtual IEnumerable<string> ReadLines(AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            var maybeStream = pathSpec.TryOpen(fileMode, fileAccess, fileShare);
            if (maybeStream.HasValue)
            {
                using (maybeStream.Value)
                {
                    return ReadLines(maybeStream.Value, encoding, detectEncodingFromByteOrderMarks, bufferSize,
                        leaveOpen);
                }
            }

            return Enumerable.Empty<string>();
        }

        public virtual IMaybe<string> TryReadText(AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return pathSpec.TryOpen(fileMode, fileAccess, fileShare).Select(
                fs =>
                {
                    using (fs)
                    {
                        return TryReadText(fs, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
                    }
                });
        }

        public virtual void WriteText(AbsolutePath pathSpec, IEnumerable<string> lines, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeStream = pathSpec.TryOpen(fileMode, fileAccess, fileShare);
            if (maybeStream.HasValue)
                using (maybeStream.Value)
                {
                    WriteLines(maybeStream.Value, lines, encoding, bufferSize, leaveOpen);
                }
        }

        public virtual void WriteText(AbsolutePath pathSpec, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeStream = pathSpec.TryOpen(fileMode, fileAccess, fileShare);
            if (maybeStream.HasValue)
                using (maybeStream.Value)
                {
                    WriteText(maybeStream.Value, text, encoding, bufferSize, leaveOpen);
                }
        }

        public virtual IEnumerable<string> ReadLines(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                string line;
                while ((line = sr.ReadLine()) != null) yield return line;
            }
        }

        public virtual IEnumerable<string> ReadLinesBackwards(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var content = string.Empty;
            // Seek file pointer to end
            stream.Seek(0, SeekOrigin.End);

            var buffer = new byte[bufferSize];

            //loop now and read backwards
            while (stream.Position > 0)
            {
                buffer.Initialize();

                int bytesRead;

                if (stream.Position - bufferSize >= 0)
                {
                    stream.Seek(-bufferSize, SeekOrigin.Current);
                    bytesRead = stream.Read(buffer, 0, bufferSize);
                    stream.Seek(-bufferSize, SeekOrigin.Current);
                }
                else
                {
                    var finalBufferSize = stream.Position;
                    stream.Seek(0, SeekOrigin.Begin);
                    bytesRead = stream.Read(buffer, 0, (int) finalBufferSize);
                    stream.Seek(0, SeekOrigin.Begin);
                }

                var strBuffer = encoding.GetString(buffer, 0, bytesRead);

                // lines is equal to what we just read, with the leftover content from last iteration appended to it.
                var lines = (strBuffer + content).Split('\n');

                // Loop through lines backwards, ignoring the first element, and yield each value
                for (var i = lines.Length - 1; i > 0; i--) yield return lines[i].Trim('\r');

                // Leftover content is part of a line defined on the line(s) that we'll read next iteration of while loop
                // so we must save leftover content for later
                content = lines[0];
            }
        }

        public virtual string TryReadText(Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                return ReadText(sr);
            }
        }

        /// <inheritdoc />
        public virtual AbsolutePath CreateTemporaryPath(PathType type)
        {
            var spec = GetTemporaryFolder() / Guid.NewGuid().ToString();
            if (type == PathType.File)
                spec.Create(PathType.File);
            if (type == PathType.Folder)
                spec.Create(PathType.Folder);
            return spec;
        }

        /// <inheritdoc />
        public abstract AbsolutePath GetTemporaryFolder();

        /// <inheritdoc />
        public abstract bool IsCaseSensitiveByDefault();

        /// <inheritdoc />
        public virtual string GetDefaultDirectorySeparatorForThisEnvironment()
        {
            if (DefaultDirectorySeparatorForThisEnvironment == null)
            {
                lock (Lock)
                {
                    if (DefaultDirectorySeparatorForThisEnvironment == null)
                    {
                        var path = Path.Combine("a", "b");
                        DefaultDirectorySeparatorForThisEnvironment = path.Substring(1, path.Length - 2);
                    }

                    return DefaultDirectorySeparatorForThisEnvironment;
                }
            }

            return DefaultDirectorySeparatorForThisEnvironment;
        }

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

        /// <inheritdoc />
        public virtual bool TryParseRelativePath(string path, out RelativePath relativePath,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            return TryParseRelativePath(path, out relativePath, out error, flags);
        }

        /// <inheritdoc />
        public virtual bool TryParseRelativePath(string path, out RelativePath relativePath, out string error,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            if (flags == CaseSensitivityMode.UseDefaultsFromEnvironment && flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                throw new ArgumentException(
                    "Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags == CaseSensitivityMode.UseDefaultsFromEnvironment)
                flags = IsCaseSensitiveByDefault() ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
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
                flags = IsCaseSensitiveByDefault() ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
            if (path == ".." || path == ".")
                relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{path});
            else
                relativePath = new RelativePath(flags == CaseSensitivityMode.CaseSensitive, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{".", path});
            return true;
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

        /// <inheritdoc />
        public virtual bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            return TryParseAbsolutePath(path, out pathSpec, out error, flags);
        }

        /// <inheritdoc />
        public virtual bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec, out string error,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment) && flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                throw new ArgumentException(
                    "Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment))
                flags = IsCaseSensitiveByDefault() ? CaseSensitivityMode.CaseSensitive : CaseSensitivityMode.CaseInsensitive;
            error = string.Empty;
            pathSpec = null;
            if (path.Contains(":") && path.Contains("/"))
            {
                path = path.Replace("/", "\\");
            }

            var backslashIndex = path.IndexOf('\\');
            var containsColon = path.Contains(":");
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
                if (path.Contains(":"))
                {
                    error = "A Unix path may not contain a : character.";
                    return false;
                }

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
        public abstract void UpdateRoots();

        private string ReadText(StreamReader streamReader)
        {
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// Writes the specified lines lazily to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the text to</param>
        /// <param name="lines">The lazily-enumerated lines of text to write to the stream</param>
        /// <param name="encoding">The text encoding to be used</param>
        /// <param name="bufferSize">The number of bytes in the StreamWriter buffer to be used</param>
        /// <param name="leaveOpen">Whether to leave the stream open after writing the lines</param>
        public virtual void WriteLines(Stream stream, IEnumerable<string> lines, Encoding encoding = null,
            int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sw = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                WriteLines(sw, lines);
            }
        }

        private void WriteLines(StreamWriter streamWriter, IEnumerable<string> lines)
        {
            foreach (var line in lines) streamWriter.WriteLine(line);
        }

        /// <summary>
        /// Write the specified text to the specified stream
        /// </summary>
        /// <param name="stream">The stream to write the text to</param>
        /// <param name="text">The text to write to the stream</param>
        /// <param name="encoding">The text encoding to be used</param>
        /// <param name="bufferSize">The number of bytes in the StreamWriter buffer to be used</param>
        /// <param name="leaveOpen">Whether to leave the stream open after writing the lines</param>
        public virtual void WriteText(Stream stream, string text, Encoding encoding = null, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sw = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                WriteText(sw, text);
            }
        }

        private void WriteText(StreamWriter streamWriter, string text)
        {
            streamWriter.Write(text);
        }

        #region File and folder extension methods

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
            foreach (var parentPath in path.Ancestors())
            {
                if (filename == null)
                    filename = parentPath.Name;
                else
                    filename = $"{parentPath.Name}.{filename}";

                yield return filename;
            }
        }

        /// <summary>
        /// Return lazily-enumerated list of child paths (optionally including both files and folders) of the specified
        /// path.
        /// </summary>
        /// <remarks>
        /// Note: because this method uses the term Children, you know that it won't return nested files and folders.
        /// The term Descendants would be used if that were the case.
        /// </remarks>
        /// <param name="path">The path whose children will be enumerated</param>
        /// <param name="includeFolders">Whether to include folders in the results</param>
        /// <param name="includeFiles">Whether to include files in the results</param>
        /// <returns>The child files and/or folders of the specified path</returns>
        public abstract IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true,
            bool includeFiles = true);

        /// <summary>
        /// Returns lazily-eumerated list of files contained in the specified folder.
        /// </summary>
        /// <remarks>
        /// Note: because this method uses the term Children, you know that it won't return nested files.
        /// The term Descendants would be used if that were the case.
        /// </remarks>
        /// <param name="path">The path whose file children will be enumerated.</param>
        /// <returns>The lazily-enumerated list of files contained in the specified folder.</returns>
        public virtual IEnumerable<AbsolutePath> EnumerateFileChildren(AbsolutePath path)
        {
            return EnumerateChildren(path, false);
        }

        /// <summary>
        /// Returns lazily-eumerated list of folders contained in the specified folder.
        /// </summary>
        /// <remarks>
        /// Note: because this method uses the term Children, you know that it won't return nested folders.
        /// The term Descendants would be used if that were the case.
        /// </remarks>
        /// <param name="path">The path whose folder children will be enumerated.</param>
        /// <returns>The lazily-enumerated list of folders contained in the specified folder.</returns>
        public virtual IEnumerable<AbsolutePath> EnumerateChildrenFolders(AbsolutePath path)
        {
            return EnumerateChildren(path, true, false);
        }

        /// <inheritdoc />
        public virtual AbsolutePath CreateEmptyFile(AbsolutePath path)
        {
            path.CreateFile().Dispose();
            return path;
        }

        /// <inheritdoc />
        public virtual Stream CreateFile(AbsolutePath path)
        {
            var stream = TryOpen(path, FileMode.CreateNew, FileAccess.ReadWrite).Value;
            return stream;
        }

        /// <inheritdoc />
        public abstract AbsolutePath DeleteFile(AbsolutePath path);

        /// <inheritdoc />
        public abstract AbsolutePath Decrypt(AbsolutePath path);

        /// <inheritdoc />
        public abstract AbsolutePath Encrypt(AbsolutePath path);

        /// <inheritdoc />
        public virtual AbsolutePath Delete(AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            if (path.GetPathType() == PathType.File) return path.DeleteFile();

            if (path.GetPathType() == PathType.Folder) return path.DeleteFolder(recursiveDeleteIfFolder);

            return path;
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

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path">The path whose ancestors will be returned.</param>
        /// <param name="includeItself">True if the first item in the enumeration should be the path you specified,
        /// false if the first path to be returned should be the parent of the specified path.</param>
        /// <returns></returns>
        public virtual IEnumerable<AbsolutePath> Ancestors(AbsolutePath path, bool includeItself)
        {
            if (includeItself)
                yield return path;
            while (true)
            {
                var maybePath = path.TryParent();
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
            return path.TryDescendant(paths.Select(p => p.ToString()).ToArray());
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
        public virtual IMaybe<AbsolutePath> TryAncestor(AbsolutePath path, int level)
        {
            var maybePath = path.ToMaybe();
            for (var i = 0; i < level; i++)
            {
                maybePath = maybePath.Select(p => p.TryParent()).SelectMany(x => x);
                if (!maybePath.HasValue)
                    return Nothing<AbsolutePath>(() => throw new InvalidOperationException($"The path {path} has no ancestor"));
            }

            return maybePath;
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

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        public virtual IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, string differentExtension)
        {
            return TryParseAbsolutePath(Path.ChangeExtension(path.ToString(), differentExtension));
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            switch (translation.Source.GetPathType())
            {
                case PathType.File:
                    CopyFile(translation, overwrite);
                    break;
                case PathType.Folder:
                    CopyFolder(translation, overwrite);
                    break;
                case PathType.None:
                    throw new IOException(
                        string.Format(
                            $"An attempt was made to copy \"{translation.Source}\" to \"{translation.Destination}\", but the source path doesn't exist."));
            }

            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            using (var source = translation.Source.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var destination = translation.Destination.Open(overwrite ? FileMode.OpenOrCreate : FileMode.CreateNew, FileAccess.Write))
            {
                byte[] buffer = new byte[32768];
                int read;
                while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destination.Write (buffer, 0, read);
                }
            }
            
            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            if (translation.Destination.Exists())
            {
                if (!overwrite)
                {
                    throw new IOException($"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" without overwriting the destination, but the destination already exists");
                }
                else
                {
                    translation.Destination.Delete();
                }
            }
            if (translation.Source.GetPathType() != PathType.Folder)
                throw new IOException(string.Format(
                    $"An attempt was made to copy a folder from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a folder."));
            translation.Destination.Create(PathType.Folder);
            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation Move(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            switch (translation.Source.GetPathType())
            {
                case PathType.File:
                    MoveFile(translation, overwrite);
                    break;
                case PathType.Folder:
                    MoveFolder(translation, overwrite);
                    break;
                case PathType.None:
                    throw new IOException(
                        string.Format(
                            $"An attempt was made to move \"{translation.Source}\" to \"{translation.Destination}\", but the source path doesn't exist."));
            }

            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            Copy(translation, overwrite);
            translation.Source.Delete();
            return translation;
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            if (translation.Destination.Exists())
            {
                if (!overwrite)
                {
                    throw new IOException($"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" without overwriting the destination, but the destination already exists");
                }
                else
                {
                    translation.Destination.Delete();
                }
            }
            if (translation.Source.GetPathType() != PathType.Folder)
                throw new IOException(string.Format(
                    $"An attempt was made to move a folder from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a folder."));
            if (translation.Destination.GetPathType() == PathType.File)
                throw new IOException(string.Format(
                    $"An attempt was made to move \"{translation.Source}\" to \"{translation.Destination}\" but the destination path is a file."));
            if (translation.Destination.IsDescendantOf(translation.Source))
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the destination path is a sub-path of the source path."));
            if (translation.Source.Children().Any())
                throw new IOException(string.Format(
                    $"An attempt was made to move the non-empty folder \"{translation.Source}\". This is not allowed because all the files should be moved first, and only then can the folder be moved, because the move operation deletes the source folder, which would of course also delete the files and folders within the source folder."));
            translation.Destination.Create(PathType.Folder);
            if (!translation.Source.Children().Any())
                translation.Source.DeleteFolder();
            return translation;
        }

        /// <inheritdoc />
        public virtual bool ContainsFiles(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File)
                return true;
            return path.Children().All(child => child.ContainsFiles());
        }

        /// <inheritdoc />
        public virtual bool FolderContainsFiles(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File)
                return false;
            return path.ContainsFiles();
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
        public IAbsolutePathTranslation Copy(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination)
        {
            return pathToBeCopied.Translate(source, destination).Copy();
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Copy(AbsolutePath source, AbsolutePath destination)
        {
            return source.Translate(destination).Copy();
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Move(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination)
        {
            return pathToBeCopied.Translate(source, destination).Move();
        }

        /// <inheritdoc />
        public IAbsolutePathTranslation Move(AbsolutePath source, AbsolutePath destination)
        {
            return source.Translate(destination).Move();
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination)
        {
            return new CalculatedAbsolutePathTranslation(pathToBeCopied, source, destination, this);
        }

        /// <inheritdoc />
        public virtual IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination)
        {
            return new AbsolutePathTranslation(source, destination, this);
        }

        /// <inheritdoc />
        public AbsolutePath ParseAbsolutePath(string path, AbsolutePath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var relativePath = TryParseRelativePath(path, flags);
            if (relativePath.HasValue)
            {
                return optionallyRelativeTo / path;
            }

            return ParseAbsolutePath(path, flags);
        }

        /// <inheritdoc />
        public IEither<AbsolutePath, RelativePath> ParsePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            var relativePath = TryParseRelativePath(path, flags);
            if (relativePath.HasValue)
            {
                return new Either<AbsolutePath, RelativePath>(relativePath.Value);
            }
            
            return new Either<AbsolutePath, RelativePath>(ParseAbsolutePath(path, flags));
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

        /// <summary>
        /// The newline character(s)
        /// </summary>
        protected string _newline;

        public virtual string GetNewlineCharacter()
        {
            return _newline;
        }
        
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
        public virtual AbsolutePath Root(AbsolutePath path)
        {
            var ancestor = path;
            IMaybe<AbsolutePath> cachedParent;
            while ((cachedParent = ancestor.TryParent()).HasValue) ancestor = cachedParent.Value;

            return ancestor;
        }

        /// <inheritdoc />
        public virtual void RenameTo(AbsolutePath source, AbsolutePath target)
        {
            Move(source.Translate(target));
        }

        /// <inheritdoc />
        public virtual bool Exists(AbsolutePath path)
        {
            return path.GetPathType() != PathType.None;
        }

        /// <inheritdoc />
        public abstract PathType GetPathType(AbsolutePath path);

        /// <inheritdoc />
        public virtual AbsolutePath ClearFolder(AbsolutePath path)
        {
            foreach (var item in path.Descendants())
            {
                item.Delete();
            }

            return path;
        }

        /// <inheritdoc />
        public abstract AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false);

        /// <inheritdoc />
        public virtual bool MayCreateFile(FileMode fileMode)
        {
            return fileMode.HasFlag(FileMode.Append) || fileMode.HasFlag(FileMode.Create) ||
                   fileMode.HasFlag(FileMode.CreateNew) || fileMode.HasFlag(FileMode.OpenOrCreate);
        }

        /// <inheritdoc />
        public virtual AbsolutePath Create(AbsolutePath path, PathType pathType)
        {
            if (pathType == PathType.File)
            {
                CreateEmptyFile(path);
            }
            else
            {
                CreateFolder(path);
            }
            
            return path;
        }

        /// <inheritdoc />
        public virtual IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode)
        {
            return TryOpen(path, fileMode, FileAccess.ReadWrite);
        }

        /// <inheritdoc />
        public virtual IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess)
        {
            var fileShare = fileAccess == FileAccess.Read ? FileShare.Read : FileShare.None;
            return TryOpen(path, fileMode, fileAccess, fileShare);
        }

        /// <inheritdoc />
        public abstract IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare);

        /// <inheritdoc />
        public abstract AbsolutePath CreateFolder(AbsolutePath path);

        /// <inheritdoc />
        public virtual void WriteAllText(AbsolutePath path, string text)
        {
            var bytes = Encoding.Default.GetBytes(text);
            WriteAllBytes(path, bytes);
        }

        /// <inheritdoc />
        public virtual void WriteAllLines(AbsolutePath path, IEnumerable<string> lines)
        {
            var maybeStream = TryOpen(path, FileMode.Create, FileAccess.ReadWrite);
            using (var stream = maybeStream.Value)
            {
                foreach (var line in lines)
                {
                    var bytes = Encoding.Default.GetBytes(line + _newline);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <inheritdoc />
        public virtual void WriteAllBytes(AbsolutePath path, byte[] bytes)
        {
            var maybeStream = TryOpen(path, FileMode.Create, FileAccess.ReadWrite);
            using (var stream = maybeStream.Value)
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        /// <inheritdoc />
        public virtual IEnumerable<string> ReadLines(AbsolutePath path)
        {
            var maybeStream = TryOpen(path, FileMode.Open, FileAccess.Read);
            using (var stream = maybeStream.Value)
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }

        /// <inheritdoc />
        public virtual string ReadAllText(AbsolutePath path)
        {
            var maybeStream = TryOpen(path, FileMode.Open, FileAccess.Read);
            using (var stream = maybeStream.Value)
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion

        #region FileSystemWatcher extension methods

        /// <inheritdoc />
        public virtual IObservable<Unit> ObserveChanges(AbsolutePath path)
        {
            return path.ObserveChanges(NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                       NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess |
                                       NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
        }

        /// <inheritdoc />
        public virtual IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters)
        {
            var parent = path.Parent();
            return parent.Children().ToLiveLinq().Where(x => x == path).AsObservable().SelectUnit();
        }

        /// <inheritdoc />
        public virtual IObservable<PathType> ObservePathType(AbsolutePath path)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Return(path.GetPathType());
            return parent.Value.Children(path.Name).ToLiveLinq().AsObservable().Select(_ => path.GetPathType())
                .DistinctUntilChanged();
        }

        /// <inheritdoc />
        public abstract IObservable<AbsolutePath> Renamings(AbsolutePath path);

        #endregion

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

        #region AbsolutePath extension methods

        /// <inheritdoc />
        public virtual RelativePath RelativeTo(AbsolutePath path, AbsolutePath relativeTo)
        {
            var simplified = path.Simplify();
            var pathStr = simplified.ToString();
            var relativeToStr = relativeTo.Simplify().ToString();

            var common = path.TryCommonWith(relativeTo);

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
        public virtual bool CanBeSimplified(AbsolutePath path)
        {
            return path.Path.SkipWhile(str => str == "..").Any(str => str == "..");
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
        
        #endregion

        #region String extension methods

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
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            var isRelative = !ComponentsAreAbsolute(pathComponents);
            for (var i = pathComponents.Count - 1; i >= 0; i--)
                if (!isRelative && i == 0)
                    result.Insert(0, pathComponents[i]);
                else if (pathComponents[i] == ".")
                    continue;
                else if (pathComponents[i] == "..")
                    numberOfComponentsToSkip++;
                else if (numberOfComponentsToSkip > 0)
                    numberOfComponentsToSkip--;
                else
                    result.Insert(0, pathComponents[i]);

            return numberOfComponentsToSkip > 0 && !isRelative;
        }

        /// <inheritdoc />
        public virtual bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            if (path[0] == "/")
                return true;
            if (char.IsLetter(path[0][0]) && path[0][1] == ':')
                return true;
            if (path[0] == "\\")
                return true;
            return false;
        }

        #endregion
        
        /// <summary>
        /// Equivalent to Path.Combine. You can also use the / operator to build paths, like this:
        /// _ioService.CurrentDirectory / "folder1" / "folder2" / "file.txt"
        /// </summary>
        public AbsolutePath Combine(AbsolutePath path, params string[] subsequentPathParts)
        {
            return Descendant(path, subsequentPathParts);
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
        public bool HasExtension(AbsolutePath path)
        {
            return path.Extension.HasValue;
        }

        /// <inheritdoc />
        public AbsolutePath Descendant(AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        /// <inheritdoc />
        public AbsolutePath Descendant(AbsolutePath path, params string[] paths)
        {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        /// <inheritdoc />
        public AbsolutePath Ancestor(AbsolutePath path, int level)
        {
            return path.IoService.TryAncestor(path, level).Value;
        }

        /// <inheritdoc />
        public AbsolutePath WithExtension(AbsolutePath path, string differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        /// <inheritdoc />
        public AbsolutePath WithExtension(AbsolutePath path, Func<string, string> differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension(path.Extension.ValueOrDefault ?? string.Empty)).Value;
        }

        /// <inheritdoc />
        public AbsolutePath GetCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonAncestry(path1, path2).Value;
        }

        /// <inheritdoc />
        public Uri GetCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonDescendants(path1, path2).Value;
        }

        /// <inheritdoc />
        public Tuple<Uri, Uri> GetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2).Value;
        }

        /// <inheritdoc />
        public Tuple<Uri, Uri> GetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2).Value;
        }

        /// <inheritdoc />
        public bool IsReadOnly(AbsolutePath path)
        {
            return path.IoService.TryIsReadOnly(path).Value;
        }

        /// <inheritdoc />
        public Information FileSize(AbsolutePath path)
        {
            return path.IoService.TryFileSize(path).Value;
        }

        /// <inheritdoc />
        public FileAttributes Attributes(AbsolutePath attributes)
        {
            return attributes.IoService.TryAttributes(attributes).Value;
        }

        /// <inheritdoc />
        public DateTimeOffset CreationTime(AbsolutePath attributes)
        {
            return attributes.IoService.TryCreationTime(attributes).Value;
        }

        /// <inheritdoc />
        public DateTimeOffset LastAccessTime(AbsolutePath attributes)
        {
            return attributes.IoService.TryLastAccessTime(attributes).Value;
        }

        /// <inheritdoc />
        public DateTimeOffset LastWriteTime(AbsolutePath attributes)
        {
            return attributes.IoService.TryLastWriteTime(attributes).Value;
        }

        /// <inheritdoc />
        public AbsolutePaths GlobFiles(AbsolutePath path, string pattern)
        {
            Func<AbsolutePath, IEnumerable<RelativePath>> patternFunc = absPath => absPath.Children(pattern).Select(x => new RelativePath(x.IsCaseSensitive, x.DirectorySeparator, x.IoService, new[]{x.Name}));
            return path / patternFunc;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsFolder(AbsolutePath path)
        {
            if (!IsFolder(path))
            {
                path.CreateFolder();
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsNotFolder(AbsolutePath path, bool recursive = false)
        {
            if (IsFolder(path))
            {
                path.DeleteFolder(recursive);
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsFile(AbsolutePath path)
        {
            if (!IsFile(path))
            {
                path.CreateEmptyFile();
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsNotFile(AbsolutePath path)
        {
            if (IsFile(path))
            {
                path.DeleteFile();
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureDoesNotExist(AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            if (path.Exists())
            {
                path.Delete(recursiveDeleteIfFolder);
            }

            return path;
        }

        /// <inheritdoc />
        public AbsolutePath EnsureIsEmptyFolder(AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            if (path.Exists())
            {
                path.Delete(recursiveDeleteIfFolder);
            }

            path.CreateFolder();

            return path;
        }

        /// <inheritdoc />
        public Stream Open(AbsolutePath path, FileMode fileMode)
        {
            return path.IoService.TryOpen(path, fileMode).Value;
        }

        /// <inheritdoc />
        public Stream Open(AbsolutePath path, FileMode fileMode, FileAccess fileAccess)
        {
            return path.IoService.TryOpen(path, fileMode, fileAccess).Value;
        }

        /// <inheritdoc />
        public Stream Open(AbsolutePath path, FileMode fileMode, FileAccess fileAccess,
            FileShare fileShare)
        {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare).Value;
        }

        /// <inheritdoc />
        public AbsolutePath CommonWith(AbsolutePath path, AbsolutePath that)
        {
            return path.IoService.TryCommonWith(path, that).Value;
        }

        /// <inheritdoc />
        public AbsolutePath Parent(AbsolutePath path)
        {
            return path.IoService.TryParent(path).Value;
        }

        /// <inheritdoc />
        public StreamWriter OpenWriter(AbsolutePath absolutePath)
        {
            return absolutePath.IoService.TryOpenWriter(absolutePath).Value;
        }

        /// <inheritdoc />
        public IMaybe<StreamReader> TryOpenReader(AbsolutePath path)
        {
            return path.TryOpen(FileMode.Open, FileAccess.Read, FileShare.Read).Select(stream => new StreamReader(stream));
        }

        /// <inheritdoc />
        public StreamReader OpenReader(AbsolutePath path)
        {
            return TryOpenReader(path).Value;
        }

        /// <inheritdoc />
        public string ReadText(AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return absolutePath.IoService.TryReadText(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen).Value;
        }

        /// <inheritdoc />
        public bool IsFile(AbsolutePath absolutePath)
        {
            return absolutePath.GetPathType() == PathType.File;
        }

        /// <inheritdoc />
        public bool IsFolder(AbsolutePath absolutePath)
        {
            return absolutePath.GetPathType() == PathType.Folder;
        }
    }
}