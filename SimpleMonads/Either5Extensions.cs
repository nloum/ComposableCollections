using System;

namespace SimpleMonads {
public static class Either5Extensions
{
public static SubTypesOf<object>.Either<T1B, T2, T3, T4, T5> Select1<TBase, T1A, T1B, T2, T3, T4, T5>(this SubTypesOf<TBase>.IEither<T1A, T2, T3, T4, T5> either, Func<T1A, T1B> selector) where T1A : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase
{
if (either.Item1 != null) {
return new Either<T1B, T2, T3, T4, T5>(selector(either.Item1));
}
else if (either.Item2 != null) {
return new Either<T1B, T2, T3, T4, T5>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1B, T2, T3, T4, T5>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1B, T2, T3, T4, T5>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1B, T2, T3, T4, T5>(either.Item5);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.Either<T1, T2B, T3, T4, T5> Select2<TBase, T1, T2A, T2B, T3, T4, T5>(this SubTypesOf<TBase>.IEither<T1, T2A, T3, T4, T5> either, Func<T2A, T2B> selector) where T1 : TBase where T2A : TBase where T3 : TBase where T4 : TBase where T5 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2B, T3, T4, T5>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2B, T3, T4, T5>(selector(either.Item2));
}
else if (either.Item3 != null) {
return new Either<T1, T2B, T3, T4, T5>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2B, T3, T4, T5>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2B, T3, T4, T5>(either.Item5);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.Either<T1, T2, T3B, T4, T5> Select3<TBase, T1, T2, T3A, T3B, T4, T5>(this SubTypesOf<TBase>.IEither<T1, T2, T3A, T4, T5> either, Func<T3A, T3B> selector) where T1 : TBase where T2 : TBase where T3A : TBase where T4 : TBase where T5 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3B, T4, T5>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3B, T4, T5>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3B, T4, T5>(selector(either.Item3));
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3B, T4, T5>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3B, T4, T5>(either.Item5);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.Either<T1, T2, T3, T4B, T5> Select4<TBase, T1, T2, T3, T4A, T4B, T5>(this SubTypesOf<TBase>.IEither<T1, T2, T3, T4A, T5> either, Func<T4A, T4B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4A : TBase where T5 : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4B, T5>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4B, T5>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4B, T5>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4B, T5>(selector(either.Item4));
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4B, T5>(either.Item5);
}
else {
throw new InvalidOperationException();
}
}
public static SubTypesOf<object>.Either<T1, T2, T3, T4, T5B> Select5<TBase, T1, T2, T3, T4, T5A, T5B>(this SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5A> either, Func<T5A, T5B> selector) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5A : TBase
{
if (either.Item1 != null) {
return new Either<T1, T2, T3, T4, T5B>(either.Item1);
}
else if (either.Item2 != null) {
return new Either<T1, T2, T3, T4, T5B>(either.Item2);
}
else if (either.Item3 != null) {
return new Either<T1, T2, T3, T4, T5B>(either.Item3);
}
else if (either.Item4 != null) {
return new Either<T1, T2, T3, T4, T5B>(either.Item4);
}
else if (either.Item5 != null) {
return new Either<T1, T2, T3, T4, T5B>(selector(either.Item5));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2, T3, T4, T5> Either<T1, T2, T3, T4, T5>(this T1 item) {
return new Either<T1, T2, T3, T4, T5>(item);
}
public static IEither<T1, T2, T3, T4, T5> Either<T1, T2, T3, T4, T5>(this T2 item) {
return new Either<T1, T2, T3, T4, T5>(item);
}
public static IEither<T1, T2, T3, T4, T5> Either<T1, T2, T3, T4, T5>(this T3 item) {
return new Either<T1, T2, T3, T4, T5>(item);
}
public static IEither<T1, T2, T3, T4, T5> Either<T1, T2, T3, T4, T5>(this T4 item) {
return new Either<T1, T2, T3, T4, T5>(item);
}
public static IEither<T1, T2, T3, T4, T5> Either<T1, T2, T3, T4, T5>(this T5 item) {
return new Either<T1, T2, T3, T4, T5>(item);
}
public static SubTypesOf<object>.IEither<T1B, T2B, T3B, T4B, T5B> Select<TBase, T1A, T2A, T3A, T4A, T5A, T1B, T2B, T3B, T4B, T5B>(this SubTypesOf<TBase>.IEither<T1A, T2A, T3A, T4A, T5A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2, Func<T3A, T3B> selector3, Func<T4A, T4B> selector4, Func<T5A, T5B> selector5) where T1A : TBase where T2A : TBase where T3A : TBase where T4A : TBase where T5A : TBase {
if (input.Item1 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector1(input.Item1));
}
else if (input.Item2 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector2(input.Item2));
}
else if (input.Item3 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector3(input.Item3));
}
else if (input.Item4 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector4(input.Item4));
}
else if (input.Item5 != null) {
return new Either<T1B, T2B, T3B, T4B, T5B>(
selector5(input.Item5));
}
else {
throw new InvalidOperationException();
}
}

public static IEitherBase<T1, T2, T3, T4, T5> ForEach<T1, T2, T3, T4, T5>(this IEitherBase<T1, T2, T3, T4, T5> input, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5) {
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
else {
throw new InvalidOperationException();
}
return input;
}

public static SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5> AsSubTypes<TBase, T1, T2, T3, T4, T5>(this ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5> either) where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase {
var item1 = either.Item1;
if (item1 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(item1);
}
var item2 = either.Item2;
if (item2 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(item2);
}
var item3 = either.Item3;
if (item3 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(item3);
}
var item4 = either.Item4;
if (item4 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(item4);
}
var item5 = either.Item5;
if (item5 != null) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(item5);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
}
