using System;
using System.Collections.Generic;
using System.IO;
using SimpleMonads;

namespace IoFluently
{
    public partial class FileOrMissingPathBase : FileOrMissingPathEither, IFileOrMissingPath
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }

        public FileOrFolderOrMissingPathAncestors Ancestors => new(this);

        public FileOrMissingPathBase(IFileOrFolderOrMissingPath item) : base(item)
        {
            Components = item.Components;
            IsCaseSensitive = item.IsCaseSensitive;
            DirectorySeparator = item.DirectorySeparator;
            FileSystem = item.FileSystem;
        }

        public bool CanBeSimplified => FileSystem.CanBeSimplified(this);
        public bool Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public bool HasExtension => FileSystem.HasExtension(this);
        public bool IsFile => FileSystem.IsFile(this);
        public bool IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public FolderPath Root => Ancestors[0].ExpectFolder();
        public IMaybe<FileOrFolderOrMissingPath> Parent => Ancestors.Count > 1 ? Ancestors[Ancestors.Count - 1].ToMaybe() : Maybe<FileOrFolderOrMissingPath>.Nothing();
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);

        public FileAttributes Attributes
        {
            get => FileSystem.GetAttributes(this);
            set => FileSystem.SetAttributes(this, value);
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

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath>
            FileOrFolderOrMissingPath => this.Collapse(
            file => new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>(file),
            missingPath =>
                new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>(missingPath));

        public IFileOrFolderOrMissingPath Value => this;

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

        public override IFilePath? Item1 => FileSystem.Type(this) == PathType.File ? new FilePath(this) : null;

        public override IMissingPath? Item2 =>
            FileSystem.Type(this) == PathType.MissingPath ? new MissingPath(this) : null;

        IFolderPath? IEitherBase<IFilePath, IFolderPath, IMissingPath>.Item2 => FileSystem.Type(this) == PathType.Folder ? new FolderPath(this) : null;

        public IMissingPath? Item3 => Item2;
    }
}