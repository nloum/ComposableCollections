namespace IoFluently
{
    public enum ZipFolderMode
    {
        EmptyFilesAreDirectories,
        AllNonExistentPathsAreFolders,
        DirectoriesDontExist,
        DirectoriesExistIfTheyContainFiles,
    }
}