namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface ITransformation<in TInput, out TOutput, in TParameter1, in TParameter2, in TParameter3, in TParameter4, in TParameter5>
    {
        TOutput Transform(TInput input, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5);
    }

    public interface ITransformation<in TInput, out TOutput, in TParameter1, in TParameter2, in TParameter3, in TParameter4>
    {
        TOutput Transform(TInput input, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4);
    }

    public interface ITransformation<in TInput, out TOutput, in TParameter1, in TParameter2, in TParameter3>
    {
        TOutput Transform(TInput input, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3);
    }

    public interface ITransformation<in TInput, out TOutput, in TParameter1, in TParameter2>
    {
        TOutput Transform(TInput input, TParameter1 parameter1, TParameter2 parameter2);
    }

    public interface ITransformation<in TInput, out TOutput, in TParameter1>
    {
        TOutput Transform(TInput input, TParameter1 parameter1);
    }

    public interface ITransformation<in TInput, out TOutput>
    {
        TOutput Transform(TInput input);
    }
}