using SimpleMonads;

namespace IoFluently
{
    public partial interface IFolderPath : IFolderOrMissingPath, IFileOrFolderPath
    {
        public FolderPathAncestors Ancestors { get; }
        public ChildFiles ChildFiles { get; }
        public ChildFolders ChildFolders { get; }
        public ChildFilesOrFolders Children { get; }
        public DescendantFiles DescendantFiles { get; }
        public DescendantFolders DescendantFolders { get; }
        public DescendantFilesOrFolders Descendants { get; }
        public IMaybe<FolderPath> Parent { get; }
    }
}