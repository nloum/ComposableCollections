using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericNumbers.Relational.LessThan
{
    internal static class LessThanUtil<T, TInput>
    {
        static LessThanUtil()
        {
            TryInit();

            UsedOperators = true;

            if (LessThan == null)
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable<TInput>).GetTypeInfo()))
                    LessThan = (arg1, input) => ((IComparable<TInput>)arg1).CompareTo(input) < 0;
                else if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable).GetTypeInfo()))
                    LessThan = (arg1, input) => ((IComparable)arg1).CompareTo(input) < 0;
                else
                {
                    LessThan = (arg1, input) =>
                    {
                        if (arg1 is IComparable<TInput>)
                            return ((IComparable<TInput>)arg1).CompareTo(input) < 0;
                        if (arg1 is IComparable)
                            return ((IComparable)arg1).CompareTo(input) < 0;
                        throw new ArgumentException("arg1");
                    };
                }

                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            LessThan = TryDirectTypeCombination();
            //LessThan = TryTypeCombination<T, T, T>();
            if (LessThan != null)
                return;
            LessThan = TryTypeCombination<T, T>();
            if (LessThan != null)
                return;
            LessThan = TryTypeCombination<TInput, T>();
            if (LessThan != null)
                return;
            LessThan = TryTypeCombination<TInput, TInput>();
            if (LessThan != null)
                return;
        }

        public static Func<T, TInput, bool> TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Multiply(var1, var2);
                var expr = Expression.Lambda(expression, var1, var2);
                return (Func<T, TInput, bool>)expr.Compile();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Func<T, TInput, bool> TryTypeCombination<T1, T2>()
        {
            try
            {
                if (LessThanUtil<T1, T2>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, bool>>(Expression.Convert(Expression.Call(typeof(LessThanUtil<T1, T2>), "LessThanMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(bool)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static bool LessThanMethod(T t, TInput input)
        {
            return LessThan(t, input);
        }

        internal static Func<T, TInput, bool> LessThan { get; set; }
    }
}
