using SimpleMonads;

namespace IoFluently
{
    public class FileOrFolderPathEither : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath>
    {
        private readonly IFileOrFolderOrMissingPath _path;

        public FileOrFolderPathEither(IFileOrFolderOrMissingPath path)
        {
            _path = path;
        }

        public override IFilePath? Item1 => _path.Type == PathType.File ? new FilePath(_path, true) : null;
        public override IFolderPath? Item2 => _path.Type == PathType.Folder ? new FolderPath(_path, true) : null;
    }
}