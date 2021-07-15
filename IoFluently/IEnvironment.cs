namespace IoFluently
{
    /// <summary>
    /// Environmental IO methods and properties. These are disconnected from the main <see cref="IFileSystem"/> because
    /// it is quite possible to have <see cref="IFileSystem"/> implementations where these environmental methods and properties
    /// don't make sense. E.g., zip files typically don't contain a temporary folder where temporary files can be written to.
    /// </summary>
    public interface IEnvironment : IFileSystem
    {
        /// <summary>
        /// When relative paths are auto-converted to absolute paths, they are assumed to be relative to the this folder.
        /// </summary>
        FolderPath CurrentDirectory { get; set; }
        
        /// <summary>
        /// Returns the path to the user's temporary folder
        /// </summary>
        /// <returns>The path to the user's temporary folder</returns>
        FolderPath TemporaryFolder { get; set; }

        /// <summary>
        /// Creates a non-existent path that is unique. The parent folder of this path is guaranteed to exist.
        /// This does not create a file or folder for this path.
        /// </summary>
        /// <param name="extension">The file extension for the path. If null, the resulting path has no extension.</param>
        /// <returns>A path that does not exist but whose parent folder exists</returns>
        MissingPath GenerateUniqueTemporaryPath(string extension = null);
        
        /// <summary>
        /// Parses the path. If the path is a relative path, assumes that it is relative to <see cref="LocalFileSystem.CurrentDirectory"/>.
        /// </summary>
        AbsolutePath ParsePathRelativeToWorkingDirectory(string path);
    }
}