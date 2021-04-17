namespace IoFluently
{
    /// <summary>
    /// Indicates the type of a path (file, folder, or none for non-existent)
    /// </summary>
    public enum PathType
    {
        /// <summary>
        /// The path is a file that exists
        /// </summary>
        File,
        
        /// <summary>
        /// The path is a folder that exists
        /// </summary>
        Folder,
        
        /// <summary>
        /// There is neither a file or folder that exists at this path
        /// </summary>
        None
    }
}