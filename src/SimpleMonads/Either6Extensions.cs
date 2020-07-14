using System;

namespace SimpleMonads {
public static class Either6Extensions
{
public static IEither<T1B, T2, T3, T4, T5, T6> Select1<T1A, T1B, T2, T3, T4, T5, T6>(IEither<T1A, T2, T3, T4, T5, T6> either, Func<T1A, T1B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6>(either.Item6.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2B, T3, T4, T5, T6> Select2<T1, T2A, T2B, T3, T4, T5, T6>(IEither<T1, T2A, T3, T4, T5, T6> either, Func<T2A, T2B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6>(either.Item6.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3B, T4, T5, T6> Select3<T1, T2, T3A, T3B, T4, T5, T6>(IEither<T1, T2, T3A, T4, T5, T6> either, Func<T3A, T3B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6>(selector(either.Item3.Value));
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6>(either.Item6.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4B, T5, T6> Select4<T1, T2, T3, T4A, T4B, T5, T6>(IEither<T1, T2, T3, T4A, T5, T6> either, Func<T4A, T4B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6>(selector(either.Item4.Value));
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6>(either.Item6.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5B, T6> Select5<T1, T2, T3, T4, T5A, T5B, T6>(IEither<T1, T2, T3, T4, T5A, T6> either, Func<T5A, T5B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6>(selector(either.Item5.Value));
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6>(either.Item6.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6B> Select6<T1, T2, T3, T4, T5, T6A, T6B>(IEither<T1, T2, T3, T4, T5, T6A> either, Func<T6A, T6B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B>(selector(either.Item6.Value));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1B, T2B, T3B, T4B, T5B, T6B> Select<T1A, T2A, T3A, T4A, T5A, T6A, T1B, T2B, T3B, T4B, T5B, T6B>(this IEither<T1A, T2A, T3A, T4A, T5A, T6A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6) {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
selector3(input.Item3.Value));
}
else if (input.Item4.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
selector4(input.Item4.Value));
}
else if (input.Item5.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
selector5(input.Item5.Value));
}
else if (input.Item6.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B>(
selector6(input.Item6.Value));
}
else {
throw new InvalidOperationException();
}
}

}
}
