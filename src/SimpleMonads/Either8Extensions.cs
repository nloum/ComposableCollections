using System;

namespace SimpleMonads {
public static class Either8Extensions
{
public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7, T8>(IEither<T1A, T2, T3, T4, T5, T6, T7, T8> either, Func<T1A, T1B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7, T8>(IEither<T1, T2A, T3, T4, T5, T6, T7, T8> either, Func<T2A, T2B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7, T8>(IEither<T1, T2, T3A, T4, T5, T6, T7, T8> either, Func<T3A, T3B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(selector(either.Item3.Value));
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7, T8>(IEither<T1, T2, T3, T4A, T5, T6, T7, T8> either, Func<T4A, T4B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(selector(either.Item4.Value));
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7, T8>(IEither<T1, T2, T3, T4, T5A, T6, T7, T8> either, Func<T5A, T5B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(selector(either.Item5.Value));
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7, T8>(IEither<T1, T2, T3, T4, T5, T6A, T7, T8> either, Func<T6A, T6B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(selector(either.Item6.Value));
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B, T8>(IEither<T1, T2, T3, T4, T5, T6, T7A, T8> either, Func<T7A, T7B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(selector(either.Item7.Value));
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8>(either.Item8.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B> Select8<T1, T2, T3, T4, T5, T6, T7, T8A, T8B>(IEither<T1, T2, T3, T4, T5, T6, T7, T8A> either, Func<T8A, T8B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B>(selector(either.Item8.Value));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B> Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8) {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector3(input.Item3.Value));
}
else if (input.Item4.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector4(input.Item4.Value));
}
else if (input.Item5.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector5(input.Item5.Value));
}
else if (input.Item6.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector6(input.Item6.Value));
}
else if (input.Item7.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector7(input.Item7.Value));
}
else if (input.Item8.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B>(
selector8(input.Item8.Value));
}
else {
throw new InvalidOperationException();
}
}

}
}
