using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class CalculatedAbsolutePathTranslation : IAbsolutePathTranslation
    {
        private readonly AbsolutePathTranslation _actualValues;

        internal CalculatedAbsolutePathTranslation(AbsolutePath pathSpec, AbsolutePath ancestorSource, AbsolutePath ancestorDestination,
            IIoService ioService)
        {
            AbsolutePath = pathSpec;
            AncestorSource = ancestorSource;
            AncestorDestination = ancestorDestination;
            IoService = ioService;
            _actualValues = Calculate();
        }

        public AbsolutePath AbsolutePath { get; }
        public AbsolutePath AncestorSource { get; }
        public AbsolutePath AncestorDestination { get; }

        public IIoService IoService { get; }

        public override string ToString()
        {
            return string.Format("Translate {0} from {1} to {2}",
                AncestorSource.GetNonCommonDescendants(Source).Value.Item2.OriginalString, AncestorSource,
                AncestorDestination);
        }

        private AbsolutePathTranslation Calculate()
        {
            if (AncestorSource.Equals(AncestorDestination))
                throw new InvalidOperationException(
                    string.Format(
                        "An attempt was made to calculate the path if a file (\"{0}\") was copied from \"{1}\" to \"{2}\". It is illegal to have the destination and source directories be the same, which is true in this case.",
                        AbsolutePath, AncestorSource, AncestorDestination));
            if (!AbsolutePath.IsDescendantOf(AncestorSource))
                throw new InvalidOperationException(
                    string.Format(
                        "The path \"{2}\" cannot be copied to \"{1}\" because the path isn't under the source path: \"{0}\"",
                        AncestorSource, AncestorDestination, AbsolutePath));
            if (AncestorSource.Equals(AbsolutePath))
                return new AbsolutePathTranslation(AncestorSource, AncestorDestination, IoService);
            var relativePath = AbsolutePath.RelativeTo(AncestorSource);
            var pathToBeCopiedDestination = AncestorDestination.Descendant(relativePath).Value;
            return new AbsolutePathTranslation(AbsolutePath, pathToBeCopiedDestination, IoService);
        }

        protected bool Equals(CalculatedAbsolutePathTranslation other)
        {
            return Equals(AncestorDestination, other.AncestorDestination) &&
                   Equals(AncestorSource, other.AncestorSource) && Equals(AbsolutePath, other.AbsolutePath);
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
                hashCode = (hashCode * 397) ^ (AbsolutePath != null ? AbsolutePath.GetHashCode() : 0);
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
            return
                Source.Children()
                    .Select(fileUri => new CalculatedAbsolutePathTranslation(fileUri, Source, Destination, IoService))
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}