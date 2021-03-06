﻿using System;
using System.Collections.Generic;
using System.IO;
using SimpleMonads;
using UnitsNet;

namespace IoFluently
{
    public partial class FilePath : IFilePath
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }

        FileOrFolderOrMissingPathAncestors IFileOrFolderOrMissingPath.Ancestors => new(this);
        public FilePathAncestors Ancestors => new(this);
        public FolderPath Root => Ancestors[0];
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);
        public DateTimeOffset CreationTime => FileSystem.CreationTime(this);
        public Information FileSize => FileSystem.FileSize(this);
        public Boolean IsReadOnly => FileSystem.IsReadOnly(this);
        public DateTimeOffset LastAccessTime => FileSystem.LastAccessTime(this);
        public DateTimeOffset LastWriteTime => FileSystem.LastWriteTime(this);
        public FolderPath Parent { get; }
        
        public FilePath(IFileOrFolderOrMissingPath path, bool skipCheck = false) : this(path.Components, path.IsCaseSensitive,
            path.DirectorySeparator, path.FileSystem, skipCheck)
        {
            
        }
        
        public FilePath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem, bool skipCheck = false)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;

            if (!skipCheck)
            {
                this.AssertExpectedType(PathType.File);
            }
        }
        
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
        /// Converts this path to a string form of the path
        /// </summary>
        /// <param name="filePathe path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(FilePath filePath)
        {
            return filePath.FullName;
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath> FileOrFolder =>
            new FileOrFolderPathEither(this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IMissingPath> FileOrMissingPath =>
            new FileOrMissingPathEither(this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath> FileOrFolderOrMissingPath =>
            new FileOrFolderOrMissingPathEither(this);

        IMaybe<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Parent => Parent.ExpectFileOrFolderOrMissingPath().ToMaybe();

        public IFileOrFolderOrMissingPath Value => this;

        public IFilePath? Item1 => FileOrFolder.Item1;

        public IFolderPath? Item2 => FileOrFolder.Item2;

        IMissingPath? IEitherBase<IFilePath, IMissingPath>.Item2 => FileOrMissingPath.Item2;

        IEitherBase<IFilePath, IFolderPath, T3> IEitherBase<IFilePath, IFolderPath>.Or<T3>()
        {
            return FileOrFolder.Or<T3>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4>()
        {
            return FileOrFolder.Or<T3, T4>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5>()
        {
            return FileOrFolder.Or<T3, T4, T5>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFilePath, IFolderPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFilePath, IFolderPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return FileOrFolder.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFilePath, IFolderPath>.Collapse<TOutput>(Func<IFilePath, TOutput> selector1, Func<IFolderPath, TOutput> selector2)
        {
            return FileOrFolder.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFilePath, IFolderPath> IEitherBase<IFilePath, IFolderPath>.ConvertTo<TBase>()
        {
            return FileOrFolder.ConvertTo<TBase>();
        }
        
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

        IFolderPath? IEitherBase<IFilePath, IFolderPath, IMissingPath>.Item2 => FileOrFolderOrMissingPath.Item2;

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