using System;

namespace SimpleMonads {
public static class Either4Extensions
{
public static SubTypesOf<object>.IEither<T1B, T2, T3, T4> Select1<TBase, T1A, T1B, T2, T3, T4>(SubTypesOf<TBase>.IEither<T1A, T2, T3, T4> either, Func<T1A, T1B> selector) where T1A : TBase where T2 : TBase where T3 : TBase where T4 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3, T4>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3, T4>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3, T4>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1B, T2, T3, T4>(either.Item4.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2B, T3, T4> Select2<TBase, T1, T2A, T2B, T3, T4>(SubTypesOf<TBase>.IEither<T1, T2A, T3, T4> either, Func<T2A, T2B> selector) where T1 : TBase where T2A : TBase where T3 : TBase where T4 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3, T4>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3, T4>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3, T4>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2B, T3, T4>(either.Item4.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3B, T4> Select3<TBase, T1, T2, T3A, T3B, T4>(SubTypesOf<TBase>.IEither<T1, T2, T3A, T4> either, Func<T3A, T3B> selector) where T1 : TBase where T2 : TBase where T3A : TBase where T4 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B, T4>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B, T4>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B, T4>(selector(either.Item3.Value));
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3B, T4>(either.Item4.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4B> Select4<TBase, T1, T2, T3, T4A, T4B>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4A> either, Func<T4A, T4B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4A : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4B>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4B>(selector(either.Item4.Value));
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1B, T2B, T3B, T4B> Select<TBase, T1A, T2A, T3A, T4A, T1B, T2B, T3B, T4B>(this SubTypesOf<TBase>.IEither<T1A, T2A, T3A, T4A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4) where T1A : TBase where T2A : TBase where T3A : TBase where T4A : TBase {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B, T4B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B, T4B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B, T4B>(
selector3(input.Item3.Value));
}
else if (input.Item4.HasValue) {
return new Either<T1B, T2B, T3B, T4B>(
selector4(input.Item4.Value));
}
else {
throw new InvalidOperationException();
}
}

public static SubTypesOf<TBase>.IEither<T1, T2, T3, T4> ForEach<TBase, T1, T2, T3, T4>(this SubTypesOf<TBase>.IEither<T1, T2, T3, T4> input, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase {
if (input.Item1.HasValue) {
action1(input.Item1.Value);
}
else if (input.Item2.HasValue) {
action2(input.Item2.Value);
}
else if (input.Item3.HasValue) {
action3(input.Item3.Value);
}
else if (input.Item4.HasValue) {
action4(input.Item4.Value);
}
else {
throw new InvalidOperationException();
}
return input;
}

}
}
