namespace IoFluently
{
    public enum EmptyFolderMode
    {
        EmptyFilesAreFolders,
        AllNonExistentPathsAreFolders,
        FoldersNeverExist,
        FoldersOnlyExistIfTheyContainFiles,
    }
}