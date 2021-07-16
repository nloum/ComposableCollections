namespace IoFluently
{
    public partial interface IMissingPath : IFileOrMissingPath, IFolderOrMissingPath
    {
        public MissingPathAncestors Ancestors { get; }
    }
}