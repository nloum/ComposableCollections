using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericNumbers.Relational.Equal
{
    internal static class EqualUtil<T, TInput>
    {
        static EqualUtil()
        {
            TryInit();

            UsedOperators = true;

            if (Equal == null)
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable<TInput>).GetTypeInfo()))
                    Equal = (arg1, input) => ((IComparable<TInput>)arg1).CompareTo(input) == 0;
                else if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable).GetTypeInfo()))
                    Equal = (arg1, input) => ((IComparable)arg1).CompareTo(input) == 0;
                else if (typeof (T).GetTypeInfo().IsAssignableFrom(typeof (IEquatable<TInput>).GetTypeInfo()))
                    Equal = (arg1, input) => ((IEquatable<TInput>) arg1).Equals(input);
                else
                {
                    Equal = (arg1, input) =>
                    {
                        if (arg1 is IComparable<TInput>)
                            return ((IComparable<TInput>) arg1).CompareTo(input) == 0;
                        if (arg1 is IComparable)
                            return ((IComparable) arg1).CompareTo(input) == 0;
                        if (arg1 is IEquatable<TInput>)
                            return ((IEquatable<TInput>) arg1).Equals(input);
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
            Equal = TryDirectTypeCombination();
            //Equal = TryTypeCombination<T, T, T>();
            if (Equal != null)
                return;
            Equal = TryTypeCombination<T, T>();
            if (Equal != null)
                return;
            Equal = TryTypeCombination<TInput, T>();
            if (Equal != null)
                return;
            Equal = TryTypeCombination<TInput, TInput>();
            if (Equal != null)
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
                if (EqualUtil<T1, T2>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, bool>>(Expression.Convert(Expression.Call(typeof(EqualUtil<T1, T2>), "EqualMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(bool)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static bool EqualMethod(T t, TInput input)
        {
            return Equal(t, input);
        }

        internal static Func<T, TInput, bool> Equal { get; set; }
    }
}
