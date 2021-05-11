using System;

namespace SimpleMonads {
public partial class SubTypesOf<TBase> {
public class Either<T1, T2> : IEither<T1, T2>, IEquatable<IEither<T1, T2>> where T1 : TBase where T2 : TBase
{
public Either(T1 item1) {
Item1 = item1.ToMaybe();
}
public Either(T2 item2) {
Item2 = item2.ToMaybe();
}
public IMaybe<T1> Item1 { get; } = Utility.Nothing<T1>();
public IMaybe<T2> Item2 { get; } = Utility.Nothing<T2>();
public TBase Value => Item1.Cast<TBase>().Otherwise(() => Item2.Value);
public SubTypesOf<TBase>.IEither<T1, T2, T3> Or<T3>() where T3 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4> Or<T3, T4>() where T3 : TBase where T4 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5> Or<T3, T4, T5>() where T3 : TBase where T4 : TBase where T5 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6> Or<T3, T4, T5, T6>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase where T15 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public SubTypesOf<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>() where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase where T15 : TBase where T16 : TBase
{
if (Item1.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item1.Value);
}
if (Item2.HasValue) {
return new SubTypesOf<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item2.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public bool Equals(SubTypesOf<TBase>.IEither<T1, T2> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2> other && Equals(other));
}

public override int GetHashCode() {
unchecked {
int hash = 17;
hash = hash * 23 + Item1.GetHashCode();
hash = hash * 23 + Item2.GetHashCode();
return hash;
}
}
public override string ToString() {
if (Item1.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {Item1.Value})";
}
if (Item2.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {Item2.Value})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator SubTypesOf<TBase>.Either<T1, T2>(T1 t1) {
return new Either<T1, T2>(t1);
}
public static implicit operator T1(SubTypesOf<TBase>.Either<T1, T2> either) {
return either.Item1.Value;
}
public static implicit operator Maybe<T1>(SubTypesOf<TBase>.Either<T1, T2> either) {
return (Maybe<T1>)either.Item1;
}
public static implicit operator SubTypesOf<TBase>.Either<T1, T2>(T2 t2) {
return new Either<T1, T2>(t2);
}
public static implicit operator T2(SubTypesOf<TBase>.Either<T1, T2> either) {
return either.Item2.Value;
}
public static implicit operator Maybe<T2>(SubTypesOf<TBase>.Either<T1, T2> either) {
return (Maybe<T2>)either.Item2;
}
}
}
public class Either<T1, T2> : SubTypesOf<object>.Either<T1, T2>, IEither<T1, T2>
{
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

}
}
