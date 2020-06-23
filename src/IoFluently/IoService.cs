using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LiveLinq;
using LiveLinq.Core;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using MoreCollections;
using ReactiveProcesses;
using SimpleMonads;
using TreeLinq;
using UtilityDisposables;
using static SimpleMonads.Utility;
using Utility = LiveLinq.Utility;

namespace IoFluently
{
    public class IoService : IIoService
    {
        private readonly HashSet<AbsolutePath> _knownStorage = new HashSet<AbsolutePath>();

        private readonly object _lock = new object();
        private string defaultDirectorySeparatorForThisEnvironment;
        private PathFlags? defaultFlagsForThisEnvironment;
        
        public IReactiveProcessFactory ReactiveProcessFactory { get; }

        public AbsolutePath CurrentDirectory => TryParseAbsolutePath(Environment.CurrentDirectory).Value;

        public IReadOnlyObservableSet<AbsolutePath> Storage { get; }

        public IMaybe<StreamWriter> TryCreateText(AbsolutePath pathSpec)
        {
            try
            {
                return new Maybe<StreamWriter>(pathSpec.AsFileInfo().CreateText());
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<StreamWriter>.Nothing;
            }
            catch (IOException)
            {
                return Maybe<StreamWriter>.Nothing;
            }
            catch (SecurityException)
            {
                return Maybe<StreamWriter>.Nothing;
            }
        }

        public IEnumerable<string> ReadLines(AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            var maybeFileStream = pathSpec.TryOpen(fileMode, fileAccess, fileShare);
            if (maybeFileStream.HasValue)
            {
                using (maybeFileStream.Value)
                {
                    return ReadLines(maybeFileStream.Value, encoding, detectEncodingFromByteOrderMarks, bufferSize,
                        leaveOpen);
                }
            }

            return EnumerableUtility.EmptyArray<string>();
        }

        public IMaybe<string> TryReadText(AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
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

        public void WriteText(AbsolutePath pathSpec, IEnumerable<string> lines, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeFileStream = pathSpec.TryOpen(fileMode, fileAccess, fileShare);
            if (maybeFileStream.HasValue)
                using (maybeFileStream.Value)
                {
                    WriteLines(maybeFileStream.Value, lines, encoding, bufferSize, leaveOpen);
                }
        }

        public void WriteText(AbsolutePath pathSpec, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeFileStream = pathSpec.TryOpen(fileMode, fileAccess, fileShare);
            if (maybeFileStream.HasValue)
                using (maybeFileStream.Value)
                {
                    WriteText(maybeFileStream.Value, text, encoding, bufferSize, leaveOpen);
                }
        }

        public IEnumerable<string> ReadLines(Stream stream, Encoding encoding = null,
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

        public IEnumerable<string> ReadLinesBackwards(Stream stream, Encoding encoding = null,
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

        public string TryReadText(Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                return ReadText(sr);
            }
        }

        public AbsolutePath CreateTemporaryPath(PathType type)
        {
            var path = Path.GetRandomFileName();
            var spec = TryParseAbsolutePath(path).Value;
            if (type == PathType.File)
                spec.Create(PathType.File);
            if (type == PathType.Folder)
                spec.Create(PathType.Folder);
            return spec;
        }

        public PathFlags GetDefaultFlagsForThisEnvironment()
        {
            lock (_lock)
            {
                if (defaultFlagsForThisEnvironment == null)
                {
                    var file = Path.GetTempFileName();
                    var caseSensitive = File.Exists(file.ToLower()) && File.Exists(file.ToUpper());
                    File.Delete(file);
                    if (caseSensitive)
                        defaultFlagsForThisEnvironment = PathFlags.CaseSensitive;
                    else
                        defaultFlagsForThisEnvironment = PathFlags.None;
                }

                return defaultFlagsForThisEnvironment.Value;
            }
        }

        public string GetDefaultDirectorySeparatorForThisEnvironment()
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
        public Regex FileNamePatternToRegex(string pattern)
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

        public RelativePath ParseRelativePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            RelativePath pathSpec;
            if (!TryParseRelativePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        public IMaybe<RelativePath> TryParseRelativePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            RelativePath pathSpec;
            if (!TryParseRelativePath(path, out pathSpec, out error, flags))
                return Nothing<RelativePath>();
            return Something(pathSpec);
        }

        public bool TryParseRelativePath(string path, out RelativePath pathSpec,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            return TryParseRelativePath(path, out pathSpec, out error, flags);
        }

        public bool TryParseRelativePath(string path, out RelativePath pathSpec, out string error,
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
                    pathSpec = new RelativePath(flags, "\\", this, components);
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
                    pathSpec = new RelativePath(flags, "\\", this, components);
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
                    pathSpec = new RelativePath(flags, "\\", this, components);
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
                    pathSpec = new RelativePath(flags, "\\", this, components);
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
                    pathSpec = new RelativePath(flags, "\\", this, components);
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
                    pathSpec = new RelativePath(flags, "/", this, components);
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
                    pathSpec = new RelativePath(flags, "/", this, components);
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
                    pathSpec = new RelativePath(flags, "/", this, components);
                }

                return true;
            }

            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                flags = GetDefaultFlagsForThisEnvironment();
            if (path == ".." || path == ".")
                pathSpec = new RelativePath(flags, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{path});
            else
                pathSpec = new RelativePath(flags, GetDefaultDirectorySeparatorForThisEnvironment(), this, new[]{".", path});
            return true;
        }

        public AbsolutePath ParseAbsolutePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            AbsolutePath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        public IMaybe<AbsolutePath> TryParseAbsolutePath(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            AbsolutePath pathSpec;
            if (!TryParseAbsolutePath(path, out pathSpec, out error, flags))
                return Nothing<AbsolutePath>();
            return Something(pathSpec);
        }

        public bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec,
            PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            var error = string.Empty;
            return TryParseAbsolutePath(path, out pathSpec, out error, flags);
        }

        public bool TryParseAbsolutePath(string path, out AbsolutePath pathSpec, out string error,
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

        public void UpdateStorage()
        {
            var currentStorage = Directory.GetLogicalDrives();
            foreach (var drive in currentStorage)
            {
                var drivePath = TryParseAbsolutePath(drive).Value;
                if (!_knownStorage.Contains(drivePath))
                    _knownStorage.Add(drivePath);
            }

            var drivesThatWereRemoved = new List<AbsolutePath>();

            foreach (var drive in _knownStorage)
                if (!currentStorage.Contains(drive + "\\"))
                    drivesThatWereRemoved.Add(drive);

            foreach (var driveThatWasRemoved in drivesThatWereRemoved) _knownStorage.Remove(driveThatWasRemoved);
        }

        private string ReadText(StreamReader streamReader)
        {
            return streamReader.ReadToEnd();
        }

        public void WriteLines(Stream stream, IEnumerable<string> lines, Encoding encoding = null,
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

        public void WriteText(Stream stream, string text, Encoding encoding = null, int bufferSize = 4096,
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

        public IEnumerable<KeyValuePair<AbsolutePath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(
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

        public IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true, bool includeFiles = true)
        {
            if (!path.IsFolder()) return ImmutableArray<AbsolutePath>.Empty;

            var fullName = path.AsDirectoryInfo().FullName;

            if (includeFiles && includeFolders)
                return Directory.GetFileSystemEntries(fullName).Select(x => ParseAbsolutePath(x));

            if (includeFiles) return Directory.GetFiles(fullName).Select(x => ParseAbsolutePath(x));

            if (includeFolders)
                return Directory.GetDirectories(fullName).Select(x => ParseAbsolutePath(x));

            return ImmutableArray<AbsolutePath>.Empty;
        }

        public IEnumerable<AbsolutePath> EnumerateFileChildren(AbsolutePath path)
        {
            return EnumerateChildren(path, false);
        }

        public IEnumerable<AbsolutePath> EnumerateChildrenFolders(AbsolutePath path)
        {
            return EnumerateChildren(path, true, false);
        }

        public AbsolutePath CreateEmptyFile(AbsolutePath path)
        {
            path.CreateFile().Dispose();
            if (path.GetPathType() != PathType.File)
                throw new IOException("Could not create file " + path);
            return path;
        }

        public FileStream CreateFile(AbsolutePath path)
        {
            if (path.Parent().GetPathType() != PathType.Folder)
                path.Parent().Create(PathType.Folder);
            var result = path.AsFileInfo().Create();
            if (path.GetPathType() != PathType.File)
                throw new IOException("Could not create file " + path);
            return result;
        }

        public AbsolutePath DeleteFile(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.None)
                return path;
            try
            {
                path.AsFileInfo().Delete();
            }
            catch (IOException)
            {
                path.AsFileInfo().Delete();
            }
            catch (UnauthorizedAccessException)
            {
                path.AsFileInfo().Delete();
            }

            return path;
        }

        public AbsolutePath Decrypt(AbsolutePath path)
        {
            path.AsFileInfo().Decrypt();
            return path;
        }

        public AbsolutePath Encrypt(AbsolutePath path)
        {
            path.AsFileInfo().Encrypt();
            return path;
        }

        public AbsolutePath Delete(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File) return path.DeleteFile();

            if (path.GetPathType() == PathType.Folder) return path.DeleteFolder(true);

            return path;
        }

        public string SurroundWithDoubleQuotesIfNecessary(string str)
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

        public bool IsAncestorOf(AbsolutePath path, AbsolutePath possibleDescendant)
        {
            return IsDescendantOf(possibleDescendant, path);
        }

        public bool IsDescendantOf(AbsolutePath path, AbsolutePath possibleAncestor)
        {
            var possibleDescendantStr = Path.GetFullPath(path.ToString()).ToLower();
            var possibleAncestorStr = Path.GetFullPath(possibleAncestor.ToString()).ToLower();
            return possibleDescendantStr.StartsWith(possibleAncestorStr);
        }

        public IEnumerable<string> Split(AbsolutePath path)
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
        public IEnumerable<AbsolutePath> Ancestors(AbsolutePath path, bool includeItself = false)
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

        public IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.TryDescendant(paths.Select(p => p.ToString()).ToArray());
        }

        public IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params string[] paths)
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

        public IMaybe<AbsolutePath> TryAncestor(AbsolutePath path, int level)
        {
            var maybePath = path.ToMaybe();
            for (var i = 0; i < level; i++)
            {
                maybePath = maybePath.Select(p => p.TryParent()).SelectMany(x => x);
                if (!maybePath.HasValue)
                    return Maybe<AbsolutePath>.Nothing;
            }

            return maybePath;
        }

        public bool HasExtension(AbsolutePath path, string extension)
        {
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
        public IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, string differentExtension)
        {
            return TryParseAbsolutePath(Path.ChangeExtension(path.ToString(), differentExtension));
        }

        public IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation, bool overwrite = false)
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

        public IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation, bool overwrite = false)
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
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(string.Format(
                    "An attempt was made to copy a file from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a file."));
            translation.Destination.TryParent().Value.Create(PathType.Folder);
            File.Copy(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }

        public IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation, bool overwrite = false)
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

        public IAbsolutePathTranslation Move(IAbsolutePathTranslation translation, bool overwrite = false)
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

        public IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, bool overwrite = false)
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
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a file."));
            if (translation.Destination.GetPathType() != PathType.None)
                throw new IOException(string.Format(
                    $"An attempt was made to move \"{translation.Source}\" to \"{translation.Destination}\" but the destination path exists."));
            if (translation.Destination.IsDescendantOf(translation.Source))
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the destination path is a sub-path of the source path."));
            translation.Destination.TryParent().Value.Create(PathType.Folder);
            File.Move(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }

        public IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation, bool overwrite = false)
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

        public bool ContainsFiles(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File)
                return true;
            return path.Children().All(child => child.ContainsFiles());
        }

        public bool FolderContainsFiles(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.File)
                return false;
            return path.ContainsFiles();
        }

        public IMaybe<AbsolutePath> TryGetCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            return TryParseAbsolutePath(path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\'));
        }

        public IMaybe<Uri> TryGetCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            try
            {
                return new Maybe<Uri>(new Uri(path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\'),
                    UriKind.Relative));
            }
            catch (ArgumentNullException)
            {
                return Maybe<Uri>.Nothing;
            }
            catch (UriFormatException)
            {
                return Maybe<Uri>.Nothing;
            }
        }

        public IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2)
        {
            try
            {
                var commonAncestry = path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\');
                return new Maybe<Tuple<Uri, Uri>>(new Tuple<Uri, Uri>(
                    new Uri(path1.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative),
                    new Uri(path2.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative)));
            }
            catch (ArgumentNullException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
            catch (UriFormatException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
        }

        public IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2)
        {
            try
            {
                var commonDescendants = path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\');
                return new Maybe<Tuple<Uri, Uri>>(new Tuple<Uri, Uri>(
                    new Uri(
                        path1.ToString().Substring(0, path1.ToString().Length - commonDescendants.Length).Trim('\\')),
                    new Uri(
                        path2.ToString().Substring(0, path2.ToString().Length - commonDescendants.Length).Trim('\\'))));
            }
            catch (ArgumentNullException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
            catch (UriFormatException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
        }

        public IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination)
        {
            return new CalculatedAbsolutePathTranslation(pathToBeCopied, source, destination, this);
        }

        public IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination)
        {
            return new AbsolutePathTranslation(source, destination, this);
        }

        public Uri Child(Uri parent, Uri child)
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

        public IoService(IReactiveProcessFactory reactiveProcessFactory)
        {
            ReactiveProcessFactory = reactiveProcessFactory;
        }

        public FileInfo AsFileInfo(AbsolutePath path)
        {
            return new FileInfo(path.ToString());
        }

        public DirectoryInfo AsDirectoryInfo(AbsolutePath path)
        {
            return new DirectoryInfo(path.ToString());
        }

        public IMaybe<bool> TryIsReadOnly(AbsolutePath path)
        {
            try
            {
                return new Maybe<bool>(path.AsFileInfo().IsReadOnly);
            }
            catch (IOException)
            {
                return Maybe<bool>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<bool>.Nothing;
            }
            catch (ArgumentException)
            {
                return Maybe<bool>.Nothing;
            }
        }

        public IMaybe<long> TryLength(AbsolutePath path)
        {
            try
            {
                return new Maybe<long>(path.AsFileInfo().Length);
            }
            catch (IOException)
            {
                return Maybe<long>.Nothing;
            }
        }

        public IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<FileAttributes>(attributes.AsFileInfo().Attributes);
            }
            catch (IOException)
            {
                return Maybe<FileAttributes>.Nothing;
            }
            catch (SecurityException)
            {
                return Maybe<FileAttributes>.Nothing;
            }
            catch (ArgumentException)
            {
                return Maybe<FileAttributes>.Nothing;
            }
        }

        public IMaybe<DateTime> TryCreationTime(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<DateTime>(attributes.AsFileInfo().CreationTime);
            }
            catch (IOException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTime>.Nothing;
            }
        }

        public IMaybe<DateTime> TryLastAccessTime(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<DateTime>(attributes.AsFileInfo().LastAccessTime);
            }
            catch (IOException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTime>.Nothing;
            }
        }

        public IMaybe<DateTime> TryLastWriteTime(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<DateTime>(attributes.AsFileInfo().LastWriteTime);
            }
            catch (IOException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTime>.Nothing;
            }
        }

        public IMaybe<string> TryFullName(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<string>(attributes.AsFileInfo().FullName);
            }
            catch (SecurityException)
            {
                return Maybe<string>.Nothing;
            }
            catch (PathTooLongException)
            {
                return Maybe<string>.Nothing;
            }
        }

        /// <summary>
        ///     Includes the period character ".". For example, function would return ".exe" if the file pointed to a file named
        ///     was "test.exe".
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public IMaybe<string> TryExtension(string pathName)
        {
            var result = Path.GetExtension(pathName);
            if (string.IsNullOrEmpty(result))
                return Maybe<string>.Nothing;
            return new Maybe<string>(result);
        }

        public bool IsImageUri(Uri uri)
        {
            if (uri == null)
                return false;
            var str = uri.ToString();
            if (!str.Contains("."))
                return false;
            var extension = str.Substring(str.LastIndexOf('.') + 1);
            return ImageFileExtensions.Any(curExtension => extension == curExtension);
        }

        public bool IsVideoUri(Uri uri)
        {
            if (uri == null)
                return false;
            var str = uri.ToString();
            if (!str.Contains("."))
                return false;
            var extension = str.Substring(str.LastIndexOf('.') + 1);
            return VideoFileExtensions.Any(curExtension => extension == curExtension);
        }

        public string StripQuotes(string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\""))
                return str.Substring(1, str.Length - 2);
            if (str.StartsWith("'") && str.EndsWith("'"))
                return str.Substring(1, str.Length - 2);
            return str;
        }


        public AbsolutePath Root(AbsolutePath path)
        {
            var ancestor = path;
            IMaybe<AbsolutePath> cachedParent;
            while ((cachedParent = ancestor.TryParent()).HasValue) ancestor = cachedParent.Value;

            return ancestor;
        }

        public void RenameTo(AbsolutePath source, AbsolutePath target)
        {
            switch (source.GetPathType())
            {
                case PathType.File:
                    File.Move(source.ToString(), target.ToString());
                    break;
                case PathType.Folder:
                    Directory.Move(source.ToString(), target.ToString());
                    break;
            }
        }

        public bool Exists(AbsolutePath path)
        {
            return path.GetPathType() != PathType.None;
        }

        public PathType GetPathType(AbsolutePath path)
        {
            var str = path.ToString();
            if (File.Exists(str))
                return PathType.File;
            if (Directory.Exists(str))
                return PathType.Folder;
            return PathType.None;
        }

        public AbsolutePath ClearFolder(AbsolutePath path)
        {
            foreach (var item in path.Descendants())
            {
                item.Delete();
            }

            return path;
        }

        public AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false)
        {
            Directory.Delete(path.ToString(), recursive);

            return path;
        }

        public bool MayCreateFile(FileMode fileMode)
        {
            return fileMode.HasFlag(FileMode.Append) || fileMode.HasFlag(FileMode.Create) ||
                   fileMode.HasFlag(FileMode.CreateNew) || fileMode.HasFlag(FileMode.OpenOrCreate);
        }

        public void Create(AbsolutePath path, PathType pathType)
        {
            switch (pathType)
            {
                case PathType.Folder:
                    Directory.CreateDirectory(path.ToString());
                    break;
                case PathType.File:
                    File.WriteAllText(path.ToString(), string.Empty);
                    break;
                default:
                    throw new ArgumentException(nameof(pathType));
            }
        }

        public IMaybe<FileStream> TryOpen(AbsolutePath path, FileMode fileMode)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(path.AsFileInfo().Open(fileMode));
            }
            catch (IOException)
            {
                return Maybe<FileStream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<FileStream>.Nothing;
            }
        }

        public IMaybe<FileStream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(path.AsFileInfo().Open(fileMode, fileAccess));
            }
            catch (IOException)
            {
                return Maybe<FileStream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<FileStream>.Nothing;
            }
        }

        public IMaybe<FileStream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(path.AsFileInfo().Open(fileMode, fileAccess, fileShare));
            }
            catch (IOException)
            {
                return Maybe<FileStream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<FileStream>.Nothing;
            }
        }

        public AbsolutePath CreateFolder(AbsolutePath path)
        {
            try
            {
                if (path.GetPathType() == PathType.Folder)
                    return path;
                path.TryParent().IfHasValue(parent => parent.CreateFolder());
                path.AsDirectoryInfo().Create();
            }
            catch (IOException)
            {
                if (path.GetPathType() != PathType.Folder)
                    throw;
            }

            if (path.GetPathType() != PathType.Folder)
                throw new IOException("Failed to create folder " + path);
            return path;
        }

        public void WriteAllText(AbsolutePath path, string text)
        {
            File.WriteAllText(path.ToString(), text);
        }

        public void WriteAllLines(AbsolutePath path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path.ToString(), lines);
        }

        public void WriteAllLines(AbsolutePath path, byte[] bytes)
        {
            File.WriteAllBytes(path.ToString(), bytes);
        }

        public IEnumerable<string> ReadLines(AbsolutePath path)
        {
            return File.ReadLines(path.ToString());
        }

        public string ReadAllText(AbsolutePath path)
        {
            return File.ReadAllText(path.ToString());
        }

        #endregion

        #region FileSystemWatcher extension methods

        public IObservable<Unit> ObserveChanges(AbsolutePath path)
        {
            return path.ObserveChanges(NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                       NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess |
                                       NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
        }

        public IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Never<Unit>();

            return Observable.Create<Unit>(observer =>
            {
                var watcher = new FileSystemWatcher(parent.Value.ToString())
                {
                    IncludeSubdirectories = false,
                    Filter = path.Name,
                    EnableRaisingEvents = true,
                    NotifyFilter = filters
                };

                var subscription = Observable
                    .FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                        handler => watcher.Changed += handler, handler => watcher.Changed -= handler)
                    .Select(_ => Unit.Default)
                    .Subscribe(observer);

                return new AnonymousDisposable(() =>
                {
                    subscription.Dispose();
                    watcher.Dispose();
                });
            });
        }

        public IObservable<PathType> ObservePathType(AbsolutePath path)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Return(path.GetPathType());
            return parent.Value.Children(path.Name).ToLiveLinq().AsObservable().Select(_ => path.GetPathType())
                .DistinctUntilChanged();
        }

        public IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Return(path);

            return Observable.Create<AbsolutePath>(
                async (observer, token) =>
                {
                    var currentPath = path;
                    while (!token.IsCancellationRequested)
                    {
                        var watcher = new FileSystemWatcher(currentPath.Parent().ToString())
                        {
                            IncludeSubdirectories = false,
                            Filter = currentPath.Name
                        };

                        var tcs = new TaskCompletionSource<AbsolutePath>();

                        RenamedEventHandler handler = (_, args) =>
                        {
                            tcs.SetResult(new AbsolutePath(path.Flags, path.DirectorySeparator, this, new[]{args.FullPath}));
                        };

                        watcher.EnableRaisingEvents = true;

                        watcher.Renamed += handler;

                        currentPath = await tcs.Task;

                        observer.OnNext(currentPath);

                        watcher.Renamed -= handler;
                        watcher.Dispose();
                    }
                });
        }

        #endregion

        #region Internal extension methods

        internal static IMaybe<IEnumerable<T>> AllOrNothing<T>(IEnumerable<IMaybe<T>> source)
        {
            var source2 = source.ToList();
            if (source2.Any(opt => !opt.HasValue))
                return Nothing<IEnumerable<T>>();
            return Something(source2.Select(opt => opt.Value));
        }

        internal static IMaybe<T> Flatten<T>(IMaybe<IMaybe<T>> opt)
        {
            if (!opt.HasValue)
                return Nothing<T>();
            return opt.Value;
        }

        public StringComparison ToStringComparison(PathFlags pathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        public StringComparison ToStringComparison(PathFlags pathFlags, PathFlags otherPathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive) && otherPathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        #endregion

        #region AbsolutePath extension methods

        public RelativePath RelativeTo(AbsolutePath path, AbsolutePath relativeTo)
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

        public IMaybe<AbsolutePath> TryCommonWith(AbsolutePath path, AbsolutePath that)
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

        public bool CanBeSimplified(AbsolutePath path)
        {
            return path.Path.SkipWhile(str => str == "..").Any(str => str == "..");
        }

        public RelativePath Simplify(RelativePath path)
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

        public AbsolutePath Simplify(AbsolutePath path)
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

        public IMaybe<AbsolutePath> TryParent(AbsolutePath path)
        {
            if (path.Path.Components.Count > 1)
            {
                return new AbsolutePath(path.Flags, path.DirectorySeparator, path.IoService,
                    path.Path.Components.Take(path.Path.Components.Count - 1)).ToMaybe();
            }
            else
            {
                return Maybe<AbsolutePath>.Nothing;
            }
        }
        
        #endregion

        #region String extension methods

        public bool IsAbsoluteWindowsPath(string path)
        {
            return char.IsLetter(path[0]) && path[1] == ':';
        }

        public bool IsAbsoluteUnixPath(string path)
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

        public bool ComponentsAreAbsolute(IReadOnlyList<string> path)
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