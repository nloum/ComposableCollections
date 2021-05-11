using System;

namespace SimpleMonads {
public static class Either3Extensions
{
public static SubTypesOf<object>.IEither<T1B, T2, T3> Select1<TBase, T1A, T1B, T2, T3>(SubTypesOf<TBase>.IEither<T1A, T2, T3> either, Func<T1A, T1B> selector) where T1A : TBase where T2 : TBase where T3 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3>(either.Item3.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2B, T3> Select2<TBase, T1, T2A, T2B, T3>(SubTypesOf<TBase>.IEither<T1, T2A, T3> either, Func<T2A, T2B> selector) where T1 : TBase where T2A : TBase where T3 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3>(either.Item3.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3B> Select3<TBase, T1, T2, T3A, T3B>(SubTypesOf<TBase>.IEither<T1, T2, T3A> either, Func<T3A, T3B> selector) where T1 : TBase where T2 : TBase where T3A : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B>(selector(either.Item3.Value));
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1B, T2B, T3B> Select<TBase, T1A, T2A, T3A, T1B, T2B, T3B>(this SubTypesOf<TBase>.IEither<T1A, T2A, T3A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3) where T1A : TBase where T2A : TBase where T3A : TBase {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B>(
selector3(input.Item3.Value));
}
else {
throw new InvalidOperationException();
}
}

public static SubTypesOf<TBase>.IEither<T1, T2, T3> ForEach<TBase, T1, T2, T3>(this SubTypesOf<TBase>.IEither<T1, T2, T3> input, Action<T1> action1, Action<T2> action2, Action<T3> action3) where T1 : TBase where T2 : TBase where T3 : TBase {
if (input.Item1.HasValue) {
action1(input.Item1.Value);
}
else if (input.Item2.HasValue) {
action2(input.Item2.Value);
}
else if (input.Item3.HasValue) {
action3(input.Item3.Value);
}
else {
throw new InvalidOperationException();
}
return input;
}

}
}
