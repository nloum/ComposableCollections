using System;

namespace SimpleMonads {
internal class CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.ICast<TBase> {
public CastImpl(T1 item) : base(item) { }
public CastImpl(T2 item) : base(item) { }
public CastImpl(T3 item) : base(item) { }
public CastImpl(T4 item) : base(item) { }
public CastImpl(T5 item) : base(item) { }
public CastImpl(T6 item) : base(item) { }
public CastImpl(T7 item) : base(item) { }
public CastImpl(T8 item) : base(item) { }
public CastImpl(T9 item) : base(item) { }
public CastImpl(T10 item) : base(item) { }
public CastImpl(T11 item) : base(item) { }
public CastImpl(T12 item) : base(item) { }
public CastImpl(T13 item) : base(item) { }
public CastImpl(T14 item) : base(item) { }
public CastImpl(T15 item) : base(item) { }
public CastImpl(T16 item) : base(item) { }
public new TBase Value => (TBase)base.Value;
}
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, IEquatable<IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>
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
public Either(T10 item10) {
Item10 = item10.ToMaybe();
}
public Either(T11 item11) {
Item11 = item11.ToMaybe();
}
public Either(T12 item12) {
Item12 = item12.ToMaybe();
}
public Either(T13 item13) {
Item13 = item13.ToMaybe();
}
public Either(T14 item14) {
Item14 = item14.ToMaybe();
}
public Either(T15 item15) {
Item15 = item15.ToMaybe();
}
public Either(T16 item16) {
Item16 = item16.ToMaybe();
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
public IMaybe<T10> Item10 { get; } = Utility.Nothing<T10>();
public IMaybe<T11> Item11 { get; } = Utility.Nothing<T11>();
public IMaybe<T12> Item12 { get; } = Utility.Nothing<T12>();
public IMaybe<T13> Item13 { get; } = Utility.Nothing<T13>();
public IMaybe<T14> Item14 { get; } = Utility.Nothing<T14>();
public IMaybe<T15> Item15 { get; } = Utility.Nothing<T15>();
public IMaybe<T16> Item16 { get; } = Utility.Nothing<T16>();
public object Value => this.Select(x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x, x => (object)x);
public bool Equals(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3) && Equals(Item4, other.Item4) && Equals(Item5, other.Item5) && Equals(Item6, other.Item6) && Equals(Item7, other.Item7) && Equals(Item8, other.Item8) && Equals(Item9, other.Item9) && Equals(Item10, other.Item10) && Equals(Item11, other.Item11) && Equals(Item12, other.Item12) && Equals(Item13, other.Item13) && Equals(Item14, other.Item14) && Equals(Item15, other.Item15) && Equals(Item16, other.Item16);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> other && Equals(other));
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
hash = hash * 23 + Item11.GetHashCode();
hash = hash * 23 + Item12.GetHashCode();
hash = hash * 23 + Item13.GetHashCode();
hash = hash * 23 + Item14.GetHashCode();
hash = hash * 23 + Item15.GetHashCode();
hash = hash * 23 + Item16.GetHashCode();
return hash;
}
}
public override string ToString() {
if (Item1.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {Item1.Value})";
}
if (Item2.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {Item2.Value})";
}
if (Item3.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {Item3.Value})";
}
if (Item4.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T4))} Item4: {Item4.Value})";
}
if (Item5.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T5))} Item5: {Item5.Value})";
}
if (Item6.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T6))} Item6: {Item6.Value})";
}
if (Item7.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T7))} Item7: {Item7.Value})";
}
if (Item8.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T8))} Item8: {Item8.Value})";
}
if (Item9.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T9))} Item9: {Item9.Value})";
}
if (Item10.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T10))} Item10: {Item10.Value})";
}
if (Item11.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T11))} Item11: {Item11.Value})";
}
if (Item12.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T12))} Item12: {Item12.Value})";
}
if (Item13.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T13))} Item13: {Item13.Value})";
}
if (Item14.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T14))} Item14: {Item14.Value})";
}
if (Item15.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T15))} Item15: {Item15.Value})";
}
if (Item16.HasValue) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))}({Utility.ConvertToCSharpTypeName(typeof(T16))} Item16: {Item16.Value})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 t1) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t1);
}
public static implicit operator T1(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item1.Value;
}
public static implicit operator Maybe<T1>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T1>)either.Item1;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T2 t2) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t2);
}
public static implicit operator T2(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item2.Value;
}
public static implicit operator Maybe<T2>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T2>)either.Item2;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T3 t3) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t3);
}
public static implicit operator T3(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item3.Value;
}
public static implicit operator Maybe<T3>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T3>)either.Item3;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T4 t4) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t4);
}
public static implicit operator T4(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item4.Value;
}
public static implicit operator Maybe<T4>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T4>)either.Item4;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T5 t5) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t5);
}
public static implicit operator T5(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item5.Value;
}
public static implicit operator Maybe<T5>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T5>)either.Item5;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T6 t6) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t6);
}
public static implicit operator T6(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item6.Value;
}
public static implicit operator Maybe<T6>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T6>)either.Item6;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T7 t7) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t7);
}
public static implicit operator T7(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item7.Value;
}
public static implicit operator Maybe<T7>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T7>)either.Item7;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T8 t8) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t8);
}
public static implicit operator T8(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item8.Value;
}
public static implicit operator Maybe<T8>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T8>)either.Item8;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T9 t9) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t9);
}
public static implicit operator T9(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item9.Value;
}
public static implicit operator Maybe<T9>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T9>)either.Item9;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T10 t10) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t10);
}
public static implicit operator T10(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item10.Value;
}
public static implicit operator Maybe<T10>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T10>)either.Item10;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T11 t11) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t11);
}
public static implicit operator T11(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item11.Value;
}
public static implicit operator Maybe<T11>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T11>)either.Item11;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T12 t12) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t12);
}
public static implicit operator T12(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item12.Value;
}
public static implicit operator Maybe<T12>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T12>)either.Item12;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T13 t13) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t13);
}
public static implicit operator T13(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item13.Value;
}
public static implicit operator Maybe<T13>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T13>)either.Item13;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T14 t14) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t14);
}
public static implicit operator T14(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item14.Value;
}
public static implicit operator Maybe<T14>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T14>)either.Item14;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T15 t15) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t15);
}
public static implicit operator T15(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item15.Value;
}
public static implicit operator Maybe<T15>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T15>)either.Item15;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T16 t16) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(t16);
}
public static implicit operator T16(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return either.Item16.Value;
}
public static implicit operator Maybe<T16>(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> either) {
return (Maybe<T16>)either.Item16;
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.ICast<TBase> Cast<TBase>() {
if (Item1.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item1.Value);
}
if (Item2.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item2.Value);
}
if (Item3.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item3.Value);
}
if (Item4.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item4.Value);
}
if (Item5.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item5.Value);
}
if (Item6.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item6.Value);
}
if (Item7.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item7.Value);
}
if (Item8.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item8.Value);
}
if (Item9.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item9.Value);
}
if (Item10.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item10.Value);
}
if (Item11.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item11.Value);
}
if (Item12.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item12.Value);
}
if (Item13.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item13.Value);
}
if (Item14.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item14.Value);
}
if (Item15.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item15.Value);
}
if (Item16.HasValue) {
return new CastImpl<TBase, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item16.Value);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
}
