using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public class FileOrMissingPathBase : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IMissingPath>, IFileOrMissingPath
    {
        public IReadOnlyList<string> Components => Value.Components;
        public bool IsCaseSensitive => Value.IsCaseSensitive;
        public string DirectorySeparator => Value.DirectorySeparator;
        public IIoService IoService => Value.IoService;

        public FileOrMissingPathBase(IFile item1) : base(item1)
        {
        }

        public FileOrMissingPathBase(IMissingPath item2) : base(item2)
        {
        }

        public FileOrMissingPathBase(SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IMissingPath> other) : base(other)
        {
        }

        public FileOrMissingPathBase(IFileOrFolderOrMissingPath item) : base(item)
        {
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return this.ConvertToString();
        }

        /// <inheritdoc />
        public string FullName => this.ConvertToString();

        /// <summary>
        /// Converts this FileOrMissingPathBase to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(FileOrMissingPathBase path)
        {
            return path.FullName;
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder, IMissingPath>
            FileOrFolderOrMissingPath => this.Collapse(
            file => new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder, IMissingPath>(file),
            missingPath =>
                new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder, IMissingPath>(missingPath));

        public IFileOrFolderOrMissingPath Value => FileOrFolderOrMissingPath.Value;

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

        public IFile? Item1 => FileOrFolderOrMissingPath.Item1;

        public IFolder? Item2 => FileOrFolderOrMissingPath.Item2;

        public IMissingPath? Item3 => FileOrFolderOrMissingPath.Item3;
    }
}