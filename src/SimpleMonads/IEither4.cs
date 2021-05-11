namespace SimpleMonads {
public partial class SubTypesOf<TBase> {
public interface IEither4 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2, out T3, out T4> : IEither4 where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase 
{
IMaybe<T1> Item1 { get; }
IMaybe<T2> Item2 { get; }
IMaybe<T3> Item3 { get; }
IMaybe<T4> Item4 { get; }
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5> Or<T5>() where T5 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6> Or<T5, T6>() where T5 : TBase where T6 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7> Or<T5, T6, T7>() where T5 : TBase where T6 : TBase where T7 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T5, T6, T7, T8>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T5, T6, T7, T8, T9>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T5, T6, T7, T8, T9, T10>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T5, T6, T7, T8, T9, T10, T11>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T5, T6, T7, T8, T9, T10, T11, T12>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase where T15 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>() where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase where T15 : TBase where T16 : TBase;
public interface ICast<out TBase> : IEither<T1, T2, T3, T4> {
TBase Value { get; }
}
}
}
public interface IEither<out T1, out T2, out T3, out T4> : SubTypesOf<object>.IEither<T1, T2, T3, T4> 
{
}
}
