using System;

namespace SimpleMonads {
public class EitherBase<T1, T2, T3, T4, T5, T6> : IEitherBase<T1, T2, T3, T4, T5, T6>, IEquatable<IEither<T1, T2, T3, T4, T5, T6>>
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
public EitherBase(EitherBase<T1, T2, T3, T4, T5, T6> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
}
public virtual T1? Item1 { get; init; } = default;
public virtual T2? Item2 { get; init; } = default;
public virtual T3? Item3 { get; init; } = default;
public virtual T4? Item4 { get; init; } = default;
public virtual T5? Item5 { get; init; } = default;
public virtual T6? Item6 { get; init; } = default;
public virtual TOutput Collapse<TOutput>(Func<T1, TOutput> selector1, Func<T2, TOutput> selector2, Func<T3, TOutput> selector3, Func<T4, TOutput> selector4, Func<T5, TOutput> selector5, Func<T6, TOutput> selector6) {
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
throw new InvalidOperationException();
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7> Or<T7>()
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
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7>(Item6);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8> Or<T7, T8>()
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
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8>(Item6);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9> Or<T7, T8, T9>()
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
if (Item4 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item4);
}
if (Item5 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item5);
}
if (Item6 != null) {
return new Either<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Item6);
}
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Or<T7, T8, T9, T10>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T7, T8, T9, T10, T11>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T7, T8, T9, T10, T11, T12>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T7, T8, T9, T10, T11, T12, T13>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T7, T8, T9, T10, T11, T12, T13, T14>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T7, T8, T9, T10, T11, T12, T13, T14, T15>()
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
throw new System.InvalidOperationException("The either has no values");
}
public IEitherBase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
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
throw new System.InvalidOperationException("The either has no values");
}
public bool Equals(IEither<T1, T2, T3, T4, T5, T6> other) {
if (ReferenceEquals(null, other)) return false;
if (ReferenceEquals(this, other)) return true;
return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3) && Equals(Item4, other.Item4) && Equals(Item5, other.Item5) && Equals(Item6, other.Item6);
}

public override bool Equals(object obj) {
return ReferenceEquals(this, obj) || (obj is IEither<T1, T2, T3, T4, T5, T6> other && Equals(other));
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
return hash;
}
}
public override string ToString() {
var item1 = Item1;
if (item1 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6>))}({Utility.ConvertToCSharpTypeName(typeof(T1))} Item1: {item1})";
}
var item2 = Item2;
if (item2 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6>))}({Utility.ConvertToCSharpTypeName(typeof(T2))} Item2: {item2})";
}
var item3 = Item3;
if (item3 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6>))}({Utility.ConvertToCSharpTypeName(typeof(T3))} Item3: {item3})";
}
var item4 = Item4;
if (item4 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6>))}({Utility.ConvertToCSharpTypeName(typeof(T4))} Item4: {item4})";
}
var item5 = Item5;
if (item5 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6>))}({Utility.ConvertToCSharpTypeName(typeof(T5))} Item5: {item5})";
}
var item6 = Item6;
if (item6 != null) {
return $"{Utility.ConvertToCSharpTypeName(typeof(Either<T1, T2, T3, T4, T5, T6>))}({Utility.ConvertToCSharpTypeName(typeof(T6))} Item6: {item6})";
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6>(T1 t1) {
return new(t1);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6>(T2 t2) {
return new(t2);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6>(T3 t3) {
return new(t3);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6>(T4 t4) {
return new(t4);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6>(T5 t5) {
return new(t5);
}
public static implicit operator EitherBase<T1, T2, T3, T4, T5, T6>(T6 t6) {
return new(t6);
}
        public static implicit operator Either<T1, T2, T3, T5, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T3, T5, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T3, T5, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T3, T5, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T3, T5, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T3, T5, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T3, T5, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T3, T5, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T3, T5, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T3, T5, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T3, T5, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T3, T5, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T3, T5, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T3, T5, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T3, T6, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T3, T6, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T3, T6, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T3, T6, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T3, T6, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T3, T6, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T3, T6, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T3, T6, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T3, T6, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T3, T6, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T3, T6, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T3, T6, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T3, T6, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T3, T6, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T4, T3, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T4, T3, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T4, T3, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T4, T3, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T4, T3, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T4, T3, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T4, T3, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T4, T3, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T4, T3, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T4, T3, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T4, T3, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T4, T3, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T4, T3, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T4, T3, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T4, T5, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T4, T5, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T4, T5, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T4, T5, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T4, T5, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T4, T5, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T4, T5, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T4, T5, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T4, T5, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T4, T5, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T4, T5, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T4, T5, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T4, T5, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T4, T5, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T4, T6, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T4, T6, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T4, T6, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T4, T6, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T4, T6, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T4, T6, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T4, T6, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T4, T6, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T4, T6, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T4, T6, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T4, T6, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T4, T6, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T4, T6, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T4, T6, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T5, T3, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T5, T3, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T5, T3, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T5, T3, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T5, T3, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T5, T3, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T5, T3, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T5, T3, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T5, T3, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T5, T3, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T5, T3, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T5, T3, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T5, T3, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T5, T3, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T5, T4, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T5, T4, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T5, T4, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T5, T4, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T5, T4, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T5, T4, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T5, T4, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T5, T4, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T5, T4, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T5, T4, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T5, T4, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T5, T4, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T5, T4, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T5, T4, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T5, T6, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T5, T6, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T5, T6, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T5, T6, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T5, T6, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T5, T6, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T5, T6, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T5, T6, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T5, T6, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T5, T6, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T5, T6, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T5, T6, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T5, T6, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T5, T6, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T6, T3, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T6, T3, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T6, T3, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T6, T3, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T6, T3, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T6, T3, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T6, T3, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T6, T3, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T6, T3, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T6, T3, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T6, T3, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T6, T3, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T6, T3, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T6, T3, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T6, T4, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T6, T4, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T6, T4, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T6, T4, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T6, T4, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T6, T4, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T6, T4, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T6, T4, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T6, T4, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T6, T4, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T6, T4, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T6, T4, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T6, T4, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T6, T4, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T6, T5, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T6, T5, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T6, T5, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T6, T5, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T6, T5, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T6, T5, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T6, T5, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T2, T6, T5, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T2, T6, T5, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T2, T6, T5, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T2, T6, T5, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T2, T6, T5, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T2, T6, T5, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T2, T6, T5, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T2, T4, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T2, T4, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T2, T4, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T2, T4, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T2, T4, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T2, T4, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T2, T4, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T2, T4, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T2, T4, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T2, T4, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T2, T4, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T2, T4, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T2, T4, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T2, T4, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T2, T5, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T2, T5, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T2, T5, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T2, T5, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T2, T5, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T2, T5, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T2, T5, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T2, T5, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T2, T5, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T2, T5, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T2, T5, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T2, T5, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T2, T5, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T2, T5, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T2, T6, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T2, T6, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T2, T6, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T2, T6, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T2, T6, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T2, T6, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T2, T6, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T2, T6, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T2, T6, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T2, T6, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T2, T6, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T2, T6, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T2, T6, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T2, T6, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T4, T2, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T4, T2, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T4, T2, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T4, T2, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T4, T2, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T4, T2, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T4, T2, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T4, T2, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T4, T2, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T4, T2, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T4, T2, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T4, T2, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T4, T2, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T4, T2, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T4, T5, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T4, T5, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T4, T5, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T4, T5, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T4, T5, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T4, T5, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T4, T5, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T4, T5, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T4, T5, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T4, T5, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T4, T5, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T4, T5, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T4, T5, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T4, T5, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T4, T6, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T4, T6, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T4, T6, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T4, T6, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T4, T6, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T4, T6, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T4, T6, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T4, T6, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T4, T6, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T4, T6, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T4, T6, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T4, T6, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T4, T6, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T4, T6, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T5, T2, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T5, T2, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T5, T2, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T5, T2, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T5, T2, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T5, T2, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T5, T2, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T5, T2, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T5, T2, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T5, T2, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T5, T2, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T5, T2, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T5, T2, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T5, T2, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T5, T4, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T5, T4, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T5, T4, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T5, T4, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T5, T4, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T5, T4, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T5, T4, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T5, T4, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T5, T4, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T5, T4, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T5, T4, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T5, T4, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T5, T4, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T5, T4, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T5, T6, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T5, T6, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T5, T6, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T5, T6, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T5, T6, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T5, T6, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T5, T6, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T5, T6, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T5, T6, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T5, T6, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T5, T6, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T5, T6, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T5, T6, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T5, T6, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T6, T2, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T6, T2, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T6, T2, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T6, T2, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T6, T2, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T6, T2, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T6, T2, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T6, T2, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T6, T2, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T6, T2, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T6, T2, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T6, T2, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T6, T2, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T6, T2, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T6, T4, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T6, T4, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T6, T4, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T6, T4, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T6, T4, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T6, T4, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T6, T4, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T6, T4, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T6, T4, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T6, T4, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T6, T4, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T6, T4, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T6, T4, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T6, T4, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T6, T5, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T6, T5, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T6, T5, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T6, T5, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T6, T5, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T6, T5, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T6, T5, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T3, T6, T5, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T3, T6, T5, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T3, T6, T5, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T3, T6, T5, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T3, T6, T5, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T3, T6, T5, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T3, T6, T5, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T2, T3, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T2, T3, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T2, T3, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T2, T3, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T2, T3, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T2, T3, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T2, T3, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T2, T3, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T2, T3, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T2, T3, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T2, T3, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T2, T3, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T2, T3, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T2, T3, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T2, T5, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T2, T5, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T2, T5, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T2, T5, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T2, T5, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T2, T5, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T2, T5, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T2, T5, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T2, T5, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T2, T5, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T2, T5, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T2, T5, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T2, T5, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T2, T5, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T2, T6, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T2, T6, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T2, T6, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T2, T6, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T2, T6, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T2, T6, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T2, T6, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T2, T6, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T2, T6, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T2, T6, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T2, T6, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T2, T6, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T2, T6, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T2, T6, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T3, T2, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T3, T2, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T3, T2, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T3, T2, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T3, T2, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T3, T2, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T3, T2, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T3, T2, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T3, T2, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T3, T2, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T3, T2, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T3, T2, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T3, T2, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T3, T2, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T3, T5, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T3, T5, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T3, T5, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T3, T5, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T3, T5, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T3, T5, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T3, T5, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T3, T5, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T3, T5, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T3, T5, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T3, T5, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T3, T5, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T3, T5, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T3, T5, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T3, T6, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T3, T6, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T3, T6, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T3, T6, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T3, T6, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T3, T6, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T3, T6, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T3, T6, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T3, T6, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T3, T6, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T3, T6, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T3, T6, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T3, T6, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T3, T6, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T5, T2, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T5, T2, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T5, T2, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T5, T2, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T5, T2, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T5, T2, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T5, T2, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T5, T2, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T5, T2, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T5, T2, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T5, T2, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T5, T2, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T5, T2, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T5, T2, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T5, T3, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T5, T3, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T5, T3, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T5, T3, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T5, T3, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T5, T3, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T5, T3, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T5, T3, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T5, T3, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T5, T3, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T5, T3, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T5, T3, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T5, T3, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T5, T3, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T5, T6, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T5, T6, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T5, T6, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T5, T6, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T5, T6, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T5, T6, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T5, T6, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T5, T6, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T5, T6, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T5, T6, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T5, T6, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T5, T6, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T5, T6, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T5, T6, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T6, T2, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T6, T2, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T6, T2, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T6, T2, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T6, T2, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T6, T2, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T6, T2, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T6, T2, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T6, T2, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T6, T2, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T6, T2, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T6, T2, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T6, T2, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T6, T2, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T6, T3, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T6, T3, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T6, T3, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T6, T3, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T6, T3, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T6, T3, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T6, T3, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T6, T3, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T6, T3, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T6, T3, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T6, T3, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T6, T3, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T6, T3, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T6, T3, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T6, T5, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T6, T5, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T6, T5, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T6, T5, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T6, T5, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T6, T5, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T6, T5, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T4, T6, T5, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T4, T6, T5, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T4, T6, T5, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T4, T6, T5, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T4, T6, T5, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T4, T6, T5, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T4, T6, T5, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T2, T3, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T2, T3, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T2, T3, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T2, T3, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T2, T3, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T2, T3, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T2, T3, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T2, T3, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T2, T3, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T2, T3, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T2, T3, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T2, T3, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T2, T3, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T2, T3, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T2, T4, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T2, T4, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T2, T4, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T2, T4, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T2, T4, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T2, T4, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T2, T4, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T2, T4, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T2, T4, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T2, T4, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T2, T4, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T2, T4, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T2, T4, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T2, T4, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T2, T6, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T2, T6, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T2, T6, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T2, T6, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T2, T6, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T2, T6, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T2, T6, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T2, T6, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T2, T6, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T2, T6, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T2, T6, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T2, T6, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T2, T6, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T2, T6, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T3, T2, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T3, T2, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T3, T2, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T3, T2, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T3, T2, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T3, T2, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T3, T2, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T3, T2, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T3, T2, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T3, T2, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T3, T2, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T3, T2, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T3, T2, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T3, T2, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T3, T4, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T3, T4, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T3, T4, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T3, T4, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T3, T4, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T3, T4, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T3, T4, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T3, T4, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T3, T4, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T3, T4, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T3, T4, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T3, T4, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T3, T4, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T3, T4, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T3, T6, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T3, T6, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T3, T6, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T3, T6, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T3, T6, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T3, T6, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T3, T6, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T3, T6, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T3, T6, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T3, T6, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T3, T6, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T3, T6, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T3, T6, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T3, T6, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T4, T2, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T4, T2, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T4, T2, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T4, T2, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T4, T2, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T4, T2, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T4, T2, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T4, T2, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T4, T2, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T4, T2, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T4, T2, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T4, T2, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T4, T2, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T4, T2, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T4, T3, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T4, T3, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T4, T3, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T4, T3, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T4, T3, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T4, T3, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T4, T3, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T4, T3, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T4, T3, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T4, T3, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T4, T3, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T4, T3, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T4, T3, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T4, T3, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T4, T6, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T4, T6, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T4, T6, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T4, T6, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T4, T6, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T4, T6, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T4, T6, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T4, T6, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T4, T6, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T4, T6, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T4, T6, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T4, T6, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T4, T6, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T4, T6, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T6, T2, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T6, T2, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T6, T2, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T6, T2, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T6, T2, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T6, T2, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T6, T2, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T6, T2, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T6, T2, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T6, T2, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T6, T2, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T6, T2, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T6, T2, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T6, T2, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T6, T3, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T6, T3, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T6, T3, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T6, T3, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T6, T3, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T6, T3, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T6, T3, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T6, T3, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T6, T3, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T6, T3, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T6, T3, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T6, T3, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T6, T3, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T6, T3, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T6, T4, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T6, T4, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T6, T4, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T6, T4, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T6, T4, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T6, T4, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T6, T4, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T5, T6, T4, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T5, T6, T4, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T5, T6, T4, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T5, T6, T4, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T5, T6, T4, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T5, T6, T4, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T5, T6, T4, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T2, T3, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T2, T3, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T2, T3, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T2, T3, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T2, T3, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T2, T3, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T2, T3, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T2, T3, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T2, T3, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T2, T3, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T2, T3, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T2, T3, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T2, T3, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T2, T3, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T2, T4, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T2, T4, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T2, T4, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T2, T4, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T2, T4, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T2, T4, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T2, T4, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T2, T4, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T2, T4, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T2, T4, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T2, T4, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T2, T4, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T2, T4, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T2, T4, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T2, T5, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T2, T5, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T2, T5, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T2, T5, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T2, T5, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T2, T5, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T2, T5, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T2, T5, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T2, T5, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T2, T5, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T2, T5, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T2, T5, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T2, T5, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T2, T5, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T3, T2, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T3, T2, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T3, T2, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T3, T2, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T3, T2, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T3, T2, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T3, T2, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T3, T2, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T3, T2, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T3, T2, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T3, T2, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T3, T2, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T3, T2, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T3, T2, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T3, T4, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T3, T4, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T3, T4, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T3, T4, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T3, T4, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T3, T4, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T3, T4, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T3, T4, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T3, T4, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T3, T4, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T3, T4, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T3, T4, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T3, T4, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T3, T4, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T3, T5, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T3, T5, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T3, T5, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T3, T5, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T3, T5, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T3, T5, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T3, T5, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T3, T5, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T3, T5, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T3, T5, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T3, T5, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T3, T5, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T3, T5, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T3, T5, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T4, T2, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T4, T2, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T4, T2, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T4, T2, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T4, T2, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T4, T2, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T4, T2, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T4, T2, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T4, T2, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T4, T2, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T4, T2, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T4, T2, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T4, T2, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T4, T2, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T4, T3, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T4, T3, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T4, T3, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T4, T3, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T4, T3, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T4, T3, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T4, T3, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T4, T3, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T4, T3, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T4, T3, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T4, T3, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T4, T3, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T4, T3, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T4, T3, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T4, T5, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T4, T5, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T4, T5, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T4, T5, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T4, T5, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T4, T5, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T4, T5, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T4, T5, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T4, T5, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T4, T5, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T4, T5, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T4, T5, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T4, T5, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T4, T5, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T5, T2, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T5, T2, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T5, T2, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T5, T2, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T5, T2, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T5, T2, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T5, T2, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T5, T2, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T5, T2, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T5, T2, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T5, T2, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T5, T2, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T5, T2, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T5, T2, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T5, T3, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T5, T3, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T5, T3, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T5, T3, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T5, T3, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T5, T3, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T5, T3, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T5, T3, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T5, T3, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T5, T3, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T5, T3, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T5, T3, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T5, T3, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T5, T3, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T5, T4, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T5, T4, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T5, T4, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T5, T4, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T5, T4, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T5, T4, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T5, T4, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T1, T6, T5, T4, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T1, T6, T5, T4, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T1, T6, T5, T4, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T1, T6, T5, T4, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T1, T6, T5, T4, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T1, T6, T5, T4, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T1, T6, T5, T4, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T3, T4, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T3, T4, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T3, T4, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T3, T4, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T3, T4, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T3, T4, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T3, T4, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T3, T4, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T3, T4, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T3, T4, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T3, T4, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T3, T4, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T3, T4, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T3, T4, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T3, T5, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T3, T5, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T3, T5, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T3, T5, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T3, T5, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T3, T5, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T3, T5, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T3, T5, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T3, T5, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T3, T5, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T3, T5, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T3, T5, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T3, T5, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T3, T5, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T3, T6, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T3, T6, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T3, T6, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T3, T6, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T3, T6, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T3, T6, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T3, T6, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T3, T6, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T3, T6, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T3, T6, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T3, T6, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T3, T6, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T3, T6, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T3, T6, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T4, T3, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T4, T3, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T4, T3, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T4, T3, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T4, T3, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T4, T3, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T4, T3, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T4, T3, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T4, T3, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T4, T3, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T4, T3, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T4, T3, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T4, T3, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T4, T3, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T4, T5, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T4, T5, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T4, T5, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T4, T5, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T4, T5, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T4, T5, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T4, T5, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T4, T5, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T4, T5, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T4, T5, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T4, T5, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T4, T5, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T4, T5, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T4, T5, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T4, T6, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T4, T6, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T4, T6, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T4, T6, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T4, T6, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T4, T6, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T4, T6, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T4, T6, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T4, T6, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T4, T6, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T4, T6, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T4, T6, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T4, T6, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T4, T6, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T5, T3, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T5, T3, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T5, T3, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T5, T3, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T5, T3, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T5, T3, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T5, T3, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T5, T3, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T5, T3, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T5, T3, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T5, T3, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T5, T3, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T5, T3, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T5, T3, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T5, T4, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T5, T4, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T5, T4, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T5, T4, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T5, T4, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T5, T4, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T5, T4, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T5, T4, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T5, T4, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T5, T4, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T5, T4, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T5, T4, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T5, T4, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T5, T4, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T5, T6, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T5, T6, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T5, T6, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T5, T6, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T5, T6, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T5, T6, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T5, T6, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T5, T6, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T5, T6, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T5, T6, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T5, T6, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T5, T6, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T5, T6, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T5, T6, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T6, T3, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T6, T3, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T6, T3, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T6, T3, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T6, T3, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T6, T3, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T6, T3, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T6, T3, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T6, T3, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T6, T3, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T6, T3, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T6, T3, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T6, T3, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T6, T3, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T6, T4, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T6, T4, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T6, T4, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T6, T4, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T6, T4, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T6, T4, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T6, T4, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T6, T4, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T6, T4, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T6, T4, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T6, T4, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T6, T4, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T6, T4, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T6, T4, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T6, T5, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T6, T5, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T6, T5, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T6, T5, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T6, T5, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T6, T5, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T6, T5, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T1, T6, T5, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T1, T6, T5, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T1, T6, T5, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T1, T6, T5, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T1, T6, T5, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T1, T6, T5, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T1, T6, T5, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T1, T4, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T1, T4, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T1, T4, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T1, T4, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T1, T4, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T1, T4, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T1, T4, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T1, T4, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T1, T4, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T1, T4, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T1, T4, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T1, T4, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T1, T4, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T1, T4, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T1, T5, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T1, T5, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T1, T5, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T1, T5, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T1, T5, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T1, T5, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T1, T5, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T1, T5, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T1, T5, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T1, T5, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T1, T5, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T1, T5, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T1, T5, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T1, T5, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T1, T6, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T1, T6, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T1, T6, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T1, T6, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T1, T6, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T1, T6, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T1, T6, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T1, T6, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T1, T6, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T1, T6, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T1, T6, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T1, T6, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T1, T6, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T1, T6, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T4, T1, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T4, T1, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T4, T1, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T4, T1, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T4, T1, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T4, T1, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T4, T1, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T4, T1, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T4, T1, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T4, T1, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T4, T1, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T4, T1, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T4, T1, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T4, T1, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T4, T5, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T4, T5, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T4, T5, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T4, T5, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T4, T5, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T4, T5, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T4, T5, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T4, T5, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T4, T5, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T4, T5, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T4, T5, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T4, T5, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T4, T5, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T4, T5, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T4, T6, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T4, T6, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T4, T6, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T4, T6, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T4, T6, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T4, T6, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T4, T6, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T4, T6, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T4, T6, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T4, T6, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T4, T6, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T4, T6, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T4, T6, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T4, T6, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T5, T1, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T5, T1, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T5, T1, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T5, T1, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T5, T1, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T5, T1, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T5, T1, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T5, T1, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T5, T1, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T5, T1, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T5, T1, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T5, T1, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T5, T1, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T5, T1, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T5, T4, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T5, T4, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T5, T4, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T5, T4, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T5, T4, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T5, T4, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T5, T4, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T5, T4, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T5, T4, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T5, T4, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T5, T4, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T5, T4, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T5, T4, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T5, T4, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T5, T6, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T5, T6, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T5, T6, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T5, T6, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T5, T6, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T5, T6, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T5, T6, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T5, T6, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T5, T6, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T5, T6, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T5, T6, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T5, T6, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T5, T6, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T5, T6, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T6, T1, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T6, T1, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T6, T1, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T6, T1, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T6, T1, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T6, T1, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T6, T1, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T6, T1, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T6, T1, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T6, T1, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T6, T1, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T6, T1, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T6, T1, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T6, T1, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T6, T4, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T6, T4, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T6, T4, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T6, T4, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T6, T4, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T6, T4, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T6, T4, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T6, T4, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T6, T4, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T6, T4, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T6, T4, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T6, T4, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T6, T4, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T6, T4, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T6, T5, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T6, T5, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T6, T5, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T6, T5, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T6, T5, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T6, T5, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T6, T5, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T3, T6, T5, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T3, T6, T5, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T3, T6, T5, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T3, T6, T5, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T3, T6, T5, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T3, T6, T5, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T3, T6, T5, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T1, T3, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T1, T3, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T1, T3, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T1, T3, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T1, T3, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T1, T3, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T1, T3, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T1, T3, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T1, T3, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T1, T3, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T1, T3, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T1, T3, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T1, T3, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T1, T3, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T1, T5, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T1, T5, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T1, T5, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T1, T5, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T1, T5, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T1, T5, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T1, T5, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T1, T5, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T1, T5, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T1, T5, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T1, T5, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T1, T5, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T1, T5, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T1, T5, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T1, T6, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T1, T6, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T1, T6, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T1, T6, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T1, T6, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T1, T6, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T1, T6, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T1, T6, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T1, T6, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T1, T6, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T1, T6, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T1, T6, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T1, T6, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T1, T6, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T3, T1, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T3, T1, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T3, T1, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T3, T1, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T3, T1, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T3, T1, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T3, T1, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T3, T1, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T3, T1, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T3, T1, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T3, T1, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T3, T1, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T3, T1, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T3, T1, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T3, T5, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T3, T5, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T3, T5, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T3, T5, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T3, T5, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T3, T5, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T3, T5, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T3, T5, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T3, T5, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T3, T5, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T3, T5, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T3, T5, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T3, T5, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T3, T5, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T3, T6, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T3, T6, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T3, T6, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T3, T6, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T3, T6, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T3, T6, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T3, T6, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T3, T6, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T3, T6, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T3, T6, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T3, T6, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T3, T6, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T3, T6, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T3, T6, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T5, T1, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T5, T1, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T5, T1, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T5, T1, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T5, T1, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T5, T1, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T5, T1, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T5, T1, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T5, T1, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T5, T1, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T5, T1, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T5, T1, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T5, T1, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T5, T1, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T5, T3, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T5, T3, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T5, T3, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T5, T3, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T5, T3, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T5, T3, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T5, T3, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T5, T3, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T5, T3, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T5, T3, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T5, T3, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T5, T3, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T5, T3, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T5, T3, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T5, T6, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T5, T6, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T5, T6, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T5, T6, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T5, T6, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T5, T6, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T5, T6, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T5, T6, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T5, T6, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T5, T6, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T5, T6, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T5, T6, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T5, T6, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T5, T6, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T6, T1, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T6, T1, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T6, T1, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T6, T1, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T6, T1, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T6, T1, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T6, T1, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T6, T1, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T6, T1, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T6, T1, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T6, T1, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T6, T1, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T6, T1, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T6, T1, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T6, T3, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T6, T3, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T6, T3, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T6, T3, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T6, T3, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T6, T3, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T6, T3, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T6, T3, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T6, T3, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T6, T3, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T6, T3, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T6, T3, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T6, T3, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T6, T3, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T6, T5, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T6, T5, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T6, T5, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T6, T5, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T6, T5, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T6, T5, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T6, T5, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T4, T6, T5, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T4, T6, T5, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T4, T6, T5, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T4, T6, T5, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T4, T6, T5, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T4, T6, T5, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T4, T6, T5, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T1, T3, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T1, T3, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T1, T3, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T1, T3, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T1, T3, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T1, T3, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T1, T3, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T1, T3, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T1, T3, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T1, T3, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T1, T3, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T1, T3, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T1, T3, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T1, T3, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T1, T4, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T1, T4, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T1, T4, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T1, T4, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T1, T4, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T1, T4, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T1, T4, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T1, T4, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T1, T4, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T1, T4, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T1, T4, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T1, T4, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T1, T4, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T1, T4, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T1, T6, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T1, T6, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T1, T6, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T1, T6, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T1, T6, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T1, T6, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T1, T6, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T1, T6, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T1, T6, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T1, T6, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T1, T6, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T1, T6, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T1, T6, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T1, T6, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T3, T1, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T3, T1, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T3, T1, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T3, T1, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T3, T1, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T3, T1, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T3, T1, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T3, T1, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T3, T1, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T3, T1, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T3, T1, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T3, T1, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T3, T1, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T3, T1, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T3, T4, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T3, T4, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T3, T4, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T3, T4, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T3, T4, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T3, T4, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T3, T4, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T3, T4, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T3, T4, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T3, T4, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T3, T4, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T3, T4, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T3, T4, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T3, T4, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T3, T6, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T3, T6, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T3, T6, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T3, T6, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T3, T6, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T3, T6, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T3, T6, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T3, T6, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T3, T6, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T3, T6, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T3, T6, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T3, T6, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T3, T6, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T3, T6, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T4, T1, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T4, T1, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T4, T1, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T4, T1, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T4, T1, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T4, T1, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T4, T1, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T4, T1, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T4, T1, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T4, T1, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T4, T1, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T4, T1, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T4, T1, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T4, T1, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T4, T3, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T4, T3, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T4, T3, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T4, T3, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T4, T3, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T4, T3, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T4, T3, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T4, T3, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T4, T3, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T4, T3, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T4, T3, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T4, T3, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T4, T3, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T4, T3, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T4, T6, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T4, T6, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T4, T6, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T4, T6, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T4, T6, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T4, T6, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T4, T6, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T4, T6, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T4, T6, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T4, T6, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T4, T6, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T4, T6, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T4, T6, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T4, T6, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T6, T1, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T6, T1, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T6, T1, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T6, T1, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T6, T1, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T6, T1, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T6, T1, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T6, T1, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T6, T1, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T6, T1, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T6, T1, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T6, T1, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T6, T1, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T6, T1, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T6, T3, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T6, T3, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T6, T3, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T6, T3, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T6, T3, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T6, T3, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T6, T3, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T6, T3, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T6, T3, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T6, T3, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T6, T3, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T6, T3, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T6, T3, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T6, T3, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T6, T4, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T6, T4, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T6, T4, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T6, T4, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T6, T4, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T6, T4, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T6, T4, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T5, T6, T4, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T5, T6, T4, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T5, T6, T4, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T5, T6, T4, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T5, T6, T4, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T5, T6, T4, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T5, T6, T4, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T1, T3, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T1, T3, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T1, T3, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T1, T3, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T1, T3, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T1, T3, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T1, T3, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T1, T3, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T1, T3, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T1, T3, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T1, T3, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T1, T3, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T1, T3, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T1, T3, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T1, T4, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T1, T4, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T1, T4, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T1, T4, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T1, T4, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T1, T4, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T1, T4, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T1, T4, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T1, T4, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T1, T4, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T1, T4, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T1, T4, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T1, T4, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T1, T4, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T1, T5, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T1, T5, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T1, T5, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T1, T5, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T1, T5, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T1, T5, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T1, T5, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T1, T5, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T1, T5, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T1, T5, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T1, T5, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T1, T5, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T1, T5, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T1, T5, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T3, T1, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T3, T1, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T3, T1, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T3, T1, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T3, T1, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T3, T1, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T3, T1, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T3, T1, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T3, T1, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T3, T1, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T3, T1, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T3, T1, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T3, T1, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T3, T1, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T3, T4, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T3, T4, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T3, T4, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T3, T4, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T3, T4, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T3, T4, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T3, T4, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T3, T4, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T3, T4, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T3, T4, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T3, T4, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T3, T4, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T3, T4, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T3, T4, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T3, T5, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T3, T5, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T3, T5, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T3, T5, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T3, T5, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T3, T5, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T3, T5, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T3, T5, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T3, T5, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T3, T5, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T3, T5, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T3, T5, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T3, T5, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T3, T5, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T4, T1, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T4, T1, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T4, T1, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T4, T1, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T4, T1, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T4, T1, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T4, T1, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T4, T1, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T4, T1, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T4, T1, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T4, T1, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T4, T1, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T4, T1, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T4, T1, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T4, T3, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T4, T3, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T4, T3, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T4, T3, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T4, T3, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T4, T3, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T4, T3, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T4, T3, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T4, T3, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T4, T3, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T4, T3, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T4, T3, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T4, T3, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T4, T3, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T4, T5, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T4, T5, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T4, T5, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T4, T5, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T4, T5, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T4, T5, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T4, T5, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T4, T5, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T4, T5, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T4, T5, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T4, T5, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T4, T5, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T4, T5, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T4, T5, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T5, T1, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T5, T1, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T5, T1, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T5, T1, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T5, T1, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T5, T1, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T5, T1, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T5, T1, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T5, T1, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T5, T1, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T5, T1, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T5, T1, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T5, T1, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T5, T1, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T5, T3, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T5, T3, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T5, T3, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T5, T3, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T5, T3, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T5, T3, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T5, T3, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T5, T3, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T5, T3, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T5, T3, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T5, T3, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T5, T3, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T5, T3, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T5, T3, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T5, T4, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T5, T4, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T5, T4, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T5, T4, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T5, T4, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T5, T4, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T5, T4, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T2, T6, T5, T4, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T2, T6, T5, T4, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T2, T6, T5, T4, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T2, T6, T5, T4, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T2, T6, T5, T4, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T2, T6, T5, T4, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T2, T6, T5, T4, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T2, T4, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T2, T4, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T2, T4, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T2, T4, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T2, T4, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T2, T4, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T2, T4, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T2, T4, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T2, T4, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T2, T4, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T2, T4, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T2, T4, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T2, T4, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T2, T4, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T2, T5, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T2, T5, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T2, T5, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T2, T5, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T2, T5, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T2, T5, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T2, T5, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T2, T5, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T2, T5, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T2, T5, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T2, T5, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T2, T5, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T2, T5, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T2, T5, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T2, T6, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T2, T6, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T2, T6, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T2, T6, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T2, T6, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T2, T6, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T2, T6, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T2, T6, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T2, T6, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T2, T6, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T2, T6, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T2, T6, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T2, T6, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T2, T6, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T4, T2, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T4, T2, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T4, T2, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T4, T2, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T4, T2, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T4, T2, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T4, T2, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T4, T2, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T4, T2, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T4, T2, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T4, T2, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T4, T2, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T4, T2, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T4, T2, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T4, T5, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T4, T5, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T4, T5, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T4, T5, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T4, T5, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T4, T5, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T4, T5, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T4, T5, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T4, T5, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T4, T5, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T4, T5, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T4, T5, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T4, T5, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T4, T5, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T4, T6, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T4, T6, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T4, T6, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T4, T6, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T4, T6, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T4, T6, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T4, T6, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T4, T6, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T4, T6, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T4, T6, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T4, T6, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T4, T6, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T4, T6, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T4, T6, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T5, T2, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T5, T2, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T5, T2, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T5, T2, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T5, T2, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T5, T2, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T5, T2, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T5, T2, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T5, T2, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T5, T2, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T5, T2, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T5, T2, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T5, T2, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T5, T2, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T5, T4, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T5, T4, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T5, T4, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T5, T4, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T5, T4, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T5, T4, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T5, T4, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T5, T4, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T5, T4, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T5, T4, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T5, T4, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T5, T4, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T5, T4, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T5, T4, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T5, T6, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T5, T6, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T5, T6, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T5, T6, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T5, T6, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T5, T6, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T5, T6, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T5, T6, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T5, T6, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T5, T6, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T5, T6, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T5, T6, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T5, T6, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T5, T6, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T6, T2, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T6, T2, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T6, T2, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T6, T2, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T6, T2, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T6, T2, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T6, T2, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T6, T2, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T6, T2, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T6, T2, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T6, T2, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T6, T2, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T6, T2, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T6, T2, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T6, T4, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T6, T4, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T6, T4, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T6, T4, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T6, T4, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T6, T4, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T6, T4, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T6, T4, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T6, T4, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T6, T4, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T6, T4, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T6, T4, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T6, T4, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T6, T4, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T6, T5, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T6, T5, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T6, T5, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T6, T5, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T6, T5, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T6, T5, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T6, T5, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T1, T6, T5, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T1, T6, T5, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T1, T6, T5, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T1, T6, T5, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T1, T6, T5, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T1, T6, T5, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T1, T6, T5, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T1, T4, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T1, T4, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T1, T4, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T1, T4, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T1, T4, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T1, T4, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T1, T4, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T1, T4, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T1, T4, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T1, T4, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T1, T4, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T1, T4, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T1, T4, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T1, T4, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T1, T5, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T1, T5, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T1, T5, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T1, T5, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T1, T5, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T1, T5, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T1, T5, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T1, T5, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T1, T5, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T1, T5, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T1, T5, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T1, T5, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T1, T5, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T1, T5, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T1, T6, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T1, T6, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T1, T6, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T1, T6, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T1, T6, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T1, T6, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T1, T6, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T1, T6, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T1, T6, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T1, T6, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T1, T6, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T1, T6, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T1, T6, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T1, T6, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T4, T1, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T4, T1, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T4, T1, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T4, T1, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T4, T1, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T4, T1, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T4, T1, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T4, T1, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T4, T1, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T4, T1, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T4, T1, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T4, T1, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T4, T1, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T4, T1, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T4, T5, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T4, T5, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T4, T5, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T4, T5, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T4, T5, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T4, T5, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T4, T5, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T4, T5, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T4, T5, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T4, T5, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T4, T5, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T4, T5, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T4, T5, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T4, T5, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T4, T6, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T4, T6, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T4, T6, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T4, T6, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T4, T6, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T4, T6, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T4, T6, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T4, T6, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T4, T6, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T4, T6, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T4, T6, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T4, T6, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T4, T6, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T4, T6, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T5, T1, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T5, T1, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T5, T1, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T5, T1, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T5, T1, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T5, T1, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T5, T1, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T5, T1, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T5, T1, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T5, T1, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T5, T1, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T5, T1, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T5, T1, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T5, T1, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T5, T4, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T5, T4, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T5, T4, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T5, T4, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T5, T4, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T5, T4, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T5, T4, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T5, T4, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T5, T4, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T5, T4, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T5, T4, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T5, T4, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T5, T4, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T5, T4, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T5, T6, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T5, T6, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T5, T6, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T5, T6, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T5, T6, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T5, T6, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T5, T6, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T5, T6, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T5, T6, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T5, T6, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T5, T6, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T5, T6, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T5, T6, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T5, T6, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T6, T1, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T6, T1, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T6, T1, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T6, T1, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T6, T1, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T6, T1, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T6, T1, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T6, T1, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T6, T1, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T6, T1, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T6, T1, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T6, T1, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T6, T1, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T6, T1, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T6, T4, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T6, T4, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T6, T4, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T6, T4, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T6, T4, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T6, T4, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T6, T4, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T6, T4, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T6, T4, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T6, T4, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T6, T4, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T6, T4, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T6, T4, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T6, T4, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T6, T5, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T6, T5, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T6, T5, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T6, T5, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T6, T5, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T6, T5, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T6, T5, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T2, T6, T5, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T2, T6, T5, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T2, T6, T5, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T2, T6, T5, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T2, T6, T5, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T2, T6, T5, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T2, T6, T5, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T1, T2, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T1, T2, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T1, T2, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T1, T2, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T1, T2, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T1, T2, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T1, T2, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T1, T2, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T1, T2, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T1, T2, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T1, T2, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T1, T2, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T1, T2, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T1, T2, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T1, T5, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T1, T5, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T1, T5, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T1, T5, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T1, T5, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T1, T5, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T1, T5, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T1, T5, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T1, T5, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T1, T5, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T1, T5, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T1, T5, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T1, T5, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T1, T5, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T1, T6, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T1, T6, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T1, T6, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T1, T6, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T1, T6, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T1, T6, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T1, T6, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T1, T6, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T1, T6, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T1, T6, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T1, T6, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T1, T6, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T1, T6, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T1, T6, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T2, T1, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T2, T1, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T2, T1, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T2, T1, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T2, T1, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T2, T1, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T2, T1, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T2, T1, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T2, T1, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T2, T1, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T2, T1, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T2, T1, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T2, T1, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T2, T1, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T2, T5, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T2, T5, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T2, T5, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T2, T5, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T2, T5, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T2, T5, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T2, T5, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T2, T5, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T2, T5, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T2, T5, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T2, T5, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T2, T5, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T2, T5, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T2, T5, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T2, T6, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T2, T6, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T2, T6, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T2, T6, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T2, T6, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T2, T6, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T2, T6, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T2, T6, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T2, T6, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T2, T6, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T2, T6, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T2, T6, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T2, T6, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T2, T6, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T5, T1, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T5, T1, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T5, T1, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T5, T1, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T5, T1, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T5, T1, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T5, T1, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T5, T1, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T5, T1, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T5, T1, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T5, T1, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T5, T1, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T5, T1, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T5, T1, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T5, T2, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T5, T2, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T5, T2, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T5, T2, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T5, T2, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T5, T2, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T5, T2, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T5, T2, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T5, T2, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T5, T2, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T5, T2, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T5, T2, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T5, T2, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T5, T2, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T5, T6, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T5, T6, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T5, T6, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T5, T6, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T5, T6, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T5, T6, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T5, T6, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T5, T6, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T5, T6, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T5, T6, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T5, T6, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T5, T6, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T5, T6, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T5, T6, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T6, T1, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T6, T1, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T6, T1, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T6, T1, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T6, T1, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T6, T1, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T6, T1, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T6, T1, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T6, T1, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T6, T1, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T6, T1, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T6, T1, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T6, T1, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T6, T1, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T6, T2, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T6, T2, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T6, T2, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T6, T2, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T6, T2, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T6, T2, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T6, T2, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T6, T2, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T6, T2, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T6, T2, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T6, T2, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T6, T2, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T6, T2, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T6, T2, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T6, T5, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T6, T5, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T6, T5, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T6, T5, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T6, T5, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T6, T5, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T6, T5, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T4, T6, T5, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T4, T6, T5, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T4, T6, T5, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T4, T6, T5, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T4, T6, T5, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T4, T6, T5, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T4, T6, T5, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T1, T2, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T1, T2, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T1, T2, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T1, T2, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T1, T2, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T1, T2, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T1, T2, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T1, T2, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T1, T2, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T1, T2, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T1, T2, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T1, T2, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T1, T2, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T1, T2, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T1, T4, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T1, T4, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T1, T4, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T1, T4, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T1, T4, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T1, T4, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T1, T4, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T1, T4, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T1, T4, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T1, T4, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T1, T4, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T1, T4, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T1, T4, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T1, T4, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T1, T6, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T1, T6, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T1, T6, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T1, T6, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T1, T6, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T1, T6, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T1, T6, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T1, T6, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T1, T6, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T1, T6, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T1, T6, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T1, T6, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T1, T6, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T1, T6, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T2, T1, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T2, T1, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T2, T1, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T2, T1, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T2, T1, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T2, T1, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T2, T1, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T2, T1, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T2, T1, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T2, T1, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T2, T1, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T2, T1, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T2, T1, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T2, T1, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T2, T4, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T2, T4, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T2, T4, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T2, T4, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T2, T4, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T2, T4, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T2, T4, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T2, T4, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T2, T4, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T2, T4, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T2, T4, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T2, T4, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T2, T4, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T2, T4, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T2, T6, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T2, T6, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T2, T6, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T2, T6, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T2, T6, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T2, T6, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T2, T6, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T2, T6, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T2, T6, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T2, T6, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T2, T6, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T2, T6, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T2, T6, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T2, T6, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T4, T1, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T4, T1, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T4, T1, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T4, T1, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T4, T1, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T4, T1, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T4, T1, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T4, T1, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T4, T1, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T4, T1, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T4, T1, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T4, T1, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T4, T1, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T4, T1, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T4, T2, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T4, T2, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T4, T2, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T4, T2, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T4, T2, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T4, T2, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T4, T2, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T4, T2, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T4, T2, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T4, T2, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T4, T2, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T4, T2, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T4, T2, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T4, T2, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T4, T6, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T4, T6, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T4, T6, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T4, T6, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T4, T6, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T4, T6, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T4, T6, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T4, T6, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T4, T6, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T4, T6, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T4, T6, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T4, T6, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T4, T6, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T4, T6, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T6, T1, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T6, T1, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T6, T1, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T6, T1, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T6, T1, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T6, T1, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T6, T1, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T6, T1, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T6, T1, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T6, T1, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T6, T1, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T6, T1, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T6, T1, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T6, T1, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T6, T2, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T6, T2, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T6, T2, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T6, T2, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T6, T2, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T6, T2, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T6, T2, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T6, T2, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T6, T2, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T6, T2, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T6, T2, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T6, T2, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T6, T2, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T6, T2, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T6, T4, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T6, T4, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T6, T4, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T6, T4, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T6, T4, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T6, T4, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T6, T4, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T5, T6, T4, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T5, T6, T4, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T5, T6, T4, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T5, T6, T4, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T5, T6, T4, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T5, T6, T4, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T5, T6, T4, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T1, T2, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T1, T2, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T1, T2, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T1, T2, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T1, T2, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T1, T2, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T1, T2, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T1, T2, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T1, T2, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T1, T2, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T1, T2, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T1, T2, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T1, T2, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T1, T2, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T1, T4, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T1, T4, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T1, T4, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T1, T4, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T1, T4, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T1, T4, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T1, T4, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T1, T4, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T1, T4, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T1, T4, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T1, T4, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T1, T4, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T1, T4, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T1, T4, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T1, T5, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T1, T5, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T1, T5, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T1, T5, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T1, T5, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T1, T5, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T1, T5, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T1, T5, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T1, T5, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T1, T5, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T1, T5, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T1, T5, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T1, T5, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T1, T5, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T2, T1, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T2, T1, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T2, T1, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T2, T1, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T2, T1, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T2, T1, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T2, T1, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T2, T1, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T2, T1, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T2, T1, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T2, T1, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T2, T1, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T2, T1, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T2, T1, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T2, T4, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T2, T4, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T2, T4, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T2, T4, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T2, T4, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T2, T4, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T2, T4, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T2, T4, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T2, T4, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T2, T4, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T2, T4, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T2, T4, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T2, T4, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T2, T4, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T2, T5, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T2, T5, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T2, T5, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T2, T5, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T2, T5, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T2, T5, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T2, T5, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T2, T5, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T2, T5, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T2, T5, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T2, T5, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T2, T5, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T2, T5, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T2, T5, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T4, T1, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T4, T1, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T4, T1, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T4, T1, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T4, T1, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T4, T1, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T4, T1, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T4, T1, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T4, T1, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T4, T1, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T4, T1, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T4, T1, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T4, T1, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T4, T1, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T4, T2, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T4, T2, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T4, T2, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T4, T2, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T4, T2, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T4, T2, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T4, T2, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T4, T2, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T4, T2, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T4, T2, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T4, T2, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T4, T2, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T4, T2, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T4, T2, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T4, T5, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T4, T5, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T4, T5, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T4, T5, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T4, T5, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T4, T5, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T4, T5, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T4, T5, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T4, T5, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T4, T5, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T4, T5, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T4, T5, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T4, T5, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T4, T5, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T5, T1, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T5, T1, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T5, T1, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T5, T1, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T5, T1, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T5, T1, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T5, T1, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T5, T1, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T5, T1, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T5, T1, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T5, T1, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T5, T1, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T5, T1, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T5, T1, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T5, T2, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T5, T2, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T5, T2, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T5, T2, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T5, T2, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T5, T2, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T5, T2, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T5, T2, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T5, T2, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T5, T2, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T5, T2, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T5, T2, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T5, T2, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T5, T2, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T5, T4, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T5, T4, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T5, T4, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T5, T4, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T5, T4, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T5, T4, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T5, T4, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T3, T6, T5, T4, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T3, T6, T5, T4, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T3, T6, T5, T4, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T3, T6, T5, T4, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T3, T6, T5, T4, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T3, T6, T5, T4, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T3, T6, T5, T4, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T2, T3, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T2, T3, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T2, T3, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T2, T3, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T2, T3, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T2, T3, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T2, T3, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T2, T3, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T2, T3, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T2, T3, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T2, T3, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T2, T3, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T2, T3, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T2, T3, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T2, T5, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T2, T5, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T2, T5, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T2, T5, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T2, T5, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T2, T5, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T2, T5, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T2, T5, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T2, T5, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T2, T5, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T2, T5, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T2, T5, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T2, T5, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T2, T5, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T2, T6, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T2, T6, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T2, T6, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T2, T6, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T2, T6, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T2, T6, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T2, T6, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T2, T6, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T2, T6, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T2, T6, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T2, T6, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T2, T6, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T2, T6, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T2, T6, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T3, T2, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T3, T2, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T3, T2, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T3, T2, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T3, T2, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T3, T2, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T3, T2, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T3, T2, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T3, T2, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T3, T2, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T3, T2, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T3, T2, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T3, T2, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T3, T2, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T3, T5, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T3, T5, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T3, T5, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T3, T5, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T3, T5, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T3, T5, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T3, T5, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T3, T5, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T3, T5, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T3, T5, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T3, T5, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T3, T5, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T3, T5, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T3, T5, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T3, T6, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T3, T6, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T3, T6, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T3, T6, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T3, T6, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T3, T6, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T3, T6, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T3, T6, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T3, T6, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T3, T6, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T3, T6, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T3, T6, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T3, T6, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T3, T6, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T5, T2, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T5, T2, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T5, T2, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T5, T2, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T5, T2, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T5, T2, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T5, T2, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T5, T2, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T5, T2, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T5, T2, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T5, T2, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T5, T2, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T5, T2, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T5, T2, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T5, T3, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T5, T3, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T5, T3, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T5, T3, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T5, T3, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T5, T3, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T5, T3, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T5, T3, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T5, T3, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T5, T3, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T5, T3, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T5, T3, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T5, T3, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T5, T3, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T5, T6, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T5, T6, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T5, T6, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T5, T6, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T5, T6, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T5, T6, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T5, T6, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T5, T6, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T5, T6, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T5, T6, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T5, T6, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T5, T6, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T5, T6, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T5, T6, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T6, T2, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T6, T2, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T6, T2, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T6, T2, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T6, T2, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T6, T2, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T6, T2, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T6, T2, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T6, T2, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T6, T2, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T6, T2, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T6, T2, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T6, T2, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T6, T2, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T6, T3, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T6, T3, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T6, T3, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T6, T3, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T6, T3, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T6, T3, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T6, T3, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T6, T3, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T6, T3, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T6, T3, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T6, T3, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T6, T3, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T6, T3, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T6, T3, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T6, T5, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T6, T5, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T6, T5, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T6, T5, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T6, T5, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T6, T5, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T6, T5, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T1, T6, T5, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T1, T6, T5, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T1, T6, T5, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T1, T6, T5, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T1, T6, T5, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T1, T6, T5, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T1, T6, T5, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T1, T3, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T1, T3, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T1, T3, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T1, T3, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T1, T3, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T1, T3, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T1, T3, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T1, T3, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T1, T3, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T1, T3, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T1, T3, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T1, T3, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T1, T3, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T1, T3, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T1, T5, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T1, T5, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T1, T5, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T1, T5, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T1, T5, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T1, T5, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T1, T5, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T1, T5, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T1, T5, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T1, T5, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T1, T5, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T1, T5, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T1, T5, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T1, T5, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T1, T6, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T1, T6, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T1, T6, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T1, T6, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T1, T6, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T1, T6, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T1, T6, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T1, T6, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T1, T6, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T1, T6, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T1, T6, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T1, T6, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T1, T6, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T1, T6, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T3, T1, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T3, T1, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T3, T1, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T3, T1, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T3, T1, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T3, T1, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T3, T1, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T3, T1, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T3, T1, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T3, T1, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T3, T1, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T3, T1, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T3, T1, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T3, T1, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T3, T5, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T3, T5, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T3, T5, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T3, T5, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T3, T5, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T3, T5, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T3, T5, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T3, T5, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T3, T5, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T3, T5, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T3, T5, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T3, T5, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T3, T5, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T3, T5, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T3, T6, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T3, T6, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T3, T6, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T3, T6, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T3, T6, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T3, T6, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T3, T6, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T3, T6, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T3, T6, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T3, T6, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T3, T6, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T3, T6, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T3, T6, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T3, T6, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T5, T1, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T5, T1, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T5, T1, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T5, T1, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T5, T1, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T5, T1, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T5, T1, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T5, T1, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T5, T1, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T5, T1, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T5, T1, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T5, T1, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T5, T1, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T5, T1, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T5, T3, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T5, T3, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T5, T3, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T5, T3, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T5, T3, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T5, T3, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T5, T3, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T5, T3, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T5, T3, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T5, T3, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T5, T3, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T5, T3, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T5, T3, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T5, T3, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T5, T6, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T5, T6, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T5, T6, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T5, T6, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T5, T6, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T5, T6, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T5, T6, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T5, T6, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T5, T6, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T5, T6, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T5, T6, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T5, T6, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T5, T6, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T5, T6, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T6, T1, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T6, T1, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T6, T1, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T6, T1, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T6, T1, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T6, T1, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T6, T1, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T6, T1, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T6, T1, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T6, T1, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T6, T1, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T6, T1, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T6, T1, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T6, T1, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T6, T3, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T6, T3, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T6, T3, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T6, T3, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T6, T3, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T6, T3, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T6, T3, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T6, T3, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T6, T3, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T6, T3, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T6, T3, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T6, T3, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T6, T3, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T6, T3, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T6, T5, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T6, T5, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T6, T5, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T6, T5, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T6, T5, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T6, T5, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T6, T5, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T2, T6, T5, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T2, T6, T5, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T2, T6, T5, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T2, T6, T5, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T2, T6, T5, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T2, T6, T5, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T2, T6, T5, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T1, T2, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T1, T2, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T1, T2, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T1, T2, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T1, T2, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T1, T2, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T1, T2, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T1, T2, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T1, T2, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T1, T2, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T1, T2, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T1, T2, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T1, T2, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T1, T2, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T1, T5, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T1, T5, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T1, T5, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T1, T5, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T1, T5, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T1, T5, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T1, T5, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T1, T5, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T1, T5, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T1, T5, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T1, T5, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T1, T5, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T1, T5, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T1, T5, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T1, T6, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T1, T6, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T1, T6, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T1, T6, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T1, T6, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T1, T6, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T1, T6, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T1, T6, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T1, T6, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T1, T6, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T1, T6, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T1, T6, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T1, T6, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T1, T6, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T2, T1, T5, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T2, T1, T5, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T2, T1, T5, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T2, T1, T5, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T2, T1, T5, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T2, T1, T5, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T2, T1, T5, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T2, T1, T6, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T2, T1, T6, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T2, T1, T6, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T2, T1, T6, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T2, T1, T6, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T2, T1, T6, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T2, T1, T6, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T2, T5, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T2, T5, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T2, T5, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T2, T5, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T2, T5, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T2, T5, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T2, T5, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T2, T5, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T2, T5, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T2, T5, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T2, T5, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T2, T5, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T2, T5, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T2, T5, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T2, T6, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T2, T6, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T2, T6, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T2, T6, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T2, T6, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T2, T6, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T2, T6, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T2, T6, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T2, T6, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T2, T6, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T2, T6, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T2, T6, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T2, T6, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T2, T6, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T5, T1, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T5, T1, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T5, T1, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T5, T1, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T5, T1, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T5, T1, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T5, T1, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T5, T1, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T5, T1, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T5, T1, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T5, T1, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T5, T1, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T5, T1, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T5, T1, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T5, T2, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T5, T2, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T5, T2, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T5, T2, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T5, T2, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T5, T2, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T5, T2, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T5, T2, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T5, T2, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T5, T2, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T5, T2, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T5, T2, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T5, T2, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T5, T2, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T5, T6, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T5, T6, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T5, T6, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T5, T6, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T5, T6, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T5, T6, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T5, T6, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T5, T6, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T5, T6, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T5, T6, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T5, T6, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T5, T6, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T5, T6, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T5, T6, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T6, T1, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T6, T1, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T6, T1, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T6, T1, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T6, T1, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T6, T1, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T6, T1, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T6, T1, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T6, T1, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T6, T1, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T6, T1, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T6, T1, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T6, T1, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T6, T1, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T6, T2, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T6, T2, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T6, T2, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T6, T2, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T6, T2, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T6, T2, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T6, T2, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T6, T2, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T6, T2, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T6, T2, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T6, T2, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T6, T2, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T6, T2, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T6, T2, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T6, T5, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T6, T5, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T6, T5, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T6, T5, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T6, T5, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T6, T5, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T6, T5, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T3, T6, T5, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T3, T6, T5, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T3, T6, T5, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T3, T6, T5, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T3, T6, T5, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T3, T6, T5, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T3, T6, T5, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T1, T2, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T1, T2, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T1, T2, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T1, T2, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T1, T2, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T1, T2, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T1, T2, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T1, T2, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T1, T2, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T1, T2, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T1, T2, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T1, T2, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T1, T2, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T1, T2, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T1, T3, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T1, T3, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T1, T3, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T1, T3, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T1, T3, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T1, T3, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T1, T3, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T1, T3, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T1, T3, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T1, T3, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T1, T3, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T1, T3, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T1, T3, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T1, T3, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T1, T6, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T1, T6, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T1, T6, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T1, T6, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T1, T6, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T1, T6, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T1, T6, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T1, T6, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T1, T6, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T1, T6, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T1, T6, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T1, T6, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T1, T6, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T1, T6, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T2, T1, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T2, T1, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T2, T1, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T2, T1, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T2, T1, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T2, T1, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T2, T1, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T2, T1, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T2, T1, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T2, T1, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T2, T1, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T2, T1, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T2, T1, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T2, T1, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T2, T3, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T2, T3, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T2, T3, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T2, T3, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T2, T3, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T2, T3, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T2, T3, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T2, T3, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T2, T3, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T2, T3, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T2, T3, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T2, T3, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T2, T3, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T2, T3, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T2, T6, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T2, T6, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T2, T6, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T2, T6, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T2, T6, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T2, T6, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T2, T6, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T2, T6, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T2, T6, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T2, T6, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T2, T6, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T2, T6, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T2, T6, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T2, T6, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T3, T1, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T3, T1, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T3, T1, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T3, T1, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T3, T1, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T3, T1, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T3, T1, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T3, T1, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T3, T1, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T3, T1, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T3, T1, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T3, T1, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T3, T1, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T3, T1, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T3, T2, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T3, T2, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T3, T2, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T3, T2, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T3, T2, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T3, T2, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T3, T2, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T3, T2, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T3, T2, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T3, T2, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T3, T2, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T3, T2, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T3, T2, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T3, T2, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T3, T6, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T3, T6, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T3, T6, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T3, T6, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T3, T6, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T3, T6, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T3, T6, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T3, T6, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T3, T6, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T3, T6, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T3, T6, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T3, T6, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T3, T6, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T3, T6, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T6, T1, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T6, T1, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T6, T1, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T6, T1, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T6, T1, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T6, T1, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T6, T1, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T6, T1, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T6, T1, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T6, T1, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T6, T1, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T6, T1, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T6, T1, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T6, T1, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T6, T2, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T6, T2, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T6, T2, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T6, T2, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T6, T2, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T6, T2, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T6, T2, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T6, T2, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T6, T2, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T6, T2, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T6, T2, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T6, T2, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T6, T2, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T6, T2, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T6, T3, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T6, T3, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T6, T3, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T6, T3, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T6, T3, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T6, T3, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T6, T3, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T5, T6, T3, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T5, T6, T3, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T5, T6, T3, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T5, T6, T3, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T5, T6, T3, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T5, T6, T3, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T5, T6, T3, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T1, T2, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T1, T2, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T1, T2, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T1, T2, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T1, T2, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T1, T2, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T1, T2, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T1, T2, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T1, T2, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T1, T2, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T1, T2, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T1, T2, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T1, T2, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T1, T2, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T1, T3, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T1, T3, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T1, T3, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T1, T3, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T1, T3, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T1, T3, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T1, T3, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T1, T3, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T1, T3, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T1, T3, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T1, T3, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T1, T3, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T1, T3, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T1, T3, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T1, T5, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T1, T5, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T1, T5, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T1, T5, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T1, T5, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T1, T5, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T1, T5, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T1, T5, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T1, T5, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T1, T5, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T1, T5, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T1, T5, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T1, T5, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T1, T5, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T2, T1, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T2, T1, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T2, T1, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T2, T1, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T2, T1, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T2, T1, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T2, T1, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T2, T1, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T2, T1, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T2, T1, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T2, T1, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T2, T1, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T2, T1, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T2, T1, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T2, T3, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T2, T3, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T2, T3, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T2, T3, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T2, T3, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T2, T3, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T2, T3, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T2, T3, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T2, T3, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T2, T3, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T2, T3, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T2, T3, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T2, T3, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T2, T3, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T2, T5, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T2, T5, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T2, T5, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T2, T5, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T2, T5, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T2, T5, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T2, T5, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T2, T5, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T2, T5, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T2, T5, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T2, T5, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T2, T5, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T2, T5, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T2, T5, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T3, T1, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T3, T1, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T3, T1, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T3, T1, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T3, T1, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T3, T1, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T3, T1, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T3, T1, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T3, T1, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T3, T1, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T3, T1, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T3, T1, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T3, T1, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T3, T1, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T3, T2, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T3, T2, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T3, T2, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T3, T2, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T3, T2, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T3, T2, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T3, T2, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T3, T2, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T3, T2, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T3, T2, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T3, T2, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T3, T2, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T3, T2, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T3, T2, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T3, T5, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T3, T5, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T3, T5, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T3, T5, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T3, T5, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T3, T5, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T3, T5, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T3, T5, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T3, T5, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T3, T5, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T3, T5, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T3, T5, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T3, T5, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T3, T5, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T5, T1, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T5, T1, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T5, T1, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T5, T1, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T5, T1, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T5, T1, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T5, T1, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T5, T1, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T5, T1, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T5, T1, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T5, T1, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T5, T1, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T5, T1, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T5, T1, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T5, T2, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T5, T2, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T5, T2, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T5, T2, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T5, T2, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T5, T2, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T5, T2, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T5, T2, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T5, T2, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T5, T2, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T5, T2, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T5, T2, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T5, T2, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T5, T2, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T5, T3, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T5, T3, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T5, T3, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T5, T3, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T5, T3, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T5, T3, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T5, T3, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T4, T6, T5, T3, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T4, T6, T5, T3, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T4, T6, T5, T3, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T4, T6, T5, T3, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T4, T6, T5, T3, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T4, T6, T5, T3, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T4, T6, T5, T3, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T2, T3, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T2, T3, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T2, T3, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T2, T3, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T2, T3, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T2, T3, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T2, T3, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T2, T3, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T2, T3, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T2, T3, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T2, T3, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T2, T3, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T2, T3, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T2, T3, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T2, T4, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T2, T4, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T2, T4, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T2, T4, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T2, T4, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T2, T4, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T2, T4, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T2, T4, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T2, T4, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T2, T4, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T2, T4, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T2, T4, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T2, T4, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T2, T4, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T2, T6, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T2, T6, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T2, T6, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T2, T6, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T2, T6, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T2, T6, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T2, T6, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T2, T6, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T2, T6, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T2, T6, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T2, T6, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T2, T6, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T2, T6, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T2, T6, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T3, T2, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T3, T2, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T3, T2, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T3, T2, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T3, T2, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T3, T2, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T3, T2, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T3, T2, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T3, T2, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T3, T2, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T3, T2, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T3, T2, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T3, T2, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T3, T2, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T3, T4, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T3, T4, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T3, T4, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T3, T4, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T3, T4, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T3, T4, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T3, T4, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T3, T4, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T3, T4, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T3, T4, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T3, T4, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T3, T4, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T3, T4, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T3, T4, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T3, T6, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T3, T6, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T3, T6, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T3, T6, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T3, T6, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T3, T6, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T3, T6, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T3, T6, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T3, T6, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T3, T6, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T3, T6, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T3, T6, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T3, T6, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T3, T6, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T4, T2, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T4, T2, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T4, T2, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T4, T2, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T4, T2, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T4, T2, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T4, T2, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T4, T2, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T4, T2, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T4, T2, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T4, T2, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T4, T2, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T4, T2, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T4, T2, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T4, T3, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T4, T3, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T4, T3, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T4, T3, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T4, T3, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T4, T3, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T4, T3, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T4, T3, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T4, T3, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T4, T3, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T4, T3, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T4, T3, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T4, T3, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T4, T3, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T4, T6, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T4, T6, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T4, T6, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T4, T6, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T4, T6, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T4, T6, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T4, T6, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T4, T6, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T4, T6, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T4, T6, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T4, T6, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T4, T6, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T4, T6, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T4, T6, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T6, T2, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T6, T2, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T6, T2, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T6, T2, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T6, T2, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T6, T2, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T6, T2, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T6, T2, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T6, T2, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T6, T2, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T6, T2, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T6, T2, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T6, T2, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T6, T2, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T6, T3, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T6, T3, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T6, T3, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T6, T3, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T6, T3, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T6, T3, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T6, T3, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T6, T3, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T6, T3, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T6, T3, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T6, T3, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T6, T3, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T6, T3, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T6, T3, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T6, T4, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T6, T4, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T6, T4, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T6, T4, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T6, T4, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T6, T4, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T6, T4, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T1, T6, T4, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T1, T6, T4, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T1, T6, T4, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T1, T6, T4, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T1, T6, T4, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T1, T6, T4, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T1, T6, T4, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T1, T3, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T1, T3, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T1, T3, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T1, T3, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T1, T3, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T1, T3, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T1, T3, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T1, T3, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T1, T3, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T1, T3, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T1, T3, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T1, T3, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T1, T3, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T1, T3, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T1, T4, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T1, T4, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T1, T4, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T1, T4, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T1, T4, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T1, T4, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T1, T4, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T1, T4, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T1, T4, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T1, T4, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T1, T4, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T1, T4, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T1, T4, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T1, T4, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T1, T6, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T1, T6, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T1, T6, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T1, T6, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T1, T6, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T1, T6, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T1, T6, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T1, T6, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T1, T6, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T1, T6, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T1, T6, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T1, T6, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T1, T6, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T1, T6, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T3, T1, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T3, T1, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T3, T1, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T3, T1, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T3, T1, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T3, T1, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T3, T1, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T3, T1, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T3, T1, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T3, T1, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T3, T1, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T3, T1, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T3, T1, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T3, T1, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T3, T4, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T3, T4, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T3, T4, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T3, T4, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T3, T4, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T3, T4, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T3, T4, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T3, T4, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T3, T4, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T3, T4, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T3, T4, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T3, T4, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T3, T4, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T3, T4, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T3, T6, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T3, T6, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T3, T6, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T3, T6, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T3, T6, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T3, T6, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T3, T6, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T3, T6, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T3, T6, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T3, T6, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T3, T6, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T3, T6, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T3, T6, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T3, T6, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T4, T1, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T4, T1, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T4, T1, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T4, T1, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T4, T1, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T4, T1, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T4, T1, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T4, T1, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T4, T1, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T4, T1, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T4, T1, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T4, T1, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T4, T1, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T4, T1, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T4, T3, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T4, T3, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T4, T3, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T4, T3, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T4, T3, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T4, T3, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T4, T3, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T4, T3, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T4, T3, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T4, T3, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T4, T3, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T4, T3, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T4, T3, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T4, T3, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T4, T6, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T4, T6, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T4, T6, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T4, T6, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T4, T6, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T4, T6, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T4, T6, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T4, T6, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T4, T6, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T4, T6, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T4, T6, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T4, T6, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T4, T6, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T4, T6, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T6, T1, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T6, T1, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T6, T1, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T6, T1, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T6, T1, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T6, T1, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T6, T1, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T6, T1, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T6, T1, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T6, T1, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T6, T1, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T6, T1, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T6, T1, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T6, T1, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T6, T3, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T6, T3, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T6, T3, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T6, T3, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T6, T3, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T6, T3, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T6, T3, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T6, T3, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T6, T3, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T6, T3, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T6, T3, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T6, T3, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T6, T3, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T6, T3, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T6, T4, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T6, T4, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T6, T4, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T6, T4, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T6, T4, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T6, T4, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T6, T4, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T2, T6, T4, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T2, T6, T4, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T2, T6, T4, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T2, T6, T4, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T2, T6, T4, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T2, T6, T4, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T2, T6, T4, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T1, T2, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T1, T2, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T1, T2, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T1, T2, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T1, T2, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T1, T2, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T1, T2, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T1, T2, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T1, T2, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T1, T2, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T1, T2, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T1, T2, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T1, T2, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T1, T2, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T1, T4, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T1, T4, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T1, T4, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T1, T4, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T1, T4, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T1, T4, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T1, T4, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T1, T4, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T1, T4, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T1, T4, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T1, T4, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T1, T4, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T1, T4, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T1, T4, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T1, T6, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T1, T6, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T1, T6, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T1, T6, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T1, T6, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T1, T6, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T1, T6, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T1, T6, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T1, T6, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T1, T6, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T1, T6, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T1, T6, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T1, T6, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T1, T6, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T2, T1, T4, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T2, T1, T4, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T2, T1, T4, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T2, T1, T4, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T2, T1, T4, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T2, T1, T4, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T2, T1, T4, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T2, T1, T6, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T2, T1, T6, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T2, T1, T6, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T2, T1, T6, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T2, T1, T6, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T2, T1, T6, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T2, T1, T6, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T2, T4, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T2, T4, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T2, T4, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T2, T4, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T2, T4, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T2, T4, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T2, T4, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T2, T4, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T2, T4, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T2, T4, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T2, T4, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T2, T4, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T2, T4, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T2, T4, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T2, T6, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T2, T6, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T2, T6, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T2, T6, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T2, T6, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T2, T6, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T2, T6, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T2, T6, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T2, T6, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T2, T6, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T2, T6, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T2, T6, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T2, T6, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T2, T6, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T4, T1, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T4, T1, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T4, T1, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T4, T1, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T4, T1, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T4, T1, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T4, T1, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T4, T1, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T4, T1, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T4, T1, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T4, T1, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T4, T1, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T4, T1, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T4, T1, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T4, T2, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T4, T2, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T4, T2, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T4, T2, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T4, T2, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T4, T2, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T4, T2, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T4, T2, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T4, T2, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T4, T2, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T4, T2, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T4, T2, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T4, T2, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T4, T2, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T4, T6, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T4, T6, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T4, T6, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T4, T6, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T4, T6, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T4, T6, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T4, T6, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T4, T6, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T4, T6, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T4, T6, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T4, T6, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T4, T6, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T4, T6, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T4, T6, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T6, T1, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T6, T1, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T6, T1, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T6, T1, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T6, T1, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T6, T1, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T6, T1, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T6, T1, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T6, T1, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T6, T1, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T6, T1, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T6, T1, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T6, T1, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T6, T1, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T6, T2, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T6, T2, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T6, T2, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T6, T2, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T6, T2, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T6, T2, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T6, T2, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T6, T2, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T6, T2, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T6, T2, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T6, T2, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T6, T2, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T6, T2, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T6, T2, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T6, T4, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T6, T4, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T6, T4, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T6, T4, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T6, T4, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T6, T4, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T6, T4, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T3, T6, T4, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T3, T6, T4, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T3, T6, T4, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T3, T6, T4, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T3, T6, T4, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T3, T6, T4, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T3, T6, T4, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T1, T2, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T1, T2, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T1, T2, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T1, T2, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T1, T2, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T1, T2, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T1, T2, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T1, T2, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T1, T2, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T1, T2, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T1, T2, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T1, T2, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T1, T2, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T1, T2, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T1, T3, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T1, T3, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T1, T3, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T1, T3, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T1, T3, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T1, T3, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T1, T3, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T1, T3, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T1, T3, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T1, T3, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T1, T3, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T1, T3, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T1, T3, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T1, T3, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T1, T6, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T1, T6, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T1, T6, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T1, T6, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T1, T6, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T1, T6, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T1, T6, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T1, T6, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T1, T6, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T1, T6, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T1, T6, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T1, T6, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T1, T6, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T1, T6, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T2, T1, T3, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T2, T1, T3, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T2, T1, T3, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T2, T1, T3, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T2, T1, T3, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T2, T1, T3, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T2, T1, T3, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T2, T1, T6, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T2, T1, T6, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T2, T1, T6, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T2, T1, T6, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T2, T1, T6, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T2, T1, T6, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T2, T1, T6, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T2, T3, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T2, T3, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T2, T3, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T2, T3, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T2, T3, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T2, T3, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T2, T3, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T2, T3, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T2, T3, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T2, T3, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T2, T3, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T2, T3, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T2, T3, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T2, T3, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T2, T6, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T2, T6, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T2, T6, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T2, T6, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T2, T6, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T2, T6, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T2, T6, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T2, T6, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T2, T6, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T2, T6, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T2, T6, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T2, T6, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T2, T6, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T2, T6, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T3, T1, T2, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T3, T1, T2, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T3, T1, T2, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T3, T1, T2, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T3, T1, T2, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T3, T1, T2, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T3, T1, T2, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T3, T1, T6, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T3, T1, T6, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T3, T1, T6, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T3, T1, T6, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T3, T1, T6, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T3, T1, T6, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T3, T1, T6, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T3, T2, T1, T6>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T3, T2, T1, T6>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T3, T2, T1, T6>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T3, T2, T1, T6>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T3, T2, T1, T6>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T3, T2, T1, T6>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T3, T2, T1, T6>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T3, T2, T6, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T3, T2, T6, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T3, T2, T6, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T3, T2, T6, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T3, T2, T6, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T3, T2, T6, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T3, T2, T6, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T3, T6, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T3, T6, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T3, T6, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T3, T6, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T3, T6, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T3, T6, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T3, T6, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T3, T6, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T3, T6, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T3, T6, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T3, T6, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T3, T6, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T3, T6, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T3, T6, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T6, T1, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T6, T1, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T6, T1, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T6, T1, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T6, T1, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T6, T1, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T6, T1, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T6, T1, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T6, T1, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T6, T1, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T6, T1, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T6, T1, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T6, T1, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T6, T1, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T6, T2, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T6, T2, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T6, T2, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T6, T2, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T6, T2, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T6, T2, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T6, T2, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T6, T2, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T6, T2, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T6, T2, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T6, T2, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T6, T2, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T6, T2, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T6, T2, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T6, T3, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T6, T3, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T6, T3, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T6, T3, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T6, T3, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T6, T3, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T6, T3, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T4, T6, T3, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T4, T6, T3, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T4, T6, T3, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T4, T6, T3, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T4, T6, T3, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T4, T6, T3, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T4, T6, T3, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T1, T2, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T1, T2, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T1, T2, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T1, T2, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T1, T2, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T1, T2, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T1, T2, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T1, T2, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T1, T2, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T1, T2, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T1, T2, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T1, T2, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T1, T2, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T1, T2, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T1, T3, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T1, T3, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T1, T3, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T1, T3, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T1, T3, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T1, T3, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T1, T3, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T1, T3, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T1, T3, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T1, T3, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T1, T3, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T1, T3, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T1, T3, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T1, T3, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T1, T4, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T1, T4, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T1, T4, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T1, T4, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T1, T4, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T1, T4, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T1, T4, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T1, T4, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T1, T4, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T1, T4, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T1, T4, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T1, T4, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T1, T4, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T1, T4, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T2, T1, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T2, T1, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T2, T1, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T2, T1, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T2, T1, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T2, T1, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T2, T1, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T2, T1, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T2, T1, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T2, T1, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T2, T1, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T2, T1, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T2, T1, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T2, T1, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T2, T3, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T2, T3, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T2, T3, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T2, T3, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T2, T3, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T2, T3, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T2, T3, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T2, T3, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T2, T3, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T2, T3, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T2, T3, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T2, T3, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T2, T3, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T2, T3, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T2, T4, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T2, T4, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T2, T4, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T2, T4, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T2, T4, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T2, T4, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T2, T4, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T2, T4, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T2, T4, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T2, T4, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T2, T4, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T2, T4, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T2, T4, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T2, T4, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T3, T1, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T3, T1, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T3, T1, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T3, T1, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T3, T1, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T3, T1, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T3, T1, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T3, T1, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T3, T1, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T3, T1, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T3, T1, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T3, T1, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T3, T1, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T3, T1, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T3, T2, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T3, T2, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T3, T2, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T3, T2, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T3, T2, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T3, T2, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T3, T2, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T3, T2, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T3, T2, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T3, T2, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T3, T2, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T3, T2, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T3, T2, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T3, T2, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T3, T4, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T3, T4, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T3, T4, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T3, T4, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T3, T4, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T3, T4, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T3, T4, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T3, T4, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T3, T4, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T3, T4, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T3, T4, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T3, T4, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T3, T4, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T3, T4, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T4, T1, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T4, T1, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T4, T1, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T4, T1, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T4, T1, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T4, T1, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T4, T1, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T4, T1, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T4, T1, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T4, T1, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T4, T1, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T4, T1, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T4, T1, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T4, T1, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T4, T2, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T4, T2, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T4, T2, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T4, T2, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T4, T2, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T4, T2, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T4, T2, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T4, T2, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T4, T2, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T4, T2, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T4, T2, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T4, T2, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T4, T2, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T4, T2, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T4, T3, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T4, T3, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T4, T3, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T4, T3, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T4, T3, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T4, T3, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T4, T3, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T5, T6, T4, T3, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T5, T6, T4, T3, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T5, T6, T4, T3, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T5, T6, T4, T3, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T5, T6, T4, T3, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T5, T6, T4, T3, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T5, T6, T4, T3, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T2, T3, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T2, T3, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T2, T3, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T2, T3, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T2, T3, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T2, T3, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T2, T3, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T2, T3, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T2, T3, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T2, T3, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T2, T3, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T2, T3, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T2, T3, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T2, T3, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T2, T4, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T2, T4, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T2, T4, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T2, T4, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T2, T4, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T2, T4, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T2, T4, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T2, T4, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T2, T4, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T2, T4, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T2, T4, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T2, T4, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T2, T4, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T2, T4, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T2, T5, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T2, T5, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T2, T5, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T2, T5, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T2, T5, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T2, T5, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T2, T5, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T2, T5, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T2, T5, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T2, T5, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T2, T5, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T2, T5, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T2, T5, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T2, T5, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T3, T2, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T3, T2, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T3, T2, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T3, T2, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T3, T2, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T3, T2, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T3, T2, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T3, T2, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T3, T2, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T3, T2, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T3, T2, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T3, T2, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T3, T2, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T3, T2, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T3, T4, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T3, T4, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T3, T4, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T3, T4, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T3, T4, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T3, T4, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T3, T4, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T3, T4, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T3, T4, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T3, T4, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T3, T4, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T3, T4, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T3, T4, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T3, T4, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T3, T5, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T3, T5, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T3, T5, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T3, T5, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T3, T5, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T3, T5, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T3, T5, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T3, T5, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T3, T5, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T3, T5, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T3, T5, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T3, T5, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T3, T5, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T3, T5, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T4, T2, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T4, T2, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T4, T2, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T4, T2, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T4, T2, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T4, T2, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T4, T2, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T4, T2, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T4, T2, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T4, T2, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T4, T2, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T4, T2, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T4, T2, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T4, T2, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T4, T3, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T4, T3, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T4, T3, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T4, T3, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T4, T3, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T4, T3, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T4, T3, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T4, T3, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T4, T3, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T4, T3, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T4, T3, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T4, T3, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T4, T3, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T4, T3, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T4, T5, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T4, T5, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T4, T5, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T4, T5, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T4, T5, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T4, T5, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T4, T5, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T4, T5, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T4, T5, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T4, T5, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T4, T5, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T4, T5, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T4, T5, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T4, T5, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T5, T2, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T5, T2, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T5, T2, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T5, T2, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T5, T2, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T5, T2, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T5, T2, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T5, T2, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T5, T2, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T5, T2, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T5, T2, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T5, T2, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T5, T2, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T5, T2, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T5, T3, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T5, T3, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T5, T3, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T5, T3, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T5, T3, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T5, T3, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T5, T3, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T5, T3, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T5, T3, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T5, T3, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T5, T3, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T5, T3, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T5, T3, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T5, T3, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T5, T4, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T5, T4, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T5, T4, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T5, T4, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T5, T4, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T5, T4, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T5, T4, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T1, T5, T4, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T1, T5, T4, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T1, T5, T4, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T1, T5, T4, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T1, T5, T4, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T1, T5, T4, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T1, T5, T4, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T1, T3, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T1, T3, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T1, T3, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T1, T3, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T1, T3, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T1, T3, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T1, T3, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T1, T3, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T1, T3, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T1, T3, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T1, T3, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T1, T3, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T1, T3, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T1, T3, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T1, T4, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T1, T4, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T1, T4, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T1, T4, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T1, T4, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T1, T4, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T1, T4, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T1, T4, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T1, T4, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T1, T4, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T1, T4, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T1, T4, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T1, T4, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T1, T4, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T1, T5, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T1, T5, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T1, T5, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T1, T5, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T1, T5, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T1, T5, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T1, T5, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T1, T5, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T1, T5, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T1, T5, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T1, T5, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T1, T5, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T1, T5, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T1, T5, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T3, T1, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T3, T1, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T3, T1, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T3, T1, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T3, T1, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T3, T1, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T3, T1, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T3, T1, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T3, T1, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T3, T1, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T3, T1, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T3, T1, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T3, T1, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T3, T1, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T3, T4, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T3, T4, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T3, T4, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T3, T4, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T3, T4, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T3, T4, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T3, T4, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T3, T4, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T3, T4, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T3, T4, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T3, T4, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T3, T4, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T3, T4, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T3, T4, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T3, T5, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T3, T5, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T3, T5, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T3, T5, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T3, T5, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T3, T5, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T3, T5, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T3, T5, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T3, T5, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T3, T5, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T3, T5, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T3, T5, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T3, T5, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T3, T5, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T4, T1, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T4, T1, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T4, T1, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T4, T1, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T4, T1, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T4, T1, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T4, T1, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T4, T1, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T4, T1, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T4, T1, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T4, T1, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T4, T1, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T4, T1, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T4, T1, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T4, T3, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T4, T3, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T4, T3, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T4, T3, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T4, T3, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T4, T3, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T4, T3, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T4, T3, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T4, T3, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T4, T3, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T4, T3, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T4, T3, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T4, T3, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T4, T3, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T4, T5, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T4, T5, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T4, T5, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T4, T5, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T4, T5, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T4, T5, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T4, T5, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T4, T5, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T4, T5, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T4, T5, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T4, T5, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T4, T5, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T4, T5, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T4, T5, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T5, T1, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T5, T1, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T5, T1, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T5, T1, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T5, T1, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T5, T1, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T5, T1, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T5, T1, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T5, T1, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T5, T1, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T5, T1, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T5, T1, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T5, T1, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T5, T1, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T5, T3, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T5, T3, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T5, T3, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T5, T3, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T5, T3, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T5, T3, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T5, T3, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T5, T3, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T5, T3, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T5, T3, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T5, T3, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T5, T3, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T5, T3, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T5, T3, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T5, T4, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T5, T4, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T5, T4, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T5, T4, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T5, T4, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T5, T4, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T5, T4, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T2, T5, T4, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T2, T5, T4, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T2, T5, T4, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T2, T5, T4, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T2, T5, T4, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T2, T5, T4, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T2, T5, T4, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T1, T2, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T1, T2, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T1, T2, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T1, T2, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T1, T2, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T1, T2, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T1, T2, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T1, T2, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T1, T2, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T1, T2, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T1, T2, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T1, T2, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T1, T2, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T1, T2, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T1, T4, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T1, T4, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T1, T4, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T1, T4, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T1, T4, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T1, T4, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T1, T4, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T1, T4, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T1, T4, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T1, T4, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T1, T4, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T1, T4, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T1, T4, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T1, T4, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T1, T5, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T1, T5, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T1, T5, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T1, T5, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T1, T5, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T1, T5, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T1, T5, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T1, T5, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T1, T5, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T1, T5, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T1, T5, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T1, T5, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T1, T5, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T1, T5, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T2, T1, T4, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T2, T1, T4, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T2, T1, T4, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T2, T1, T4, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T2, T1, T4, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T2, T1, T4, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T2, T1, T4, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T2, T1, T5, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T2, T1, T5, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T2, T1, T5, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T2, T1, T5, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T2, T1, T5, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T2, T1, T5, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T2, T1, T5, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T2, T4, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T2, T4, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T2, T4, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T2, T4, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T2, T4, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T2, T4, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T2, T4, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T2, T4, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T2, T4, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T2, T4, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T2, T4, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T2, T4, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T2, T4, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T2, T4, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T2, T5, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T2, T5, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T2, T5, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T2, T5, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T2, T5, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T2, T5, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T2, T5, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T2, T5, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T2, T5, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T2, T5, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T2, T5, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T2, T5, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T2, T5, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T2, T5, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T4, T1, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T4, T1, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T4, T1, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T4, T1, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T4, T1, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T4, T1, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T4, T1, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T4, T1, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T4, T1, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T4, T1, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T4, T1, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T4, T1, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T4, T1, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T4, T1, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T4, T2, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T4, T2, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T4, T2, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T4, T2, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T4, T2, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T4, T2, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T4, T2, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T4, T2, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T4, T2, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T4, T2, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T4, T2, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T4, T2, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T4, T2, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T4, T2, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T4, T5, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T4, T5, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T4, T5, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T4, T5, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T4, T5, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T4, T5, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T4, T5, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T4, T5, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T4, T5, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T4, T5, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T4, T5, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T4, T5, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T4, T5, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T4, T5, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T5, T1, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T5, T1, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T5, T1, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T5, T1, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T5, T1, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T5, T1, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T5, T1, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T5, T1, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T5, T1, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T5, T1, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T5, T1, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T5, T1, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T5, T1, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T5, T1, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T5, T2, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T5, T2, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T5, T2, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T5, T2, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T5, T2, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T5, T2, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T5, T2, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T5, T2, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T5, T2, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T5, T2, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T5, T2, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T5, T2, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T5, T2, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T5, T2, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T5, T4, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T5, T4, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T5, T4, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T5, T4, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T5, T4, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T5, T4, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T5, T4, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T3, T5, T4, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T3, T5, T4, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T3, T5, T4, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T3, T5, T4, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T3, T5, T4, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T3, T5, T4, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T3, T5, T4, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T1, T2, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T1, T2, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T1, T2, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T1, T2, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T1, T2, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T1, T2, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T1, T2, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T1, T2, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T1, T2, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T1, T2, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T1, T2, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T1, T2, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T1, T2, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T1, T2, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T1, T3, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T1, T3, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T1, T3, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T1, T3, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T1, T3, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T1, T3, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T1, T3, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T1, T3, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T1, T3, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T1, T3, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T1, T3, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T1, T3, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T1, T3, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T1, T3, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T1, T5, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T1, T5, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T1, T5, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T1, T5, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T1, T5, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T1, T5, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T1, T5, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T1, T5, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T1, T5, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T1, T5, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T1, T5, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T1, T5, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T1, T5, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T1, T5, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T2, T1, T3, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T2, T1, T3, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T2, T1, T3, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T2, T1, T3, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T2, T1, T3, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T2, T1, T3, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T2, T1, T3, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T2, T1, T5, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T2, T1, T5, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T2, T1, T5, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T2, T1, T5, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T2, T1, T5, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T2, T1, T5, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T2, T1, T5, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T2, T3, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T2, T3, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T2, T3, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T2, T3, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T2, T3, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T2, T3, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T2, T3, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T2, T3, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T2, T3, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T2, T3, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T2, T3, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T2, T3, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T2, T3, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T2, T3, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T2, T5, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T2, T5, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T2, T5, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T2, T5, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T2, T5, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T2, T5, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T2, T5, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T2, T5, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T2, T5, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T2, T5, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T2, T5, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T2, T5, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T2, T5, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T2, T5, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T3, T1, T2, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T3, T1, T2, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T3, T1, T2, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T3, T1, T2, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T3, T1, T2, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T3, T1, T2, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T3, T1, T2, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T3, T1, T5, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T3, T1, T5, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T3, T1, T5, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T3, T1, T5, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T3, T1, T5, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T3, T1, T5, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T3, T1, T5, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T3, T2, T1, T5>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T3, T2, T1, T5>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T3, T2, T1, T5>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T3, T2, T1, T5>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T3, T2, T1, T5>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T3, T2, T1, T5>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T3, T2, T1, T5>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T3, T2, T5, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T3, T2, T5, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T3, T2, T5, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T3, T2, T5, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T3, T2, T5, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T3, T2, T5, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T3, T2, T5, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T3, T5, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T3, T5, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T3, T5, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T3, T5, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T3, T5, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T3, T5, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T3, T5, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T3, T5, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T3, T5, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T3, T5, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T3, T5, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T3, T5, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T3, T5, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T3, T5, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T5, T1, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T5, T1, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T5, T1, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T5, T1, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T5, T1, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T5, T1, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T5, T1, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T5, T1, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T5, T1, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T5, T1, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T5, T1, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T5, T1, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T5, T1, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T5, T1, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T5, T2, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T5, T2, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T5, T2, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T5, T2, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T5, T2, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T5, T2, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T5, T2, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T5, T2, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T5, T2, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T5, T2, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T5, T2, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T5, T2, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T5, T2, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T5, T2, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T5, T3, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T5, T3, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T5, T3, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T5, T3, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T5, T3, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T5, T3, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T5, T3, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T4, T5, T3, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T4, T5, T3, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T4, T5, T3, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T4, T5, T3, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T4, T5, T3, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T4, T5, T3, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T4, T5, T3, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T1, T2, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T1, T2, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T1, T2, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T1, T2, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T1, T2, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T1, T2, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T1, T2, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T1, T2, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T1, T2, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T1, T2, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T1, T2, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T1, T2, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T1, T2, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T1, T2, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T1, T3, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T1, T3, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T1, T3, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T1, T3, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T1, T3, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T1, T3, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T1, T3, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T1, T3, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T1, T3, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T1, T3, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T1, T3, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T1, T3, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T1, T3, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T1, T3, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T1, T4, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T1, T4, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T1, T4, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T1, T4, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T1, T4, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T1, T4, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T1, T4, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T1, T4, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T1, T4, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T1, T4, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T1, T4, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T1, T4, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T1, T4, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T1, T4, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T2, T1, T3, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T2, T1, T3, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T2, T1, T3, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T2, T1, T3, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T2, T1, T3, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T2, T1, T3, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T2, T1, T3, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T2, T1, T4, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T2, T1, T4, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T2, T1, T4, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T2, T1, T4, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T2, T1, T4, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T2, T1, T4, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T2, T1, T4, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T2, T3, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T2, T3, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T2, T3, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T2, T3, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T2, T3, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T2, T3, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T2, T3, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T2, T3, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T2, T3, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T2, T3, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T2, T3, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T2, T3, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T2, T3, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T2, T3, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T2, T4, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T2, T4, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T2, T4, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T2, T4, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T2, T4, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T2, T4, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T2, T4, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T2, T4, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T2, T4, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T2, T4, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T2, T4, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T2, T4, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T2, T4, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T2, T4, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T3, T1, T2, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T3, T1, T2, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T3, T1, T2, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T3, T1, T2, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T3, T1, T2, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T3, T1, T2, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T3, T1, T2, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T3, T1, T4, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T3, T1, T4, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T3, T1, T4, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T3, T1, T4, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T3, T1, T4, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T3, T1, T4, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T3, T1, T4, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T3, T2, T1, T4>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T3, T2, T1, T4>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T3, T2, T1, T4>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T3, T2, T1, T4>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T3, T2, T1, T4>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T3, T2, T1, T4>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T3, T2, T1, T4>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T3, T2, T4, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T3, T2, T4, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T3, T2, T4, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T3, T2, T4, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T3, T2, T4, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T3, T2, T4, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T3, T2, T4, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T3, T4, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T3, T4, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T3, T4, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T3, T4, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T3, T4, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T3, T4, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T3, T4, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T3, T4, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T3, T4, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T3, T4, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T3, T4, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T3, T4, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T3, T4, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T3, T4, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T4, T1, T2, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T4, T1, T2, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T4, T1, T2, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T4, T1, T2, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T4, T1, T2, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T4, T1, T2, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T4, T1, T2, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T4, T1, T3, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T4, T1, T3, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T4, T1, T3, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T4, T1, T3, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T4, T1, T3, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T4, T1, T3, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T4, T1, T3, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T4, T2, T1, T3>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T4, T2, T1, T3>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T4, T2, T1, T3>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T4, T2, T1, T3>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T4, T2, T1, T3>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T4, T2, T1, T3>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T4, T2, T1, T3>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T4, T2, T3, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T4, T2, T3, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T4, T2, T3, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T4, T2, T3, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T4, T2, T3, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T4, T2, T3, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T4, T2, T3, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T4, T3, T1, T2>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T4, T3, T1, T2>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T4, T3, T1, T2>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T4, T3, T1, T2>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T4, T3, T1, T2>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T4, T3, T1, T2>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T4, T3, T1, T2>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
        public static implicit operator Either<T6, T5, T4, T3, T2, T1>(EitherBase<T1, T2, T3, T4, T5, T6> either) {
            var item1 = either.Item1;
            if (item1 != null) {
                return new Either<T6, T5, T4, T3, T2, T1>(item1);
            }
            var item2 = either.Item2;
            if (item2 != null) {
                return new Either<T6, T5, T4, T3, T2, T1>(item2);
            }
            var item3 = either.Item3;
            if (item3 != null) {
                return new Either<T6, T5, T4, T3, T2, T1>(item3);
            }
            var item4 = either.Item4;
            if (item4 != null) {
                return new Either<T6, T5, T4, T3, T2, T1>(item4);
            }
            var item5 = either.Item5;
            if (item5 != null) {
                return new Either<T6, T5, T4, T3, T2, T1>(item5);
            }
            var item6 = either.Item6;
            if (item6 != null) {
                return new Either<T6, T5, T4, T3, T2, T1>(item6);
            }
            throw new InvalidOperationException("The Either has no values");
        }
public ConvertibleTo<TBase>.IEither<T1, T2, T3, T4, T5, T6> ConvertTo<TBase>() {
var item1 = Item1;
if (item1 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>(item1);
}
var item2 = Item2;
if (item2 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>(item2);
}
var item3 = Item3;
if (item3 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>(item3);
}
var item4 = Item4;
if (item4 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>(item4);
}
var item5 = Item5;
if (item5 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>(item5);
}
var item6 = Item6;
if (item6 != null) {
return new ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>(item6);
}
throw new InvalidOperationException("None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?");
}
}
public partial class ConvertibleTo<TBase> {
public class Either<T1, T2, T3, T4, T5, T6> : EitherBase<T1, T2, T3, T4, T5, T6>, IEither<T1, T2, T3, T4, T5, T6>
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(T4 item4) : base(item4) { }

public Either(T5 item5) : base(item5) { }

public Either(T6 item6) : base(item6) { }

public Either(Either<T1, T2, T3, T4, T5, T6> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T6 t6) {
return new(t6);
}
public static implicit operator TBase(Either<T1, T2, T3, T4, T5, T6> either) {
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
throw new InvalidOperationException($"None of the items in the Either were convertible to {typeof(TBase)}");
}
}
}
}
public partial class SubTypesOf<TBase> {
public class Either<T1, T2, T3, T4, T5, T6> : ConvertibleTo<TBase>.Either<T1, T2, T3, T4, T5, T6>, IEither<T1, T2, T3, T4, T5, T6> where T1 : TBase where T2 : TBase where T3 : TBase where T4 : TBase where T5 : TBase where T6 : TBase
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(T4 item4) : base(item4) { }

public Either(T5 item5) : base(item5) { }

public Either(T6 item6) : base(item6) { }

public Either(Either<T1, T2, T3, T4, T5, T6> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
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
throw new ArgumentException($"Expected argument to be either a {typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}, {typeof(T4).Name}, {typeof(T5).Name}, or {typeof(T6).Name} but instead got a type of {typeof(TBase).Name}: {item.GetType().Name}", "name");
}
public virtual TBase Value => Item1 ?? Item2 ?? Item3 ?? Item4 ?? Item5 ?? (TBase)Item6;
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T6 t6) {
return new(t6);
}
}
}
public class Either<T1, T2, T3, T4, T5, T6> : SubTypesOf<object>.Either<T1, T2, T3, T4, T5, T6>, IEither<T1, T2, T3, T4, T5, T6>
{
protected Either() { }
public Either(T1 item1) : base(item1) { }

public Either(T2 item2) : base(item2) { }

public Either(T3 item3) : base(item3) { }

public Either(T4 item4) : base(item4) { }

public Either(T5 item5) : base(item5) { }

public Either(T6 item6) : base(item6) { }

public Either(Either<T1, T2, T3, T4, T5, T6> other) {
Item1 = other.Item1;
Item2 = other.Item2;
Item3 = other.Item3;
Item4 = other.Item4;
Item5 = other.Item5;
Item6 = other.Item6;
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T1 t1) {
return new(t1);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T2 t2) {
return new(t2);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T3 t3) {
return new(t3);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T4 t4) {
return new(t4);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T5 t5) {
return new(t5);
}
public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T6 t6) {
return new(t6);
}
}
}
