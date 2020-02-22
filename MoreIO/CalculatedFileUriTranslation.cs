using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreIO
{
    public class CalculatedFileUriTranslation : IFileUriTranslation
    {
        public IIoService IoService
         { get; }
        private readonly FileUriTranslation _actualValues;

        internal CalculatedFileUriTranslation(PathSpec pathSpec, PathSpec ancestorSource, PathSpec ancestorDestination, IIoService ioService)
        {
            PathSpec = pathSpec;
            AncestorSource = ancestorSource;
            AncestorDestination = ancestorDestination;
            IoService = ioService;
            _actualValues = Calculate();
        }

        public PathSpec PathSpec { get; private set; }
        public PathSpec AncestorSource { get; private set; }
        public PathSpec AncestorDestination { get; private set; }

        public override string ToString()
        {
            return String.Format("Translate {0} from {1} to {2}", AncestorSource.GetNonCommonDescendants(Source).Value.Item2.OriginalString, AncestorSource, AncestorDestination);
        }

        #region IFileUriTranslation Members

        public PathSpec Source
        {
            get { return _actualValues.Source; }
        }

        public PathSpec Destination
        {
            get { return _actualValues.Destination; }
        }

        public IEnumerator<CalculatedFileUriTranslation> GetEnumerator()
        {
            return
                Source.Children().Select(fileUri => new CalculatedFileUriTranslation(fileUri, Source, Destination, IoService)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private FileUriTranslation Calculate()
        {
            if (AncestorSource.Equals(AncestorDestination))
                throw new InvalidOperationException(
                    String.Format(
                        "An attempt was made to calculate the path if a file (\"{0}\") was copied from \"{1}\" to \"{2}\". It is illegal to have the destination and source directories be the same, which is true in this case.",
                        PathSpec, AncestorSource, AncestorDestination));
            if (!PathSpec.IsDescendantOf(AncestorSource))
                throw new InvalidOperationException(
                    String.Format(
                        "The path \"{2}\" cannot be copied to \"{1}\" because the path isn't under the source path: \"{0}\"",
                        AncestorSource, AncestorDestination, PathSpec));
            if (AncestorSource.Equals(PathSpec))
                return new FileUriTranslation(AncestorSource, AncestorDestination, IoService);
            var relativePath = PathSpec.RelativeTo(AncestorSource);
            PathSpec pathToBeCopiedDestination = AncestorDestination.Descendant(relativePath).Value;
            return new FileUriTranslation(PathSpec, pathToBeCopiedDestination, IoService);
        }

        protected bool Equals(CalculatedFileUriTranslation other)
        {
            return Equals(AncestorDestination, other.AncestorDestination) && Equals(AncestorSource, other.AncestorSource) && Equals(PathSpec, other.PathSpec);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CalculatedFileUriTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (AncestorDestination != null ? AncestorDestination.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (AncestorSource != null ? AncestorSource.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (PathSpec != null ? PathSpec.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(CalculatedFileUriTranslation left, CalculatedFileUriTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalculatedFileUriTranslation left, CalculatedFileUriTranslation right)
        {
            return !Equals(left, right);
        }
    }
}
