using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public sealed class PathTranslation : IPathTranslation
    {
        internal PathTranslation(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, IFileSystem fileSystem)
        {
            Source = new FileOrFolderOrMissingPath(source);
            Destination = new FileOrFolderOrMissingPath(destination);
            FileSystem = fileSystem;
        }

        public IFileSystem FileSystem { get; }

        public FileOrFolderOrMissingPath Source { get; }
        public FileOrFolderOrMissingPath Destination { get; }

        public IPathTranslation Invert()
        {
            return new PathTranslation(Destination, Source, FileSystem);
        }

        public IEnumerator<CalculatedPathTranslation> GetEnumerator()
        {
            return Source.Collapse(
                file => Enumerable.Empty<CalculatedPathTranslation>(),
                folder => folder.FileSystem.EnumerateChildren(folder)
                    .Select(child => new CalculatedPathTranslation(child, Source, Destination, FileSystem)),
                missingPath => Enumerable.Empty<CalculatedPathTranslation>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("Translate {0} to {1}", Source, Destination);
        }

        public Tuple<FileOrFolderOrMissingPath, FileOrFolderOrMissingPath> ToTuple()
        {
            return new Tuple<FileOrFolderOrMissingPath, FileOrFolderOrMissingPath>(Source, Destination);
        }

        private bool Equals(PathTranslation other)
        {
            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is PathTranslation && Equals((PathTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^
                       (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PathTranslation left, PathTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PathTranslation left, PathTranslation right)
        {
            return !Equals(left, right);
        }
    }
}
