namespace GenericNumbers.ConvertTo
{
    public struct ConverterTo<T>
    {
        public T Value { get; private set; }

        public ConverterTo(T value) : this()
        {
            Value = value;
        }

        public T2 To<T2>()
        {
            return ConvertToUtil<T, T2>.ConvertTo(Value);
        }
    }
}