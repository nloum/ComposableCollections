using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public partial class MissingPath : IMissingPath
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }

        public MissingPath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IIoService ioService)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolder, IMissingPath> FolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFolder, IMissingPath>((IMissingPath)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IMissingPath> FileOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IMissingPath>((IMissingPath)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder, IMissingPath> FileOrFolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder, IMissingPath>((IMissingPath)this);

        IMissingPath? IEitherBase<IFile, IMissingPath>.Item2 => FileOrMissingPath.Item2;

        IFolder? IEitherBase<IFolder, IMissingPath>.Item1 => FolderOrMissingPath.Item1;

        IMissingPath? IEitherBase<IFolder, IMissingPath>.Item2 => FolderOrMissingPath.Item2;

        IEitherBase<IFile, IMissingPath, T3> IEitherBase<IFile, IMissingPath>.Or<T3>()
        {
            return FileOrMissingPath.Or<T3>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4> IEitherBase<IFile, IMissingPath>.Or<T3, T4>()
        {
            return FileOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5>()
        {
            return FileOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFile, IMissingPath>.Collapse<TOutput>(Func<IFile, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return FileOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFile, IMissingPath> IEitherBase<IFile, IMissingPath>.ConvertTo<TBase>()
        {
            return FileOrMissingPath.ConvertTo<TBase>();
        }

        IEitherBase<IFolder, IMissingPath, T3> IEitherBase<IFolder, IMissingPath>.Or<T3>()
        {
            return FolderOrMissingPath.Or<T3>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4> IEitherBase<IFolder, IMissingPath>.Or<T3, T4>()
        {
            return FolderOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFolder, IMissingPath>.Collapse<TOutput>(Func<IFolder, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return FolderOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFolder, IMissingPath> IEitherBase<IFolder, IMissingPath>.ConvertTo<TBase>()
        {
            return FolderOrMissingPath.ConvertTo<TBase>();
        }

        public IFileOrFolderOrMissingPath Value => FileOrFolderOrMissingPath.Value;

        public IFile? Item1 => FileOrFolderOrMissingPath.Item1;

        public IFolder? Item2 => FileOrFolderOrMissingPath.Item2;

        public IMissingPath? Item3 => FileOrFolderOrMissingPath.Item3;

        public IEitherBase<IFile, IFolder, IMissingPath, T4> Or<T4>()
        {
            return FileOrFolderOrMissingPath.Or<T4>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5> Or<T4, T5>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6> Or<T4, T5, T6>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7> Or<T4, T5, T6, T7>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8> Or<T4, T5, T6, T7, T8>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9> Or<T4, T5, T6, T7, T8, T9>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10> Or<T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11> Or<T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEitherBase<IFile, IFolder, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<IFile, TOutput> selector1, Func<IFolder, TOutput> selector2, Func<IMissingPath, TOutput> selector3)
        {
            return FileOrFolderOrMissingPath.Collapse(selector1, selector2, selector3);
        }

        public ConvertibleTo<TBase>.IEither<IFile, IFolder, IMissingPath> ConvertTo<TBase>()
        {
            return FileOrFolderOrMissingPath.ConvertTo<TBase>();
        }
    }
}