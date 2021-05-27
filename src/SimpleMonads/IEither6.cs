using System;

namespace SimpleMonads {
public interface IEitherBase<out T1, out T2, out T3, out T4, out T5, out T6> : IEither {
T1? Item1 { get; }
T2? Item2 { get; }
T3? Item3 { get; }
T4? Item4 { get; }
T5? Item5 { get; }
T6? Item6 { get; }
IEither<T1, T2, T3, T4, T5, T6, T7> Or<T7>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T7, T8>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T7, T8, T9>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T7, T8, T9, T10>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T7, T8, T9, T10, T11>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T7, T8, T9, T10, T11, T12>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T7, T8, T9, T10, T11, T12, T13>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T7, T8, T9, T10, T11, T12, T13, T14>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T7, T8, T9, T10, T11, T12, T13, T14, T15>();
IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
TOutput Collapse<TOutput>(Func<T1, TOutput> selector1, Func<T2, TOutput> selector2, Func<T3, TOutput> selector3, Func<T4, TOutput> selector4, Func<T5, TOutput> selector5, Func<T6, TOutput> selector6);
ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5, T6> ConvertTo<TBase>();
}
public partial class ConvertibleTo<TBase> {
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6> : SubTypesOf<TBase>.IEither6, IEitherBase<T1, T2, T3, T4, T5, T6> 
{
}
}
public partial class SubTypesOf<TBase> {
public interface IEither6 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6> : IEither6, IEitherBase<T1, T2, T3, T4, T5, T6> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase 
{
}
}
public interface IEither<out T1, out T2, out T3, out T4, out T5, out T6> : SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6> 
{
}
}
