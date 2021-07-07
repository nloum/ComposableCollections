using System;
using SimpleMonads;

namespace IoFluently
{
    public partial class FileOrFolder : SubTypesOf<IHasAbsolutePath>.Either<File, Folder>, IHasAbsolutePath
    {
        public FileOrFolder(File item1) : base(item1)
        {
        }

        public FileOrFolder(Folder item3) : base(item3)
        {
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FileOrFolder(SubTypesOf<IHasAbsolutePath>.Either<File, Folder> other) : base(other)
        {
        }

        public FileOrFolder(IHasAbsolutePath item) : base(item)
        {
        }

        public File ExpectFile()
        {
            if (Item1 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a file but it was a {Path.Type}");
            }
            return Item1;
        }

        public Folder ExpectFolder()
        {
            if (Item2 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a folder but it was a {Path.Type}");
            }
            return Item2;
        }

        public AbsolutePath Path => Value.Path;
        public IIoService IoService => Path.IoService;
    }
}