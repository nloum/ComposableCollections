namespace SimpleMonads {
public interface IEitherBase<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9, out T10, out T11, out T12, out T13, out T14> : IEither {
T1? Item1 { get; }
T2? Item2 { get; }
T3? Item3 { get; }
T4? Item4 { get; }
T5? Item5 { get; }
T6? Item6 { get; }
T7? Item7 { get; }
T8? Item8 { get; }
T9? Item9 { get; }
T10? Item10 { get; }
T11? Item11 { get; }
T12? Item12 { get; }
T13? Item13 { get; }
T14? Item14 { get; }
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T15>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T15, T16>();
ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> ConvertTo<TBase>();
}
public partial class ConvertibleTo<TBase> {
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9, out T10, out T11, out T12, out T13, out T14> : SubTypesOf<TBase>.IEither14, IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> 
{
}
}
public partial class SubTypesOf<TBase> {
public interface IEither14 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9, out T10, out T11, out T12, out T13, out T14> : IEither14, IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase 
{
}
}
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9, out T10, out T11, out T12, out T13, out T14> : SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> 
{
}
}
