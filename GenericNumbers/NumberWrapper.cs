using System;
using GenericNumbers.Arithmetic.Abs;
using GenericNumbers.Arithmetic.Ceiling;
using GenericNumbers.Arithmetic.DividedBy;
using GenericNumbers.Arithmetic.Floor;
using GenericNumbers.Arithmetic.Minus;
using GenericNumbers.Arithmetic.Plus;
using GenericNumbers.Arithmetic.RaisedTo;
using GenericNumbers.Arithmetic.Remainder;
using GenericNumbers.Arithmetic.Round;
using GenericNumbers.Arithmetic.Times;
using GenericNumbers.Relational;

namespace GenericNumbers
{
    public abstract class NumberWrapper<T, TNumberWrapper> : IComparable<TNumberWrapper>,
        IAbs<TNumberWrapper>,
        ICeiling<TNumberWrapper>,
        IFloor<TNumberWrapper>,
        IRaisedTo<TNumberWrapper>,
        IRound<TNumberWrapper>,
        IPlus<TNumberWrapper>,
        IMinus<TNumberWrapper>,
        ITimes<TNumberWrapper>,
        IDividedBy<TNumberWrapper>,
        IRemainder<TNumberWrapper>
        where TNumberWrapper : NumberWrapper<T, TNumberWrapper>
    {
        public T Value { get; private set; }

        protected NumberWrapper(T value)
        {
            Value = value;
        }

        protected abstract TNumberWrapper Create(T value);

        public int CompareTo(TNumberWrapper other)
        {
            return Value.CompareTo(other.Value);
        }

        public void Abs(out TNumberWrapper output)
        {
            output = Create(Value.Abs());
        }

        public void Ceiling(out TNumberWrapper output)
        {
            output = Create(Value.Ceiling());
        }

        public void Floor(out TNumberWrapper output)
        {
            output = Create(Value.Floor());
        }

        public void RaisedTo(TNumberWrapper input, out TNumberWrapper output)
        {
            output = Create(Value.RaisedTo(input.Value));
        }

        public void Round(out TNumberWrapper output)
        {
            output = Create(Value.Round());
        }

        public void Plus(TNumberWrapper input, out TNumberWrapper output)
        {
            output = Create(Value.Plus(input.Value));
        }

        public void Minus(TNumberWrapper input, out TNumberWrapper output)
        {
            output = Create(Value.Minus(input.Value));
        }

        public void Times(TNumberWrapper input, out TNumberWrapper output)
        {
            output = Create(Value.Times(input.Value));
        }

        public void DividedBy(TNumberWrapper input, out TNumberWrapper output)
        {
            output = Create(Value.DividedBy(input.Value));
        }

        public void Remainder(TNumberWrapper input, out TNumberWrapper output)
        {
            output = Create(Value.Remainder(input.Value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class NumberWrapper<T> : NumberWrapper<T, NumberWrapper<T>>
    {
        public NumberWrapper(T value) : base(value)
        {
        }

        protected override NumberWrapper<T> Create(T value)
        {
            return new NumberWrapper<T>(value);
        }
    }
}