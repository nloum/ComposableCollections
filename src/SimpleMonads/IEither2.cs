namespace SimpleMonads {
public interface IEitherBase<out T1, out T2> : IEither {
IMaybe<T1> Item1 { get; }
IMaybe<T2> Item2 { get; }
IEither<T1, T2, T3> Or<T3>();
IEither<T1, T2, T3, T4> Or<T3, T4>();
IEither<T1, T2, T3, T4, T5> Or<T3, T4, T5>();
IEither<T1, T2, T3, T4, T5, T6> Or<T3, T4, T5, T6>();
IEither<T1, T2, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
ConvertibleTo<TBase>.IEither<T1, T2> ConvertTo<TBase>();
}
public partial class ConvertibleTo<TBase> {
public interface IEither<out T1, out T2> : SubTypesOf<TBase>.IEither2, IEitherBase<T1, T2> 
{
}
}
public partial class SubTypesOf<TBase> {
public interface IEither2 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2> : IEither2, IEitherBase<T1, T2> where T1 : TBase where T2 : TBase 
{
}
}
public interface IEither<out T1, out T2> : SubTypesOf<object>.IEither<T1, T2> 
{
}
}
