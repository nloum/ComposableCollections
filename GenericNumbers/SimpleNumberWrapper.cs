using System;
using GenericNumbers.Arithmetic.Abs;
using GenericNumbers.Arithmetic.Ceiling;
using GenericNumbers.Arithmetic.Floor;
using GenericNumbers.Arithmetic.RaisedTo;
using GenericNumbers.Arithmetic.Round;
using GenericNumbers.Relational;

namespace GenericNumbers
{
    public class SimpleNumberWrapper<T> : IComparable<SimpleNumberWrapper<T>>,
        IAbs<SimpleNumberWrapper<T>>,
        ICeiling<SimpleNumberWrapper<T>>,
        IFloor<SimpleNumberWrapper<T>>,
        IRaisedTo<SimpleNumberWrapper<T>>,
        IRound<SimpleNumberWrapper<T>>
    {
        public SimpleNumberWrapper(T value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public T Value { get; }

        public void Abs(out SimpleNumberWrapper<T> output)
        {
            output = new SimpleNumberWrapper<T>(Value.Abs());
        }

        public void Ceiling(out SimpleNumberWrapper<T> output)
        {
            output = new SimpleNumberWrapper<T>(Value.Ceiling());
        }

        public int CompareTo(SimpleNumberWrapper<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        public void Floor(out SimpleNumberWrapper<T> output)
        {
            output = new SimpleNumberWrapper<T>(Value.Floor());
        }

        public void RaisedTo(SimpleNumberWrapper<T> input, out SimpleNumberWrapper<T> output)
        {
            output = new SimpleNumberWrapper<T>(Value.RaisedTo(input.Value));
        }

        public void Round(out SimpleNumberWrapper<T> output)
        {
            output = new SimpleNumberWrapper<T>(Value.Round());
        }

        public static SimpleNumberWrapper<T> operator +(SimpleNumberWrapper<T> a, SimpleNumberWrapper<T> b)
        {
            return new SimpleNumberWrapper<T>(a.Value.Plus(b.Value));
        }

        public static SimpleNumberWrapper<T> operator -(SimpleNumberWrapper<T> a, SimpleNumberWrapper<T> b)
        {
            return new SimpleNumberWrapper<T>(a.Value.Minus(b.Value));
        }

        public static SimpleNumberWrapper<T> operator *(SimpleNumberWrapper<T> a, SimpleNumberWrapper<T> b)
        {
            return new SimpleNumberWrapper<T>(a.Value.Times(b.Value));
        }

        public static SimpleNumberWrapper<T> operator /(SimpleNumberWrapper<T> a, SimpleNumberWrapper<T> b)
        {
            return new SimpleNumberWrapper<T>(a.Value.DividedBy(b.Value));
        }

        public static SimpleNumberWrapper<T> operator %(SimpleNumberWrapper<T> a, SimpleNumberWrapper<T> b)
        {
            return new SimpleNumberWrapper<T>(a.Value.Remainder(b.Value));
        }

        public override string ToString()
        {
            return Value!.ToString();
        }
    }
}