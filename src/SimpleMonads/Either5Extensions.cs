using System;

namespace SimpleMonads {
public static class Either5Extensions
{
public static IEither<T1B, T2, T3, T4, T5> Select1<T1A, T1B, T2, T3, T4, T5>(IEither<T1A, T2, T3, T4, T5> either, Func<T1A, T1B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3, T4, T5>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3, T4, T5>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3, T4, T5>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1B, T2, T3, T4, T5>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1B, T2, T3, T4, T5>(either.Item5.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2B, T3, T4, T5> Select2<T1, T2A, T2B, T3, T4, T5>(IEither<T1, T2A, T3, T4, T5> either, Func<T2A, T2B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3, T4, T5>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3, T4, T5>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3, T4, T5>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2B, T3, T4, T5>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2B, T3, T4, T5>(either.Item5.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3B, T4, T5> Select3<T1, T2, T3A, T3B, T4, T5>(IEither<T1, T2, T3A, T4, T5> either, Func<T3A, T3B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B, T4, T5>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B, T4, T5>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B, T4, T5>(selector(either.Item3.Value));
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3B, T4, T5>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3B, T4, T5>(either.Item5.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4B, T5> Select4<T1, T2, T3, T4A, T4B, T5>(IEither<T1, T2, T3, T4A, T5> either, Func<T4A, T4B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4B, T5>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4B, T5>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4B, T5>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4B, T5>(selector(either.Item4.Value));
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4B, T5>(either.Item5.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5B> Select5<T1, T2, T3, T4, T5A, T5B>(IEither<T1, T2, T3, T4, T5A> either, Func<T5A, T5B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5B>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5B>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5B>(selector(either.Item5.Value));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1B, T2B, T3B, T4B, T5B> Select<T1A, T2A, T3A, T4A, T5A, T1B, T2B, T3B, T4B, T5B>(this IEither<T1A, T2A, T3A, T4A, T5A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5) {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector3(input.Item3.Value));
}
else if (input.Item4.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector4(input.Item4.Value));
}
else if (input.Item5.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector5(input.Item5.Value));
}
else {
throw new InvalidOperationException();
}
}

}
}
