using System;
using SimpleMonads;

namespace IoFluently
{
    public partial class FileOrMissingPath : SubTypesOf<IHasAbsolutePath>.Either<File, MissingPath>, IHasAbsolutePath
    {
        public FileOrMissingPath(File item1) : base(item1)
        {
        }

        public FileOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public FileOrMissingPath(SubTypesOf<IHasAbsolutePath>.Either<File, MissingPath> other) : base(other)
        {
        }

        public FileOrMissingPath(IHasAbsolutePath item) : base(item)
        {
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public File ExpectFile()
        {
            if (Item1 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a file but it was a {Path.Type}");
            }
            return Item1;
        }

        public MissingPath ExpectMissingPath()
        {
            if (Item2 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a missing path but it was a {Path.Type}");
            }
            return Item2;
        }

        public AbsolutePath Path => Value.Path;
        public IIoService IoService => Path.IoService;
    }
}