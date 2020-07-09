using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace GenericNumbers
{
    public abstract class NumbersWrapper1<T, TNumbersWrapper> :
        IAbs<TNumbersWrapper>,
        ICeiling<TNumbersWrapper>,
        IFloor<TNumbersWrapper>,
        IRound<TNumbersWrapper>,
        IRaisedTo<TNumbersWrapper>,
        IPlus<TNumbersWrapper>,
        IMinus<TNumbersWrapper>,
        ITimes<TNumbersWrapper>,
        IDividedBy<TNumbersWrapper>,
        IRemainder<TNumbersWrapper>,
        IReadOnlyList<T>
        where TNumbersWrapper : NumbersWrapper1<T, TNumbersWrapper>
    {
        protected abstract TNumbersWrapper Create(params T[] values);

        protected TNumbersWrapper Create(IEnumerable<T> values)
        {
            return Create(values.ToArray());
        }

        public void Abs(out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Abs()));
        }

        public void Ceiling(out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Ceiling()));
        }

        public void Floor(out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Floor()));
        }

        public void RaisedTo(TNumbersWrapper input, out TNumbersWrapper output)
        {
            output = Create(this.Select((v, i) => v.RaisedTo(input[i])));
        }

        public void Round(out TNumbersWrapper output)
        {
            output = Create(this.Select(v => v.Round()));
        }

        public void Plus(TNumbersWrapper input, out TNumbersWrapper output)
        {
            output = Create(this.Select((v, i) => v.Plus(input[i])));
        }

        public void Minus(TNumbersWrapper input, out TNumbersWrapper output)
        {
            output = Create(this.Select((v, i) => v.Minus(input[i])));
        }

        public void Times(TNumbersWrapper input, out TNumbersWrapper output)
        {
            output = Create(this.Select((v, i) => v.Times(input[i])));
        }

        public void DividedBy(TNumbersWrapper input, out TNumbersWrapper output)
        {
            output = Create(this.Select((v, i) => v.DividedBy(input[i])));
        }

        public void Remainder(TNumbersWrapper input, out TNumbersWrapper output)
        {
            output = Create(this.Select((v, i) => v.Remainder(input[i])));
        }

        public static TNumbersWrapper operator +(NumbersWrapper1<T, TNumbersWrapper> a, TNumbersWrapper b)
        {
            TNumbersWrapper output;
            a.Plus(b, out output);
            return output;
        }

        public static TNumbersWrapper operator -(NumbersWrapper1<T, TNumbersWrapper> a, TNumbersWrapper b)
        {
            TNumbersWrapper output;
            a.Minus(b, out output);
            return output;
        }

        public static TNumbersWrapper operator *(NumbersWrapper1<T, TNumbersWrapper> a, TNumbersWrapper b)
        {
            TNumbersWrapper output;
            a.Times(b, out output);
            return output;
        }

        public static TNumbersWrapper operator /(NumbersWrapper1<T, TNumbersWrapper> a, TNumbersWrapper b)
        {
            TNumbersWrapper output;
            a.DividedBy(b, out output);
            return output;
        }

        public static TNumbersWrapper operator %(NumbersWrapper1<T, TNumbersWrapper> a, TNumbersWrapper b)
        {
            TNumbersWrapper output;
            a.Remainder(b, out output);
            return output;
        }

        public override string ToString()
        {
            return string.Join(", ", this.Select(v => v.ToString()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<T> GetEnumerator();
        public abstract int Count { get; }
        public abstract T this[int index] { get; }
    }
}