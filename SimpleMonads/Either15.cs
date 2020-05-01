using System;

namespace SimpleMonads
{
    public sealed class
        Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IEither<T1, T2, T3, T4, T5, T6, T7,
            T8, T9, T10, T11, T12, T13, T14, T15>
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

        public Either(T12 item12)
        {
            Item12 = item12.ToMaybe();
        }

        public Either(T13 item13)
        {
            Item13 = item13.ToMaybe();
        }

        public Either(T14 item14)
        {
            Item14 = item14.ToMaybe();
        }

        public Either(T15 item15)
        {
            Item15 = item15.ToMaybe();
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
        public IMaybe<T12> Item12 { get; } = Utility.Nothing<T12>();
        public IMaybe<T13> Item13 { get; } = Utility.Nothing<T13>();
        public IMaybe<T14> Item14 { get; } = Utility.Nothing<T14>();
        public IMaybe<T15> Item15 { get; } = Utility.Nothing<T15>();

        public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T16>()
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

            if (Item12.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item12.Value);
            }

            if (Item13.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item13.Value);
            }

            if (Item14.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item14.Value);
            }

            if (Item15.HasValue)
            {
                return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item15.Value);
            }

            throw new System.InvalidOperationException("The either has no values");
        }
    }
}