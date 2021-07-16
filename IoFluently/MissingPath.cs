using System;
using System.Collections.Generic;
using System.IO;
using SimpleMonads;

namespace IoFluently
{
    public partial class MissingPath : IMissingPath
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }
        
        public MissingPath(IFileOrFolderOrMissingPath path, bool skipCheck = false) : this(path.Components, path.IsCaseSensitive,
            path.DirectorySeparator, path.FileSystem, skipCheck)
        {
            
        }
        
        public MissingPath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem, bool skipCheck = false)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;

            if (!skipCheck)
            {
                this.AssertExpectedType(PathType.MissingPath);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ConvertToString();
        }

        public FileAttributes Attributes
        {
            get => FileSystem.GetAttributes(this);
            set => FileSystem.SetAttributes(this, value);
        }

        /// <inheritdoc />
        public string FullName => this.ConvertToString();

        /// <summary>
        /// Converts this MissingPath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(MissingPath path)
        {
            return path.FullName;
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolderPath, IMissingPath> FolderOrMissingPath =>
            new FolderOrMissingPathEither(this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IMissingPath> FileOrMissingPath =>
            new FileOrMissingPathEither(this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath> FileOrFolderOrMissingPath =>
            new FileOrFolderOrMissingPathEither(this);

        IMissingPath? IEitherBase<IFilePath, IMissingPath>.Item2 => FileOrMissingPath.Item2;

        IFolderPath? IEitherBase<IFolderPath, IMissingPath>.Item1 => FolderOrMissingPath.Item1;

        IMissingPath? IEitherBase<IFolderPath, IMissingPath>.Item2 => FolderOrMissingPath.Item2;

        IEitherBase<IFilePath, IMissingPath, T3> IEitherBase<IFilePath, IMissingPath>.Or<T3>()
        {
            return FileOrMissingPath.Or<T3>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4>()
        {
            return FileOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5>()
        {
            return FileOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFilePath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFilePath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFilePath, IMissingPath>.Collapse<TOutput>(Func<IFilePath, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return FileOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFilePath, IMissingPath> IEitherBase<IFilePath, IMissingPath>.ConvertTo<TBase>()
        {
            return FileOrMissingPath.ConvertTo<TBase>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3> IEitherBase<IFolderPath, IMissingPath>.Or<T3>()
        {
            return FolderOrMissingPath.Or<T3>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4>()
        {
            return FolderOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFolderPath, IMissingPath>.Collapse<TOutput>(Func<IFolderPath, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return FolderOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFolderPath, IMissingPath> IEitherBase<IFolderPath, IMissingPath>.ConvertTo<TBase>()
        {
            return FolderOrMissingPath.ConvertTo<TBase>();
        }

        public IFileOrFolderOrMissingPath Value => this;

        public IFilePath? Item1 => FileOrFolderOrMissingPath.Item1;

        public IFolderPath? Item2 => FileOrFolderOrMissingPath.Item2;

        public IMissingPath? Item3 => FileOrFolderOrMissingPath.Item3;

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
    }
}