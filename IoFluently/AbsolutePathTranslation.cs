using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public sealed class AbsolutePathTranslation : IAbsolutePathTranslation
    {
        internal AbsolutePathTranslation(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, IFileSystem fileSystem)
        {
            Source = new AbsolutePath(source);
            Destination = new AbsolutePath(destination);
            FileSystem = fileSystem;
        }

        public IFileSystem FileSystem { get; }

        public AbsolutePath Source { get; }
        public AbsolutePath Destination { get; }

        public IAbsolutePathTranslation Invert()
        {
            return new AbsolutePathTranslation(Destination, Source, FileSystem);
        }

        public IEnumerator<CalculatedAbsolutePathTranslation> GetEnumerator()
        {
            return Source.Collapse(
                file => Enumerable.Empty<CalculatedAbsolutePathTranslation>(),
                folder => folder.FileSystem.EnumerateChildren(folder)
                    .Select(child => new CalculatedAbsolutePathTranslation(child, Source, Destination, FileSystem)),
                missingPath => Enumerable.Empty<CalculatedAbsolutePathTranslation>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("Translate {0} to {1}", Source, Destination);
        }

        public Tuple<AbsolutePath, AbsolutePath> ToTuple()
        {
            return new Tuple<AbsolutePath, AbsolutePath>(Source, Destination);
        }

        private bool Equals(AbsolutePathTranslation other)
        {
            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is AbsolutePathTranslation && Equals((AbsolutePathTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^
                       (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        public static bool operator ==(AbsolutePathTranslation left, AbsolutePathTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AbsolutePathTranslation left, AbsolutePathTranslation right)
        {
            return !Equals(left, right);
        }
    }
}
