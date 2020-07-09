using System;
using System.Runtime.Serialization;

namespace SimpleMonads
{
    [DataContract]
    public class Identity<T> : IIdentity<T>
    {
        [DataMember]
        public T Value { get; set; }

        public Identity()
        {
        }

        public Identity(T value)
        {
            Value = value;
        }

        public TMonad2 Bind<TMonad2, TElement2>(Func<T, TMonad2> f) where TMonad2 : IMonad<TElement2>
        {
            return f(Value);
        }
    }

    public static class Identity
    {
        public static IIdentity<TElement> ToIdentity<TElement>(this TElement element)
        {
            return new Identity<TElement>(element);
        }

        public static IIdentity<TNumber3> SelectMany<TNumber1, TNumber2, TNumber3>(this IIdentity<TNumber1> a,
                                                                                Func<TNumber1, IIdentity<TNumber2>> func,
                                                                                Func<TNumber1, TNumber2, TNumber3>
                                                                                    select)
        {
            return a.SelectMany(func, @select, ToIdentity);
        }
    }
}
