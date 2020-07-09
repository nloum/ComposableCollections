using System;

namespace SimpleMonads
{
    public static class Either3Extensions
    {
        public static IEither<T1B, T2, T3> Select1<T1A, T1B, T2, T3>(IEither<T1A, T2, T3> either,
            Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3>(either.Item3.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3> Select2<T1, T2A, T2B, T3>(IEither<T1, T2A, T3> either,
            Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3>(either.Item3.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B> Select3<T1, T2, T3A, T3B>(IEither<T1, T2, T3A> either,
            Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B>(selector(either.Item3.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B> Select<T1A, T2A, T3A, T1B, T2B, T3B>(this IEither<T1A, T2A, T3A> input,
            Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B>(
                    selector3(input.Item3.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}