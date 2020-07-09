namespace SimpleMonads
{
    public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6>
    {
        IMaybe<T1> Item1 { get; }
        IMaybe<T2> Item2 { get; }
        IMaybe<T3> Item3 { get; }
        IMaybe<T4> Item4 { get; }
        IMaybe<T5> Item5 { get; }
        IMaybe<T6> Item6 { get; }
        IEither<T1, T2, T3, T4, T5, T6, T7> Or<T7>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T7, T8>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T7, T8, T9>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T7, T8, T9, T10>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T7, T8, T9, T10, T11>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T7, T8, T9, T10, T11, T12>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T7, T8, T9, T10, T11, T12, T13>();
        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T7, T8, T9, T10, T11, T12, T13, T14>();

        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T7, T8, T9, T10, T11, T12, T13,
            T14, T15>();

        IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T7, T8, T9, T10, T11, T12,
            T13, T14, T15, T16>();
    }
}