using System;

namespace SimpleMonads {
public static class Either10Extensions
{
public static SubTypesOf<object>.IEither<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10> Select1<TBase, T1A, T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1A, T2, T3, T4, T5, T6, T7, T8, T9, T10> either, Func<T1A, T1B> selector) where T1A : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(selector(either.Item1));
}
else if (either.Item2 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1B, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10> Select2<TBase, T1, T2A, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2A, T3, T4, T5, T6, T7, T8, T9, T10> either, Func<T2A, T2B> selector) where T1 : TBase where T2A : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(selector(either.Item2));
}
else if (either.Item3 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2B, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10> Select3<TBase, T1, T2, T3A, T3B, T4, T5, T6, T7, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3A, T4, T5, T6, T7, T8, T9, T10> either, Func<T3A, T3B> selector) where T1 : TBase where T2 : TBase where T3A : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(selector(either.Item3));
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3B, T4, T5, T6, T7, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10> Select4<TBase, T1, T2, T3, T4A, T4B, T5, T6, T7, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4A, T5, T6, T7, T8, T9, T10> either, Func<T4A, T4B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4A : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(selector(either.Item4));
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4B, T5, T6, T7, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10> Select5<TBase, T1, T2, T3, T4, T5A, T5B, T6, T7, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5A, T6, T7, T8, T9, T10> either, Func<T5A, T5B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5A : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(selector(either.Item5));
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4, T5B, T6, T7, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10> Select6<TBase, T1, T2, T3, T4, T5, T6A, T6B, T7, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6A, T7, T8, T9, T10> either, Func<T6A, T6B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6A : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(selector(either.Item6));
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6B, T7, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10> Select7<TBase, T1, T2, T3, T4, T5, T6, T7A, T7B, T8, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7A, T8, T9, T10> either, Func<T7A, T7B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7A : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(selector(either.Item7));
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7B, T8, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10> Select8<TBase, T1, T2, T3, T4, T5, T6, T7, T8A, T8B, T9, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8A, T9, T10> either, Func<T8A, T8B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8A : TBase where T9 : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(selector(either.Item8));
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8B, T9, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10> Select9<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9A, T9B, T10>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9A, T10> either, Func<T9A, T9B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9A : TBase where T10 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(selector(either.Item9));
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9B, T10>(either.Item10);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B> Select10<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A, T10B>(SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10A> either, Func<T10A, T10B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10A : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item5);
}
else if (either.Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item6);
}
else if (either.Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item7);
}
else if (either.Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item8);
}
else if (either.Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(either.Item9);
}
else if (either.Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10B>(selector(either.Item10));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T1 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T2 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T3 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T4 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T5 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T6 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T7 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T8 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T9 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T10 item) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item);
}
public static SubTypesOf<object>.IEither<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B> Select<TBase, T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A, T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(this SubTypesOf<TBase>.IEither<T1A, T2A, T3A, T4A, T5A, T6A, T7A, T8A, T9A, T10A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5, Func<T6A, T6B> selector6, Func<T7A, T7B> selector7, Func<T8A, T8B> selector8, Func<T9A, T9B> selector9, Func<T10A, T10B> selector10) where T1A : TBase where T2A : TBase where T3A : TBase where T4A : TBase where T5A : TBase where T6A : TBase where T7A : TBase where T8A : TBase where T9A : TBase where T10A : TBase {
if (input.Item1 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector1(input.Item1));
}
else if (input.Item2 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector2(input.Item2));
}
else if (input.Item3 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector3(input.Item3));
}
else if (input.Item4 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector4(input.Item4));
}
else if (input.Item5 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector5(input.Item5));
}
else if (input.Item6 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector6(input.Item6));
}
else if (input.Item7 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector7(input.Item7));
}
else if (input.Item8 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector8(input.Item8));
}
else if (input.Item9 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector9(input.Item9));
}
else if (input.Item10 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B, T6B, T7B, T8B, T9B, T10B>(
selector10(input.Item10));
}
else {
throw new InvalidOperationException();
}
}

public static IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> input, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6, Action<T7> action7, Action<T8> action8, Action<T9> action9, Action<T10> action10) {
if (input.Item1 != null) {
action1(input.Item1);
}
else if (input.Item2 != null) {
action2(input.Item2);
}
else if (input.Item3 != null) {
action3(input.Item3);
}
else if (input.Item4 != null) {
action4(input.Item4);
}
else if (input.Item5 != null) {
action5(input.Item5);
}
else if (input.Item6 != null) {
action6(input.Item6);
}
else if (input.Item7 != null) {
action7(input.Item7);
}
else if (input.Item8 != null) {
action8(input.Item8);
}
else if (input.Item9 != null) {
action9(input.Item9);
}
else if (input.Item10 != null) {
action10(input.Item10);
}
else {
throw new InvalidOperationException();
}
return input;
}

public static SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> AsSubTypes<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase {
if (either.Item1 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item1);
}
if (either.Item2 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item2);
}
if (either.Item3 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item3);
}
if (either.Item4 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item4);
}
if (either.Item5 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item5);
}
if (either.Item6 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item6);
}
if (either.Item7 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item7);
}
if (either.Item8 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item8);
}
if (either.Item9 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item9);
}
if (either.Item10 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(either.Item10);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
}
