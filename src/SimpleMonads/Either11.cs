using System;

namespace SimpleMonads
{
    public class
        Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        public Either(T1 item1)
        {
            Item1 = item1.ToMaybe();
        }

        public Either(T2 item2)
        {
            Item2 = item2.ToMaybe();
        }

        public Either(T3 item3)
        {
            Item3 = item3.ToMaybe();
        }

        public Either(T4 item4)
        {
            Item4 = item4.ToMaybe();
        }

        public Either(T5 item5)
        {
            Item5 = item5.ToMaybe();
        }

        public Either(T6 item6)
        {
            Item6 = item6.ToMaybe();
        }

        public Either(T7 item7)
        {
            Item7 = item7.ToMaybe();
        }

        public Either(T8 item8)
        {
            Item8 = item8.ToMaybe();
        }

        public Either(T9 item9)
        {
            Item9 = item9.ToMaybe();
        }

        public Either(T10 item10)
        {
            Item10 = item10.ToMaybe();
        }

        public Either(T11 item11)
        {
            Item11 = item11.ToMaybe();
        }

        public IMaybe<T1> Item1 { get; } = Utility.Nothing<T1>();
        public IMaybe<T2> Item2 { get; } = Utility.Nothing<T2>();
        public IMaybe<T3> Item3 { get; } = Utility.Nothing<T3>();
        public IMaybe<T4> Item4 { get; } = Utility.Nothing<T4>();
        public IMaybe<T5> Item5 { get; } = Utility.Nothing<T5>();
        public IMaybe<T6> Item6 { get; } = Utility.Nothing<T6>();
        public IMaybe<T7> Item7 { get; } = Utility.Nothing<T7>();
        public IMaybe<T8> Item8 { get; } = Utility.Nothing<T8>();
        public IMaybe<T9> Item9 { get; } = Utility.Nothing<T9>();
        public IMaybe<T10> Item10 { get; } = Utility.Nothing<T10>();
        public IMaybe<T11> Item11 { get; } = Utility.Nothing<T11>();

        public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T12>()
        {
            if (Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item1.Value);
            }

            if (Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item2.Value);
            }

            if (Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item3.Value);
            }

            if (Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item4.Value);
            }

            if (Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item5.Value);
            }

            if (Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item6.Value);
            }

            if (Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item7.Value);
            }

            if (Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item8.Value);
            }

            if (Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item9.Value);
            }

            if (Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item10.Value);
            }

            if (Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item11.Value);
            }

            throw new System.InvalidOperationException("The either has no values");
        }

        public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T12, T13>()
        {
            if (Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item1.Value);
            }

            if (Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item2.Value);
            }

            if (Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item3.Value);
            }

            if (Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item4.Value);
            }

            if (Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item5.Value);
            }

            if (Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item6.Value);
            }

            if (Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item7.Value);
            }

            if (Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item8.Value);
            }

            if (Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item9.Value);
            }

            if (Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item10.Value);
            }

            if (Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item11.Value);
            }

            throw new System.InvalidOperationException("The either has no values");
        }

        public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T12, T13, T14>()
        {
            if (Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item1.Value);
            }

            if (Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item2.Value);
            }

            if (Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item3.Value);
            }

            if (Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item4.Value);
            }

            if (Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item5.Value);
            }

            if (Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item6.Value);
            }

            if (Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item7.Value);
            }

            if (Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item8.Value);
            }

            if (Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item9.Value);
            }

            if (Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item10.Value);
            }

            if (Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item11.Value);
            }

            throw new System.InvalidOperationException("The either has no values");
        }

        public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T12, T13, T14, T15>()
        {
            if (Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item1.Value);
            }

            if (Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item2.Value);
            }

            if (Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item3.Value);
            }

            if (Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item4.Value);
            }

            if (Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item5.Value);
            }

            if (Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item6.Value);
            }

            if (Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item7.Value);
            }

            if (Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item8.Value);
            }

            if (Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item9.Value);
            }

            if (Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item10.Value);
            }

            if (Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item11.Value);
            }

            throw new System.InvalidOperationException("The either has no values");
        }

        public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T12, T13, T14, T15,
            T16>()
        {
            if (Item1.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item1.Value);
            }

            if (Item2.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item2.Value);
            }

            if (Item3.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item3.Value);
            }

            if (Item4.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item4.Value);
            }

            if (Item5.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item5.Value);
            }

            if (Item6.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item6.Value);
            }

            if (Item7.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item7.Value);
            }

            if (Item8.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item8.Value);
            }

            if (Item9.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item9.Value);
            }

            if (Item10.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item10.Value);
            }

            if (Item11.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item11.Value);
            }

            throw new System.InvalidOperationException("The either has no values");
        }
    }
}