using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public sealed class AbsolutePathTranslation : IAbsolutePathTranslation
    {
        internal AbsolutePathTranslation(AbsolutePath source, AbsolutePath destination, IIoService ioService)
        {
            Source = source;
            Destination = destination;
            IoService = ioService;
        }

        public IIoService IoService { get; }

        public AbsolutePath Source { get; }
        public AbsolutePath Destination { get; }

        public IAbsolutePathTranslation Invert()
        {
            return new AbsolutePathTranslation(Destination, Source, IoService);
        }

        public IEnumerator<CalculatedAbsolutePathTranslation> GetEnumerator()
        {
            return Source.Children()
                .Select(child => new CalculatedAbsolutePathTranslation(child, Source, Destination, IoService))
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