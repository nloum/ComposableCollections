using System;
using SimpleMonads;

namespace IoFluently
{
    public class FolderOrMissingPath : FolderOrMissingPath<Folder, MissingPath>, IFolderOrMissingPath
    {
        public FolderOrMissingPath(Folder item2) : base(item2)
        {
        }

        public FolderOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public FolderOrMissingPath(IFileOrFolderOrMissingPath<File, Folder, MissingPath> path) : base(path)
        {
        }
    }
    
    public partial class FolderOrMissingPath<TFolder, TMissingPath> : FileOrFolderOrMissingPath<File, TFolder, TMissingPath>, IFolderOrMissingPath<TFolder, TMissingPath>
        where TFolder : Folder where TMissingPath : MissingPath
    {
        private IEither<TFolder, TMissingPath> _folderOrMissingPath => this.Collapse(
            _ => throw new InvalidOperationException(),
            folder => new Either<TFolder, TMissingPath>(folder),
            missingPath => new Either<TFolder, TMissingPath>(missingPath));
        
        public FolderOrMissingPath(TFolder item2) : base(item2)
        {
        }

        public FolderOrMissingPath(TMissingPath item3) : base(item3)
        {
        }

        public FolderOrMissingPath(IFileOrFolderOrMissingPath<File, TFolder, TMissingPath> path) : base(path)
        {
        }

        public TOutput Collapse<TOutput>(Func<TFolder, TOutput> selector1, Func<TMissingPath, TOutput> selector2)
        {
            return base.Collapse(_ => throw new InvalidOperationException(), selector1, selector2);
        }

        public TFolder? Item1 => _folderOrMissingPath.Item1;

        public TMissingPath? Item2 => _folderOrMissingPath.Item2;

        public IEither<TFolder, TMissingPath, T3> Or<T3>()
        {
            return _folderOrMissingPath.Or<T3>();
        }

        public IEither<TFolder, TMissingPath, T3, T4> Or<T3, T4>()
        {
            return _folderOrMissingPath.Or<T3, T4>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<TFolder, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public ConvertibleTo<TBase>.IEither<TFolder, TMissingPath> ConvertTo<TBase>()
        {
            return _folderOrMissingPath.ConvertTo<TBase>();
        }
    }
}