using System;
using System.Runtime.Serialization;

namespace SimpleMonads
{
    public static class Either
    {
        public static IEither<T3, T4> Select<T1, T2, T3, T4>(this IEither<T1, T2> source, Func<T1, T3> selector1,
            Func<T2, T4> selector2)
        {
            if (source.Item1.HasValue)
            {
                return selector1(source.Item1.Value).ToEither<T3, T4>();
            }
            else
            {
                return selector2(source.Item2.Value).ToEither<T3, T4>();
            }
        }

        public static IEither<T3, T2> SelectItem1<T1, T2, T3>(this IEither<T1, T2> source, Func<T1, T3> selector1)
        {
            if (source.Item1.HasValue)
            {
                return selector1(source.Item1.Value).ToEither<T3, T2>();
            }
            else
            {
                return source.Item2.Value.ToEither<T3, T2>();
            }
        }

        public static IEither<T1, T3> SelectItem2<T1, T2, T3>(this IEither<T1, T2> source, Func<T2, T3> selector2)
        {
            if (source.Item1.HasValue)
            {
                return source.Item1.Value.ToEither<T1, T3>();
            }
            else
            {
                return selector2(source.Item2.Value).ToEither<T1, T3>();
            }
        }

        public static IEither<T3, T2> SelectItem1<T1, T2, T3>(this IEither<T1, T2> source, Func<T1, IEither<T3, T2>> selector1)
        {
            if (source.Item1.HasValue)
            {
                return selector1(source.Item1.Value);
            }
            else
            {
                return source.Item2.Value.ToEither<T3, T2>();
            }
        }

        public static IEither<T1, T3> SelectItem2<T1, T2, T3>(this IEither<T1, T2> source, Func<T2, IEither<T1, T3>> selector2)
        {
            if (source.Item1.HasValue)
            {
                return source.Item1.Value.ToEither<T1, T3>();
            }
            else
            {
                return selector2(source.Item2.Value);
            }
        }

        public static IEither<T1, T2> ToEither<T1, T2>(this T1 item1)
        {
            return new Either<T1, T2>(item1);
        }

        public static IEither<T1, T2> ToEither<T1, T2>(this T2 item2)
        {
            return new Either<T1, T2>(item2);
        }

        public static IEither<T1, T2> Or<T1, T2>(this IMaybe<T1> item1, Func<T2> item2)
        {
            if (item1.HasValue)
                return item1.Value.ToEither<T1, T2>();
            else
                return item2().ToEither<T1, T2>();
        }

        public static IEither<T1, T2> Or<T1, T2>(this IMaybe<T1> item1, T2 item2)
        {
            return item1.Or(() => item2);
        }

        public static IEither<T2, T1> Swap<T1, T2>(this IEither<T1, T2> source)
        {
            return source.Item2.Or(() => source.Item1.Value);
        }
    }

    [DataContract]
    public class Either<T1, T2> : IEither<T1, T2>
    {
        public Either(T1 item1)
        {
            Item1 = Utility.Something(item1);
            Item2 = Utility.Nothing<T2>();
        }

        public Either(T2 item2)
        {
            Item1 = Utility.Nothing<T1>();
            Item2 = Utility.Something(item2);
        }

        [DataMember]
        public IMaybe<T1> Item1 { get; set; }

        [DataMember]
        public IMaybe<T2> Item2 { get; set; }
    }
}
