using System;

namespace IoFluently
{
    public class FileSystemEnvironmentDecorator : FileSystemDecoratorBase, IEnvironment
    {
        public FileSystemEnvironmentDecorator(IFileSystem decorated) : base(decorated)
        {
        }

        public Folder CurrentDirectory { get; set; }

        public Folder TemporaryFolder { get; set; }

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