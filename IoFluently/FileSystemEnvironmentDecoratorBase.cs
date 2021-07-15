using System;

namespace IoFluently
{
    public class FileSystemEnvironmentDecoratorBase : FileSystemDecoratorBase, IEnvironment
    {
        private readonly IEnvironment _decorated;

        public FileSystemEnvironmentDecoratorBase(IEnvironment decorated) : base(decorated)
        {
            _decorated = decorated;
        }

        public Folder CurrentDirectory
        {
            get => _decorated.CurrentDirectory;
            set => _decorated.CurrentDirectory = value;
        }

        public Folder TemporaryFolder
        {
            get => _decorated.TemporaryFolder;
            set => _decorated.TemporaryFolder = value;
        }

        public MissingPath GenerateUniqueTemporaryPath(string extension = null)
        {
            return (TemporaryFolder / Guid.NewGuid().ToString()).ExpectMissingPath();
        }

        public AbsolutePath ParsePathRelativeToWorkingDirectory(string path)
        {
            return ParseAbsolutePath(path, CurrentDirectory);
        }
    }
}