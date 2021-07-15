using System;

namespace SimpleMonads {
public class EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEquatable<IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
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
public EitherBase(EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
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
public virtual TOutput Collapse<TOutput>(Func<T1, TOutput> selector1, Func<T2, TOutput> selector2, Func<T3, TOutput> selector3, Func<T4, TOutput> selector4, Func<T5, TOutput> selector5, Func<T6, TOutput> selector6, Func<T7, TOutput> selector7, Func<T8, TOutput> selector8, Func<T9, TOutput> selector9) {
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
throw new InvalidOperationException();
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T10>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Item9);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T10, T11>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Item9);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T10, T11, T12>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Item9);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T10, T11, T12, T13>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Item9);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T10, T11, T12, T13, T14>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item3);
}
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item6);
}
if (Item7 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item7);
}
if (Item8 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item8);
}
if (Item9 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Item9);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T10, T11, T12, T13, T14, T15>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T10, T11, T12, T13, T14, T15, T16>()
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
var item1 = Item1;
if (item1 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {item1})";
}
var item2 = Item2;
if (item2 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {item2})";
}
var item3 = Item3;
if (item3 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {item3})";
}
var item4 = Item4;
if (item4 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T4))} Item4: {item4})";
}
var item5 = Item5;
if (item5 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T5))} Item5: {item5})";
}
var item6 = Item6;
if (item6 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T6))} Item6: {item6})";
}
var item7 = Item7;
if (item7 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T7))} Item7: {item7})";
}
var item8 = Item8;
if (item8 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T8))} Item8: {item8})";
}
var item9 = Item9;
if (item9 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>))}({Utility.ConvertToCSharpTypeName(typeof(T9))} Item9: {item9})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1) {
return new(t1);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T2 t2) {
return new(t2);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T3 t3) {
return new(t3);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T4 t4) {
return new(t4);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T5 t5) {
return new(t5);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T6 t6) {
return new(t6);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T7 t7) {
return new(t7);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T8 t8) {
return new(t8);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T9 t9) {
return new(t9);
}
public ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> ConvertTo<TBase>() {
var item1 = Item1;
if (item1 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item1);
}
var item2 = Item2;
if (item2 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item2);
}
var item3 = Item3;
if (item3 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item3);
}
var item4 = Item4;
if (item4 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item4);
}
var item5 = Item5;
if (item5 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item5);
}
var item6 = Item6;
if (item6 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item6);
}
var item7 = Item7;
if (item7 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item7);
}
var item8 = Item8;
if (item8 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item8);
}
var item9 = Item9;
if (item9 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item9);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
public partial class ConvertibleTo<TBase> {
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> : EitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>
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

public Either(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T9 t9) {
return new(t9);
}
public static implicit operator TBase(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> either) {
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
throw new InvalidOperationException($"None of the items in the Either were convertible to {typeof(TBase)}");
}
}
}
}
public partial class SubTypesOf<TBase> {
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase where T7 : TBase where T8 : TBase where T9 : TBase
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

public Either(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
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
throw new ArgumentException($"Expected argument to be either a {typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}, {typeof(T4).Name}, {typeof(T5).Name}, {typeof(T6).Name}, {typeof(T7).Name}, {typeof(T8).Name}, or {typeof(T9).Name} but instead got a type of {typeof(TBase).Name}: {item.GetType().Name}", "name");
}
public virtual TBase Value => Item1 ?? Item2 ?? Item3 ?? Item4 ?? Item5 ?? Item6 ?? Item7 ?? Item8 ?? (TBase)Item9;
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T9 t9) {
return new(t9);
}
}
}
public class Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> : SubTypesOf<object>.Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9>
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

public Either(Either<T1, T2, T3, T4, T5, T6, T7, T8, T9> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
Item7 = other.Item7;
Item8 = other.Item8;
Item9 = other.Item9;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T6 t6) {
return new(t6);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T7 t7) {
return new(t7);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T8 t8) {
return new(t8);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T9 t9) {
return new(t9);
}
}
}
