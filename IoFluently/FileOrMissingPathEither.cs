using SimpleMonads;

namespace IoFluently
{
    public class FileOrMissingPathEither : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IMissingPath>
    {
        private readonly IFileOrFolderOrMissingPath _path;

        public FileOrMissingPathEither(IFileOrFolderOrMissingPath path)
        {
            _path = path;
        }

        public override IFilePath? Item1 => _path.Type == PathType.File ? new FilePath(_path, true) : null;
        public override IMissingPath? Item2 => _path.Type == PathType.MissingPath ? new MissingPath(_path, true) : null;
    }
}