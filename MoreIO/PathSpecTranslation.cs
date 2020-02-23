using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreIO
{
    public sealed class PathSpecTranslation : IPathSpecTranslation
    {
        internal PathSpecTranslation(PathSpec source, PathSpec destination, IIoService ioService)
        {
            Source = source;
            Destination = destination;
            IoService = ioService;
        }

        public IIoService IoService { get; }

        public PathSpec Source { get; }
        public PathSpec Destination { get; }

        public IEnumerator<CalculatedPathSpecTranslation> GetEnumerator()
        {
            return Source.Children()
                .Select(child => new CalculatedPathSpecTranslation(child, Source, Destination, IoService))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("Translate {0} to {1}", Source, Destination);
        }

        public Tuple<PathSpec, PathSpec> ToTuple()
        {
            return new Tuple<PathSpec, PathSpec>(Source, Destination);
        }

        private bool Equals(PathSpecTranslation other)
        {
            return Equals(Source, other.Source) && Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is PathSpecTranslation && Equals((PathSpecTranslation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^
                       (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PathSpecTranslation left, PathSpecTranslation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PathSpecTranslation left, PathSpecTranslation right)
        {
            return !Equals(left, right);
        }
    }
}