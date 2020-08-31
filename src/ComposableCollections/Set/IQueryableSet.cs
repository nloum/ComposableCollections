namespace ComposableCollections.Set
{
    public interface IQueryableSet<TValue> : ISet<TValue>, IQueryableReadOnlySet<TValue> {
    }
}