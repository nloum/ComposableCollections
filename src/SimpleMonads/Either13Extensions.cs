using System;

namespace SimpleMonads {
public static class Either13Extensions
{
public static IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select1<T1A, T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> either, Func<T1A, T1B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select2<T1, T2A, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> either, Func<T2A, T2B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select3<T1, T2, T3A, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> either, Func<T3A, T3B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(selector(either.Item3.Value));
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select4<T1, T2, T3, T4A, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10, T11, T12, T13> either, Func<T4A, T4B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(selector(either.Item4.Value));
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13> Select5<T1, T2, T3, T4, T5A, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10, T11, T12, T13> either, Func<T5A, T5B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(selector(either.Item5.Value));
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13> Select6<T1, T2, T3, T4, T5, T6A, T6B, T7, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10, T11, T12, T13> either, Func<T6A, T6B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(selector(either.Item6.Value));
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13> Select7<T1, T2, T3, T4, T5, T6, T7A, T7B, T8, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10, T11, T12, T13> either, Func<T7A, T7B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(selector(either.Item7.Value));
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13> Select8<T1, T2, T3, T4, T5, T6, T7, T8A, T8B, T9, T10, T11, T12, T13>(IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10, T11, T12, T13> either, Func<T8A, T8B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(selector(either.Item8.Value));
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13> Select9<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T9B, T10, T11, T12, T13>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10, T11, T12, T13> either, Func<T9A, T9B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(selector(either.Item9.Value));
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13> Select10<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T10B, T11, T12, T13>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T11, T12, T13> either, Func<T10A, T10B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(selector(either.Item10.Value));
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B, T11, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13> Select11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T11B, T12, T13>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11A, T12, T13> either, Func<T11A, T11B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(selector(either.Item11.Value));
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11B, T12, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13> Select12<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T12B, T13>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12A, T13> either, Func<T12A, T12B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(selector(either.Item12.Value));
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12B, T13>(either.Item13.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B> Select13<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A, T13B>(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13A> either, Func<T13A, T13B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item9.Value);
}
else if (either.Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item10.Value);
}
else if (either.Item11.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item11.Value);
}
else if (either.Item12.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(either.Item12.Value);
}
else if (either.Item13.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13B>(selector(either.Item13.Value));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B> Select<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(this IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T11A, T12A, T13A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8, Func<T9A, T9B> selector9, Func<T10A, T10B> selector10, Func<T11A, T11B> selector11, Func<T12A, T12B> selector12, Func<T13A, T13B> selector13) {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector3(input.Item3.Value));
}
else if (input.Item4.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector4(input.Item4.Value));
}
else if (input.Item5.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector5(input.Item5.Value));
}
else if (input.Item6.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector6(input.Item6.Value));
}
else if (input.Item7.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector7(input.Item7.Value));
}
else if (input.Item8.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector8(input.Item8.Value));
}
else if (input.Item9.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector9(input.Item9.Value));
}
else if (input.Item10.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector10(input.Item10.Value));
}
else if (input.Item11.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector11(input.Item11.Value));
}
else if (input.Item12.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector12(input.Item12.Value));
}
else if (input.Item13.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B, T11B, T12B, T13B>(
selector13(input.Item13.Value));
}
else {
throw new InvalidOperationException();
}
}

}
}
