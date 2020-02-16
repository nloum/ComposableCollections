using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static SimpleMonads.Utility;
using System.Text;
using System.Text.RegularExpressions;
using MoreCollections;
using LiveLinq.Set;
using SimpleMonads;

namespace MoreIO
{
    public static partial class PathUtility
    {
        public static PathSpec CreateTemporaryPath(PathType type)
        {
            var path = Path.GetRandomFileName();
            var spec = path.ToPathSpec().Value;
            if (type == PathType.File)
                spec.Create(PathType.File);
            if (type == PathType.Folder)
                spec.Create(PathType.Folder);
            return spec;
        }

        private static object _lock = new object();
        private static PathFlags? defaultFlagsForThisEnvironment;
        private static string defaultDirectorySeparatorForThisEnvironment;

        public static PathFlags GetDefaultFlagsForThisEnvironment()
        {
            lock(_lock)
            {
                if (defaultFlagsForThisEnvironment == null)
                {
                    var file = CreateTemporaryPath(PathType.None);
                    file = (file.ToString() + "a").ToPathSpec().Value;
                    file.Create(PathType.File);
                    var caseSensitive = file.ToString().ToUpper().ToPathSpec().Value.GetPathType() != PathType.File;
                    file.Delete();
                    if (caseSensitive)
                        defaultFlagsForThisEnvironment = PathFlags.CaseSensitive;
                    else
                        defaultFlagsForThisEnvironment = PathFlags.None;
                }
                return defaultFlagsForThisEnvironment.Value;
            }
        }

        public static string GetDefaultDirectorySeparatorForThisEnvironment()
        {
            lock(_lock)
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
        ///   Returns a regex that filters files the same as the specified pattern.
        ///   From here: http://www.java2s.com/Code/CSharp/Regular-Expressions/Checksifnamematchespatternwithandwildcards.htm
        /// Copyright:   Julijan ?ribar, 2004-2007
        /// 
        /// This software is provided 'as-is', without any express or implied
        /// warranty.  In no event will the author(s) be held liable for any damages
        /// arising from the use of this software.
        /// Permission is granted to anyone to use this software for any purpose,
        /// including commercial applications, and to alter it and redistribute it
        /// freely, subject to the following restrictions:
        /// 1. The origin of this software must not be misrepresented; you must not
        ///   claim that you wrote the original software. If you use this software
        ///   in a product, an acknowledgment in the product documentation would be
        ///   appreciated but is not required.
        /// 2. Altered source versions must be plainly marked as such, and must not be
        ///   misrepresented as being the original software.
        /// 3. This notice may not be removed or altered from any source distribution.
        /// </summary>
        /// <param name="filename">
        ///   Name to match.
        /// </param>
        /// <param name="pattern">
        ///   Pattern to match to.
        /// </param>
        /// <returns>
        ///   <c>true</c> if name matches pattern, otherwise <c>false</c>.
        /// </returns>
        public static Regex FileNamePatternToRegex(string pattern)
        {
            // prepare the pattern to the form appropriate for Regex class
            var sb = new StringBuilder(pattern);
            // remove superflous occurences of  "?*" and "*?"
            while (sb.ToString().IndexOf("?*") != -1)
            {
                sb.Replace("?*", "*");
            }
            while (sb.ToString().IndexOf("*?") != -1)
            {
                sb.Replace("*?", "*");
            }
            // remove superflous occurences of asterisk '*'
            while (sb.ToString().IndexOf("**") != -1)
            {
                sb.Replace("**", "*");
            }
            // if only asterisk '*' is left, the mask is ".*"
            if (sb.ToString().Equals("*"))
                pattern = ".*";
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
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex;
        }

        public static PathSpec ParsePathSpec(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            string error = string.Empty;
            PathSpec pathSpec;
            if (!TryParsePathSpec(path, out pathSpec, out error, flags))
                throw new ArgumentException(error);
            return pathSpec;
        }

        public static IMaybe<PathSpec> TryParsePathSpec(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            string error = string.Empty;
            PathSpec pathSpec;
            if (!TryParsePathSpec(path, out pathSpec, out error, flags))
                return Nothing<PathSpec>();
            return Something(pathSpec);
        }

        public static bool TryParsePathSpec(string path, out PathSpec pathSpec, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            string error = string.Empty;
            return TryParsePathSpec(path, out pathSpec, out error, flags);
        }

        public static bool TryParsePathSpec(string path, out PathSpec pathSpec, out string error, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
        {
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility) && flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                throw new ArgumentException("Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                flags = GetDefaultFlagsForThisEnvironment();
            error = string.Empty;
            pathSpec = null;
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
                if (colonIdx > -1 && (colonIdx != 1 || !char.IsLetter(path[0]) || (path.Length > 2 && path[2] != '\\')))
                {
                    error = "A Windows path may not contain a : character, except as part of the drive specifier.";
                    return false;
                }
                var isAbsolute = path.IsAbsoluteWindowsPath();
                if (isAbsolute)
                {
                    var components = path.Split('\\').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "\\", components);
                }
                else if (path.StartsWith("."))
                {
                    var components = path.Split('\\').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "\\", components);
                }
                else if (path.StartsWith("\\\\"))
                {
                    var components = "\\\\".ItemConcat(path.Substring(2).Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "\\", components);
                }
                else if (path.StartsWith("\\"))
                {
                    var components = "\\".ItemConcat(path.Substring(1).Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "\\", components);
                }
                else
                {
                    var components = ".".ItemConcat(path.Split('\\')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "\\", components);
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
                var isAbsolute = path.IsAbsoluteUnixPath();
                if (isAbsolute)
                {
                    var components = "/".ItemConcat(path.Substring(1).Split('/')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "/", components);
                }
                else if (path.StartsWith("."))
                {
                    var components = path.Split('/').ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "/", components);
                }
                else
                {
                    var components = ".".ItemConcat(path.Split('/')).ToList();
                    components.RemoveWhere((i, str) => i != 0 && str == ".");
                    if (components.Any(String.IsNullOrEmpty))
                    {
                        error = "Must not contain any directories that have empty names";
                        return false;
                    }
                    if (components.IsAncestorOfRoot())
                    {
                        error = "Must not point to an ancestor of the filesystem root";
                        return false;
                    }
                    pathSpec = new PathSpec(flags, "/", components);
                }
                return true;
            }
            // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
            // path with one element.
            if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                flags = GetDefaultFlagsForThisEnvironment();
            if (path == ".." || path == ".")
                pathSpec = new PathSpec(flags, GetDefaultDirectorySeparatorForThisEnvironment(), path);
            else
                pathSpec = new PathSpec(flags, GetDefaultDirectorySeparatorForThisEnvironment(), ".", path);
            return true;
        }

        public static PathSpec CurrentDirectory => Environment.CurrentDirectory.ToPathSpec().Value;

        public static void UpdateStorage()
        {
            var currentStorage = System.IO.Directory.GetLogicalDrives();
            foreach (var drive in currentStorage)
            {
                var drivePath = drive.ToPathSpec().Value;
                if (!_knownStorage.Contains(drivePath))
                    _knownStorage.Add(drivePath);
            }

            var drivesThatWereRemoved = new List<PathSpec>();

            foreach (var drive in _knownStorage)
            {
                if (!currentStorage.Contains(drive + "\\"))
                    drivesThatWereRemoved.Add(drive);
            }

            foreach (var driveThatWasRemoved in drivesThatWereRemoved)
            {
                _knownStorage.Remove(driveThatWasRemoved);
            }
        }

        private static readonly HashSet<PathSpec> _knownStorage = new HashSet<PathSpec>();

        public static IReadOnlySet<PathSpec> Storage { get; }
    }
}
