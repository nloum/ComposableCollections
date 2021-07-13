using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public partial class File : IFile
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }

        public File(IFileOrFolderOrMissingPath path) : this(path.Components, path.IsCaseSensitive,
            path.DirectorySeparator, path.IoService)
        {
            
        }
        
        public File(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IIoService ioService)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            IoService = ioService;

            this.AssertExpectedType(PathType.File);
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return this.ConvertToString();
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder> FileOrFolder =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder>((IFile)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IMissingPath> IFileOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IMissingPath>((IFile)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder, IMissingPath> FileOrFolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder, IMissingPath>((IFile)this);

        public IFileOrFolderOrMissingPath Value => FileOrFolder.Value;

        public IFile? Item1 => FileOrFolder.Item1;

        public IFolder? Item2 => FileOrFolder.Item2;

        IMissingPath? IEitherBase<IFile, IMissingPath>.Item2 => IFileOrMissingPath.Item2;

        IEitherBase<IFile, IFolder, T3> IEitherBase<IFile, IFolder>.Or<T3>()
        {
            return FileOrFolder.Or<T3>();
        }

        IEitherBase<IFile, IFolder, T3, T4> IEitherBase<IFile, IFolder>.Or<T3, T4>()
        {
            return FileOrFolder.Or<T3, T4>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5> IEitherBase<IFile, IFolder>.Or<T3, T4, T5>()
        {
            return FileOrFolder.Or<T3, T4, T5>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFile, IFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFile, IFolder>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFile, IFolder>.Collapse<TOutput>(Func<IFile, TOutput> selector1, Func<IFolder, TOutput> selector2)
        {
            return FileOrFolder.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFile, IFolder> IEitherBase<IFile, IFolder>.ConvertTo<TBase>()
        {
            return FileOrFolder.ConvertTo<TBase>();
        }
        
        IEitherBase<IFile, IMissingPath, T3> IEitherBase<IFile, IMissingPath>.Or<T3>()
        {
            return IFileOrMissingPath.Or<T3>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4> IEitherBase<IFile, IMissingPath>.Or<T3, T4>()
        {
            return IFileOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFile, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFile, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return IFileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFile, IMissingPath>.Collapse<TOutput>(Func<IFile, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return IFileOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFile, IMissingPath> IEitherBase<IFile, IMissingPath>.ConvertTo<TBase>()
        {
            return IFileOrMissingPath.ConvertTo<TBase>();
        }

        IFolder? IEitherBase<IFile, IFolder, IMissingPath>.Item2 => FileOrFolderOrMissingPath.Item2;

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