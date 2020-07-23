using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LiveLinq;
using LiveLinq.Set;
using MoreCollections;
using ReactiveProcesses;
using SimpleMonads;
using UnitsNet;
using UtilityDisposables;
using EnumerableUtility = MoreCollections.EnumerableUtility;
using static SimpleMonads.Utility;

namespace IoFluently
{
    public abstract class IoServiceBase : IIoService
    {
        protected readonly object _lock = new object();
        protected string defaultDirectorySeparatorForThisEnvironment;
        protected PathFlags? defaultFlagsForThisEnvironment;
        
        public virtual IReactiveProcessFactory ReactiveProcessFactory { get; }

        public virtual AbsolutePath CurrentDirectory => TryParseAbsolutePath(Environment.CurrentDirectory).Value;

        public abstract IReadOnlyObservableSet<AbsolutePath> Storage { get; }

        public abstract ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);

        public abstract IMaybe<StreamWriter> TryOpenWriter(AbsolutePath pathSpec);

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

            return EnumerableUtility.EmptyArray<string>();
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

        public virtual AbsolutePath CreateTemporaryPath(PathType type)
        {
            var path = Path.GetRandomFileName();
            var spec = TryParseAbsolutePath(path).Value;
            if (type == PathType.File)
                spec.Create(PathType.File);
            if (type == PathType.Folder)
                spec.Create(PathType.Folder);
            return spec;
        }

        public abstract PathFlags GetDefaultFlagsForThisEnvironment();

        public virtual string GetDefaultDirectorySeparatorForThisEnvironment()
        {
            lock (_lock)
            {
                if (defaultDirectorySeparatorForThisEnvironment == null)
                {
                    var path = Path.Combine("a", "b");
                    defaultDirectorySeparatorForThisEnvironment = path.Substring(1, path.Length - 2);
                }

                return defaultDirectorySeparatorForThisEnvironment;
            }
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

        public virtual RelativePath ParseRelativePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            RelativePath pathSpec;
            if (!TryParseRelativePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        public virtual IMaybe<RelativePath> TryParseRelativePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            RelativePath pathSpec;
            if (!TryParseRelativePath(path, out pathSpec, out error, flags))
                return Nothing<RelativePath>();
            return Something(pathSpec);
        }

        public virtual bool TryParseRelativePath(string path, out RelativePath relativePath,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            return TryParseRelativePath(path, out relativePath, out error, flags);
        }

        public virtual bool TryParseRelativePath(string path, out RelativePath relativePath, out string error,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility) && flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                throw new ArgumentException(
                    "Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                flags = GetDefaultFlagsForThisEnvironment();
            error = string.Empty;
            relativePath = null;
            if (path.Contains(":") && path.Contains("/"))
            {
                path = path.Replace("/", "\\");
            }
            var isWindowsStyle = path.Contains("\\") || path.Contains(":");
            var isUnixStyle = path.Contains("/");
            if (isWindowsStyle && isUnixStyle)
            {
                error = "Cannot mix slashes and backslashes in the same path";
                return false;
            }

            if (isWindowsStyle)
            {
                if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                    flags = PathFlags.None;
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
                    relativePath = new RelativePath(flags, "\\", this, components);
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
                    relativePath = new RelativePath(flags, "\\", this, components);
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
                    relativePath = new RelativePath(flags, "\\", this, components);
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
                    relativePath = new RelativePath(flags, "\\", this, components);
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
                    relativePath = new RelativePath(flags, "\\", this, components);
                }

                return true;
            }

            if (isUnixStyle)
            {
                if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                    flags = PathFlags.CaseSensitive;
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
                    relativePath = new RelativePath(flags, "/", this, components);
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
                    relativePath = new RelativePath(flags, "/", this, components);
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
                    relativePath = new RelativePath(flags, "/", this, components);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                flags = GetDefaultFlagsForThisEnvironment();
            if (path == ".." || path == ".")
                relativePath = new RelativePath(flags, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{path});
            else
                relativePath = new RelativePath(flags, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{".", path});
            return true;
        }

        public virtual AbsolutePath ParseAbsolutePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            AbsolutePath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        public virtual IMaybe<AbsolutePath> TryParseAbsolutePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            AbsolutePath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                return Nothing<AbsolutePath>();
            return Something(pathSpec);
        }

        public virtual bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            return TryParseAbsolutePath(path, out pathSpec, out error, flags);
        }

        public virtual bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec, out string error,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility) && flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                throw new ArgumentException(
                    "Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                flags = GetDefaultFlagsForThisEnvironment();
            error = string.Empty;
            pathSpec = null;
            if (path.Contains(":") && path.Contains("/"))
            {
                path = path.Replace("/", "\\");
            }
            var isWindowsStyle = path.Contains("\\") || path.Contains(":");
            var isUnixStyle = path.Contains("/");
            if (isWindowsStyle && isUnixStyle)
            {
                error = "Cannot mix slashes and backslashes in the same path";
                return false;
            }

            if (isWindowsStyle)
            {
                if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                    flags = PathFlags.None;
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

                    pathSpec = new AbsolutePath(flags, "\\", this, components);
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
                    pathSpec = new AbsolutePath(flags, "\\", this, components);
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
                    pathSpec = new AbsolutePath(flags, "\\", this, components);
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
                    pathSpec = new AbsolutePath(flags, "\\", this, components);
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
                    pathSpec = new AbsolutePath(flags, "\\", this, components);
                }

                return true;
            }

            if (isUnixStyle)
            {
                if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                    flags = PathFlags.CaseSensitive;
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
                    pathSpec = new AbsolutePath(flags, "/", this, components);
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
                    pathSpec = new AbsolutePath(flags, "/", this, components);
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
                    pathSpec = new AbsolutePath(flags, "/", this, components);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            error = "Must be an absolute path";
            return false;
        }

        public abstract void UpdateStorage();

        private string ReadText(StreamReader streamReader)
        {
            return streamReader.ReadToEnd();
        }

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

        public abstract IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true,
            bool includeFiles = true);

        public virtual IEnumerable<AbsolutePath> EnumerateFileChildren(AbsolutePath path)
        {
            return EnumerateChildren(path, false);
        }

        public virtual IEnumerable<AbsolutePath> EnumerateChildrenFolders(AbsolutePath path)
        {
            return EnumerateChildren(path, true, false);
        }

        public virtual AbsolutePath CreateEmptyFile(AbsolutePath path)
        {
            path.CreateFile().Dispose();
            return path;
        }

        public virtual Stream CreateFile(AbsolutePath path)
        {
            var stream = TryOpen(path, FileMode.CreateNew, FileAccess.ReadWrite).Value;
            return stream;
        }

        public abstract AbsolutePath DeleteFile(AbsolutePath path);

        public abstract AbsolutePath Decrypt(AbsolutePath path);

        public abstract AbsolutePath Encrypt(AbsolutePath path);

        public virtual AbsolutePath Delete(AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            if (path.GetPathType() == PathType.File) return path.DeleteFile();

            if (path.GetPathType() == PathType.Folder) return path.DeleteFolder(recursiveDeleteIfFolder);

            return path;
        }

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

        public virtual bool IsAncestorOf(AbsolutePath path, AbsolutePath possibleDescendant)
        {
            return IsDescendantOf(possibleDescendant, path);
        }

        public virtual bool IsDescendantOf(AbsolutePath path, AbsolutePath possibleAncestor)
        {
            var possibleDescendantStr = Path.GetFullPath(path.ToString()).ToLower();
            var possibleAncestorStr = Path.GetFullPath(possibleAncestor.ToString()).ToLower();
            return possibleDescendantStr.StartsWith(possibleAncestorStr);
        }

        public virtual IEnumerable<string> Split(AbsolutePath path)
        {
            return Ancestors(path, true).Select(pathName => Path.GetFileName(pathName.ToString())).Reverse();
        }

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        public virtual IEnumerable<AbsolutePath> Ancestors(AbsolutePath path, bool includeItself = false)
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

        public virtual IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.TryDescendant(paths.Select(p => p.ToString()).ToArray());
        }

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

        public virtual IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            switch (translation.Source.GetPathType())
            {
                case PathType.File:
                    translation.CopyFile(overwrite);
                    break;
                case PathType.Folder:
                    translation.CopyFolder(overwrite);
                    break;
                case PathType.None:
                    throw new IOException(
                        string.Format(
                            $"An attempt was made to copy \"{translation.Source}\" to \"{translation.Destination}\", but the source path doesn't exist."));
            }

            return translation;
        }

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

        public virtual IAbsolutePathTranslation Move(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            switch (translation.Source.GetPathType())
            {
                case PathType.File:
                    translation.MoveFile(overwrite);
                    break;
                case PathType.Folder:
                    translation.MoveFolder(overwrite);
                    break;
                case PathType.None:
                    throw new IOException(
                        string.Format(
                            $"An attempt was made to move \"{translation.Source}\" to \"{translation.Destination}\", but the source path doesn't exist."));
            }

            return translation;
        }

        public virtual IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            translation.Copy(overwrite);
            translation.Source.Delete();
            return translation;
        }

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

        public virtual bool ContainsFiles(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File)
                return true;
            return path.Children().All(child => child.ContainsFiles());
        }

        public virtual bool FolderContainsFiles(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File)
                return false;
            return path.ContainsFiles();
        }

        public virtual IMaybe<AbsolutePath> TryGetCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            return TryParseAbsolutePath(path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\'));
        }

        public virtual IMaybe<Uri> TryGetCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            return MaybeCatch(() => new Uri(path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\'),
                    UriKind.Relative));
        }

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

        public virtual IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination)
        {
            return new CalculatedAbsolutePathTranslation(pathToBeCopied, source, destination, this);
        }

        public virtual IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination)
        {
            return new AbsolutePathTranslation(source, destination, this);
        }

        public AbsolutePath ParseAbsolutePath(string path, AbsolutePath optionallyRelativeTo,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var relativePath = TryParseRelativePath(path, flags);
            if (relativePath.HasValue)
            {
                return optionallyRelativeTo / path;
            }

            return ParseAbsolutePath(path, flags);
        }

        public IEither<AbsolutePath, RelativePath> ParsePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var relativePath = TryParseRelativePath(path, flags);
            if (relativePath.HasValue)
            {
                return new Either<AbsolutePath, RelativePath>(relativePath.Value);
            }
            
            return new Either<AbsolutePath, RelativePath>(ParseAbsolutePath(path, flags));
        }

        public bool IsRelativePath(string path)
        {
            return TryParseRelativePath(path, PathFlags.UseDefaultsForGivenPath).HasValue;
        }

        public bool IsAbsolutePath(string path)
        {
            return TryParseAbsolutePath(path, PathFlags.UseDefaultsForGivenPath).HasValue;
        }

        public virtual Uri Child(Uri parent, Uri child)
        {
            var parentLocalPath = parent.ToString();
            if (!parentLocalPath.EndsWith(Path.DirectorySeparatorChar.ToString())
                && !parentLocalPath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                parentLocalPath += Path.DirectorySeparatorChar;
            return new Uri(parentLocalPath).MakeRelativeUri(child);
        }

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

        protected string _newline;

        protected IoServiceBase(IReactiveProcessFactory reactiveProcessFactory, string newline)
        {
            ReactiveProcessFactory = reactiveProcessFactory;
            _newline = newline;
        }

        public abstract IMaybe<bool> TryIsReadOnly(AbsolutePath path);

        public abstract IMaybe<Information> TryFileSize(AbsolutePath path);

        public abstract IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes);

        public abstract IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath attributes);

        public abstract IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath attributes);

        public abstract IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath attributes);

        public abstract IMaybe<string> TryFullName(AbsolutePath attributes);

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

        public virtual string StripQuotes(string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\""))
                return str.Substring(1, str.Length - 2);
            if (str.StartsWith("'") && str.EndsWith("'"))
                return str.Substring(1, str.Length - 2);
            return str;
        }


        public virtual AbsolutePath Root(AbsolutePath path)
        {
            var ancestor = path;
            IMaybe<AbsolutePath> cachedParent;
            while ((cachedParent = ancestor.TryParent()).HasValue) ancestor = cachedParent.Value;

            return ancestor;
        }

        public virtual void RenameTo(AbsolutePath source, AbsolutePath target)
        {
            source.Translate(target).Move();
        }

        public virtual bool Exists(AbsolutePath path)
        {
            return path.GetPathType() != PathType.None;
        }

        public abstract PathType GetPathType(AbsolutePath path);

        public virtual AbsolutePath ClearFolder(AbsolutePath path)
        {
            foreach (var item in path.Descendants())
            {
                item.Delete();
            }

            return path;
        }

        public abstract AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false);

        public virtual bool MayCreateFile(FileMode fileMode)
        {
            return fileMode.HasFlag(FileMode.Append) || fileMode.HasFlag(FileMode.Create) ||
                   fileMode.HasFlag(FileMode.CreateNew) || fileMode.HasFlag(FileMode.OpenOrCreate);
        }

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

        public virtual IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode)
        {
            return TryOpen(path, fileMode, FileAccess.ReadWrite);
        }

        public virtual IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess)
        {
            var fileShare = fileAccess == FileAccess.Read ? FileShare.Read : FileShare.None;
            return TryOpen(path, fileMode, fileAccess, fileShare);
        }

        public abstract IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare);

        public abstract AbsolutePath CreateFolder(AbsolutePath path);

        public virtual void WriteAllText(AbsolutePath path, string text)
        {
            var bytes = Encoding.Default.GetBytes(text);
            WriteAllBytes(path, bytes);
        }

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
        
        public virtual void WriteAllBytes(AbsolutePath path, byte[] bytes)
        {
            var maybeStream = TryOpen(path, FileMode.Create, FileAccess.ReadWrite);
            using (var stream = maybeStream.Value)
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

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

        public virtual IObservable<Unit> ObserveChanges(AbsolutePath path)
        {
            return path.ObserveChanges(NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                       NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess |
                                       NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
        }

        public virtual IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters)
        {
            var parent = path.Parent();
            return parent.Children().ToLiveLinq().Where(x => x == path).AsObservable().SelectUnit();
        }

        public virtual IObservable<PathType> ObservePathType(AbsolutePath path)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Return(path.GetPathType());
            return parent.Value.Children(path.Name).ToLiveLinq().AsObservable().Select(_ => path.GetPathType())
                .DistinctUntilChanged();
        }

        public abstract IObservable<AbsolutePath> Renamings(AbsolutePath path);

        #endregion

        #region Internal extension methods

        public virtual StringComparison ToStringComparison(PathFlags pathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        public virtual StringComparison ToStringComparison(PathFlags pathFlags, PathFlags otherPathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive) && otherPathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        #endregion

        #region AbsolutePath extension methods

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

        public virtual IMaybe<AbsolutePath> TryCommonWith(AbsolutePath path, AbsolutePath that)
        {
            var path1Str = path.ToString();
            var path2Str = that.ToString();

            if (!path.Flags.HasFlag(PathFlags.CaseSensitive) || !that.Flags.HasFlag(PathFlags.CaseSensitive))
            {
                path1Str = path1Str.ToUpper();
                path2Str = path2Str.ToUpper();
            }

            var caseSensitive = path.Flags.HasFlag(PathFlags.CaseSensitive) ||
                                that.Flags.HasFlag(PathFlags.CaseSensitive);
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

        public virtual bool CanBeSimplified(AbsolutePath path)
        {
            return path.Path.SkipWhile(str => str == "..").Any(str => str == "..");
        }

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
            
            return new RelativePath(path.Flags, path.DirectorySeparator, path.IoService, result);
        }

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
            return TryParseAbsolutePath(str, path.Flags).Value;
        }

        public virtual IMaybe<AbsolutePath> TryParent(AbsolutePath path)
        {
            if (path.Path.Components.Count > 1)
            {
                return new AbsolutePath(path.Flags, path.DirectorySeparator, path.IoService,
                    path.Path.Components.Take(path.Path.Components.Count - 1)).ToMaybe();
            }
            else
            {
                return Nothing<AbsolutePath>(() => throw new InvalidOperationException($"The path {path} has only one component, so there is no parent"));
            }
        }
        
        #endregion

        #region String extension methods

        public virtual bool IsAbsoluteWindowsPath(string path)
        {
            return char.IsLetter(path[0]) && path[1] == ':';
        }

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
    }
}