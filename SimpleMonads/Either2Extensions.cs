using System;

namespace SimpleMonads {
public static class Either2Extensions
{
public static SubTypesOf<object>.Either<T1B, T2> Select1<TBase, T1A, T1B, T2>(this SubTypesOf<TBase>.IEither<T1A, T2> either, Func<T1A, T1B> selector) where T1A : TBase where T2 : TBase
{
if (either.Item1 != null) {
return new Either<T1B, T2>(selector(either.Item1));
}
else if (either.Item2 != null) {
return new Either<T1B, T2>(either.Item2);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.Either<T1, T2B> Select2<TBase, T1, T2A, T2B>(this SubTypesOf<TBase>.IEither<T1, T2A> either, Func<T2A, T2B> selector) where T1 : TBase where T2A : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2B>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2B>(selector(either.Item2));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2> Either<T1, T2>(this T1 item) {
return new Either<T1, T2>(item);
}
public static IEither<T1, T2> Either<T1, T2>(this T2 item) {
return new Either<T1, T2>(item);
}
public static SubTypesOf<object>.IEither<T1B, T2B> Select<TBase, T1A, T2A, T1B, T2B>(this SubTypesOf<TBase>.IEither<T1A, T2A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2) where T1A : TBase where T2A : TBase {
if (input.Item1 != null) {
return new Either<T1B, T2B>(
selector1(input.Item1));
}
else if (input.Item2 != null) {
return new Either<T1B, T2B>(
selector2(input.Item2));
}
else {
throw new InvalidOperationException();
}
}

public static IEitherBase<T1, T2> ForEach<T1, T2>(this IEitherBase<T1, T2> input, Action<T1> action1, Action<T2> action2) {
if (input.Item1 != null) {
action1(input.Item1);
}
else if (input.Item2 != null) {
action2(input.Item2);
}
else {
throw new InvalidOperationException();
}
return input;
}

public static SubTypesOf<TBase>.IEither<T1, T2> AsSubTypes<TBase, T1, T2>(this ConvertibleTo<TBase>.IEither<T1, T2> either) where T1 : TBase where T2 : TBase {
if (either.Item1 != null) {
return new SubTypesOf<TBase>.Either<T1, T2>(either.Item1);
}
if (either.Item2 != null) {
return new SubTypesOf<TBase>.Either<T1, T2>(either.Item2);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
}
