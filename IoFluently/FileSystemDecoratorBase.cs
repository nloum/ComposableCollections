﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Set;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using SimpleMonads;
using UnitsNet;

namespace IoFluently
{
    public abstract class FileSystemDecoratorBase : IFileSystem
    {
        private readonly IFileSystem _decorated;

        public FileSystemDecoratorBase(IFileSystem decorated)
        {
            _decorated = decorated;
        }

        public ChildFilesOrFolders Children(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.Children(folderPath, searchPattern);
        }

        public ChildFiles ChildFiles(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.ChildFiles(folderPath, searchPattern);
        }

        public ChildFolders ChildFolders(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.ChildFolders(folderPath, searchPattern);
        }

        public DescendantFilesOrFolders Descendants(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.Descendants(folderPath, searchPattern);
        }

        public DescendantFolders DescendantFolders(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.DescendantFolders(folderPath, searchPattern);
        }

        public DescendantFiles DescendantFiles(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.DescendantFiles(folderPath, searchPattern);
        }

        public ChildFilesOrFolders Children(IFolderPath folderPath)
        {
            return _decorated.Children(folderPath);
        }

        public ChildFiles ChildFiles(IFolderPath folderPath)
        {
            return _decorated.ChildFiles(folderPath);
        }

        public ChildFolders ChildFolders(IFolderPath folderPath)
        {
            return _decorated.ChildFolders(folderPath);
        }

        public DescendantFilesOrFolders Descendants(IFolderPath folderPath)
        {
            return _decorated.Descendants(folderPath);
        }

        public DescendantFolders DescendantFolders(IFolderPath folderPath)
        {
            return _decorated.DescendantFolders(folderPath);
        }

        public DescendantFiles DescendantFiles(IFolderPath folderPath)
        {
            return _decorated.DescendantFiles(folderPath);
        }

        public IObservableReadOnlySet<FolderPath> Roots => _decorated.Roots;

        public FolderPath DefaultRoot => _decorated.DefaultRoot;

        public void UpdateRoots()
        {
            _decorated.UpdateRoots();
        }

        public virtual IFileInfo GetFileInfo(string subpath)
        {
            return _decorated.GetFileInfo(subpath);
        }

        public virtual IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _decorated.GetDirectoryContents(subpath);
        }

        public virtual IChangeToken Watch(string filter)
        {
            return _decorated.Watch(filter);
        }

        public virtual Information DefaultBufferSize
        {
            get => _decorated.DefaultBufferSize;
            set => _decorated.DefaultBufferSize = value;
        }

        public virtual int GetBufferSizeOrDefaultInBytes(Information? bufferSize)
        {
            return _decorated.GetBufferSizeOrDefaultInBytes(bufferSize);
        }

        public virtual EmptyFolderMode EmptyFolderMode => _decorated.EmptyFolderMode;

        public virtual bool CanEmptyDirectoriesExist => _decorated.CanEmptyDirectoriesExist;

        public virtual string DefaultDirectorySeparator => _decorated.DefaultDirectorySeparator;

        public virtual bool IsCaseSensitiveByDefault => _decorated.IsCaseSensitiveByDefault;

        public virtual FolderPath CreateFolder(IMissingPath path,  bool createRecursively = true)
        {
            return _decorated.CreateFolder(path, createRecursively);
        }

        public virtual MissingPath DeleteFolder(IFolderPath folderPath,  bool recursive = true)
        {
            return _decorated.DeleteFolder(folderPath, recursive);
        }

        public virtual MissingPath DeleteFile(IFilePath path)
        {
            return _decorated.DeleteFile(path);
        }

        public virtual MissingPath Delete(IFileOrFolderPath path, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.Delete(path, recursiveDeleteIfFolder);
        }

        public virtual Task<MissingPath> DeleteFolderAsync(IFolderPath folderPath, CancellationToken cancellationToken,  bool recursive = true)
        {
            return _decorated.DeleteFolderAsync(folderPath, cancellationToken, recursive);
        }

        public virtual Task<MissingPath> DeleteFileAsync(IFilePath path, CancellationToken cancellationToken)
        {
            return _decorated.DeleteFileAsync(path, cancellationToken);
        }

        public virtual Task<MissingPath> DeleteAsync(IFileOrFolderPath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public virtual FolderPath EnsureIsFolder(IFileOrFolderOrMissingPath path,  bool createRecursively = true)
        {
            return _decorated.EnsureIsFolder(path, createRecursively);
        }

        public virtual FolderPath EnsureIsEmptyFolder(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true,
             bool createRecursively = true)
        {
            return _decorated.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public virtual Task<FolderPath> EnsureIsFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
             bool createRecursively = true)
        {
            return _decorated.EnsureIsFolderAsync(path, cancellationToken, createRecursively);
        }

        public virtual Task<FolderPath> EnsureIsEmptyFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true,  bool createRecursively = true)
        {
            return _decorated.EnsureIsEmptyFolderAsync(path, cancellationToken, recursiveDeleteIfFolder, createRecursively);
        }

        public virtual IFileOrMissingPath EnsureIsNotFolder(IFileOrFolderOrMissingPath path,  bool recursive = true)
        {
            return _decorated.EnsureIsNotFolder(path, recursive);
        }

        public virtual IFolderOrMissingPath EnsureIsNotFile(IFileOrFolderOrMissingPath path)
        {
            return _decorated.EnsureIsNotFile(path);
        }

        public virtual MissingPath EnsureDoesNotExist(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public virtual Task<IFileOrMissingPath> EnsureIsNotFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
             bool recursive = true)
        {
            return _decorated.EnsureIsNotFolderAsync(path, cancellationToken, recursive);
        }

        public virtual Task<IFolderOrMissingPath> EnsureIsNotFileAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken)
        {
            return _decorated.EnsureIsNotFileAsync(path, cancellationToken);
        }

        public virtual Task<MissingPath> EnsureDoesNotExistAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true)
        {
            return _decorated.EnsureDoesNotExistAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public virtual bool HasExtension(IFileOrFolderOrMissingPath path, string extension)
        {
            return _decorated.HasExtension(path, extension);
        }

        public virtual string Name(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Name(path);
        }

        public virtual string? Extension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Extension(path);
        }

        public virtual bool MayCreateFile(FileMode fileMode)
        {
            return _decorated.MayCreateFile(fileMode);
        }

        public virtual bool IsImageUri(Uri uri)
        {
            return _decorated.IsImageUri(uri);
        }

        public virtual bool IsVideoUri(Uri uri)
        {
            return _decorated.IsVideoUri(uri);
        }

        public virtual string StripQuotes(string str)
        {
            return _decorated.StripQuotes(str);
        }

        public virtual string SurroundWithDoubleQuotesIfNecessary(string str)
        {
            return _decorated.SurroundWithDoubleQuotesIfNecessary(str);
        }

        public virtual Regex FileNamePatternToRegex(string pattern)
        {
            return _decorated.FileNamePatternToRegex(pattern);
        }

        public virtual IOpenFilesTrackingService OpenFilesTrackingService => _decorated.OpenFilesTrackingService;

        public virtual IEnumerable<KeyValuePair<FileOrFolderOrMissingPath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(IEnumerable<FileOrFolderOrMissingPath> paths)
        {
            return _decorated.ProposeUniqueNamesForMovingPathsToSameFolder(paths);
        }

        public virtual bool IsAbsoluteWindowsPath(string path)
        {
            return _decorated.IsAbsoluteWindowsPath(path);
        }

        public virtual bool IsAbsoluteUnixPath(string path)
        {
            return _decorated.IsAbsoluteUnixPath(path);
        }

        public virtual StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode)
        {
            return _decorated.ToStringComparison(caseSensitivityMode);
        }

        public virtual StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode,
            CaseSensitivityMode otherCaseSensitivityMode)
        {
            return _decorated.ToStringComparison(caseSensitivityMode, otherCaseSensitivityMode);
        }
        
        public virtual bool IsRelativePath(string path)
        {
            return _decorated.IsRelativePath(path);
        }

        public virtual bool IsAbsolutePath(string path)
        {
            return _decorated.IsAbsolutePath(path);
        }

        public virtual bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            return _decorated.ComponentsAreAbsolute(path);
        }

        public virtual IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseRelativePath(path, flags);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryParseAbsolutePath(string path, IFolderPath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseAbsolutePath(path, optionallyRelativeTo, flags);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseAbsolutePath(path, flags);
        }

        public virtual RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseRelativePath(path, flags);
        }

        public virtual FileOrFolderOrMissingPath ParseAbsolutePath(string path, IFolderPath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseAbsolutePath(path, optionallyRelativeTo, flags);
        }

        public virtual FileOrFolderOrMissingPath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseAbsolutePath(path, flags);
        }

        public virtual Task<IPathTranslation> CopyFileAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> CopyFolderAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> MoveFileAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> MoveFolderAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual IPathTranslation CopyFile(IPathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyFile(translation, bufferSize, overwrite);
        }

        public virtual IPathTranslation CopyFolder(IPathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyFolder(translation, bufferSize, overwrite);
        }

        public virtual IPathTranslation MoveFile(IPathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveFile(translation, bufferSize, overwrite);
        }

        public virtual IPathTranslation MoveFolder(IPathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveFolder(translation, bufferSize, overwrite);
        }

        public virtual IPathTranslation Translate(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination)
        {
            return _decorated.Translate(pathToBeCopied, source, destination);
        }

        public virtual IPathTranslation Translate(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination)
        {
            return _decorated.Translate(source, destination);
        }

        public virtual IPathTranslation Copy(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Copy(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public virtual IPathTranslation Copy(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Copy(source, destination, bufferSize, overwrite);
        }

        public virtual IPathTranslation Move(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Move(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public virtual IPathTranslation Move(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Move(source, destination, bufferSize, overwrite);
        }

        public virtual IPathTranslation RenameTo(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.RenameTo(source, target, bufferSize, overwrite);
        }

        public virtual IPathTranslation Copy(IPathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.Copy(translation, bufferSize, overwrite);
        }

        public virtual IPathTranslation Move(IPathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.Move(translation, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> CopyAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> CopyAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> MoveAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> MoveAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> RenameToAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.RenameToAsync(source, target, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> CopyAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IPathTranslation> MoveAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath, string searchPattern, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.EnumerateChildren(folderPath, searchPattern, includeFolders, includeFiles);
        }

        public virtual IEnumerable<FilePath> EnumerateChildFiles(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.EnumerateChildFiles(folderPath, searchPattern);
        }

        public virtual IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.EnumerateChildFolders(folderPath, searchPattern);
        }

        public virtual IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath, string searchPattern, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.EnumerateDescendants(folderPath, searchPattern, includeFolders, includeFiles);
        }

        public virtual IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.EnumerateDescendantFolders(folderPath, searchPattern);
        }

        public virtual IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath folderPath, string searchPattern)
        {
            return _decorated.EnumerateDescendantFiles(folderPath, searchPattern);
        }

        public virtual IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath)
        {
            return _decorated.EnumerateChildren(folderPath);
        }

        public virtual IEnumerable<FilePath> EnumerateChildFiles(IFolderPath folderPath)
        {
            return _decorated.EnumerateChildFiles(folderPath);
        }

        public virtual IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath folderPath)
        {
            return _decorated.EnumerateChildFolders(folderPath);
        }

        public virtual IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath)
        {
            return _decorated.EnumerateDescendants(folderPath);
        }

        public virtual IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath folderPath)
        {
            return _decorated.EnumerateDescendantFolders(folderPath);
        }

        public virtual IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath folderPath)
        {
            return _decorated.EnumerateDescendantFiles(folderPath);
        }

        public virtual bool CanBeSimplified(IFileOrFolderOrMissingPath path)
        {
            return _decorated.CanBeSimplified(path);
        }

        public virtual RelativePath RelativeTo(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo)
        {
            return _decorated.RelativeTo(path, relativeTo);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryCommonWith(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that)
        {
            return _decorated.TryCommonWith(path, that);
        }

        public virtual FileOrFolderOrMissingPath Simplify(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Simplify(path);
        }

        public virtual RelativePath Simplify(RelativePath path)
        {
            return _decorated.Simplify(path);
        }

        public virtual FileOrFolderOrMissingPath Combine(IFolderPath folderPath, params string[] subsequentPathParts)
        {
            return _decorated.Combine(folderPath, subsequentPathParts);
        }

        public virtual FileOrFolderOrMissingPath WithoutExtension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.WithoutExtension(path);
        }

        public virtual Uri Child(Uri parent, Uri child)
        {
            return _decorated.Child(parent, child);
        }

        public virtual FilesOrFoldersOrMissingPaths GlobFiles(IFolderPath folderPath, string pattern)
        {
            return _decorated.GlobFiles(folderPath, pattern);
        }

        public virtual IEnumerable<FolderPath> Ancestors(IFolderPath folderPath, bool includeItself)
        {
            return _decorated.Ancestors(folderPath, includeItself);
        }

        public virtual IEnumerable<IFileOrFolderPath> Ancestors(IFilePath filePath, bool includeItself)
        {
            return _decorated.Ancestors(filePath, includeItself);
        }

        public IEnumerable<IFileOrFolderOrMissingPath> Ancestors(IMissingPath missingPath, bool includeItself)
        {
            return _decorated.Ancestors(missingPath, includeItself);
        }

        public FolderPathAncestors Ancestors(IFolderPath folderPath)
        {
            return _decorated.Ancestors(folderPath);
        }

        public FilePathAncestors Ancestors(IFilePath filePath)
        {
            return _decorated.Ancestors(filePath);
        }

        public MissingPathAncestors Ancestors(IMissingPath missingPath)
        {
            return _decorated.Ancestors(missingPath);
        }

        public IEnumerable<FileOrFolderOrMissingPath> Ancestors(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath, bool includeItself)
        {
            return _decorated.Ancestors(fileOrFolderOrMissingPath, includeItself);
        }

        public FileOrFolderOrMissingPathAncestors Ancestors(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return _decorated.Ancestors(fileOrFolderOrMissingPath);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryDescendant(IFileOrFolderOrMissingPath path, params IFileOrFolderOrMissingPath[] paths)
        {
            return _decorated.TryDescendant(path, paths);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryDescendant(IFileOrFolderOrMissingPath path, params string[] paths)
        {
            return _decorated.TryDescendant(path, paths);
        }

        public virtual bool IsAncestorOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant)
        {
            return _decorated.IsAncestorOf(path, possibleDescendant);
        }

        public virtual bool IsDescendantOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor)
        {
            return _decorated.IsDescendantOf(path, possibleAncestor);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryGetCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetCommonAncestry(path1, path2);
        }

        public virtual IMaybe<Uri> TryGetCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetCommonDescendants(path1, path2);
        }

        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetNonCommonDescendants(path1, path2);
        }

        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetNonCommonAncestry(path1, path2);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryWithExtension(IFileOrFolderOrMissingPath path, string differentExtension)
        {
            return _decorated.TryWithExtension(path, differentExtension);
        }

        public virtual IMaybe<FileOrFolderOrMissingPath> TryWithExtension(IFileOrFolderOrMissingPath path, Func<string, string> differentExtension)
        {
            return _decorated.TryWithExtension(path, differentExtension);
        }

        public virtual bool Exists(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Exists(path);
        }

        public virtual PathType Type(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Type(path);
        }

        public virtual bool HasExtension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.HasExtension(path);
        }

        public virtual bool IsFile(IFileOrFolderOrMissingPath path)
        {
            return _decorated.IsFile(path);
        }

        public virtual bool IsFolder(IFileOrFolderOrMissingPath path)
        {
            return _decorated.IsFolder(path);
        }

        public virtual bool IsReadOnly(IFilePath filePath)
        {
            return _decorated.IsReadOnly(filePath);
        }

        public virtual Information FileSize(IFilePath filePath)
        {
            return _decorated.FileSize(filePath);
        }

        public FileAttributes GetAttributes(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return _decorated.GetAttributes(fileOrFolderOrMissingPath);
        }

        public void SetAttributes(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath, FileAttributes fileAttributes)
        {
            _decorated.SetAttributes(fileOrFolderOrMissingPath, fileAttributes);
        }

        public virtual DateTimeOffset CreationTime(IFilePath filePath)
        {
            return _decorated.CreationTime(filePath);
        }

        public virtual DateTimeOffset LastAccessTime(IFilePath filePath)
        {
            return _decorated.LastAccessTime(filePath);
        }

        public virtual DateTimeOffset LastWriteTime(IFilePath filePath)
        {
            return _decorated.LastWriteTime(filePath);
        }

        public virtual BufferEnumerator ReadBuffers(IFilePath path, FileShare fileShare = FileShare.None, Information? bufferSize = default,
            int paddingAtStart = 0, int paddingAtEnd = 0)
        {
            return _decorated.ReadBuffers(path, fileShare, bufferSize, paddingAtStart, paddingAtEnd);
        }

        public virtual FilePath WriteAllBytes(IFileOrMissingPath path, byte[] bytes,  bool createRecursively = true)
        {
            return _decorated.WriteAllBytes(path, bytes, createRecursively);
        }

        public virtual Stream Open(IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.None | FileOptions.SequentialScan | FileOptions.Asynchronous,
            Information? bufferSize = default,  bool createRecursively = true)
        {
            return _decorated.Open(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public virtual IQueryable<FileOrFolderOrMissingPath> Query()
        {
            return _decorated.Query();
        }

        public virtual ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq(IFolderPath folderPath, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            return _decorated.ToLiveLinq(folderPath, includeFileContentChanges, includeSubFolders, pattern);
        }
    }
}