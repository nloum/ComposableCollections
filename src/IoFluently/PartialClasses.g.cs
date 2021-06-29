using System;
using System.Collections.Generic;
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
public partial class AbsolutePath {
public IEnumerable<AbsolutePath> Ancestors => IoService.Ancestors(this);
public FileAttributes Attributes => IoService.Attributes(this);
public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
public Boolean ContainsFiles => IoService.ContainsFiles(this);
public DateTimeOffset CreationTime => IoService.CreationTime(this);
public Boolean Exists => IoService.Exists(this);
public Information FileSize => IoService.FileSize(this);
public Boolean FolderContainsFiles => IoService.FolderContainsFiles(this);
public Boolean HasExtension => IoService.HasExtension(this);
public Boolean IsFile => IoService.IsFile(this);
public Boolean IsFolder => IoService.IsFolder(this);
public DateTimeOffset LastAccessTime => IoService.LastAccessTime(this);
public DateTimeOffset LastWriteTime => IoService.LastWriteTime(this);
public AbsolutePath Parent => IoService.Parent(this);
public AbsolutePath Root => IoService.Root(this);
public PathType Type => IoService.Type(this);
public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
}
}
