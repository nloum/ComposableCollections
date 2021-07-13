﻿using System;
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

// TODO - copy from PartialClasses.g.cs and then change the types to the default implementations for each interface.
// TODO - make this automated in IoFluently.CodeGenerator

namespace IoFluently {
    public partial class MissingPath {
        public IEnumerable<IFolderOrMissingPath> Ancestors => IoService.Ancestors(this);
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
namespace IoFluently {
    public partial class AbsolutePath {
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
namespace IoFluently {
    public partial class Folder {
        public IEnumerable<Folder> Ancestors => IoService.Ancestors(this);
        public IEnumerable<File> ChildFiles => IoService.ChildFiles(this);
        public IEnumerable<Folder> ChildFolders => IoService.ChildFolders(this);
        public IEnumerable<IFileOrFolder> Children => IoService.Children(this);
        public IEnumerable<File> DescendantFiles => IoService.DescendantFiles(this);
        public IEnumerable<Folder> DescendantFolders => IoService.DescendantFolders(this);
        public IEnumerable<IFileOrFolder> Descendants => IoService.Descendants(this);
        public IMaybe<Folder> Parent => IoService.TryParent(this);
    }
}