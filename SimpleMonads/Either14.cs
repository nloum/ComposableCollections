using System;

namespace SimpleMonads {
public class EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, IEquatable<IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>
{
protected EitherBase() { }
public EitherBase(T1 item1) {
Item1 = item1;
}
public EitherBase(T2 item2) {
Item2 = item2;
}
public EitherBase(T3 item3) {
Item3 = item3;
}
public EitherBase(T4 item4) {
Item4 = item4;
}
public EitherBase(T5 item5) {
Item5 = item5;
}
public EitherBase(T6 item6) {
Item6 = item6;
}
public EitherBase(T7 item7) {
Item7 = item7;
}
public EitherBase(T8 item8) {
Item8 = item8;
}
public EitherBase(T9 item9) {
Item9 = item9;
}
public EitherBase(T10 item10) {
Item10 = item10;
}
public EitherBase(T11 item11) {
Item11 = item11;
}
public EitherBase(T12 item12) {
Item12 = item12;
}
public EitherBase(T13 item13) {
Item13 = item13;
}
public EitherBase(T14 item14) {
Item14 = item14;
}
public EitherBase(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
Item10 = other.Item10;
Item11 = other.Item11;
Item12 = other.Item12;
Item13 = other.Item13;
Item14 = other.Item14;
}
public virtual T1? Item1 { get; init; } = default;
public virtual T2? Item2 { get; init; } = default;
public virtual T3? Item3 { get; init; } = default;
public virtual T4? Item4 { get; init; } = default;
public virtual T5? Item5 { get; init; } = default;
public virtual T6? Item6 { get; init; } = default;
public virtual T7? Item7 { get; init; } = default;
public virtual T8? Item8 { get; init; } = default;
public virtual T9? Item9 { get; init; } = default;
public virtual T10? Item10 { get; init; } = default;
public virtual T11? Item11 { get; init; } = default;
public virtual T12? Item12 { get; init; } = default;
public virtual T13? Item13 { get; init; } = default;
public virtual T14? Item14 { get; init; } = default;
public virtual TOutput Collapse<TOutput>(Func<T1, TOutput> selector1, Func<T2, TOutput> selector2, Func<T3, TOutput> selector3, Func<T4, TOutput> selector4, Func<T5, TOutput> selector5, Func<T6, TOutput> selector6, Func<T7, TOutput> selector7, Func<T8, TOutput> selector8, Func<T9, TOutput> selector9, Func<T10, TOutput> selector10, Func<T11, TOutput> selector11, Func<T12, TOutput> selector12, Func<T13, TOutput> selector13, Func<T14, TOutput> selector14) {
var item1 = Item1;
if (item1 != null) return selector1(item1);
var item2 = Item2;
if (item2 != null) return selector2(item2);
var item3 = Item3;
if (item3 != null) return selector3(item3);
var item4 = Item4;
if (item4 != null) return selector4(item4);
var item5 = Item5;
if (item5 != null) return selector5(item5);
var item6 = Item6;
if (item6 != null) return selector6(item6);
var item7 = Item7;
if (item7 != null) return selector7(item7);
var item8 = Item8;
if (item8 != null) return selector8(item8);
var item9 = Item9;
if (item9 != null) return selector9(item9);
var item10 = Item10;
if (item10 != null) return selector10(item10);
var item11 = Item11;
if (item11 != null) return selector11(item11);
var item12 = Item12;
if (item12 != null) return selector12(item12);
var item13 = Item13;
if (item13 != null) return selector13(item13);
var item14 = Item14;
if (item14 != null) return selector14(item14);
throw new InvalidOperationException();
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T15>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item9);
}
if (Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item10);
}
if (Item11 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item11);
}
if (Item12 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item12);
}
if (Item13 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item13);
}
if (Item14 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Item14);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T15, T16>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item9);
}
if (Item10 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item10);
}
if (Item11 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item11);
}
if (Item12 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item12);
}
if (Item13 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item13);
}
if (Item14 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Item14);
}
throw new System.InvalidOperationException("The either has no values");
}
public bool Equals(IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3) && Equals(Item4, other.Item4) && Equals(Item5, other.Item5) && Equals(Item6, other.Item6) && Equals(Item7, other.Item7) && Equals(Item8, other.Item8) && Equals(Item9, other.Item9) && Equals(Item10, other.Item10) && Equals(Item11, other.Item11) && Equals(Item12, other.Item12) && Equals(Item13, other.Item13) && Equals(Item14, other.Item14);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other && Equals(other));
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
return hash;
}
}
public override string ToString() {
var item1 = Item1;
if (item1 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {item1})";
}
var item2 = Item2;
if (item2 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {item2})";
}
var item3 = Item3;
if (item3 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {item3})";
}
var item4 = Item4;
if (item4 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T4))} Item4: {item4})";
}
var item5 = Item5;
if (item5 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T5))} Item5: {item5})";
}
var item6 = Item6;
if (item6 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T6))} Item6: {item6})";
}
var item7 = Item7;
if (item7 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T7))} Item7: {item7})";
}
var item8 = Item8;
if (item8 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T8))} Item8: {item8})";
}
var item9 = Item9;
if (item9 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T9))} Item9: {item9})";
}
var item10 = Item10;
if (item10 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T10))} Item10: {item10})";
}
var item11 = Item11;
if (item11 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T11))} Item11: {item11})";
}
var item12 = Item12;
if (item12 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T12))} Item12: {item12})";
}
var item13 = Item13;
if (item13 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T13))} Item13: {item13})";
}
var item14 = Item14;
if (item14 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))}({Utility.ConvertToCSharpTypeName(typeof(T14))} Item14: {item14})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 t1) {
return new(t1);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T2 t2) {
return new(t2);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T3 t3) {
return new(t3);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T4 t4) {
return new(t4);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T5 t5) {
return new(t5);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T6 t6) {
return new(t6);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T7 t7) {
return new(t7);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T8 t8) {
return new(t8);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T9 t9) {
return new(t9);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T10 t10) {
return new(t10);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T11 t11) {
return new(t11);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T12 t12) {
return new(t12);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T13 t13) {
return new(t13);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T14 t14) {
return new(t14);
}
public ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> ConvertTo<TBase>() {
var item1 = Item1;
if (item1 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item1);
}
var item2 = Item2;
if (item2 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item2);
}
var item3 = Item3;
if (item3 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item3);
}
var item4 = Item4;
if (item4 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item4);
}
var item5 = Item5;
if (item5 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item5);
}
var item6 = Item6;
if (item6 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item6);
}
var item7 = Item7;
if (item7 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item7);
}
var item8 = Item8;
if (item8 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item8);
}
var item9 = Item9;
if (item9 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item9);
}
var item10 = Item10;
if (item10 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item10);
}
var item11 = Item11;
if (item11 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item11);
}
var item12 = Item12;
if (item12 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item12);
}
var item13 = Item13;
if (item13 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item13);
}
var item14 = Item14;
if (item14 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item14);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
public partial class ConvertibleTo<TBase> {
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
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

public Either(T11 item11) : base(item11) { }

public Either(T12 item12) : base(item12) { }

public Either(T13 item13) : base(item13) { }

public Either(T14 item14) : base(item14) { }

public Either(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
Item10 = other.Item10;
Item11 = other.Item11;
Item12 = other.Item12;
Item13 = other.Item13;
Item14 = other.Item14;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T9 t9) {
return new(t9);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T10 t10) {
return new(t10);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T11 t11) {
return new(t11);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T12 t12) {
return new(t12);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T13 t13) {
return new(t13);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T14 t14) {
return new(t14);
}
public static implicit operator TBase(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> either) {
return either;
}
protected TBase Convert1(T1 item1) {
if (item1 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T1).Name} to {typeof(TBase).Name}");
}
protected TBase Convert2(T2 item2) {
if (item2 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T2).Name} to {typeof(TBase).Name}");
}
protected TBase Convert3(T3 item3) {
if (item3 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T3).Name} to {typeof(TBase).Name}");
}
protected TBase Convert4(T4 item4) {
if (item4 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T4).Name} to {typeof(TBase).Name}");
}
protected TBase Convert5(T5 item5) {
if (item5 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T5).Name} to {typeof(TBase).Name}");
}
protected TBase Convert6(T6 item6) {
if (item6 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T6).Name} to {typeof(TBase).Name}");
}
protected TBase Convert7(T7 item7) {
if (item7 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T7).Name} to {typeof(TBase).Name}");
}
protected TBase Convert8(T8 item8) {
if (item8 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T8).Name} to {typeof(TBase).Name}");
}
protected TBase Convert9(T9 item9) {
if (item9 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T9).Name} to {typeof(TBase).Name}");
}
protected TBase Convert10(T10 item10) {
if (item10 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T10).Name} to {typeof(TBase).Name}");
}
protected TBase Convert11(T11 item11) {
if (item11 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T11).Name} to {typeof(TBase).Name}");
}
protected TBase Convert12(T12 item12) {
if (item12 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T12).Name} to {typeof(TBase).Name}");
}
protected TBase Convert13(T13 item13) {
if (item13 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T13).Name} to {typeof(TBase).Name}");
}
protected TBase Convert14(T14 item14) {
if (item14 is TBase @base) {
return @base;
}
throw new NotImplementedException($"Cannot convert from {typeof(T14).Name} to {typeof(TBase).Name}");
}
public virtual TBase Value {
get {

var item1 = Item1;
if (item1 != null) return Convert1(item1);
var item2 = Item2;
if (item2 != null) return Convert2(item2);
var item3 = Item3;
if (item3 != null) return Convert3(item3);
var item4 = Item4;
if (item4 != null) return Convert4(item4);
var item5 = Item5;
if (item5 != null) return Convert5(item5);
var item6 = Item6;
if (item6 != null) return Convert6(item6);
var item7 = Item7;
if (item7 != null) return Convert7(item7);
var item8 = Item8;
if (item8 != null) return Convert8(item8);
var item9 = Item9;
if (item9 != null) return Convert9(item9);
var item10 = Item10;
if (item10 != null) return Convert10(item10);
var item11 = Item11;
if (item11 != null) return Convert11(item11);
var item12 = Item12;
if (item12 != null) return Convert12(item12);
var item13 = Item13;
if (item13 != null) return Convert13(item13);
var item14 = Item14;
if (item14 != null) return Convert14(item14);
throw new InvalidOperationException($"None of the items in the Either were convertible to {typeof(TBase)}");
}
}
}
}
public partial class SubTypesOf<TBase> {
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase where T10 : TBase where T11 : TBase where T12 : TBase where T13 : TBase where T14 : TBase
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

public Either(T11 item11) : base(item11) { }

public Either(T12 item12) : base(item12) { }

public Either(T13 item13) : base(item13) { }

public Either(T14 item14) : base(item14) { }

public Either(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
Item10 = other.Item10;
Item11 = other.Item11;
Item12 = other.Item12;
Item13 = other.Item13;
Item14 = other.Item14;
}
public Either(TBase item) {
if (item == null) throw new ArgumentNullException("item");
if (item is T1 item1) {
Item1 = item1;
return;
}
if (item is T2 item2) {
Item2 = item2;
return;
}
if (item is T3 item3) {
Item3 = item3;
return;
}
if (item is T4 item4) {
Item4 = item4;
return;
}
if (item is T5 item5) {
Item5 = item5;
return;
}
if (item is T6 item6) {
Item6 = item6;
return;
}
if (item is T7 item7) {
Item7 = item7;
return;
}
if (item is T8 item8) {
Item8 = item8;
return;
}
if (item is T9 item9) {
Item9 = item9;
return;
}
if (item is T10 item10) {
Item10 = item10;
return;
}
if (item is T11 item11) {
Item11 = item11;
return;
}
if (item is T12 item12) {
Item12 = item12;
return;
}
if (item is T13 item13) {
Item13 = item13;
return;
}
if (item is T14 item14) {
Item14 = item14;
return;
}
throw new ArgumentException($"Expected argument to be either a {typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}, {typeof(T4).Name}, {typeof(T5).Name}, {typeof(T6).Name}, {typeof(T7).Name}, {typeof(T8).Name}, {typeof(T9).Name}, {typeof(T10).Name}, {typeof(T11).Name}, {typeof(T12).Name}, {typeof(T13).Name}, or {typeof(T14).Name} but instead got a type of {typeof(TBase).Name}: {item.GetType().Name}", "name");
}
public virtual TBase Value => Item1 ?? Item2 ?? Item3 ?? Item4 ?? Item5 ?? Item6 ?? Item7 ?? Item8 ?? Item9 ?? Item10 ?? Item11 ?? Item12 ?? Item13 ?? (TBase)Item14;
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T9 t9) {
return new(t9);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T10 t10) {
return new(t10);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T11 t11) {
return new(t11);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T12 t12) {
return new(t12);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T13 t13) {
return new(t13);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T14 t14) {
return new(t14);
}
}
}
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : SubTypesOf<object>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
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

public Either(T11 item11) : base(item11) { }

public Either(T12 item12) : base(item12) { }

public Either(T13 item13) : base(item13) { }

public Either(T14 item14) : base(item14) { }

public Either(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
Item10 = other.Item10;
Item11 = other.Item11;
Item12 = other.Item12;
Item13 = other.Item13;
Item14 = other.Item14;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T9 t9) {
return new(t9);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T10 t10) {
return new(t10);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T11 t11) {
return new(t11);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T12 t12) {
return new(t12);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T13 t13) {
return new(t13);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T14 t14) {
return new(t14);
}
}
}
