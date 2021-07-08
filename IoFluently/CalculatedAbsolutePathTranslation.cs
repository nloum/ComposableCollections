using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class CalculatedAbsolutePathTranslation : IAbsolutePathTranslation
    {
        private readonly AbsolutePathTranslation _actualValues;

        internal CalculatedAbsolutePathTranslation(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath, IFileOrFolderOrMissingPath ancestorSource, IFileOrFolderOrMissingPath ancestorDestination,
            IIoService ioService)
        {
            FileOrFolderOrMissingPath = fileOrFolderOrMissingPath.IoService.Simplify(fileOrFolderOrMissingPath);
            AncestorSource = ancestorSource.IoService.Simplify(ancestorSource);
            AncestorDestination = ancestorDestination.IoService.Simplify(ancestorDestination);
            IoService = ioService;
            _actualValues = Calculate();
        }

        public IAbsolutePathTranslation Invert()
        {
            return new CalculatedAbsolutePathTranslation(FileOrFolderOrMissingPath, AncestorDestination, AncestorSource, IoService);
        }

        public FileOrFolderOrMissingPath FileOrFolderOrMissingPath { get; }
        public FileOrFolderOrMissingPath AncestorSource { get; }
        public FileOrFolderOrMissingPath AncestorDestination { get; }

        public IIoService IoService { get; }

        public override string ToString()
        {
            return string.Format("Translate {0} from {1} to {2}",
                AncestorSource.IoService.TryGetNonCommonDescendants(AncestorSource, Source).Value.Item2.OriginalString, AncestorSource,
                AncestorDestination);
        }

        private AbsolutePathTranslation Calculate()
        {
            if (AncestorSource.Equals(AncestorDestination) && object.ReferenceEquals(AncestorSource.IoService, AncestorDestination.IoService))
                throw new InvalidOperationException(
                    string.Format(
                        "An attempt was made to calculate the path if a file (\"{0}\") was copied from \"{1}\" to \"{2}\". It is illegal to have the destination and source directories be the same, which is true in this case.",
                        FileOrFolderOrMissingPath, AncestorSource, AncestorDestination));
            if (!FileOrFolderOrMissingPath.IoService.IsDescendantOf(FileOrFolderOrMissingPath, AncestorSource))
                throw new InvalidOperationException(
                    string.Format(
                        "The path \"{2}\" cannot be copied to \"{1}\" because the path isn't under the source path: \"{0}\"",
                        AncestorSource, AncestorDestination, FileOrFolderOrMissingPath));
            if (AncestorSource.Equals(FileOrFolderOrMissingPath))
                return new AbsolutePathTranslation(AncestorSource, AncestorDestination, IoService);
            var relativePath = FileOrFolderOrMissingPath.IoService.RelativeTo(FileOrFolderOrMissingPath, AncestorSource);
            var pathToBeCopiedDestination = AncestorDestination / relativePath;
            return new AbsolutePathTranslation(FileOrFolderOrMissingPath, pathToBeCopiedDestination, IoService);
        }

        protected bool Equals(CalculatedAbsolutePathTranslation other)
        {
            return Equals(AncestorDestination, other.AncestorDestination) &&
                   Equals(AncestorSource, other.AncestorSource) && Equals(FileOrFolderOrMissingPath, other.FileOrFolderOrMissingPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CalculatedAbsolutePathTranslation) obj);
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

        public static bool operator ==(CalculatedAbsolutePathTranslation left, CalculatedAbsolutePathTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalculatedAbsolutePathTranslation left, CalculatedAbsolutePathTranslation right)
        {
            return !Equals(left, right);
        }

        #region IFileUriTranslation Members

        public IFileOrFolderOrMissingPath Source => _actualValues.Source;

        public IFileOrFolderOrMissingPath Destination => _actualValues.Destination;

        public IEnumerator<CalculatedAbsolutePathTranslation> GetEnumerator()
        {
            return Source.Collapse(
                file => Enumerable.Empty<CalculatedAbsolutePathTranslation>(),
                folder => folder.IoService.Descendants(folder)
                    .Select(fileUri => new CalculatedAbsolutePathTranslation(fileUri, Source, Destination, IoService)),
                missingPath => Enumerable.Empty<CalculatedAbsolutePathTranslation>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}