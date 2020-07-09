using System;
using System.Linq.Expressions;

namespace GenericNumbers.Arithmetic.Remainder
{
    public static class RemainderUtil<T, TInput, TOutput>
    {
        static RemainderUtil()
        {
            TryInit();

            UsedOperators = true;

            if (Remainder == null)
            {
                Remainder = (arg1, input) =>
                {
                    TOutput output;
                    ((IRemainder<TInput, TOutput>)arg1).Remainder(input, out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            Remainder = TryDirectTypeCombination();
            //Remainder = TryTypeCombination<T, T, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, T, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, T, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, TInput, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, TInput, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, TInput, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, TOutput, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, TOutput, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<T, TOutput, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, T, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, T, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, T, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, TInput, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, TInput, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, TInput, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, TOutput, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, TOutput, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TInput, TOutput, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, T, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, T, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, T, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, TInput, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, TInput, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, TInput, TOutput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, TOutput, T>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, TOutput, TInput>();
            if (Remainder != null)
                return;
            Remainder = TryTypeCombination<TOutput, TOutput, TOutput>();
            if (Remainder != null)
                return;
        }

        internal static Func<T, TInput, TOutput> TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Modulo(var1, var2);
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
                if (RemainderUtil<T1, T2, T3>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, TOutput>>(Expression.Convert(Expression.Call(typeof(RemainderUtil<T1, T2, T3>), "RemainderMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(TOutput)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput RemainderMethod(T t, TInput input)
        {
            return Remainder(t, input);
        }

        public static Func<T, TInput, TOutput> Remainder { get; set; }
    }
}
