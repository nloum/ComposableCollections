﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using SimpleMonads;
using TreeLinq;

namespace IoFluently
{
    public partial class AbsolutePath : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder, IMissingPath>, IFileOrFolderOrMissingPath
    {
        private readonly AbsoluteTreePath<string> _treePath;
        public IReadOnlyList<string> Components => _treePath.Components;
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }

        protected AbsolutePath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IIoService ioService)
        {
            _treePath = new AbsoluteTreePath<string>(components);
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
        }

        public override IFile? Item1 => IoService.Type(this) == PathType.File
            ? new File(Components, IsCaseSensitive, DirectorySeparator, IoService) : null;
        public override IFolder? Item2 => IoService.Type(this) == PathType.Folder
            ? new Folder(Components, IsCaseSensitive, DirectorySeparator, IoService) : null;
        public override IMissingPath? Item3 => IoService.Type(this) == PathType.MissingPath
            ? new MissingPath(Components, IsCaseSensitive, DirectorySeparator, IoService) : null;
        
        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (_treePath != null ? _treePath.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public int CompareTo(AbsolutePath other)
        {
            var compareCounts = Components.Count - other.Components.Count;
            if (compareCounts != 0)
                return compareCounts;
            for (var i = 0; i < Components.Count; i++)
            {
                var compareElement = Components[i].CompareTo(other.Components[i]);
                if (compareElement != 0)
                    return compareElement;
            }

            return 0;
        }

        /// <inheritdoc />
        public bool Equals(AbsolutePath other)
        {
            return CompareTo(other) == 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Components.Count; i++)
            {
                sb.Append(Components[i]);
                if (Components[i] != DirectorySeparator && i + 1 != Components.Count &&
                    sb.ToString() != DirectorySeparator)
                    sb.Append(DirectorySeparator);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts this AbsolutePath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(AbsolutePath path)
        {
            return path.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AbsolutePath) obj);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator /(AbsolutePath absPath, string whatToAdd)
        {
            if (string.IsNullOrEmpty(whatToAdd))
            {
                return absPath;
            }

            return new AbsolutePath(absPath.Components.Concat(whatToAdd.Split('/', '\\')).ToImmutableList(), absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator /(AbsolutePath absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService,
                new AbsoluteTreePaths<string>(absPath.Components.Select(component => 
                    new Either<string, IEnumerable<RelativeTreePath<string>>, Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>>>(component))
                .Concat<IEither<string, IEnumerable<RelativeTreePath<string>>, Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>>>>(whatToAdd.Select(component => 
                    new Either<string, IEnumerable<RelativeTreePath<string>>, Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>>>(component)))));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator /(AbsolutePath absPath,
            Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService,
                absPath._treePath / (x =>
                    whatToAdd(new AbsolutePath(x.Components, absPath.IsCaseSensitive, absPath.DirectorySeparator,
                        absPath.IoService)).Select(x => x.Path)));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator /(AbsolutePath absPath, RelativePath whatToAdd)
        {
            if (whatToAdd == null)
            {
                return absPath;
            }

            return new AbsolutePath(absPath.Components.Concat(whatToAdd.Path).ToImmutableList(), absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator /(AbsolutePath absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService,
                absPath._treePath / whatToAdd.Select(x => new RelativeTreePath<string>(x.Split('/', '\\'))));
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(AbsolutePath left, AbsolutePath right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(AbsolutePath left, AbsolutePath right)
        {
            return !Equals(left, right);
        }
    }
}