using System;

namespace SimpleMonads {
public class EitherBase<T1, T2, T3> : IEitherBase<T1, T2, T3>, IEquatable<IEither<T1, T2, T3>>
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
public EitherBase(EitherBase<T1, T2, T3> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
}
public virtual T1? Item1 { get; init; } = default;
public virtual T2? Item2 { get; init; } = default;
public virtual T3? Item3 { get; init; } = default;
public virtual TOutput Collapse<TOutput>(Func<T1, TOutput> selector1, Func<T2, TOutput> selector2, Func<T3, TOutput> selector3) {
if (Item1 != null) return selector1(Item1);
if (Item2 != null) return selector2(Item2);
if (Item3 != null) return selector3(Item3);
throw new InvalidOperationException();
}
public IEither<T1, T2, T3, T4> Or<T4>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4>(Item3);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5> Or<T4, T5>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5>(Item3);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6> Or<T4, T5, T6>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6>(Item3);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7> Or<T4, T5, T6, T7>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7>(Item3);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8> Or<T4, T5, T6, T7, T8>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item3);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T4, T5, T6, T7, T8, T9>()
{
if (Item1 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item1);
}
if (Item2 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item2);
}
if (Item3 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item3);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T4, T5, T6, T7, T8, T9, T10>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T4, T5, T6, T7, T8, T9, T10, T11>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
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
throw new System.InvalidOperationException("The either has no values");
}
public bool Equals(IEither<T1, T2, T3> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2, T3> other && Equals(other));
}

public override int GetHashCode() {
unchecked {
int hash = 17;
hash = hash * 23 + Item1.GetHashCode();
hash = hash * 23 + Item2.GetHashCode();
hash = hash * 23 + Item3.GetHashCode();
return hash;
}
}
public override string ToString() {
if (Item1 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {Item1})";
}
if (Item2 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {Item2})";
}
if (Item3 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {Item3})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator EitherBase<T1, T2, T3>(T1 t1) {
return new(t1);
}
public static implicit operator EitherBase<T1, T2, T3>(T2 t2) {
return new(t2);
}
public static implicit operator EitherBase<T1, T2, T3>(T3 t3) {
return new(t3);
}
public static implicit operator T1(EitherBase<T1, T2, T3> either) {
return either.Item1;
}
public static implicit operator T2(EitherBase<T1, T2, T3> either) {
return either.Item2;
}
public static implicit operator T3(EitherBase<T1, T2, T3> either) {
return either.Item3;
}
public ConvertibleTo<TBase>.IEither<T1, T2, T3> ConvertTo<TBase>() {
if (Item1 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3>(Item1);
}
if (Item2 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3>(Item2);
}
if (Item3 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3>(Item3);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
public partial class ConvertibleTo<TBase> {
public class Either<T1, T2, T3> : EitherBase<T1, T2, T3>, IEither<T1, T2, T3>
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(Either<T1, T2, T3> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
}
public static implicit operator Either<T1, T2, T3>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3>(T3 t3) {
return new(t3);
}
public static implicit operator TBase(Either<T1, T2, T3> either) {
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
public virtual TBase Value => (TBase)(Item1 != null ? Convert1(Item1) : default) ?? (TBase)(Item2 != null ? Convert2(Item2) : default) ?? (TBase)(Item3 != null ? Convert3(Item3) : default);
}
}
public partial class SubTypesOf<TBase> {
public class Either<T1, T2, T3> : ConvertibleTo<TBase>.Either<T1, T2, T3>, IEither<T1, T2, T3> where T1 : TBase where T2 : TBase where T3 : TBase
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(Either<T1, T2, T3> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
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
throw new ArgumentException($"Expected argument to be either a {typeof(T1).Name}, {typeof(T2).Name} or {typeof(T3).Name} but instead got a type of {typeof(TBase).Name}: {item.GetType().Name}", "name");
}
public virtual TBase Value => Item1 ?? Item2 ?? (TBase)Item3;
public static implicit operator Either<T1, T2, T3>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3>(T3 t3) {
return new(t3);
}
}
}
public class Either<T1, T2, T3> : SubTypesOf<object>.Either<T1, T2, T3>, IEither<T1, T2, T3>
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(Either<T1, T2, T3> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
}
public static implicit operator Either<T1, T2, T3>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3>(T3 t3) {
return new(t3);
}
}
}
