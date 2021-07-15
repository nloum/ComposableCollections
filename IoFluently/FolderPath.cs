using System;
using System.Collections.Generic;
using System.IO;
using SimpleMonads;

namespace IoFluently
{
    public partial class FolderPath : IFolderPath
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }

        public FolderPath(IFileOrFolderOrMissingPath path) : this(path.Components, path.IsCaseSensitive,
            path.DirectorySeparator, path.FileSystem)
        {
            
        }
        
        public FolderPath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;

            this.AssertExpectedType(PathType.Folder);
        }

        public FolderPath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem, bool skipCheck)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;

            if (!skipCheck)
            {
                this.AssertExpectedType(PathType.Folder);
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
        /// Converts this AbsolutePath to a string form of the path
        /// </summary>
        /// <param name="folderPath">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(FolderPath folderPath)
        {
            return folderPath.FullName;
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath> FileOrFolder =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath>((IFolderPath)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolderPath, IMissingPath> IFolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFolderPath, IMissingPath>((IFolderPath)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath> FileOrFolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>((IFolderPath)this);

        public IFileOrFolderOrMissingPath Value => FileOrFolder.Value;

        public IFilePath? Item1 => FileOrFolder.Item1;

        public IFolderPath? Item2 => FileOrFolder.Item2;

        IFolderPath? IEitherBase<IFolderPath, IMissingPath>.Item1 => IFolderOrMissingPath.Item1;

        IMissingPath? IEitherBase<IFolderPath, IMissingPath>.Item2 => IFolderOrMissingPath.Item2;

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

        

        

        IEitherBase<IFolderPath, IMissingPath, T3> IEitherBase<IFolderPath, IMissingPath>.Or<T3>()
        {
            return IFolderOrMissingPath.Or<T3>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4>()
        {
            return IFolderOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFolderPath, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFolderPath, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFolderPath, IMissingPath>.Collapse<TOutput>(Func<IFolderPath, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return IFolderOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFolderPath, IMissingPath> IEitherBase<IFolderPath, IMissingPath>.ConvertTo<TBase>()
        {
            return IFolderOrMissingPath.ConvertTo<TBase>();
        }


        
        
        
        
        
        

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
        
        
        
        
        
        
        
        
        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath operator /(FolderPath folderPath, string whatToAdd)
        {
            return new FileOrFolderOrMissingPath(folderPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator /(FolderPath folderPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new FileOrFolderOrMissingPath(folderPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator /(FolderPath folderPath,
            Func<FileOrFolderOrMissingPath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new FileOrFolderOrMissingPath(folderPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath operator /(FolderPath folderPath, RelativePath whatToAdd)
        {
            return new FileOrFolderOrMissingPath(folderPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator /(FolderPath folderPath, IEnumerable<string> whatToAdd)
        {
            return new FileOrFolderOrMissingPath(folderPath.Value) / whatToAdd;
        }
    }
}