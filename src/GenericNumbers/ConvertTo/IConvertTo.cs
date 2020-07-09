namespace GenericNumbers.ConvertTo
{
    public interface IConvertTo<TOutput>
    {
        void ConvertTo(out TOutput output);
    }
}