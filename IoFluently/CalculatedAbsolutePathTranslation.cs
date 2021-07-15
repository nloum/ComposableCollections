using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class CalculatedAbsolutePathTranslation : IAbsolutePathTranslation
    {
        private readonly AbsolutePathTranslation _actualValues;

        internal CalculatedAbsolutePathTranslation(IFileOrFolderOrMissingPath absolutePath, IFileOrFolderOrMissingPath ancestorSource, IFileOrFolderOrMissingPath ancestorDestination,
            IIoService ioService)
        {
            AbsoluteAbsolutePath = absolutePath.Simplify();
            AncestorSource = ancestorSource.Simplify();
            AncestorDestination = ancestorDestination.Simplify();
            IoService = ioService;
            _actualValues = Calculate();
        }

        public IAbsolutePathTranslation Invert()
        {
            return new CalculatedAbsolutePathTranslation(AbsoluteAbsolutePath, AncestorDestination, AncestorSource, IoService);
        }

        public AbsolutePath AbsoluteAbsolutePath { get; }
        public AbsolutePath AncestorSource { get; }
        public AbsolutePath AncestorDestination { get; }

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
                        AbsoluteAbsolutePath, AncestorSource, AncestorDestination));
            if (!AbsoluteAbsolutePath.IoService.IsDescendantOf(AbsoluteAbsolutePath, AncestorSource))
                throw new InvalidOperationException(
                    string.Format(
                        "The path \"{2}\" cannot be copied to \"{1}\" because the path isn't under the source path: \"{0}\"",
                        AncestorSource, AncestorDestination, AbsoluteAbsolutePath));
            if (AncestorSource.Equals(AbsoluteAbsolutePath))
                return new AbsolutePathTranslation(AncestorSource, AncestorDestination, IoService);
            var relativePath = AbsoluteAbsolutePath.IoService.RelativeTo(AbsoluteAbsolutePath, AncestorSource);
            var pathToBeCopiedDestination = AncestorDestination / relativePath;
            return new AbsolutePathTranslation(AbsoluteAbsolutePath, pathToBeCopiedDestination, IoService);
        }

        protected bool Equals(CalculatedAbsolutePathTranslation other)
        {
            return Equals(AncestorDestination, other.AncestorDestination) &&
                   Equals(AncestorSource, other.AncestorSource) && Equals(AbsoluteAbsolutePath, other.AbsoluteAbsolutePath);
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
                hashCode = (hashCode * 397) ^ (AbsoluteAbsolutePath != null ? AbsoluteAbsolutePath.GetHashCode() : 0);
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

        public AbsolutePath Source => _actualValues.Source;

        public AbsolutePath Destination => _actualValues.Destination;

        public IEnumerator<CalculatedAbsolutePathTranslation> GetEnumerator()
        {
            return Source.Collapse(
                file => Enumerable.Empty<CalculatedAbsolutePathTranslation>(),
                folder => folder.IoService.EnumerateChildren(folder)
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