using System;

namespace SimpleMonads {
public interface IEitherBase<out T1, out T2, out T3> : IEither {
T1? Item1 { get; }
T2? Item2 { get; }
T3? Item3 { get; }
IEitherBase<T1, T2, T3, T4> Or<T4>();
IEitherBase<T1, T2, T3, T4, T5> Or<T4, T5>();
IEitherBase<T1, T2, T3, T4, T5, T6> Or<T4, T5, T6>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7> Or<T4, T5, T6, T7>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8> Or<T4, T5, T6, T7, T8>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T4, T5, T6, T7, T8, T9>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T4, T5, T6, T7, T8, T9, T10>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T4, T5, T6, T7, T8, T9, T10, T11>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
TOutput Collapse<TOutput>(Func<T1, TOutput> selector1, Func<T2, TOutput> selector2, Func<T3, TOutput> selector3);
ConvertibleTo<TBase>.IEither<T1, T2, T3> ConvertTo<TBase>();
}
public partial class ConvertibleTo<TBase> {
public interface IEither<out T1, out T2, out T3> : SubTypesOf<TBase>.IEither3, IEitherBase<T1, T2, T3> 
{
}
}
public partial class SubTypesOf<TBase> {
public interface IEither3 : IEither {
TBase Value { get; }
}
public interface IEither<out T1, out T2, out T3> : IEither3, IEitherBase<T1, T2, T3> where T1 : TBase where T2 : TBase where T3 : TBase 
{
}
}
public interface IEither<out T1, out T2, out T3> : SubTypesOf<object>.IEither<T1, T2, T3> 
{
}
}
