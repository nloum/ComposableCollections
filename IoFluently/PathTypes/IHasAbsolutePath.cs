namespace IoFluently
{
    public partial interface IHasAbsolutePath
    {
        AbsolutePath Path { get; }
        IIoService IoService => Path.IoService;
    }
}