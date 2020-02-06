using System;
using System.Linq.Expressions;
using GenericNumbers.Arithmetic.Times;

namespace GenericNumbers.Arithmetic.Times
{
    public static class TimesUtil<T, TInput, TOutput>
    {
        static TimesUtil()
        {
            TryInit();

            UsedOperators = true;

            if (Times == null)
            {
                Times = (arg1, input) =>
                {
                    TOutput output;
                    ((ITimes<TInput, TOutput>)arg1).Times(input, out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            Times = TryDirectTypeCombination();
            //Times = TryTypeCombination<T, T, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, T, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, T, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, TInput, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, TInput, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, TInput, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, TOutput, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, TOutput, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<T, TOutput, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, T, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, T, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, T, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, TInput, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, TInput, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, TInput, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, TOutput, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, TOutput, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TInput, TOutput, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, T, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, T, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, T, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, TInput, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, TInput, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, TInput, TOutput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, TOutput, T>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, TOutput, TInput>();
            if (Times != null)
                return;
            Times = TryTypeCombination<TOutput, TOutput, TOutput>();
            if (Times != null)
                return;
        }

        internal static Func<T, TInput, TOutput> TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Multiply(var1, var2);
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
                if (TimesUtil<T1, T2, T3>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, TOutput>>(Expression.Convert(Expression.Call(typeof(Times.TimesUtil<T1, T2, T3>), "TimesMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(TOutput)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput TimesMethod(T t, TInput input)
        {
            return Times(t, input);
        }

        public static Func<T, TInput, TOutput> Times { get; set; }
    }
}
