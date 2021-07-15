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
        public IEnumerable<Folder> Ancestors => IoService.Ancestors(this);
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public string Extension => IoService.Extension(this);
        public Boolean HasExtension => IoService.HasExtension(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public string Name => IoService.Name(this);
        public Folder Root => IoService.Root(this);
        public IMaybe<Folder> Parent => IoService.TryParent(this);
        public PathType Type => IoService.Type(this);
        public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
    }
}
namespace IoFluently 
{
    public partial class AbsolutePath 
    {
        public IEnumerable<AbsolutePath> Ancestors => IoService.Ancestors(this);
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public string Extension => IoService.Extension(this);
        public Boolean HasExtension => IoService.HasExtension(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public string Name => IoService.Name(this);
        public Folder Root => IoService.Root(this);
        public IMaybe<AbsolutePath> Parent => IoService.TryParent(this);
        public PathType Type => IoService.Type(this);
        public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
    }
}
namespace IoFluently 
{
    public partial class File 
    {
        public IEnumerable<Folder> Ancestors => IoService.Ancestors(this);
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public string Extension => IoService.Extension(this);
        public Boolean HasExtension => IoService.HasExtension(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public string Name => IoService.Name(this);
        public Folder Root => IoService.Root(this);
        public IMaybe<AbsolutePath> Parent => IoService.TryParent(this);
        public PathType Type => IoService.Type(this);
        public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
        public FileAttributes Attributes => IoService.Attributes(this);
        public DateTimeOffset CreationTime => IoService.CreationTime(this);
        public Information FileSize => IoService.FileSize(this);
        public Boolean IsReadOnly => IoService.IsReadOnly(this);
        public DateTimeOffset LastAccessTime => IoService.LastAccessTime(this);
        public DateTimeOffset LastWriteTime => IoService.LastWriteTime(this);
    }
}
namespace IoFluently 
{
    public partial class MissingPath 
    {
        public IEnumerable<IFolderOrMissingPath> Ancestors => IoService.Ancestors(this);
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public string Extension => IoService.Extension(this);
        public Boolean HasExtension => IoService.HasExtension(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public string Name => IoService.Name(this);
        public Folder Root => IoService.Root(this);
        public IMaybe<AbsolutePath> Parent => IoService.TryParent(this);
        public PathType Type => IoService.Type(this);
        public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
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
        public IMaybe<Folder> Parent { get; }
    }
}
namespace IoFluently 
{
    public partial interface IMissingPath 
    {
        public IEnumerable<IFolderOrMissingPath> Ancestors { get; }
    }
}
