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
        public IEnumerable<AbsolutePath> Ancestors { get; }
        public Boolean CanBeSimplified { get; }
        public Boolean Exists { get; }
        public string Extension { get; }
        public Boolean HasExtension { get; }
        public Boolean IsFile { get; }
        public Boolean IsFolder { get; }
        public string Name { get; }
        public Folder Root { get; }
        public IMaybe<AbsolutePath> Parent { get; }
        public PathType Type { get; }
        public AbsolutePath WithoutExtension { get; }
    }
}
namespace IoFluently 
{
    public partial class Folder 
    {
        public IEnumerable<Folder> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public Folder Root => FileSystem.Root(this);
        public IMaybe<Folder> Parent => FileSystem.TryParent(this);
        public PathType Type => FileSystem.Type(this);
        public AbsolutePath WithoutExtension => FileSystem.WithoutExtension(this);
        public AbsolutePathChildFiles ChildFiles => FileSystem.ChildFiles(this);
        public AbsolutePathChildFolders ChildFolders => FileSystem.ChildFolders(this);
        public AbsolutePathChildren Children => FileSystem.Children(this);
        public AbsolutePathDescendantFiles DescendantFiles => FileSystem.DescendantFiles(this);
        public AbsolutePathDescendantFolders DescendantFolders => FileSystem.DescendantFolders(this);
        public AbsolutePathDescendants Descendants => FileSystem.Descendants(this);
    }
}
namespace IoFluently 
{
    public partial class AbsolutePath 
    {
        public IEnumerable<AbsolutePath> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public Folder Root => FileSystem.Root(this);
        public IMaybe<AbsolutePath> Parent => FileSystem.TryParent(this);
        public PathType Type => FileSystem.Type(this);
        public AbsolutePath WithoutExtension => FileSystem.WithoutExtension(this);
    }
}
namespace IoFluently 
{
    public partial class File 
    {
        public IEnumerable<Folder> Ancestors => FileSystem.Ancestors(this);
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public Folder Root => FileSystem.Root(this);
        public Folder Parent => FileSystem.Parent(this);
        public PathType Type => FileSystem.Type(this);
        public AbsolutePath WithoutExtension => FileSystem.WithoutExtension(this);
        public FileAttributes Attributes => FileSystem.Attributes(this);
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
        public Folder Root => FileSystem.Root(this);
        public IMaybe<AbsolutePath> Parent => FileSystem.TryParent(this);
        public PathType Type => FileSystem.Type(this);
        public AbsolutePath WithoutExtension => FileSystem.WithoutExtension(this);
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
    public partial interface IFile 
    {
        public IEnumerable<Folder> Ancestors { get; }
        public FileAttributes Attributes { get; }
        public DateTimeOffset CreationTime { get; }
        public Information FileSize { get; }
        public Boolean IsReadOnly { get; }
        public DateTimeOffset LastAccessTime { get; }
        public DateTimeOffset LastWriteTime { get; }
        public Folder Parent { get; }
    }
}
namespace IoFluently 
{
    public partial interface IFolder 
    {
        public IEnumerable<Folder> Ancestors { get; }
        public AbsolutePathChildFiles ChildFiles { get; }
        public AbsolutePathChildFolders ChildFolders { get; }
        public AbsolutePathChildren Children { get; }
        public AbsolutePathDescendantFiles DescendantFiles { get; }
        public AbsolutePathDescendantFolders DescendantFolders { get; }
        public AbsolutePathDescendants Descendants { get; }
        public IMaybe<Folder> Parent { get; }
    }
}
