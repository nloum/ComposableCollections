using System;

namespace SimpleMonads
{
    public static class Either2Extensions
    {
        public static IEither<T1B, T2> Select1<T1A, T1B, T2>(IEither<T1A, T2> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2>(either.Item2.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B> Select2<T1, T2A, T2B>(IEither<T1, T2A> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B>(selector(either.Item2.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B> Select<T1A, T2A, T1B, T2B>(this IEither<T1A, T2A> input,
            Func<T1A, T1B> selector1, Func<T2A, T2B> selector2)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B>(
                    selector2(input.Item2.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

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

    public static class Either4Extensions
    {
        public static IEither<T1B, T2, T3, T4> Select1<T1A, T1B, T2, T3, T4>(IEither<T1A, T2, T3, T4> either,
            Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4>(either.Item4.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4> Select2<T1, T2A, T2B, T3, T4>(IEither<T1, T2A, T3, T4> either,
            Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4>(either.Item4.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4> Select3<T1, T2, T3A, T3B, T4>(IEither<T1, T2, T3A, T4> either,
            Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4>(either.Item4.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B> Select4<T1, T2, T3, T4A, T4B>(IEither<T1, T2, T3, T4A> either,
            Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B>(selector(either.Item4.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B> Select<T1A, T2A, T3A, T4A, T1B, T2B, T3B, T4B>(
            this IEither<T1A, T2A, T3A, T4A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2,
            Func<T3A, T3B> selector3, Func<T4A, T4B> selector4)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B>(
                    selector4(input.Item4.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either5Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5> Select1<T1A, T1B, T2, T3, T4, T5>(
            IEither<T1A, T2, T3, T4, T5> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5>(either.Item5.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5> Select2<T1, T2A, T2B, T3, T4, T5>(
            IEither<T1, T2A, T3, T4, T5> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5>(either.Item5.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5> Select3<T1, T2, T3A, T3B, T4, T5>(
            IEither<T1, T2, T3A, T4, T5> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5>(either.Item5.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5> Select4<T1, T2, T3, T4A, T4B, T5>(
            IEither<T1, T2, T3, T4A, T5> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5>(either.Item5.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B> Select5<T1, T2, T3, T4, T5A, T5B>(
            IEither<T1, T2, T3, T4, T5A> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B>(selector(either.Item5.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B> Select<T1A, T2A, T3A, T4A, T5A, T1B, T2B, T3B, T4B, T5B>(
            this IEither<T1A, T2A, T3A, T4A, T5A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2,
            Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B>(
                    selector5(input.Item5.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either6Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6> Select1<T1A, T1B, T2, T3, T4, T5, T6>(
            IEither<T1A, T2, T3, T4, T5, T6> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6>(either.Item6.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6> Select2<T1, T2A, T2B, T3, T4, T5, T6>(
            IEither<T1, T2A, T3, T4, T5, T6> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6>(either.Item6.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6> Select3<T1, T2, T3A, T3B, T4, T5, T6>(
            IEither<T1, T2, T3A, T4, T5, T6> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6>(either.Item6.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6> Select4<T1, T2, T3, T4A, T4B, T5, T6>(
            IEither<T1, T2, T3, T4A, T5, T6> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6>(either.Item6.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6> Select5<T1, T2, T3, T4, T5A, T5B, T6>(
            IEither<T1, T2, T3, T4, T5A, T6> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6>(either.Item6.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B> Select6<T1, T2, T3, T4, T5, T6A, T6B>(
            IEither<T1, T2, T3, T4, T5, T6A> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B>(selector(either.Item6.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T1B, T2B, T3B, T4B, T5B, T6B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2,
                Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
                    selector6(input.Item6.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either7Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7>(
            IEither<T1A, T2, T3, T4, T5, T6, T7> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7>(either.Item7.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7>(
            IEither<T1, T2A, T3, T4, T5, T6, T7> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7>(either.Item7.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7>(
            IEither<T1, T2, T3A, T4, T5, T6, T7> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7>(either.Item7.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7>(
            IEither<T1, T2, T3, T4A, T5, T6, T7> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7>(either.Item7.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7>(
            IEither<T1, T2, T3, T4, T5A, T6, T7> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7>(either.Item7.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7>(
            IEither<T1, T2, T3, T4, T5, T6A, T7> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7>(either.Item7.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B>(
            IEither<T1, T2, T3, T4, T5, T6, T7A> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B>(selector(either.Item7.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A> input, Func<T1A, T1B> selector1,
                Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5,
                Func<T6A, T6B> selector6, Func<T7A, T7B> selector7)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B>(
                    selector7(input.Item7.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either8Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7, T8>(
            IEither<T1A, T2, T3, T4, T5, T6, T7, T8> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7, T8>(
            IEither<T1, T2A, T3, T4, T5, T6, T7, T8> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7, T8>(
            IEither<T1, T2, T3A, T4, T5, T6, T7, T8> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7, T8>(
            IEither<T1, T2, T3, T4A, T5, T6, T7, T8> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7, T8>(
            IEither<T1, T2, T3, T4, T5A, T6, T7, T8> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7, T8>(
            IEither<T1, T2, T3, T4, T5, T6A, T7, T8> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B, T8>(
            IEither<T1, T2, T3, T4, T5, T6, T7A, T8> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item8.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B> Select8<T1, T2, T3, T4, T5, T6, T7, T8A, T8B>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8A> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(selector(either.Item8.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A> input, Func<T1A, T1B> selector1,
                Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5,
                Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
                    selector8(input.Item8.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either9Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7, T8, T9>(
            IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7, T8, T9>(
            IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7, T8, T9>(
            IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7, T8, T9>(
            IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7, T8, T9>(
            IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7, T8, T9>(
            IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B, T8, T9>(
            IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9> Select8<T1, T2, T3, T4, T5, T6, T7, T8A, T8B, T9>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item9.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B> Select9<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T9B>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A> either, Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(selector(either.Item9.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A> input, Func<T1A, T1B> selector1,
                Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5,
                Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8, Func<T9A, T9B> selector9)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
                    selector9(input.Item9.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either10Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7, T8,
            T9, T10>(IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7, T8,
            T9, T10>(IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7, T8,
            T9, T10>(IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7, T8,
            T9, T10>(IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7, T8,
            T9, T10>(IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7, T8,
            T9, T10>(IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B, T8,
            T9, T10>(IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10> Select8<T1, T2, T3, T4, T5, T6, T7, T8A, T8B,
            T9, T10>(IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10> Select9<T1, T2, T3, T4, T5, T6, T7, T8, T9A,
            T9B, T10>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10> either, Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item10.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B> Select10<T1, T2, T3, T4, T5, T6, T7, T8, T9,
            T10A, T10B>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A> either, Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(selector(either.Item10.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B> Select<T1A, T2A, T3A, T4A, T5A, T6A,
            T7A, T8A, T9A, T10A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
            this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A> input, Func<T1A, T1B> selector1,
            Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5,
            Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8, Func<T9A, T9B> selector9,
            Func<T10A, T10B> selector10)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
                    selector10(input.Item10.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either11Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7,
            T8, T9, T10, T11>(IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7,
            T8, T9, T10, T11>(IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7,
            T8, T9, T10, T11>(IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7,
            T8, T9, T10, T11>(IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7,
            T8, T9, T10, T11>(IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7,
            T8, T9, T10, T11>(IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B,
            T8, T9, T10, T11>(IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11> Select8<T1, T2, T3, T4, T5, T6, T7, T8A,
            T8B, T9, T10, T11>(IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11> Select9<T1, T2, T3, T4, T5, T6, T7, T8,
            T9A, T9B, T10, T11>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11> either, Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11> Select10<T1, T2, T3, T4, T5, T6, T7, T8,
            T9, T10A, T10B, T11>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11> either,
            Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(selector(either.Item10.Value));
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11>(either.Item11.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B> Select11<T1, T2, T3, T4, T5, T6, T7, T8,
            T9, T10, T11A, T11B>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A> either,
            Func<T11A, T11B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B>(selector(either.Item11.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B> Select<T1A, T2A, T3A, T4A, T5A,
            T6A, T7A, T8A, T9A, T10A, T11A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
            this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A> input, Func<T1A, T1B> selector1,
            Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5,
            Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8, Func<T9A, T9B> selector9,
            Func<T10A, T10B> selector10, Func<T11A, T11B> selector11)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector10(input.Item10.Value));
            }
            else if (input.Item11.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B>(
                    selector11(input.Item11.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either12Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Select1<T1A, T1B, T2, T3, T4, T5, T6,
            T7, T8, T9, T10, T11, T12>(IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> either,
            Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Select2<T1, T2A, T2B, T3, T4, T5, T6,
            T7, T8, T9, T10, T11, T12>(IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> either,
            Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12> Select3<T1, T2, T3A, T3B, T4, T5, T6,
            T7, T8, T9, T10, T11, T12>(IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11, T12> either,
            Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12> Select4<T1, T2, T3, T4A, T4B, T5, T6,
            T7, T8, T9, T10, T11, T12>(IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11, T12> either,
            Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12> Select5<T1, T2, T3, T4, T5A, T5B, T6,
            T7, T8, T9, T10, T11, T12>(IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11, T12> either,
            Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12> Select6<T1, T2, T3, T4, T5, T6A, T6B,
            T7, T8, T9, T10, T11, T12>(IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11, T12> either,
            Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12> Select7<T1, T2, T3, T4, T5, T6, T7A,
            T7B, T8, T9, T10, T11, T12>(IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11, T12> either,
            Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12> Select8<T1, T2, T3, T4, T5, T6, T7,
            T8A, T8B, T9, T10, T11, T12>(IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11, T12> either,
            Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12> Select9<T1, T2, T3, T4, T5, T6, T7,
            T8, T9A, T9B, T10, T11, T12>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11, T12> either,
            Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12> Select10<T1, T2, T3, T4, T5, T6, T7,
            T8, T9, T10A, T10B, T11, T12>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11, T12> either,
            Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(selector(either.Item10.Value));
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12> Select11<T1, T2, T3, T4, T5, T6, T7,
            T8, T9, T10, T11A, T11B, T12>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T12> either,
            Func<T11A, T11B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(selector(either.Item11.Value));
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12>(either.Item12.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B> Select12<T1, T2, T3, T4, T5, T6, T7,
            T8, T9, T10, T11, T12A, T12B>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A> either,
            Func<T12A, T12B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B>(selector(either.Item12.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T1B, T2B, T3B, T4B, T5B, T6B, T7B,
                T8B, T9B, T10B, T11B, T12B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A> input,
                Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4,
                Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8,
                Func<T9A, T9B> selector9, Func<T10A, T10B> selector10, Func<T11A, T11B> selector11,
                Func<T12A, T12B> selector12)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector10(input.Item10.Value));
            }
            else if (input.Item11.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector11(input.Item11.Value));
            }
            else if (input.Item12.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B>(
                    selector12(input.Item12.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either13Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select1<T1A, T1B, T2, T3, T4, T5,
            T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> either,
            Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select2<T1, T2A, T2B, T3, T4, T5,
            T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> either,
            Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select3<T1, T2, T3A, T3B, T4, T5,
            T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> either,
            Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select4<T1, T2, T3, T4A, T4B, T5,
            T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11, T12, T13> either,
            Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13> Select5<T1, T2, T3, T4, T5A, T5B,
            T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11, T12, T13> either,
            Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13> Select6<T1, T2, T3, T4, T5, T6A,
            T6B, T7, T8, T9, T10, T11, T12, T13>(
            IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11, T12, T13> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13> Select7<T1, T2, T3, T4, T5, T6,
            T7A, T7B, T8, T9, T10, T11, T12, T13>(
            IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11, T12, T13> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(
                    selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13> Select8<T1, T2, T3, T4, T5, T6,
            T7, T8A, T8B, T9, T10, T11, T12, T13>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11, T12, T13> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(
                    selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13> Select9<T1, T2, T3, T4, T5, T6,
            T7, T8, T9A, T9B, T10, T11, T12, T13>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11, T12, T13> either, Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(
                    selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13> Select10<T1, T2, T3, T4, T5, T6,
            T7, T8, T9, T10A, T10B, T11, T12, T13>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11, T12, T13> either, Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(
                    selector(either.Item10.Value));
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13> Select11<T1, T2, T3, T4, T5, T6,
            T7, T8, T9, T10, T11A, T11B, T12, T13>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T12, T13> either, Func<T11A, T11B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(
                    selector(either.Item11.Value));
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13> Select12<T1, T2, T3, T4, T5, T6,
            T7, T8, T9, T10, T11, T12A, T12B, T13>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T13> either, Func<T12A, T12B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(
                    selector(either.Item12.Value));
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item13.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B> Select13<T1, T2, T3, T4, T5, T6,
            T7, T8, T9, T10, T11, T12, T13A, T13B>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A> either, Func<T13A, T13B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(
                    selector(either.Item13.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T1B, T2B, T3B, T4B, T5B, T6B,
                T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A> input,
                Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4,
                Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8,
                Func<T9A, T9B> selector9, Func<T10A, T10B> selector10, Func<T11A, T11B> selector11,
                Func<T12A, T12B> selector12, Func<T13A, T13B> selector13)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector10(input.Item10.Value));
            }
            else if (input.Item11.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector11(input.Item11.Value));
            }
            else if (input.Item12.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector12(input.Item12.Value));
            }
            else if (input.Item13.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
                    selector13(input.Item13.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either14Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select1<T1A, T1B, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select2<T1, T2A, T2B, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select3<T1, T2, T3A, T3B,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select4<T1, T2, T3, T4A,
            T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select5<T1, T2, T3, T4, T5A,
            T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11, T12, T13, T14> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14> Select6<T1, T2, T3, T4, T5,
            T6A, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11, T12, T13, T14> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14> Select7<T1, T2, T3, T4, T5,
            T6, T7A, T7B, T8, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11, T12, T13, T14> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14> Select8<T1, T2, T3, T4, T5,
            T6, T7, T8A, T8B, T9, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11, T12, T13, T14> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(
                    selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14> Select9<T1, T2, T3, T4, T5,
            T6, T7, T8, T9A, T9B, T10, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11, T12, T13, T14> either, Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(
                    selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14> Select10<T1, T2, T3, T4, T5,
            T6, T7, T8, T9, T10A, T10B, T11, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11, T12, T13, T14> either, Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(
                    selector(either.Item10.Value));
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14> Select11<T1, T2, T3, T4, T5,
            T6, T7, T8, T9, T10, T11A, T11B, T12, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T12, T13, T14> either, Func<T11A, T11B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(
                    selector(either.Item11.Value));
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14> Select12<T1, T2, T3, T4, T5,
            T6, T7, T8, T9, T10, T11, T12A, T12B, T13, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T13, T14> either, Func<T12A, T12B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(
                    selector(either.Item12.Value));
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14> Select13<T1, T2, T3, T4, T5,
            T6, T7, T8, T9, T10, T11, T12, T13A, T13B, T14>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A, T14> either, Func<T13A, T13B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(
                    selector(either.Item13.Value));
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14>(either.Item14.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B> Select14<T1, T2, T3, T4, T5,
            T6, T7, T8, T9, T10, T11, T12, T13, T14A, T14B>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14A> either, Func<T14A, T14B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item1.Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item2.Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item3.Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item4.Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item5.Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item6.Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item7.Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item8.Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item9.Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B>(
                    selector(either.Item14.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T14A, T1B, T2B, T3B, T4B, T5B,
                T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T14A> input,
                Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4,
                Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8,
                Func<T9A, T9B> selector9, Func<T10A, T10B> selector10, Func<T11A, T11B> selector11,
                Func<T12A, T12B> selector12, Func<T13A, T13B> selector13, Func<T14A, T14B> selector14)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector10(input.Item10.Value));
            }
            else if (input.Item11.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector11(input.Item11.Value));
            }
            else if (input.Item12.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector12(input.Item12.Value));
            }
            else if (input.Item13.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector13(input.Item13.Value));
            }
            else if (input.Item14.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B>(
                    selector14(input.Item14.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either15Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select1<T1A, T1B, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select2<T1, T2A, T2B,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select3<T1, T2, T3A,
            T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select4<T1, T2, T3,
            T4A, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select5<T1, T2, T3, T4,
            T5A, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select6<T1, T2, T3, T4,
            T5, T6A, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15> Select7<T1, T2, T3, T4,
            T5, T6, T7A, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11, T12, T13, T14, T15> either, Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15> Select8<T1, T2, T3, T4,
            T5, T6, T7, T8A, T8B, T9, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11, T12, T13, T14, T15> either, Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15> Select9<T1, T2, T3, T4,
            T5, T6, T7, T8, T9A, T9B, T10, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11, T12, T13, T14, T15> either, Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15> Select10<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10A, T10B, T11, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11, T12, T13, T14, T15> either,
            Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(
                    selector(either.Item10.Value));
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15> Select11<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11A, T11B, T12, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T12, T13, T14, T15> either,
            Func<T11A, T11B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(
                    selector(either.Item11.Value));
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15> Select12<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12A, T12B, T13, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T13, T14, T15> either,
            Func<T12A, T12B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(
                    selector(either.Item12.Value));
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15> Select13<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A, T13B, T14, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A, T14, T15> either,
            Func<T13A, T13B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(
                    selector(either.Item13.Value));
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15> Select14<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14A, T14B, T15>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14A, T15> either,
            Func<T14A, T14B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(
                    selector(either.Item14.Value));
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15>(
                    either.Item15.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B> Select15<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15A, T15B>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15A> either,
            Func<T15A, T15B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(
                    either.Item10.Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(
                    either.Item11.Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(
                    either.Item12.Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(
                    either.Item13.Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(
                    either.Item14.Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B>(
                    selector(either.Item15.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T14A, T15A, T1B, T2B, T3B, T4B,
                T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T14A, T15A> input,
                Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4,
                Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8,
                Func<T9A, T9B> selector9, Func<T10A, T10B> selector10, Func<T11A, T11B> selector11,
                Func<T12A, T12B> selector12, Func<T13A, T13B> selector13, Func<T14A, T14B> selector14,
                Func<T15A, T15B> selector15)
        {
            if (input.Item1.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector10(input.Item10.Value));
            }
            else if (input.Item11.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector11(input.Item11.Value));
            }
            else if (input.Item12.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector12(input.Item12.Value));
            }
            else if (input.Item13.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector13(input.Item13.Value));
            }
            else if (input.Item14.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector14(input.Item14.Value));
            }
            else if (input.Item15.HasValue)
            {
                return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B>(
                    selector15(input.Item15.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static class Either16Extensions
    {
        public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select1<T1A, T1B,
            T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T1A, T1B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item1.Value));
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select2<T1, T2A,
            T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T2A, T2B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item2.Value));
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select3<T1, T2,
            T3A, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T3A, T3B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item3.Value));
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select4<T1, T2,
            T3, T4A, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T4A, T4B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item4.Value));
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select5<T1, T2,
            T3, T4, T5A, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T5A, T5B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item5.Value));
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select6<T1, T2,
            T3, T4, T5, T6A, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T6A, T6B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item6.Value));
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16> Select7<T1, T2,
            T3, T4, T5, T6, T7A, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T7A, T7B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item7.Value));
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16> Select8<T1, T2,
            T3, T4, T5, T6, T7, T8A, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T8A, T8B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item8.Value));
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16> Select9<T1, T2,
            T3, T4, T5, T6, T7, T8, T9A, T9B, T10, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11, T12, T13, T14, T15, T16> either,
            Func<T9A, T9B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item9.Value));
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16> Select10<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10A, T10B, T11, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11, T12, T13, T14, T15, T16> either,
            Func<T10A, T10B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(
                    selector(either.Item10.Value));
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16> Select11<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11A, T11B, T12, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T12, T13, T14, T15, T16> either,
            Func<T11A, T11B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(
                    selector(either.Item11.Value));
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16> Select12<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T12B, T13, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T13, T14, T15, T16> either,
            Func<T12A, T12B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(
                    selector(either.Item12.Value));
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16> Select13<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A, T13B, T14, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A, T14, T15, T16> either,
            Func<T13A, T13B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(
                    selector(either.Item13.Value));
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B, T14, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16> Select14<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14A, T14B, T15, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14A, T15, T16> either,
            Func<T14A, T14B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(
                    selector(either.Item14.Value));
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14B, T15, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16> Select15<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15A, T15B, T16>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15A, T16> either,
            Func<T15A, T15B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(
                    selector(either.Item15.Value));
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15B, T16>(either.Item16
                    .Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B> Select16<T1, T2,
            T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16A, T16B>(
            IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16A> either,
            Func<T16A, T16B> selector)
        {
            if (either.Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item1
                    .Value);
            }
            else if (either.Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item2
                    .Value);
            }
            else if (either.Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item3
                    .Value);
            }
            else if (either.Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item4
                    .Value);
            }
            else if (either.Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item5
                    .Value);
            }
            else if (either.Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item6
                    .Value);
            }
            else if (either.Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item7
                    .Value);
            }
            else if (either.Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item8
                    .Value);
            }
            else if (either.Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item9
                    .Value);
            }
            else if (either.Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item10
                    .Value);
            }
            else if (either.Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item11
                    .Value);
            }
            else if (either.Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item12
                    .Value);
            }
            else if (either.Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item13
                    .Value);
            }
            else if (either.Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item14
                    .Value);
            }
            else if (either.Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(either.Item15
                    .Value);
            }
            else if (either.Item16.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16B>(
                    selector(either.Item16.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>
            Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T14A, T15A, T16A, T1B, T2B, T3B,
                T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T14A, T15A, T16A>
                    input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3,
                Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7,
                Func<T8A, T8B> selector8, Func<T9A, T9B> selector9, Func<T10A, T10B> selector10,
                Func<T11A, T11B> selector11, Func<T12A, T12B> selector12, Func<T13A, T13B> selector13,
                Func<T14A, T14B> selector14, Func<T15A, T15B> selector15, Func<T16A, T16B> selector16)
        {
            if (input.Item1.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector1(input.Item1.Value));
            }
            else if (input.Item2.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector2(input.Item2.Value));
            }
            else if (input.Item3.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector3(input.Item3.Value));
            }
            else if (input.Item4.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector4(input.Item4.Value));
            }
            else if (input.Item5.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector5(input.Item5.Value));
            }
            else if (input.Item6.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector6(input.Item6.Value));
            }
            else if (input.Item7.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector7(input.Item7.Value));
            }
            else if (input.Item8.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector8(input.Item8.Value));
            }
            else if (input.Item9.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector9(input.Item9.Value));
            }
            else if (input.Item10.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector10(input.Item10.Value));
            }
            else if (input.Item11.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector11(input.Item11.Value));
            }
            else if (input.Item12.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector12(input.Item12.Value));
            }
            else if (input.Item13.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector13(input.Item13.Value));
            }
            else if (input.Item14.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector14(input.Item14.Value));
            }
            else if (input.Item15.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector15(input.Item15.Value));
            }
            else if (input.Item16.HasValue)
            {
                return new
                    Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B, T14B, T15B, T16B>(
                        selector16(input.Item16.Value));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}