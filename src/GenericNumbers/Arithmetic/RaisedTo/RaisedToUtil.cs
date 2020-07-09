using System;
using System.Linq.Expressions;

namespace GenericNumbers.Arithmetic.RaisedTo
{
    public static class RaisedToUtil<T, TInput, TOutput>
    {
        static RaisedToUtil()
        {
            TryInit();

            UsedOperators = true;

            if (RaisedTo == null)
            {
                RaisedTo = (arg1, input) =>
                {
                    TOutput output;
                    ((IRaisedTo<TInput, TOutput>)arg1).RaisedTo(input, out output);
                    return output;
                };
                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            RaisedTo = TryDirectTypeCombination();
            //RaisedTo = TryTypeCombination<T, T, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, T, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, T, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, TInput, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, TInput, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, TInput, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, TOutput, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, TOutput, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<T, TOutput, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, T, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, T, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, T, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, TInput, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, TInput, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, TInput, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, TOutput, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, TOutput, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TInput, TOutput, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, T, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, T, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, T, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, TInput, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, TInput, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, TInput, TOutput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, TOutput, T>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, TOutput, TInput>();
            if (RaisedTo != null)
                return;
            RaisedTo = TryTypeCombination<TOutput, TOutput, TOutput>();
            if (RaisedTo != null)
                return;
        }

        internal static Func<T, TInput, TOutput> TryDirectTypeCombination()
        {
            try
            {
                var param1 = Expression.Variable(typeof(T));
                var param2 = Expression.Variable(typeof(TInput));

                Expression var1 = param1;
                if (typeof(T) != typeof(double) && typeof(T) != typeof(float))
                    var1 = Expression.Convert(var1, typeof(double));
                Expression var2 = param2;
                if (typeof(T) != typeof(double) && typeof(T) != typeof(float))
                    var2 = Expression.Convert(var2, typeof(double));

                Expression expression = Expression.Convert(Expression.Power(var1, var2), typeof(TOutput));

                var expr = Expression.Lambda(expression, param1, param2);
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
                if (RaisedToUtil<T1, T2, T3>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, TOutput>>(Expression.Convert(Expression.Call(typeof(RaisedTo.RaisedToUtil<T1, T2, T3>), "RaisedToMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(TOutput)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static TOutput RaisedToMethod(T t, TInput input)
        {
            return RaisedTo(t, input);
        }

        public static Func<T, TInput, TOutput> RaisedTo { get; set; }
    }
}
