﻿using System;
using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public partial class Folder : IFolder
    {
        public IReadOnlyList<string> Components { get; }
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }

        public Folder(IFileOrFolderOrMissingPath path) : this(path.Components, path.IsCaseSensitive,
            path.DirectorySeparator, path.FileSystem)
        {
            
        }
        
        public Folder(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem)
        {
            Components = components;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;

            this.AssertExpectedType(PathType.Folder);
        }

        public Folder(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem, bool skipCheck)
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

        /// <inheritdoc />
        public string FullName => this.ConvertToString();

        /// <summary>
        /// Converts this AbsolutePath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(Folder path)
        {
            return path.FullName;
        }

        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder> FileOrFolder =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder>((IFolder)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolder, IMissingPath> IFolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFolder, IMissingPath>((IFolder)this);
        protected SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder, IMissingPath> FileOrFolderOrMissingPath =>
            new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFile, IFolder, IMissingPath>((IFolder)this);

        public IFileOrFolderOrMissingPath Value => FileOrFolder.Value;

        public IFile? Item1 => FileOrFolder.Item1;

        public IFolder? Item2 => FileOrFolder.Item2;

        IFolder? IEitherBase<IFolder, IMissingPath>.Item1 => IFolderOrMissingPath.Item1;

        IMissingPath? IEitherBase<IFolder, IMissingPath>.Item2 => IFolderOrMissingPath.Item2;

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

        

        

        IEitherBase<IFolder, IMissingPath, T3> IEitherBase<IFolder, IMissingPath>.Or<T3>()
        {
            return IFolderOrMissingPath.Or<T3>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4> IEitherBase<IFolder, IMissingPath>.Or<T3, T4>()
        {
            return IFolderOrMissingPath.Or<T3, T4>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        IEitherBase<IFolder, IMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IEitherBase<IFolder, IMissingPath>.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return IFolderOrMissingPath.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        TOutput IEitherBase<IFolder, IMissingPath>.Collapse<TOutput>(Func<IFolder, TOutput> selector1, Func<IMissingPath, TOutput> selector2)
        {
            return IFolderOrMissingPath.Collapse(selector1, selector2);
        }

        ConvertibleTo<TBase>.IEither<IFolder, IMissingPath> IEitherBase<IFolder, IMissingPath>.ConvertTo<TBase>()
        {
            return IFolderOrMissingPath.ConvertTo<TBase>();
        }


        
        
        
        
        
        

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
        
        
        
        
        
        
        
        
        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator /(Folder absPath, string whatToAdd)
        {
            return new AbsolutePath(absPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator /(Folder absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePath(absPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator /(Folder absPath,
            Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePath(absPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator /(Folder absPath, RelativePath whatToAdd)
        {
            return new AbsolutePath(absPath.Value) / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator /(Folder absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePath(absPath.Value) / whatToAdd;
        }
    }
}