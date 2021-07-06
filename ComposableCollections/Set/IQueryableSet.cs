namespace ComposableCollections.Set
{
    public interface IQueryableSet<TValue> : IComposableSet<TValue>, IQueryableReadOnlySet<TValue> {
    }
}