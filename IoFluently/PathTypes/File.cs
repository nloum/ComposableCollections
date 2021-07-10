using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public interface IFile<out TFile> : IFileOrFolder<TFile, Folder>, IFileOrMissingPath<TFile, MissingPath>
        where TFile : File
    {
    }

    public partial class File : IFile<File>
    {
        private File? _item1;
        private MissingPath? _item2;
        protected IEither<File, Folder> FileOrFolder => new Either<File, Folder>((File)this);
        protected IEither<File, MissingPath> FileOrMissingPath => new Either<File, MissingPath>((File)this);
        protected IEither<File, Folder, MissingPath> FileOrFolderOrMissingPath => new Either<File, Folder, MissingPath>((File)this);
        
        public File(IAbsolutePath path)
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
    
        File? IEitherBase<File, MissingPath>.Item1 => _item1;
    
        MissingPath? IEitherBase<File, MissingPath>.Item2 => _item2;
    
        IEither<File, MissingPath, T3> IEitherBase<File, MissingPath>.Or<T3>()
        {
            return FileOrMissingPath.Or<T3>();
        }
    
        IEither<File, MissingPath, T3, T4> IEitherBase<File, MissingPath>.Or<T3, T4>()
        {
            return FileOrMissingPath.Or<T3, T4>();
        }
    
        IEither<File, MissingPath, T3, T4, T5> IEitherBase<File, MissingPath>.Or<T3, T4, T5>()
        {
            return FileOrMissingPath.Or<T3, T4, T5>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }
    
        IEither<File, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<File, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }
    
        public TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<MissingPath, TOutput> selector2)
        {
            return FileOrMissingPath.Collapse(selector1, selector2);
        }
    
        ConvertibleTo<TBase>.IEither<File, MissingPath> IEitherBase<File, MissingPath>.ConvertTo<TBase>()
        {
            return FileOrMissingPath.ConvertTo<TBase>();
        }
    
        public ConvertibleTo<TBase>.IEither<File, Folder, MissingPath> ConvertTo<TBase>()
        {
            return FileOrFolderOrMissingPath.ConvertTo<TBase>();
        }
    }

    public partial class File<TFile> : File, IFile<TFile>
        where TFile : File
    {
        private TFile? _item1;
        private MissingPath? _item2;
        protected IEither<TFile, Folder> _fileOrFolder => base.FileOrFolder.Collapse(x => new Either<TFile, Folder>((TFile)x), _ => throw new InvalidOperationException());
        private IEither<TFile, MissingPath> _fileOrMissingPath => FileOrMissingPath.Collapse<IEither<TFile, MissingPath>>(x => new Either<TFile, MissingPath>((TFile)x), x => throw new InvalidOperationException());
        private IEither<TFile, Folder, MissingPath> _fileOrFolderOrMissingPath => FileOrFolderOrMissingPath.Collapse(x => new Either<TFile, Folder, MissingPath>((TFile)x), _ => throw new InvalidOperationException(), _ => throw new InvalidOperationException());
        
        public File(IAbsolutePath path) : base(path)
        {
        }

        public MissingPath? Item3 => _fileOrFolderOrMissingPath.Item3;
        
        public TOutput Collapse<TOutput>(Func<TFile, TOutput> selector1, Func<Folder, TOutput> selector2, Func<MissingPath, TOutput> selector3)
        {
            return _fileOrFolderOrMissingPath.Collapse(selector1, selector2, selector3);
        }

        public object Value => _fileOrFolder.Value;

        public TFile? Item1 => _fileOrFolder.Item1;

        public Folder? Item2 => _fileOrFolder.Item2;

        public IEither<TFile, Folder, T3> Or<T3>()
        {
            return _fileOrFolder.Or<T3>();
        }

        public IEither<TFile, Folder, T3, T4> Or<T3, T4>()
        {
            return _fileOrFolder.Or<T3, T4>();
        }

        public IEither<TFile, Folder, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _fileOrFolder.Or<T3, T4, T5>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<TFile, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _fileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<TFile, TOutput> selector1, Func<Folder, TOutput> selector2)
        {
            return _fileOrFolder.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<TFile, Folder> IEitherBase<TFile, Folder>.ConvertTo<TBase>()
        {
            return _fileOrFolder.ConvertTo<TBase>();
        }

        IEither<TFile, Folder, MissingPath, T4> IEitherBase<TFile, Folder, MissingPath>.Or<T4>()
        {
            return _fileOrFolderOrMissingPath.Or<T4>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _fileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEither<TFile, Folder, MissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<TFile, Folder, MissingPath>.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
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

        TFile? IEitherBase<TFile, MissingPath>.Item1 => _item1;

        MissingPath? IEitherBase<TFile, MissingPath>.Item2 => _item2;

        IEither<TFile, MissingPath, T3> IEitherBase<TFile, MissingPath>.Or<T3>()
        {
            return _fileOrMissingPath.Or<T3>();
        }

        IEither<TFile, MissingPath, T3, T4> IEitherBase<TFile, MissingPath>.Or<T3, T4>()
        {
            return _fileOrMissingPath.Or<T3, T4>();
        }

        IEither<TFile, MissingPath, T3, T4, T5> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEither<TFile, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<TFile, MissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _fileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<TFile, TOutput> selector1, Func<MissingPath, TOutput> selector2)
        {
            return _fileOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<TFile, MissingPath> IEitherBase<TFile, MissingPath>.ConvertTo<TBase>()
        {
            return _fileOrMissingPath.ConvertTo<TBase>();
        }

        public ConvertibleTo<TBase>.IEither<TFile, Folder, MissingPath> ConvertTo<TBase>()
        {
            return _fileOrFolderOrMissingPath.ConvertTo<TBase>();
        }
    }
}