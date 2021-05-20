using System;
using System.Linq.Expressions;

namespace GenericNumbers.ConvertTo
{
    internal static class ConvertToUtil<T, TOutput>
    {
        static ConvertToUtil()
        {
            TryInit();

            UsedOperators = true;

            if (ConvertTo == null)
            {
                ConvertTo = arg1 =>
                {
                    TOutput output;
                    ((IConvertTo<TOutput>)arg1!).ConvertTo(out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            ConvertTo = TryDirectTypeCombination()
                ?? TryTypeCombination<T, T>()
                ?? TryTypeCombination<T, TOutput>()
                ?? TryTypeCombination<TOutput, T>()
                ?? TryTypeCombination<TOutput, TOutput>();
        }

        public static Func<T,  TOutput>? TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                Expression expression = Expression.Convert(var1, typeof(TOutput));
                var expr = Expression.Lambda(expression, var1);
                return (Func<T,  TOutput>)expr.Compile();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Func<T,  TOutput>? TryTypeCombination<T1, T2>()
        {
            try
            {
                if (ConvertToUtil<T1, T2>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var expr = Expression.Lambda<Func<T,  TOutput>>(Expression.Convert(Expression.Call(typeof(ConvertToUtil<T1, T2>), "ConvertToMethod", new Type[0], Expression.Convert(var1, typeof(T1))), typeof(TOutput)), var1);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput ConvertToMethod(T t)
        {
            return ConvertTo!(t);
        }

        internal static Func<T,  TOutput>? ConvertTo { get; set; }
    }
}
