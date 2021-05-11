namespace SimpleMonads {
public partial class SubTypesOf<TBase> {
public interface IEither11 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9, out T10, out T11> : IEither11 where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase 
{
IMaybe<T1> Item1 { get; }
IMaybe<T2> Item2 { get; }
IMaybe<T3> Item3 { get; }
IMaybe<T4> Item4 { get; }
IMaybe<T5> Item5 { get; }
IMaybe<T6> Item6 { get; }
IMaybe<T7> Item7 { get; }
IMaybe<T8> Item8 { get; }
IMaybe<T9> Item9 { get; }
IMaybe<T10> Item10 { get; }
IMaybe<T11> Item11 { get; }
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T12>() where T12 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T12, T13>() where T12 : TBase where T13 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T12, T13, T14>() where T12 : TBase where T13 : TBase where T14 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T12, T13, T14, T15>() where T12 : TBase where T13 : TBase where T14 : TBase where T15 : TBase;
SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T12, T13, T14, T15, T16>() where T12 : TBase where T13 : TBase where T14 : TBase where T15 : TBase where T16 : TBase;
public interface ICast<out TBase> : IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> {
TBase Value { get; }
}
}
}
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9, out T10, out T11> : SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> 
{
}
}
