using System.IO;

namespace IoFluently
{
    public partial class File : IHasAbsolutePath
    {
        public File(AbsolutePath path)
        {
            Path = path;
            if (!Path.IsFile)
            {
                throw new IOException($"{Path} is not a file");
            }
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FileOrFolder ExpectFileOrFolder()
        {
            return new(this);
        }
        
        public FileOrMissingPath ExpectFileOrMissingPath()
        {
            return new(this);
        }
        
        public AbsolutePath ExpectFileOrFolderOrMissingPath()
        {
            return Path;
        }
        
        public AbsolutePath Path { get; }
        public IIoService IoService => Path.IoService;
    }
}