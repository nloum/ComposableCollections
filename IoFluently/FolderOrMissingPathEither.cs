using SimpleMonads;

namespace IoFluently
{
    public class FolderOrMissingPathEither : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFolderPath, IMissingPath>
    {
        private readonly IFileOrFolderOrMissingPath _path;

        public FolderOrMissingPathEither(IFileOrFolderOrMissingPath path)
        {
            _path = path;
        }

        public override IFolderPath? Item1 => _path.Type == PathType.Folder ? new FolderPath(_path, true) : null;
        public override IMissingPath? Item2 => _path.Type == PathType.MissingPath ? new MissingPath(_path, true) : null;
    }
}