namespace IoFluently
{
    public class FolderPathAncestors : Ancestors<FolderPath>
    {
        public FolderPathAncestors(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        protected override FolderPath Expect(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return fileOrFolderOrMissingPath.ExpectFolder();
        }
    }
}