using System;

namespace IoFluently
{
    public class IoServiceEnvironmentDecorator : IoServiceDecoratorBase, IIoEnvironmentService
    {
        public IoServiceEnvironmentDecorator(IIoService decorated) : base(decorated)
        {
        }

        public Folder WorkingDirectory { get; set; }
        public Folder TemporaryFolder { get; set; }
        
        public MissingPath GenerateUniqueTemporaryPath(string extension = null)
        {
            return (TemporaryFolder / Guid.NewGuid().ToString()).ExpectMissingPath();
        }

        public AbsolutePath ParsePathRelativeToWorkingDirectory(string path)
        {
            return ParseAbsolutePath(path, WorkingDirectory);
        }
    }
}