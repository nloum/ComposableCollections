using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public partial class FileOrMissingPathBase : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IMissingPath>, IFileOrMissingPath
    {
        public IReadOnlyList<string> Components => Value.Components;
        public bool IsCaseSensitive => Value.IsCaseSensitive;
        public string DirectorySeparator => Value.DirectorySeparator;
        public IFileSystem FileSystem => Value.FileSystem;

        public FileOrMissingPathBase(IFilePath item1) : base(item1)
        {
        }

        public FileOrMissingPathBase(IMissingPath item2) : base(item2)
        {
        }

        public FileOrMissingPathBase(SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IMissingPath> other) : base(other)
        {
        }

        public FileOrMissingPathBase(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public bool CanBeSimplified => Value.CanBeSimplified;
        public bool Exists => Value.Exists;
        public string Extension => Value.Extension;
        public bool HasExtension => Value.HasExtension;
        public bool IsFile => Value.IsFile;
        public bool IsFolder => Value.IsFolder;
        public string Name => Value.Name;
        public FolderPath Root => Value.Root;
        public IMaybe<AbsolutePath> Parent => Value.Parent;
        public PathType Type => Value.Type;
        public AbsolutePath WithoutExtension => Value.WithoutExtension;

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

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath>
            FileOrFolderOrMissingPath => this.Collapse(
            file => new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>(file),
            missingPath =>
                new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>(missingPath));

        public IFileOrFolderOrMissingPath Value => FileOrFolderOrMissingPath.Value;

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4> Or<T4>()
        {
            return FileOrFolderOrMissingPath.Or<T4>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5> Or<T4, T5>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6> Or<T4, T5, T6>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7> Or<T4, T5, T6, T7>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8> Or<T4, T5, T6, T7, T8>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9> Or<T4, T5, T6, T7, T8, T9>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10> Or<T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11> Or<T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEitherBase<IFilePath, IFolderPath, IMissingPath, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrFolderOrMissingPath.Or<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<IFilePath, TOutput> selector1, Func<IFolderPath, TOutput> selector2, Func<IMissingPath, TOutput> selector3)
        {
            return FileOrFolderOrMissingPath.Collapse(selector1, selector2, selector3);
        }

        public ConvertibleTo<TBase>.IEither<IFilePath, IFolderPath, IMissingPath> ConvertTo<TBase>()
        {
            return FileOrFolderOrMissingPath.ConvertTo<TBase>();
        }

        public IFilePath? Item1 => FileOrFolderOrMissingPath.Item1;

        public IFolderPath? Item2 => FileOrFolderOrMissingPath.Item2;

        public IMissingPath? Item3 => FileOrFolderOrMissingPath.Item3;
    }
}