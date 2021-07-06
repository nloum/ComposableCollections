using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleMonads
{
    internal class Lazy<T> : ILazy<T>
    {
        private readonly Func<T> _calculateValue;
        private readonly object _lock = new object();
        private T? _value;

        public Lazy(Func<T> calculateValue)
        {
            if (calculateValue == null) throw new ArgumentNullException(nameof(calculateValue));
            _calculateValue = calculateValue;
            LazyState = LazyState.Uncalculated;
        }

        public Lazy(T value)
        {
            _calculateValue = () => throw new NotImplementedException();
            _value = value ?? throw new ArgumentNullException(nameof(value));
            LazyState = LazyState.Calculated;
        }

        public LazyState LazyState { get; private set; }

        public T Value
        {
            get
            {
                lock (_lock)
                {
                    if (LazyState == LazyState.Uncalculated)
                    {
                        LazyState = LazyState.Calculating;
                        _value = _calculateValue();
                        LazyState = LazyState.Calculated;
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(ObjectValue));
                    }

                    return _value!;
                }
            }
        }

        public object ObjectValue => Value!;

        public TMonad2 Bind<TMonad2, TElement2>(Func<T, TMonad2> f) where TMonad2 : IMonad<TElement2>
        {
            return f(Value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            switch (LazyState)
            {
                case LazyState.Calculated:
                    return "Lazily Calculated " + Value;
                case LazyState.Calculating:
                    return "Lazily Calculating " + typeof(T).FullName;
                case LazyState.Uncalculated:
                    return "Lazily Uncalculated " + typeof(T).FullName;
                default:
                    throw new Exception($"Unknown LazyState: {LazyState}");
            }
        }

        protected bool Equals(Lazy<T> other)
        {
            return LazyState == other.LazyState && EqualityComparer<T>.Default.Equals(_value!, other._value!);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Lazy<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return EqualityComparer<T>.Default.GetHashCode(Value) * 397;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class Lazy
    {
        public static ILazy<TElement> ToLazy<TElement>(this TElement element)
        {
            return new Lazy<TElement>(element);
        }

        public static ILazy<TNumber3> SelectMany<TNumber1, TNumber2, TNumber3>(this ILazy<TNumber1> a,
            Func<TNumber1, ILazy<TNumber2>> func,
            Func<TNumber1, TNumber2, TNumber3>
                select)
        {
            return a.SelectMany(func, select, ToLazy);
        }

        public static ILazy<T2> Select<T1, T2>(this ILazy<T1> source, Func<T1, T2> selector,
            bool beLazyEvenIfSourceIsAlreadyCalculated = true)
        {
            if (source.LazyState == LazyState.Calculated && !beLazyEvenIfSourceIsAlreadyCalculated)
                return ToLazy(selector(source.Value));
            return Utility.Lazify(() => selector(source.Value));
        }

        public static ILazy<T2> SelectMany<T1, T2>(this ILazy<T1> source, Func<T1, ILazy<T2>> selector)
        {
            if (source.LazyState == LazyState.Calculated)
                return selector(source.Value);
            return Utility.Lazify(() => selector(source.Value).Value);
        }
    }
}