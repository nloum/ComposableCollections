using SimpleMonads;

namespace IoFluently
{
    public class FileOrFolderOrMissingPathEither : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>
    {
        private readonly IFileOrFolderOrMissingPath _path;

        public FileOrFolderOrMissingPathEither(IFileOrFolderOrMissingPath path)
        {
            _path = path;
        }

        public override IFilePath? Item1 => _path.Type == PathType.File ? new FilePath(_path, true) : null;
        public override IFolderPath? Item2 => _path.Type == PathType.Folder ? new FolderPath(_path, true) : null;
        public override IMissingPath? Item3 => _path.Type == PathType.MissingPath ? new MissingPath(_path, true) : null;
    }
}