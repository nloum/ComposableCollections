using System;

namespace SimpleMonads {
public static class Either2Extensions
{
public static IEither<T1B, T2> Select1<T1A, T1B, T2>(IEither<T1A, T2> either, Func<T1A, T1B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1B, T2>(selector(either.Item1.Value));
}
else if (either.Item2.HasValue) {
return new Either<T1B, T2>(either.Item2.Value);
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1, T2B> Select2<T1, T2A, T2B>(IEither<T1, T2A> either, Func<T2A, T2B> selector)
{
if (either.Item1.HasValue) {
return new Either<T1, T2B>(either.Item1.Value);
}
else if (either.Item2.HasValue) {
return new Either<T1, T2B>(selector(either.Item2.Value));
}
else {
throw new InvalidOperationException();
}
}
public static IEither<T1B, T2B> Select<T1A, T2A, T1B, T2B>(this IEither<T1A, T2A> input, Func<T1A, T1B> selector1, Func<T2A, T2B> selector2) {
if (input.Item1.HasValue) {
return new Either<T1B, T2B>(
selector1(input.Item1.Value));
}
else if (input.Item2.HasValue) {
return new Either<T1B, T2B>(
selector2(input.Item2.Value));
}
else {
throw new InvalidOperationException();
}
}

}
}
