using System;
using System.Collections.Generic;
using System.IO;
using SimpleMonads;

namespace IoFluently
{
    public partial class FolderPath : IFolderPath
    {
        private FileOrFolderOrMissingPathAncestors _ancestors;
        private IMaybe<FileOrFolderOrMissingPath> _parent;
        private IMaybe<FolderPath> _parent1;
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }

        FileOrFolderOrMissingPathAncestors IFileOrFolderOrMissingPath.Ancestors => new(this);

        public FolderPath Root => Ancestors.Count > 0 ? Ancestors[0] : this;

        IMaybe<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Parent => Ancestors.Count > 0 ? Ancestors[Ancestors.Count - 1].ExpectFileOrFolderOrMissingPath().ToMaybe() : Maybe<FileOrFolderOrMissingPath>.Nothing();

        IMaybe<FolderPath> IFolderPath.Parent => Ancestors.Count > 0 ? Ancestors[Ancestors.Count - 1].ToMaybe() : Maybe<FolderPath>.Nothing();

        public FolderPathAncestors Ancestors => FileSystem.Ancestors(this);
        public ChildFiles ChildFiles => FileSystem.ChildFiles(this);
        public ChildFolders ChildFolders => FileSystem.ChildFolders(this);
        public ChildFilesOrFolders Children => FileSystem.Children(this);
        public DescendantFiles DescendantFiles => FileSystem.DescendantFiles(this);
        public DescendantFolders DescendantFolders => FileSystem.DescendantFolders(this);
        public DescendantFilesOrFolders Descendants => FileSystem.Descendants(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);
        
        public FolderPath(IFileOrFolderOrMissingPath path, bool skipCheck = false) : this(path.Components, path.IsCaseSensitive,
            path.DirectorySeparator, path.FileSystem, skipCheck)
        {
            
        }
        
        public FolderPath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem, bool skipCheck = false)
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
            new FileOrFolderPathEither(this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolderPath, IMissingPath> FolderOrMissingPath =>
            new FolderOrMissingPathEither(this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath> FileOrFolderOrMissingPath =>
            new FileOrFolderOrMissingPathEither(this);

        public IFileOrFolderOrMissingPath Value => this;

        public IFilePath? Item1 => FileOrFolder.Item1;

        public IFolderPath? Item2 => FileOrFolder.Item2;

        IFolderPath? IEitherBase<IFolderPath, IMissingPath>.Item1 => FolderOrMissingPath.Item1;

        IMissingPath? IEitherBase<IFolderPath, IMissingPath>.Item2 => FolderOrMissingPath.Item2;

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