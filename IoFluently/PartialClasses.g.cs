using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;
using TreeLinq;
using UnitsNet;


namespace IoFluently 
{
    public partial interface IFileOrFolderOrMissingPath 
    {
        public IEnumerable<FileOrFolderOrMissingPath> Ancestors { get; }
        public Boolean CanBeSimplified { get; }
        public Boolean Exists { get; }
        public string Extension { get; }
        public Boolean HasExtension { get; }
        public Boolean IsFile { get; }
        public Boolean IsFolder { get; }
        public string Name { get; }
        public FolderPath Root { get; }
        public IMaybe<FileOrFolderOrMissingPath> Parent { get; }
        public PathType Type { get; }
        public FileOrFolderOrMissingPath WithoutExtension { get; }
    }
}
namespace IoFluently 
{
    public partial class FolderPath 
    {
        public IEnumerable<FolderPath> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public FolderPath Root => FileSystem.Root(this);
        public IMaybe<FolderPath> Parent => FileSystem.TryParent(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);
        public ChildFiles ChildFiles => FileSystem.ChildFiles(this);
        public ChildFolders ChildFolders => FileSystem.ChildFolders(this);
        public ChildFilesOrFolders Children => FileSystem.Children(this);
        public DescendantFiles DescendantFiles => FileSystem.DescendantFiles(this);
        public DescendantFolders DescendantFolders => FileSystem.DescendantFolders(this);
        public DescendantFilesOrFolders Descendants => FileSystem.Descendants(this);
    }
}
namespace IoFluently 
{
    public partial class FileOrFolderOrMissingPath 
    {
        public IEnumerable<FileOrFolderOrMissingPath> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public FolderPath Root => FileSystem.Root(this);
        public IMaybe<FileOrFolderOrMissingPath> Parent => FileSystem.TryParent(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);
    }
}
namespace IoFluently 
{
    public partial class FilePath 
    {
        public IEnumerable<FolderPath> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public FolderPath Root => FileSystem.Root(this);
        public FolderPath Parent => FileSystem.Parent(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);
        public DateTimeOffset CreationTime => FileSystem.CreationTime(this);
        public Information FileSize => FileSystem.FileSize(this);
        public Boolean IsReadOnly => FileSystem.IsReadOnly(this);
        public DateTimeOffset LastAccessTime => FileSystem.LastAccessTime(this);
        public DateTimeOffset LastWriteTime => FileSystem.LastWriteTime(this);
    }
}
namespace IoFluently 
{
    public partial class MissingPath 
    {
        public IEnumerable<IFolderOrMissingPath> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public FolderPath Root => FileSystem.Root(this);
        public IMaybe<FileOrFolderOrMissingPath> Parent => FileSystem.TryParent(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);
    }
}
namespace IoFluently 
{
    public partial interface IMissingPath 
    {
        public IEnumerable<IFolderOrMissingPath> Ancestors { get; }
    }
}
namespace IoFluently 
{
    public partial interface IFilePath 
    {
        public IEnumerable<FolderPath> Ancestors { get; }
        public FileAttributes Attributes { get; }
        public DateTimeOffset CreationTime { get; }
        public Information FileSize { get; }
        public Boolean IsReadOnly { get; }
        public DateTimeOffset LastAccessTime { get; }
        public DateTimeOffset LastWriteTime { get; }
        public FolderPath Parent { get; }
    }
}
namespace IoFluently 
{
    public partial interface IFolderPath 
    {
        public IEnumerable<FolderPath> Ancestors { get; }
        public ChildFiles ChildFiles { get; }
        public ChildFolders ChildFolders { get; }
        public ChildFilesOrFolders Children { get; }
        public DescendantFiles DescendantFiles { get; }
        public DescendantFolders DescendantFolders { get; }
        public DescendantFilesOrFolders Descendants { get; }
        public IMaybe<FolderPath> Parent { get; }
    }
}
