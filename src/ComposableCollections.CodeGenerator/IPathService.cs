using IoFluently;

namespace ComposableCollections.CodeGenerator
{
    public interface IPathService
    {
        void Initialize(AbsolutePath sourceCodeRootFolder);
        AbsolutePath SourceCodeRootFolder { get; }
    }
}