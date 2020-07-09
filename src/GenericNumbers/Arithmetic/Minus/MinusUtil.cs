using System;
using System.Linq.Expressions;
using GenericNumbers.Arithmetic.Minus;

namespace GenericNumbers.Arithmetic.Minus
{
    public static class MinusUtil<T, TInput, TOutput>
    {
        static MinusUtil()
        {
            TryInit();

            UsedOperators = true;

            if (Minus == null)
            {
                Minus = (arg1, input) =>
                {
                    TOutput output;
                    ((IMinus<TInput, TOutput>)arg1).Minus(input, out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            Minus = TryDirectTypeCombination();
            //Minus = TryTypeCombination<T, T, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, T, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, T, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, TInput, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, TInput, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, TInput, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, TOutput, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, TOutput, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<T, TOutput, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, T, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, T, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, T, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, TInput, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, TInput, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, TInput, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, TOutput, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, TOutput, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TInput, TOutput, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, T, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, T, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, T, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, TInput, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, TInput, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, TInput, TOutput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, TOutput, T>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, TOutput, TInput>();
            if (Minus != null)
                return;
            Minus = TryTypeCombination<TOutput, TOutput, TOutput>();
            if (Minus != null)
                return;
        }

        internal static Func<T, TInput, TOutput> TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Subtract(var1, var2);
                var expr = Expression.Lambda(expression, var1, var2);
                return (Func<T, TInput, TOutput>)expr.Compile();
            }
            catch (Exception)
            {
            }
            return null;
        }

        internal static Func<T, TInput, TOutput> TryTypeCombination<T1, T2, T3>()
        {
            try
            {
                if (MinusUtil<T1, T2, T3>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, TOutput>>(Expression.Convert(Expression.Call(typeof(Minus.MinusUtil<T1, T2, T3>), "MinusMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(TOutput)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput MinusMethod(T t, TInput input)
        {
            return Minus(t, input);
        }

        public static Func<T, TInput, TOutput> Minus { get; set; }
    }
}
