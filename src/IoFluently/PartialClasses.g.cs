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
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public Boolean HasExtension => IoService.HasExtension(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public AbsolutePath Root => IoService.Root(this);
        public FileAttributes? Attributes => IoService.TryAttributes(this).ValueOrDefault;
        public DateTimeOffset? CreationTime => IoService.TryCreationTime(this).ValueOrDefault;
        public Information? FileSize => IoService.TryFileSize(this).ValueOrDefault;
        public Boolean? IsReadOnly => IoService.TryIsReadOnly(this).ValueOrDefault;
        public DateTimeOffset? LastAccessTime => IoService.TryLastAccessTime(this).ValueOrDefault;
        public DateTimeOffset? LastWriteTime => IoService.TryLastWriteTime(this).ValueOrDefault;
        public AbsolutePath Parent => IoService.TryParent(this).ValueOrDefault;
        public PathType Type => IoService.Type(this);
        public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
    }
}
