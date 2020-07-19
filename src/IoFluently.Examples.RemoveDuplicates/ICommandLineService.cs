namespace IoFluently.Examples.RemoveDuplicates
{
    public interface ICommandLineService<TVerb>
    {
        void Run(TVerb verb);
    }
}