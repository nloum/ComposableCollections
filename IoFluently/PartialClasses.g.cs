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


namespace IoFluently {
    public partial class MissingPath {
        public IEnumerable<FolderOrMissingPath> Ancestors => IoService.Ancestors(this);
    }
}
namespace IoFluently {
    public partial interface IAbsolutePath
    {
        PathType Type => IoService.Type(this);
    }
    
    public partial class FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> {
        public IEnumerable<FileOrFolderOrMissingPath> Ancestors => IoService.Ancestors(this);
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public Folder Root => IoService.Root(this);
        public IMaybe<FileOrFolderOrMissingPath> Parent => IoService.TryParent(this);
        public PathType Type => IoService.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => IoService.WithoutExtension(this);
    }
}
namespace IoFluently {
    public partial class Folder {
        public IEnumerable<Folder> Ancestors => IoService.Ancestors(this);
        public IMaybe<Folder> Parent => IoService.TryParent(this);
    }
}
namespace IoFluently {
    public partial class File {
        public IEnumerable<Folder> Ancestors => IoService.Ancestors(this);
        public FileAttributes Attributes => IoService.Attributes(this);
        public DateTimeOffset CreationTime => IoService.CreationTime(this);
        public Information FileSize => IoService.FileSize(this);
        public Boolean IsReadOnly => IoService.IsReadOnly(this);
        public DateTimeOffset LastAccessTime => IoService.LastAccessTime(this);
        public DateTimeOffset LastWriteTime => IoService.LastWriteTime(this);
        public Folder Parent => IoService.Parent(this);
    }
}