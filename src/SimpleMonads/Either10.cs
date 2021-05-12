using System;

namespace SimpleMonads {
public class EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, IEquatable<IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>
{
protected EitherBase() { }
public EitherBase(T1 item1) {
Item1 = item1.ToMaybe();
}
public EitherBase(T2 item2) {
Item2 = item2.ToMaybe();
}
public EitherBase(T3 item3) {
Item3 = item3.ToMaybe();
}
public EitherBase(T4 item4) {
Item4 = item4.ToMaybe();
}
public EitherBase(T5 item5) {
Item5 = item5.ToMaybe();
}
public EitherBase(T6 item6) {
Item6 = item6.ToMaybe();
}
public EitherBase(T7 item7) {
Item7 = item7.ToMaybe();
}
public EitherBase(T8 item8) {
Item8 = item8.ToMaybe();
}
public EitherBase(T9 item9) {
Item9 = item9.ToMaybe();
}
public EitherBase(T10 item10) {
Item10 = item10.ToMaybe();
}
public IMaybe<T1> Item1 { get; init; } = Utility.Nothing<T1>();
public IMaybe<T2> Item2 { get; init; } = Utility.Nothing<T2>();
public IMaybe<T3> Item3 { get; init; } = Utility.Nothing<T3>();
public IMaybe<T4> Item4 { get; init; } = Utility.Nothing<T4>();
public IMaybe<T5> Item5 { get; init; } = Utility.Nothing<T5>();
public IMaybe<T6> Item6 { get; init; } = Utility.Nothing<T6>();
public IMaybe<T7> Item7 { get; init; } = Utility.Nothing<T7>();
public IMaybe<T8> Item8 { get; init; } = Utility.Nothing<T8>();
public IMaybe<T9> Item9 { get; init; } = Utility.Nothing<T9>();
public IMaybe<T10> Item10 { get; init; } = Utility.Nothing<T10>();
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T11>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item9.Value);
}
if (Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item10.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T11, T12>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item9.Value);
}
if (Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item10.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T11, T12, T13>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item9.Value);
}
if (Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item10.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T11, T12, T13, T14>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item9.Value);
}
if (Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item10.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T11, T12, T13, T14, T15>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item9.Value);
}
if (Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item10.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T11, T12, T13, T14, T15, T16>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item9.Value);
}
if (Item10.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item10.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public bool Equals(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3) && Equals(Item4, other.Item4) && Equals(Item5, other.Item5) && Equals(Item6, other.Item6) && Equals(Item7, other.Item7) && Equals(Item8, other.Item8) && Equals(Item9, other.Item9) && Equals(Item10, other.Item10);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> other && Equals(other));
}

public override int GetHashCode() {
unchecked {
int hash = 17;
hash = hash * 23 + Item1.GetHashCode();
hash = hash * 23 + Item2.GetHashCode();
hash = hash * 23 + Item3.GetHashCode();
hash = hash * 23 + Item4.GetHashCode();
hash = hash * 23 + Item5.GetHashCode();
hash = hash * 23 + Item6.GetHashCode();
hash = hash * 23 + Item7.GetHashCode();
hash = hash * 23 + Item8.GetHashCode();
hash = hash * 23 + Item9.GetHashCode();
hash = hash * 23 + Item10.GetHashCode();
return hash;
}
}
public override string ToString() {
if (Item1.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {Item1.Value})";
}
if (Item2.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {Item2.Value})";
}
if (Item3.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {Item3.Value})";
}
if (Item4.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T4))} Item4: {Item4.Value})";
}
if (Item5.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T5))} Item5: {Item5.Value})";
}
if (Item6.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T6))} Item6: {Item6.Value})";
}
if (Item7.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T7))} Item7: {Item7.Value})";
}
if (Item8.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T8))} Item8: {Item8.Value})";
}
if (Item9.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T9))} Item9: {Item9.Value})";
}
if (Item10.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))}({Utility.ConvertToCSharpTypeName(typeof(T10))} Item10: {Item10.Value})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 t1) {
return new(t1);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T2 t2) {
return new(t2);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T3 t3) {
return new(t3);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T4 t4) {
return new(t4);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T5 t5) {
return new(t5);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T6 t6) {
return new(t6);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T7 t7) {
return new(t7);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T8 t8) {
return new(t8);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T9 t9) {
return new(t9);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T10 t10) {
return new(t10);
}
public static implicit operator T1(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item1.Value;
}
public static implicit operator T2(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item2.Value;
}
public static implicit operator T3(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item3.Value;
}
public static implicit operator T4(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item4.Value;
}
public static implicit operator T5(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item5.Value;
}
public static implicit operator T6(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item6.Value;
}
public static implicit operator T7(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item7.Value;
}
public static implicit operator T8(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item8.Value;
}
public static implicit operator T9(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item9.Value;
}
public static implicit operator T10(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Item10.Value;
}
public static implicit operator Maybe<T1>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T1>)either.Item1;
}
public static implicit operator Maybe<T2>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T2>)either.Item2;
}
public static implicit operator Maybe<T3>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T3>)either.Item3;
}
public static implicit operator Maybe<T4>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T4>)either.Item4;
}
public static implicit operator Maybe<T5>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T5>)either.Item5;
}
public static implicit operator Maybe<T6>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T6>)either.Item6;
}
public static implicit operator Maybe<T7>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T7>)either.Item7;
}
public static implicit operator Maybe<T8>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T8>)either.Item8;
}
public static implicit operator Maybe<T9>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T9>)either.Item9;
}
public static implicit operator Maybe<T10>(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return (Maybe<T10>)either.Item10;
}
public Cast<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Cast<TBase>() {
if (Item1.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item1.Value);
}
if (Item2.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item2.Value);
}
if (Item3.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item3.Value);
}
if (Item4.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item4.Value);
}
if (Item5.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item5.Value);
}
if (Item6.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item6.Value);
}
if (Item7.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item7.Value);
}
if (Item8.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item8.Value);
}
if (Item9.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item9.Value);
}
if (Item10.HasValue) {
return new Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item10.Value);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
public partial class Cast<TBase> {
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(T4 item4) : base(item4) { }

public Either(T5 item5) : base(item5) { }

public Either(T6 item6) : base(item6) { }

public Either(T7 item7) : base(item7) { }

public Either(T8 item8) : base(item8) { }

public Either(T9 item9) : base(item9) { }

public Either(T10 item10) : base(item10) { }

public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T9 t9) {
return new(t9);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T10 t10) {
return new(t10);
}
public static implicit operator TBase(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> either) {
return either.Value;
}
public virtual TBase Value => Item1.Cast<TBase>().Otherwise(Item2.Cast<TBase>().Otherwise(Item3.Cast<TBase>().Otherwise(Item4.Cast<TBase>().Otherwise(Item5.Cast<TBase>().Otherwise(Item6.Cast<TBase>().Otherwise(Item7.Cast<TBase>().Otherwise(Item8.Cast<TBase>().Otherwise(Item9.Cast<TBase>().Otherwise(() => (TBase)Item10.Cast<TBase>().Value)))))))));
}
}
public partial class SubTypesOf<TBase> {
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : Cast<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(T4 item4) : base(item4) { }

public Either(T5 item5) : base(item5) { }

public Either(T6 item6) : base(item6) { }

public Either(T7 item7) : base(item7) { }

public Either(T8 item8) : base(item8) { }

public Either(T9 item9) : base(item9) { }

public Either(T10 item10) : base(item10) { }

public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T9 t9) {
return new(t9);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T10 t10) {
return new(t10);
}
}
}
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : SubTypesOf<object>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(T4 item4) : base(item4) { }

public Either(T5 item5) : base(item5) { }

public Either(T6 item6) : base(item6) { }

public Either(T7 item7) : base(item7) { }

public Either(T8 item8) : base(item8) { }

public Either(T9 item9) : base(item9) { }

public Either(T10 item10) : base(item10) { }

public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T9 t9) {
return new(t9);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T10 t10) {
return new(t10);
}
}
}
