using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreIO
{
    public sealed class FileUriTranslation : IFileUriTranslation
    {
        internal FileUriTranslation(PathSpec source, PathSpec destination)
        {
            Source = source;
            Destination = destination;
        }

        public PathSpec Source { get; private set; }
        public PathSpec Destination { get; private set; }

        public IEnumerator<CalculatedFileUriTranslation> GetEnumerator()
        {
            return Source.Children().Select(child => new CalculatedFileUriTranslation(child, Source, Destination)).GetEnumerator();
        }

        public override string ToString()
        {
            return String.Format("Translate {0} to {1}", Source, Destination);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Tuple<PathSpec, PathSpec> ToTuple()
        {
            return new Tuple<PathSpec, PathSpec>(Source, Destination);
        }

        private bool Equals(FileUriTranslation other)
        {
            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is FileUriTranslation && Equals((FileUriTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0)*397) ^ (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        public static bool operator ==(FileUriTranslation left, FileUriTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileUriTranslation left, FileUriTranslation right)
        {
            return !Equals(left, right);
        }
    }
}
