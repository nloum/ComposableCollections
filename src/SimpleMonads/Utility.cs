using System;

namespace SimpleMonads
{
    public static class Utility
    {
        /// <summary>
        /// Returns a maybe with the result of the calculation, unless an exception is thrown.
        /// If an exception is thrown by calculation, returns a Nothing.
        /// </summary>
        public static IMaybe<T> MaybeCatch<T>(Func<T> calculation)
        {
            try
            {
                return calculation().ToMaybe();
            }
            catch (Exception e)
            {
                return Nothing<T>(() => throw e);
            }
        }
    
        public static IMaybe<TElement> Nothing<TElement>()
        {
            return Maybe<TElement>.Nothing();
        }

        public static IMaybe<TElement> Nothing<TElement>(Action throwOnNothingAccessed)
        {
            return Maybe<TElement>.Nothing(throwOnNothingAccessed);
        }

        public static IMaybe<TElement> Something<TElement>(TElement element)
        {
            return new Maybe<TElement>(element);
        }

        /// <summary>
        /// This way you don't have to specify the generic parameter for Nothing. This is useful if the
        /// generic parameter is an anonymous type.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="hasValue">If this value is true, return Something(element()); otherwise return Nothing<TElement>()</param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IMaybe<TElement> SomethingOrNothing<TElement>(bool hasValue, Func<TElement> element)
        {
            if (hasValue)
                return Something(element());
            return Nothing<TElement>();
        }

        /// <summary>
        /// This way you don't have to specify the generic parameter for Nothing. This is useful if the
        /// generic parameter is an anonymous type.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="hasValue">If this value is true, return Something(element); otherwise return Nothing<TElement>()</param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IMaybe<TElement> SomethingOrNothing<TElement>(bool hasValue, TElement element)
        {
            if (hasValue)
                return Something(element);
            return Nothing<TElement>();
        }

        /// <summary>
        /// This way you don't have to specify the generic parameter for Nothing. This is useful if the
        /// generic parameter is an anonymous type.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="hasValue">If this value is true, return Something(element()); otherwise return Nothing<TElement>()</param>
        /// <param name="element"></param>
        /// <param name="errorMessage">The error message the maybe should have if hasValue is false</param>
        /// <returns></returns>
        public static IMaybe<TElement> SomethingOrNothing<TElement>(bool hasValue, Func<TElement> element, Action throwOnNothingAccessed)
        {
            if (hasValue)
                return Something(element());
            return Nothing<TElement>(throwOnNothingAccessed);
        }

        /// <summary>
        /// This way you don't have to specify the generic parameter for Nothing. This is useful if the
        /// generic parameter is an anonymous type.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="hasValue">If this value is true, return Something(element); otherwise return Nothing<TElement>()</param>
        /// <param name="element"></param>
        /// <param name="errorMessage">The error message the maybe should have if hasValue is false</param>
        /// <returns></returns>
        public static IMaybe<TElement> SomethingOrNothing<TElement>(bool hasValue, TElement element, Action throwOnNothingAccessed)
        {
            if (hasValue)
                return Something(element);
            return Nothing<TElement>(throwOnNothingAccessed);
        }

        public static ILazy<TElement> Lazify<TElement>(TElement element)
        {
            return new Lazy<TElement>(element);
        }

        public static ILazy<TElement> Lazify<TElement>(Func<TElement> element)
        {
            return new Lazy<TElement>(element);
        }

        public static TMonad3 SelectMany<TElement1, TElement2, TElement3, TMonad3>(this IMonad<TElement1> a, Func<TElement1, IMonad<TElement2>> func, Func<TElement1, TElement2, TElement3> @select, Func<TElement3, TMonad3> create)
            where TMonad3 : IMonad<TElement3>
        {
            return a.Bind<TMonad3, TElement3>(t1 => a.Bind<IMonad<TElement2>, TElement2>(func).Bind<TMonad3, TElement3>(t2 => create(@select(t1, t2))));
        }
    }
}