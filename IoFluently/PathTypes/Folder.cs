using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public interface IFolder<out TFolder> : IFileOrFolder<File, TFolder>, IFolderOrMissingPath<TFolder, MissingPath>
        where TFolder : Folder
    {
    }

    public partial class Folder : IFolder<Folder>
    {
        private Folder? _item1;
        private MissingPath? _item2;
        protected IEither<File, Folder> FileOrFolder => new Either<File, Folder>((Folder)this);
        protected IEither<Folder, MissingPath> FolderOrMissingPath => new Either<Folder, MissingPath>((Folder)this);
        protected IEither<File, Folder, MissingPath> FileOrFolderOrMissingPath => new Either<File, Folder, MissingPath>((Folder)this);
        
        public Folder(IAbsolutePath path)
        {
        }
    
        public MissingPath? Item3 => FileOrFolderOrMissingPath.Item3;
    
    
        public TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<Folder, TOutput> selector2, Func<MissingPath, TOutput> selector3)
        {
            return FileOrFolderOrMissingPath.Collapse(selector1, selector2, selector3);
        }
    
        public object Value => FileOrFolder.Value;
    
        public File? Item1 => FileOrFolder.Item1;
    
        public Folder? Item2 => FileOrFolder.Item2;
    
        public IEither<File, Folder, T3> Or<T3>()
        {
            return FileOrFolder.Or<T3>();
        }
    
        public IEither<File, Folder, T3, T4> Or<T3, T4>()
        {
            return FileOrFolder.Or<T3, T4>();
        }
    
        public IEither<File, Folder, T3, T4, T5> Or<T3, T4, T5>()
        {
            return FileOrFolder.Or<T3, T4, T5>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }
    
        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }
    
        public TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<Folder, TOutput> selector2)
        {
            return FileOrFolder.Collapse(selector1, selector2);
        }
    
    
        ConvertibleTo<TBase>.IEither<File, Folder> IEitherBase<File, Folder>.ConvertTo<TBase>()
        {
            return FileOrFolder.ConvertTo<TBase>();
        }
    
        IEither<File, Folder, MissingPath, T4> IEitherBase<File, Folder, MissingPath>.Or<T4>()
        {
            return FileOrFolderOrMissingPath.Or<T4>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5> IEitherBase<File, Folder, MissingPath>.Or<T4, T5>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }
    
        IEither<File, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<File, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }
    
        public int CompareTo(object? obj)
        {
            return ToString().CompareTo(obj?.ToString());
        }
    
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }
        public IReadOnlyList<string> Components { get; }
        public string Name { get; }
        public IMaybe<string> Extension { get; }
    
        Folder? IEitherBase<Folder, MissingPath>.Item1 => _item1;
    
        MissingPath? IEitherBase<Folder, MissingPath>.Item2 => _item2;
    
        IEither<Folder, MissingPath, T3> IEitherBase<Folder, MissingPath>.Or<T3>()
        {
            return FolderOrMissingPath.Or<T3>();
        }
    
        IEither<Folder, MissingPath, T3, T4> IEitherBase<Folder, MissingPath>.Or<T3, T4>()
        {
            return FolderOrMissingPath.Or<T3, T4>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }
    
        IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<Folder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }
    
        public TOutput Collapse<TOutput>(Func<Folder, TOutput> selector1, Func<MissingPath, TOutput> selector2)
        {
            return FolderOrMissingPath.Collapse(selector1, selector2);
        }
    
        ConvertibleTo<TBase>.IEither<Folder, MissingPath> IEitherBase<Folder, MissingPath>.ConvertTo<TBase>()
        {
            return FolderOrMissingPath.ConvertTo<TBase>();
        }
    
        public ConvertibleTo<TBase>.IEither<File, Folder, MissingPath> ConvertTo<TBase>()
        {
            return FileOrFolderOrMissingPath.ConvertTo<TBase>();
        }
    }

    public partial class Folder<TFolder> : Folder, IFolder<TFolder>
        where TFolder : Folder
    {
        private TFolder? _item1;
        private MissingPath? _item2;
        protected IEither<File, TFolder> _fileOrFolder => base.FileOrFolder.Collapse(x => new Either<File, TFolder>(x), x => new Either<File, TFolder>((TFolder)x));
        private IEither<TFolder, MissingPath> _folderOrMissingPath => FolderOrMissingPath.Collapse<IEither<TFolder, MissingPath>>(x => throw new InvalidOperationException(), x => new Either<TFolder, MissingPath>((TFolder)x));
        private IEither<File, TFolder, MissingPath> _fileOrFolderOrMissingPath => FileOrFolderOrMissingPath.Collapse(_ => throw new InvalidOperationException(), x => new Either<File, TFolder, MissingPath>((TFolder)x), _ => throw new InvalidOperationException());
        
        public Folder(IAbsolutePath path) : base(path)
        {
        }

        public MissingPath? Item3 => _fileOrFolderOrMissingPath.Item3;
        
        public TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<TFolder, TOutput> selector2, Func<MissingPath, TOutput> selector3)
        {
            return _fileOrFolderOrMissingPath.Collapse(selector1, selector2, selector3);
        }

        public object Value => _fileOrFolder.Value;

        public File? Item1 => _fileOrFolder.Item1;

        public TFolder? Item2 => _fileOrFolder.Item2;

        public IEither<File, TFolder, T3> Or<T3>()
        {
            return _fileOrFolder.Or<T3>();
        }

        public IEither<File, TFolder, T3, T4> Or<T3, T4>()
        {
            return _fileOrFolder.Or<T3, T4>();
        }

        public IEither<File, TFolder, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _fileOrFolder.Or<T3, T4, T5>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<File, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<TFolder, TOutput> selector2)
        {
            return _fileOrFolder.Collapse(selector1, selector2);
        }


        ConvertibleTo<TBase>.IEither<File, TFolder> IEitherBase<File, TFolder>.ConvertTo<TBase>()
        {
            return _fileOrFolder.ConvertTo<TBase>();
        }

        IEither<File, TFolder, MissingPath, T4> IEitherBase<File, TFolder, MissingPath>.Or<T4>()
        {
            return _fileOrFolderOrMissingPath.Or<T4>();
        }

        IEither<File, TFolder, MissingPath, T4, T5> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEither<File, TFolder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<File, TFolder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public int CompareTo(object? obj)
        {
            return ToString().CompareTo(obj?.ToString());
        }

        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }
        public IReadOnlyList<string> Components { get; }
        public string Name { get; }
        public IMaybe<string> Extension { get; }

        TFolder? IEitherBase<TFolder, MissingPath>.Item1 => _item1;

        MissingPath? IEitherBase<TFolder, MissingPath>.Item2 => _item2;

        IEither<TFolder, MissingPath, T3> IEitherBase<TFolder, MissingPath>.Or<T3>()
        {
            return _folderOrMissingPath.Or<T3>();
        }

        IEither<TFolder, MissingPath, T3, T4> IEitherBase<TFolder, MissingPath>.Or<T3, T4>()
        {
            return _folderOrMissingPath.Or<T3, T4>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEither<TFolder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<TFolder, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _folderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<TFolder, TOutput> selector1, Func<MissingPath, TOutput> selector2)
        {
            return _folderOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<TFolder, MissingPath> IEitherBase<TFolder, MissingPath>.ConvertTo<TBase>()
        {
            return _folderOrMissingPath.ConvertTo<TBase>();
        }

        public ConvertibleTo<TBase>.IEither<File, TFolder, MissingPath> ConvertTo<TBase>()
        {
            return _fileOrFolderOrMissingPath.ConvertTo<TBase>();
        }
    }
}