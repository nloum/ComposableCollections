using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreIO
{
    public class CalculatedPathSpecTranslation : IPathSpecTranslation
    {
        private readonly PathSpecTranslation _actualValues;

        internal CalculatedPathSpecTranslation(PathSpec pathSpec, PathSpec ancestorSource, PathSpec ancestorDestination,
            IIoService ioService)
        {
            PathSpec = pathSpec;
            AncestorSource = ancestorSource;
            AncestorDestination = ancestorDestination;
            IoService = ioService;
            _actualValues = Calculate();
        }

        public PathSpec PathSpec { get; }
        public PathSpec AncestorSource { get; }
        public PathSpec AncestorDestination { get; }

        public IIoService IoService { get; }

        public override string ToString()
        {
            return string.Format("Translate {0} from {1} to {2}",
                AncestorSource.GetNonCommonDescendants(Source).Value.Item2.OriginalString, AncestorSource,
                AncestorDestination);
        }

        private PathSpecTranslation Calculate()
        {
            if (AncestorSource.Equals(AncestorDestination))
                throw new InvalidOperationException(
                    string.Format(
                        "An attempt was made to calculate the path if a file (\"{0}\") was copied from \"{1}\" to \"{2}\". It is illegal to have the destination and source directories be the same, which is true in this case.",
                        PathSpec, AncestorSource, AncestorDestination));
            if (!PathSpec.IsDescendantOf(AncestorSource))
                throw new InvalidOperationException(
                    string.Format(
                        "The path \"{2}\" cannot be copied to \"{1}\" because the path isn't under the source path: \"{0}\"",
                        AncestorSource, AncestorDestination, PathSpec));
            if (AncestorSource.Equals(PathSpec))
                return new PathSpecTranslation(AncestorSource, AncestorDestination, IoService);
            var relativePath = PathSpec.RelativeTo(AncestorSource);
            var pathToBeCopiedDestination = AncestorDestination.Descendant(relativePath).Value;
            return new PathSpecTranslation(PathSpec, pathToBeCopiedDestination, IoService);
        }

        protected bool Equals(CalculatedPathSpecTranslation other)
        {
            return Equals(AncestorDestination, other.AncestorDestination) &&
                   Equals(AncestorSource, other.AncestorSource) && Equals(PathSpec, other.PathSpec);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CalculatedPathSpecTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AncestorDestination != null ? AncestorDestination.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (AncestorSource != null ? AncestorSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PathSpec != null ? PathSpec.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(CalculatedPathSpecTranslation left, CalculatedPathSpecTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalculatedPathSpecTranslation left, CalculatedPathSpecTranslation right)
        {
            return !Equals(left, right);
        }

        #region IFileUriTranslation Members

        public PathSpec Source => _actualValues.Source;

        public PathSpec Destination => _actualValues.Destination;

        public IEnumerator<CalculatedPathSpecTranslation> GetEnumerator()
        {
            return
                Source.Children()
                    .Select(fileUri => new CalculatedPathSpecTranslation(fileUri, Source, Destination, IoService))
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}