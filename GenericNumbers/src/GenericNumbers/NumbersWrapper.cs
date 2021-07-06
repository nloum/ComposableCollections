using System.Collections.Generic;
using System.Linq;
using GenericNumbers.Arithmetic.DividedBy;
using GenericNumbers.Arithmetic.Minus;
using GenericNumbers.Arithmetic.Plus;
using GenericNumbers.Arithmetic.RaisedTo;
using GenericNumbers.Arithmetic.Remainder;
using GenericNumbers.Arithmetic.Times;

namespace GenericNumbers
{
    public abstract class NumbersWrapper<T, TNumbersWrapper> : NumbersWrapper1<T, TNumbersWrapper>,
        IRaisedTo<T, TNumbersWrapper>,
        IPlus<T, TNumbersWrapper>,
        IMinus<T, TNumbersWrapper>,
        ITimes<T, TNumbersWrapper>,
        IDividedBy<T, TNumbersWrapper>,
        IRemainder<T, TNumbersWrapper>
        where TNumbersWrapper : NumbersWrapper<T, TNumbersWrapper>
    {
        public void RaisedTo(T input, out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.RaisedTo(input)));
        }

        public void Plus(T input, out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Plus(input)));
        }

        public void Minus(T input, out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Minus(input)));
        }

        public void Times(T input, out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Times(input)));
        }

        public void DividedBy(T input, out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.DividedBy(input)));
        }

        public void Remainder(T input, out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Remainder(input)));
        }

        public static TNumbersWrapper operator +(NumbersWrapper<T, TNumbersWrapper> a, T b)
        {
            TNumbersWrapper output;
            a.Plus(b, out output);
            return output;
        }

        public static TNumbersWrapper operator -(NumbersWrapper<T, TNumbersWrapper> a, T b)
        {
            TNumbersWrapper output;
            a.Minus(b, out output);
            return output;
        }

        public static TNumbersWrapper operator *(NumbersWrapper<T, TNumbersWrapper> a, T b)
        {
            TNumbersWrapper output;
            a.Times(b, out output);
            return output;
        }

        public static TNumbersWrapper operator /(NumbersWrapper<T, TNumbersWrapper> a, T b)
        {
            TNumbersWrapper output;
            a.DividedBy(b, out output);
            return output;
        }

        public static TNumbersWrapper operator %(NumbersWrapper<T, TNumbersWrapper> a, T b)
        {
            TNumbersWrapper output;
            a.Remainder(b, out output);
            return output;
        }
    }

    public static class NumbersWrapper
    {
        public static NumbersWrapper<T> Create<T>(params T[] values)
        {
            return new NumbersWrapper<T>(values);
        }
    }

    public class NumbersWrapper<T> : NumbersWrapper<T, NumbersWrapper<T>>
    {
        private readonly T[] _values;

        internal NumbersWrapper(params T[] values)
        {
            _values = values;
        }

        protected override NumbersWrapper<T> Create(params T[] values)
        {
            return new NumbersWrapper<T>(values);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return _values.AsEnumerable().GetEnumerator();
        }

        public override int Count
        {
            get { return _values.Length; }
        }

        public override T this[int index]
        {
            get { return _values[index]; }
        }
    }
}
