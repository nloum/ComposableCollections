using System;

namespace SimpleMonads {
internal class CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9> : Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>.ICast<TBase> {
public CastImpl(T1 item) : base(item) { }
public CastImpl(T2 item) : base(item) { }
public CastImpl(T3 item) : base(item) { }
public CastImpl(T4 item) : base(item) { }
public CastImpl(T5 item) : base(item) { }
public CastImpl(T6 item) : base(item) { }
public CastImpl(T7 item) : base(item) { }
public CastImpl(T8 item) : base(item) { }
public CastImpl(T9 item) : base(item) { }
public new TBase Value => (TBase)base.Value;
}
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEquatable<IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
{
public Either(T1 item1) {
Item1 = item1.ToMaybe();
}
public Either(T2 item2) {
Item2 = item2.ToMaybe();
}
public Either(T3 item3) {
Item3 = item3.ToMaybe();
}
public Either(T4 item4) {
Item4 = item4.ToMaybe();
}
public Either(T5 item5) {
Item5 = item5.ToMaybe();
}
public Either(T6 item6) {
Item6 = item6.ToMaybe();
}
public Either(T7 item7) {
Item7 = item7.ToMaybe();
}
public Either(T8 item8) {
Item8 = item8.ToMaybe();
}
public Either(T9 item9) {
Item9 = item9.ToMaybe();
}
public IMaybe<T1> Item1 { get; } = Utility.Nothing<T1>();
public IMaybe<T2> Item2 { get; } = Utility.Nothing<T2>();
public IMaybe<T3> Item3 { get; } = Utility.Nothing<T3>();
public IMaybe<T4> Item4 { get; } = Utility.Nothing<T4>();
public IMaybe<T5> Item5 { get; } = Utility.Nothing<T5>();
public IMaybe<T6> Item6 { get; } = Utility.Nothing<T6>();
public IMaybe<T7> Item7 { get; } = Utility.Nothing<T7>();
public IMaybe<T8> Item8 { get; } = Utility.Nothing<T8>();
public IMaybe<T9> Item9 { get; } = Utility.Nothing<T9>();
public object Value => this.Select(x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x);
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T10>()
{
if (Item1.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item1.Value);
}
if (Item2.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item2.Value);
}
if (Item3.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item3.Value);
}
if (Item4.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item4.Value);
}
if (Item5.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item5.Value);
}
if (Item6.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item6.Value);
}
if (Item7.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item7.Value);
}
if (Item8.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item8.Value);
}
if (Item9.HasValue) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item9.Value);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T10, T11>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T10, T11, T12>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T10, T11, T12, T13>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T10, T11, T12, T13, T14>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T10, T11, T12, T13, T14, T15>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T10, T11, T12, T13, T14, T15, T16>()
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
throw new System.InvalidOperationException("The either has no values");
}
public bool Equals(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3) && Equals(Item4, other.Item4) && Equals(Item5, other.Item5) && Equals(Item6, other.Item6) && Equals(Item7, other.Item7) && Equals(Item8, other.Item8) && Equals(Item9, other.Item9);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> other && Equals(other));
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
return hash;
}
}
public override string ToString() {
if (Item1.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {Item1.Value})";
}
if (Item2.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {Item2.Value})";
}
if (Item3.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {Item3.Value})";
}
if (Item4.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T4))} Item4: {Item4.Value})";
}
if (Item5.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T5))} Item5: {Item5.Value})";
}
if (Item6.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T6))} Item6: {Item6.Value})";
}
if (Item7.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T7))} Item7: {Item7.Value})";
}
if (Item8.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T8))} Item8: {Item8.Value})";
}
if (Item9.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T9))} Item9: {Item9.Value})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t1);
}
public static implicit operator T1(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item1.Value;
}
public static implicit operator Maybe<T1>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T1>)either.Item1;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T2 t2) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t2);
}
public static implicit operator T2(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item2.Value;
}
public static implicit operator Maybe<T2>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T2>)either.Item2;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T3 t3) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t3);
}
public static implicit operator T3(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item3.Value;
}
public static implicit operator Maybe<T3>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T3>)either.Item3;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T4 t4) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t4);
}
public static implicit operator T4(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item4.Value;
}
public static implicit operator Maybe<T4>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T4>)either.Item4;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T5 t5) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t5);
}
public static implicit operator T5(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item5.Value;
}
public static implicit operator Maybe<T5>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T5>)either.Item5;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T6 t6) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t6);
}
public static implicit operator T6(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item6.Value;
}
public static implicit operator Maybe<T6>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T6>)either.Item6;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T7 t7) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t7);
}
public static implicit operator T7(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item7.Value;
}
public static implicit operator Maybe<T7>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T7>)either.Item7;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T8 t8) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t8);
}
public static implicit operator T8(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item8.Value;
}
public static implicit operator Maybe<T8>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T8>)either.Item8;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T9 t9) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(t9);
}
public static implicit operator T9(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return either.Item9.Value;
}
public static implicit operator Maybe<T9>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
return (Maybe<T9>)either.Item9;
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>.ICast<TBase> Cast<TBase>() {
if (Item1.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item1.Value);
}
if (Item2.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item2.Value);
}
if (Item3.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item3.Value);
}
if (Item4.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item4.Value);
}
if (Item5.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item5.Value);
}
if (Item6.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item6.Value);
}
if (Item7.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item7.Value);
}
if (Item8.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item8.Value);
}
if (Item9.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item9.Value);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
}
