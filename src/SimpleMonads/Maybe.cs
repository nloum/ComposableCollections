using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;

namespace SimpleMonads
{
    [DataContract]
    public class Maybe<T> : IMaybe<T>
    {
        private T _value;

        [DataMember]
        public bool HasValue { get; set; }
        public static Maybe<T> Nothing { get; } = new Maybe<T>();

        private Maybe()
        {
            HasValue = false;
        }

        public Maybe(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            HasValue = true;
            Value = value;
        }

        public T Value
        {
            get {
                if (object.ReferenceEquals(this, Nothing))
                {
                    throw new MissingMemberException("Cannot access value of a Nothing");
                }
                return _value;
            }
            set => _value = value;
        }
        
        [DataMember]
        public T ValueOrDefault
        {
            get => _value;
            set => _value = value;
        }

        protected bool Equals(IMaybe<T> other)
        {
            return HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IMaybe<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value) *397) ^ HasValue.GetHashCode();
            }
        }

        public override string ToString()
        {
            return HasValue ? $"Some({Value})" : "None";
        }

        public object ObjectValue => Value;

        public TMonad2 Bind<TMonad2, TElement2>(Func<T, TMonad2> f) where TMonad2 : IMonad<TElement2>
        {
            return f(Value);
        }
    }
    
    public static class Maybe
    {
        public static IMaybe<TElement> ToMaybe<TElement>(this TElement element)
        {
            if (element == null)
                return Maybe<TElement>.Nothing;
            return new Maybe<TElement>(element);
        }

        public static IMaybe<TNumber3> SelectMany<TNumber1, TNumber2, TNumber3>(this IMaybe<TNumber1> a,
                                                                                Func<TNumber1, IMaybe<TNumber2>> func,
                                                                                Func<TNumber1, TNumber2, TNumber3>
                                                                                    select)
        {
            return a.SelectMany(func, @select, ToMaybe);
        }

        public static IMaybe<T2> Select<T1, T2>(this IMaybe<T1> source, Func<T1, T2> selector)
        {
            if (source.HasValue)
                return selector(source.Value).ToMaybe();
            return Utility.Nothing<T2>();
        }

        public static IMaybe<T2> SelectMany<T1, T2>(this IMaybe<T1> source, Func<T1, IMaybe<T2>> selector)
        {
            if (source.HasValue)
                return selector(source.Value);
            return Utility.Nothing<T2>();
        }

        public static T OtherwiseThrow<T>(this IMaybe<T> source)
        {
            return source.Otherwise(() =>
            {
                throw new MissingMemberException("Cannot access value of a Nothing");
            });
        }

        public static T Otherwise<T>(this IMaybe<T> source, Func<T> fallback)
        {
            if (source.HasValue)
                return source.Value;
            return fallback();
        }

        public static T Otherwise<T>(this IMaybe<T> source, T fallback)
        {
            if (source.HasValue)
                return source.Value;
            return fallback;
        }

        public static IEnumerable<T> ToEnumerable<T>(this IMaybe<T> source)
        {
            var result = source.Select(value => ImmutableList<T>.Empty.Add(value)).Otherwise(() => ImmutableList<T>.Empty);
            return result;
        }

        public static IMaybe<T> IfHasValue<T>(this IMaybe<T> maybe, Action<T> action)
        {
            if (maybe.HasValue)
            {
                action(maybe.Value);
            }
            return maybe;
        }

        public static IMaybe<IReadOnlyList<T>> AllOrNothing<T>(this IEnumerable<IMaybe<T>> maybes)
        {
            var all = new List<T>();
            foreach (var maybe in maybes)
            {
                if (!maybe.HasValue)
                {
                    return Maybe<IReadOnlyList<T>>.Nothing;
                }
                
                all.Add(maybe.Value);
            }

            return all.ToMaybe();
        }
        
        public static IEnumerable<T> WhereHasValue<T>(this IEnumerable<IMaybe<T>> maybes)
        {
            return maybes.Where(m => m.HasValue).Select(m => m.Value);
        }
    }
}
