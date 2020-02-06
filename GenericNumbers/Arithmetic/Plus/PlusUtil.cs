using System;
using System.Linq.Expressions;

namespace GenericNumbers.Arithmetic.Plus
{
    public static class PlusUtil<T, TInput, TOutput>
    {
        static PlusUtil()
        {
            TryInit();

            UsedOperators = true;

            if (Plus == null)
            {
                Plus = (arg1, input) =>
                {
                    TOutput output;
                    ((IPlus<TInput, TOutput>)arg1).Plus(input, out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            Plus = TryDirectTypeCombination();
            //Plus = TryTypeCombination<T, T, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, T, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, T, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, TInput, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, TInput, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, TInput, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, TOutput, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, TOutput, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<T, TOutput, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, T, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, T, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, T, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, TInput, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, TInput, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, TInput, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, TOutput, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, TOutput, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TInput, TOutput, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, T, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, T, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, T, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, TInput, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, TInput, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, TInput, TOutput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, TOutput, T>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, TOutput, TInput>();
            if (Plus != null)
                return;
            Plus = TryTypeCombination<TOutput, TOutput, TOutput>();
            if (Plus != null)
                return;
        }

        internal static Func<T, TInput, TOutput> TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Add(var1, var2);
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
                if (PlusUtil<T1, T2, T3>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, TOutput>>(Expression.Convert(Expression.Call(typeof(PlusUtil<T1, T2, T3>), "PlusMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(TOutput)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput PlusMethod(T t, TInput input)
        {
            return Plus(t, input);
        }

        public static Func<T, TInput, TOutput> Plus { get; set; }
    }
}
