using System;
using SimpleMonads;

namespace IoFluently
{
    public partial class FolderOrMissingPath : SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath>, IHasAbsolutePath
    {
        public FolderOrMissingPath(Folder item1) : base(item1)
        {
        }

        public FolderOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FolderOrMissingPath(SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath> other) : base(other)
        {
        }

        public FolderOrMissingPath(IHasAbsolutePath item) : base(item)
        {
        }

        public Folder ExpectFolder()
        {
            if (Item1 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a folder but it was a {Path.Type}");
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