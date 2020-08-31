using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetWrite<TValue>
    {
        public static SetWrite<TValue> CreateAdd(TValue value)
        {
            return new SetWrite<TValue>(SetWriteType.Add, value);
        }

        public static SetWrite<TValue> CreateTryAdd(TValue value)
        {
            return new SetWrite<TValue>(SetWriteType.TryAdd, value);
        }

        public static SetWrite<TValue> CreateRemove(TValue value)
        {
            return new SetWrite<TValue>(SetWriteType.Remove, value);
        }

        public static SetWrite<TValue> CreateTryRemove(TValue value)
        {
            return new SetWrite<TValue>(SetWriteType.TryRemove, value);
        }

        public SetWrite(SetWriteType type, TValue value)
        {
            Type = type;
            Value = value;
        }

        public SetWriteType Type { get; }
        public TValue Value { get; }

        public override string ToString()
        {
            return $"{Type} {Value}";
        }
    }
}