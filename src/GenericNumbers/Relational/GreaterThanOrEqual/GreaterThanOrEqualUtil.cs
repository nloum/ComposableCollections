using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericNumbers.Relational.GreaterThanOrEqual
{
    internal static class GreaterThanOrEqualUtil<T, TInput>
    {
        static GreaterThanOrEqualUtil()
        {
            TryInit();

            UsedOperators = true;

            if (GreaterThanOrEqual == null)
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable<TInput>).GetTypeInfo()))
                    GreaterThanOrEqual = (arg1, input) => ((IComparable<TInput>)arg1).CompareTo(input) >= 0;
                else if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable).GetTypeInfo()))
                    GreaterThanOrEqual = (arg1, input) => ((IComparable)arg1).CompareTo(input) >= 0;
                else
                {
                    GreaterThanOrEqual = (arg1, input) =>
                    {
                        if (arg1 is IComparable<TInput>)
                            return ((IComparable<TInput>)arg1).CompareTo(input) >= 0;
                        if (arg1 is IComparable)
                            return ((IComparable)arg1).CompareTo(input) >= 0;
                        throw new ArgumentException($"An attempt was made to determine whether a {NumbersUtility.ConvertToCSharpTypeName(typeof(T))} was greater than or equal to a {NumbersUtility.ConvertToCSharpTypeName(typeof(TInput))} but {NumbersUtility.ConvertToCSharpTypeName(typeof(T))} is not an IComparable<{NumbersUtility.ConvertToCSharpTypeName(typeof(TInput))}> or an IComparable");
                    };
                }

                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            GreaterThanOrEqual = TryDirectTypeCombination();
            //GreaterThanOrEqual = TryTypeCombination<T, T, T>();
            if (GreaterThanOrEqual != null)
                return;
            GreaterThanOrEqual = TryTypeCombination<T, T>();
            if (GreaterThanOrEqual != null)
                return;
            GreaterThanOrEqual = TryTypeCombination<TInput, T>();
            if (GreaterThanOrEqual != null)
                return;
            GreaterThanOrEqual = TryTypeCombination<TInput, TInput>();
            if (GreaterThanOrEqual != null)
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
                if (GreaterThanOrEqualUtil<T1, T2>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, bool>>(Expression.Convert(Expression.Call(typeof(GreaterThanOrEqualUtil<T1, T2>), "GreaterThanOrEqualMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(bool)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static bool GreaterThanOrEqualMethod(T t, TInput input)
        {
            return GreaterThanOrEqual(t, input);
        }

        internal static Func<T, TInput, bool> GreaterThanOrEqual { get; set; }
    }
}
