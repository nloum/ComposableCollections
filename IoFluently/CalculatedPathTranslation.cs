using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class CalculatedPathTranslation : IPathTranslation
    {
        private readonly PathTranslation _actualValues;

        internal CalculatedPathTranslation(IFileOrFolderOrMissingPath absolutePath, IFileOrFolderOrMissingPath ancestorSource, IFileOrFolderOrMissingPath ancestorDestination,
            IFileSystem fileSystem)
        {
            FileOrFolderOrMissingPath = absolutePath.Simplify();
            AncestorSource = ancestorSource.Simplify();
            AncestorDestination = ancestorDestination.Simplify();
            FileSystem = fileSystem;
            _actualValues = Calculate();
        }

        public IPathTranslation Invert()
        {
            return new CalculatedPathTranslation(FileOrFolderOrMissingPath, AncestorDestination, AncestorSource, FileSystem);
        }

        public FileOrFolderOrMissingPath FileOrFolderOrMissingPath { get; }
        public FileOrFolderOrMissingPath AncestorSource { get; }
        public FileOrFolderOrMissingPath AncestorDestination { get; }

        public IFileSystem FileSystem { get; }

        public override string ToString()
        {
            return string.Format("Translate {0} from {1} to {2}",
                AncestorSource.FileSystem.TryGetNonCommonDescendants(AncestorSource, Source).Value.Item2.OriginalString, AncestorSource,
                AncestorDestination);
        }

        private PathTranslation Calculate()
        {
            if (AncestorSource.Equals(AncestorDestination) && object.ReferenceEquals(AncestorSource.FileSystem, AncestorDestination.FileSystem))
                throw new InvalidOperationException(
                    string.Format(
                        "An attempt was made to calculate the path if a file (\"{0}\") was copied from \"{1}\" to \"{2}\". It is illegal to have the destination and source directories be the same, which is true in this case.",
                        FileOrFolderOrMissingPath, AncestorSource, AncestorDestination));
            if (!FileOrFolderOrMissingPath.FileSystem.IsDescendantOf(FileOrFolderOrMissingPath, AncestorSource))
                throw new InvalidOperationException(
                    string.Format(
                        "The path \"{2}\" cannot be copied to \"{1}\" because the path isn't under the source path: \"{0}\"",
                        AncestorSource, AncestorDestination, FileOrFolderOrMissingPath));
            if (AncestorSource.Equals(FileOrFolderOrMissingPath))
                return new PathTranslation(AncestorSource, AncestorDestination, FileSystem);
            var relativePath = FileOrFolderOrMissingPath.FileSystem.RelativeTo(FileOrFolderOrMissingPath, AncestorSource);
            var pathToBeCopiedDestination = AncestorDestination / relativePath;
            return new PathTranslation(FileOrFolderOrMissingPath, pathToBeCopiedDestination, FileSystem);
        }

        protected bool Equals(CalculatedPathTranslation other)
        {
            return Equals(AncestorDestination, other.AncestorDestination) &&
                   Equals(AncestorSource, other.AncestorSource) && Equals(FileOrFolderOrMissingPath, other.FileOrFolderOrMissingPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CalculatedPathTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AncestorDestination != null ? AncestorDestination.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (AncestorSource != null ? AncestorSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileOrFolderOrMissingPath != null ? FileOrFolderOrMissingPath.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(CalculatedPathTranslation left, CalculatedPathTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalculatedPathTranslation left, CalculatedPathTranslation right)
        {
            return !Equals(left, right);
        }

        #region IFileUriTranslation Members

        public FileOrFolderOrMissingPath Source => _actualValues.Source;

        public FileOrFolderOrMissingPath Destination => _actualValues.Destination;

        public IEnumerator<CalculatedPathTranslation> GetEnumerator()
        {
            return Source.Collapse(
                file => Enumerable.Empty<CalculatedPathTranslation>(),
                folder => folder.FileSystem.EnumerateDescendants(folder)
                    .Select(fileUri => new CalculatedPathTranslation(fileUri, Source, Destination, FileSystem)),
                missingPath => Enumerable.Empty<CalculatedPathTranslation>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}