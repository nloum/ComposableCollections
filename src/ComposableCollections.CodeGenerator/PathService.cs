using IoFluently;

namespace ComposableCollections.CodeGenerator
{
    public class PathService : IPathService
    {
        public void Initialize(AbsolutePath sourceCodeRootFolder)
        {
            SourceCodeRootFolder = sourceCodeRootFolder;
        }
        
        public AbsolutePath SourceCodeRootFolder { get; private set; }
    }
}