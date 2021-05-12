using System;

namespace SimpleMonads {
public static class Either9Extensions
{
public static SubTypesOf<object>.IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9> Select1<TBase, T1A, T1B, T2, T3, T4, T5, T6, T7, T8, T9>(SubTypesOf<TBase>.IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9> either, Func<T1A, T1B> selector) where T1A : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9> Select2<TBase, T1, T2A, T2B, T3, T4, T5, T6, T7, T8, T9>(SubTypesOf<TBase>.IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9> either, Func<T2A, T2B> selector) where T1 : TBase where T2A : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(selector(either.Item2.Value));
}
else if (either.Item3.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9> Select3<TBase, T1, T2, T3A, T3B, T4, T5, T6, T7, T8, T9>(SubTypesOf<TBase>.IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9> either, Func<T3A, T3B> selector) where T1 : TBase where T2 : TBase where T3A : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(selector(either.Item3.Value));
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9> Select4<TBase, T1, T2, T3, T4A, T4B, T5, T6, T7, T8, T9>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9> either, Func<T4A, T4B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4A : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(selector(either.Item4.Value));
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9> Select5<TBase, T1, T2, T3, T4, T5A, T5B, T6, T7, T8, T9>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9> either, Func<T5A, T5B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5A : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(selector(either.Item5.Value));
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9> Select6<TBase, T1, T2, T3, T4, T5, T6A, T6B, T7, T8, T9>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9> either, Func<T6A, T6B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6A : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(selector(either.Item6.Value));
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9> Select7<TBase, T1, T2, T3, T4, T5, T6, T7A, T7B, T8, T9>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9> either, Func<T7A, T7B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7A : TBase where T8 : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(selector(either.Item7.Value));
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9> Select8<TBase, T1, T2, T3, T4, T5, T6, T7, T8A, T8B, T9>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9> either, Func<T8A, T8B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8A : TBase where T9 : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(selector(either.Item8.Value));
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9>(either.Item9.Value);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B> Select9<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9A, T9B>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A> either, Func<T9A, T9B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9A : TBase
{
if (either.Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item2.Value);
}
else if (either.Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item3.Value);
}
else if (either.Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item4.Value);
}
else if (either.Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item5.Value);
}
else if (either.Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item6.Value);
}
else if (either.Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item7.Value);
}
else if (either.Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(either.Item8.Value);
}
else if (either.Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B>(selector(either.Item9.Value));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T1 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T2 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T3 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T4 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T5 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T6 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T7 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T8 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T9 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item);
}
public static SubTypesOf<object>.IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B> Select<TBase, T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(this SubTypesOf<TBase>.IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8, Func<T9A, T9B> selector9) where T1A : TBase where T2A : TBase where T3A : TBase where T4A : TBase where T5A : TBase where T6A : TBase where T7A : TBase where T8A : TBase where T9A : TBase {
if (input.Item1.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector2(input.Item2.Value));
}
else if (input.Item3.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector3(input.Item3.Value));
}
else if (input.Item4.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector4(input.Item4.Value));
}
else if (input.Item5.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector5(input.Item5.Value));
}
else if (input.Item6.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector6(input.Item6.Value));
}
else if (input.Item7.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector7(input.Item7.Value));
}
else if (input.Item8.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector8(input.Item8.Value));
}
else if (input.Item9.HasValue) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B>(
selector9(input.Item9.Value));
}
else {
throw new InvalidOperationException();
}
}

public static IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9> ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9> input, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6, Action<T7> action7, Action<T8> action8, Action<T9> action9) {
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
else if (input.Item5.HasValue) {
action5(input.Item5.Value);
}
else if (input.Item6.HasValue) {
action6(input.Item6.Value);
}
else if (input.Item7.HasValue) {
action7(input.Item7.Value);
}
else if (input.Item8.HasValue) {
action8(input.Item8.Value);
}
else if (input.Item9.HasValue) {
action9(input.Item9.Value);
}
else {
throw new InvalidOperationException();
}
return input;
}

public static SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Safely<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Cast<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase {
if (either.Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item1.Value);
}
if (either.Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item2.Value);
}
if (either.Item3.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item3.Value);
}
if (either.Item4.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item4.Value);
}
if (either.Item5.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item5.Value);
}
if (either.Item6.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item6.Value);
}
if (either.Item7.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item7.Value);
}
if (either.Item8.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item8.Value);
}
if (either.Item9.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(either.Item9.Value);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
}
