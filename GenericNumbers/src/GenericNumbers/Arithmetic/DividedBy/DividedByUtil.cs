using System;
using System.Linq.Expressions;
using GenericNumbers.Arithmetic.DividedBy;

namespace GenericNumbers.Arithmetic.DividedBy
{
    public static class DividedByUtil<T, TInput, TOutput>
    {
        static DividedByUtil()
        {
            TryInit();

            UsedOperators = true;

            if (DividedBy == null)
            {
                DividedBy = (arg1, input) =>
                {
                    TOutput output;
                    ((IDividedBy<TInput, TOutput>)arg1!).DividedBy(input, out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            DividedBy = TryDirectTypeCombination();
            //DividedBy = TryTypeCombination<T, T, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, T, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, T, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, TInput, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, TInput, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, TInput, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, TOutput, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, TOutput, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<T, TOutput, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, T, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, T, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, T, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, TInput, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, TInput, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, TInput, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, TOutput, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, TOutput, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TInput, TOutput, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, T, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, T, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, T, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, TInput, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, TInput, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, TInput, TOutput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, TOutput, T>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, TOutput, TInput>();
            if (DividedBy != null)
                return;
            DividedBy = TryTypeCombination<TOutput, TOutput, TOutput>();
            if (DividedBy != null)
                return;
        }

        internal static Func<T, TInput, TOutput>? TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Divide(var1, var2);
                var expr = Expression.Lambda(expression, var1, var2);
                return (Func<T, TInput, TOutput>)expr.Compile();
            }
            catch (Exception)
            {
            }
            return null;
        }

        internal static Func<T, TInput, TOutput>? TryTypeCombination<T1, T2, T3>()
        {
            try
            {
                if (DividedByUtil<T1, T2, T3>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, TOutput>>(Expression.Convert(Expression.Call(typeof(DividedBy.DividedByUtil<T1, T2, T3>), "DividedByMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(TOutput)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput DividedByMethod(T t, TInput input)
        {
            return DividedBy(t, input);
        }

        public static Func<T, TInput, TOutput> DividedBy { get; set; }
    }
}
