namespace SimpleMonads {
public interface IEitherBase<out T1, out T2, out T3, out T4> : IEither {
IMaybe<T1> Item1 { get; }
IMaybe<T2> Item2 { get; }
IMaybe<T3> Item3 { get; }
IMaybe<T4> Item4 { get; }
IEither<T1, T2, T3, T4, T5> Or<T5>();
IEither<T1, T2, T3, T4, T5, T6> Or<T5, T6>();
IEither<T1, T2, T3, T4, T5, T6, T7> Or<T5, T6, T7>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T5, T6, T7, T8>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T5, T6, T7, T8, T9>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T5, T6, T7, T8, T9, T10>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T5, T6, T7, T8, T9, T10, T11>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T5, T6, T7, T8, T9, T10, T11, T12>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
Cast<TBase>.IEither<T1, T2, T3, T4> Cast<TBase>();
}
public partial class Cast<TBase> {
public interface IEither<out T1, out T2, out T3, out T4> : SubTypesOf<TBase>.IEither4, IEitherBase<T1, T2, T3, T4> 
{
}
}
public partial class SubTypesOf<TBase> {
public interface IEither4 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2, out T3, out T4> : IEither4, IEitherBase<T1, T2, T3, T4> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase 
{
}
}
public interface IEither<out T1, out T2, out T3, out T4> : SubTypesOf<object>.IEither<T1, T2, T3, T4> 
{
}
}
