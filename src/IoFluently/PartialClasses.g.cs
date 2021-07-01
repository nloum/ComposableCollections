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
    public partial class AbsolutePath {
        public IEnumerable<AbsolutePath> Ancestors => IoService.Ancestors(this);
        public Boolean CanBeSimplified => IoService.CanBeSimplified(this);
        public Boolean Exists => IoService.Exists(this);
        public Boolean HasExtension => IoService.HasExtension(this);
        public Boolean IsFile => IoService.IsFile(this);
        public Boolean IsFolder => IoService.IsFolder(this);
        public AbsolutePath Root => IoService.Root(this);
        public FileAttributes? Attributes => IoService.TryAttributes(this).Select(x => (FileAttributes?)x).ValueOrDefault;
        public DateTimeOffset? CreationTime => IoService.TryCreationTime(this).Select(x => (DateTimeOffset?)x).ValueOrDefault;
        public Information? FileSize => IoService.TryFileSize(this).Select(x => (Information?)x).ValueOrDefault;
        public Encoding Encoding => IoService.TryGetEncoding(this).ValueOrDefault;
        public string NewLine => IoService.TryGetNewLine(this).ValueOrDefault;
        public Boolean? IsReadOnly => IoService.TryIsReadOnly(this).Select(x => (Boolean?)x).ValueOrDefault;
        public DateTimeOffset? LastAccessTime => IoService.TryLastAccessTime(this).Select(x => (DateTimeOffset?)x).ValueOrDefault;
        public DateTimeOffset? LastWriteTime => IoService.TryLastWriteTime(this).Select(x => (DateTimeOffset?)x).ValueOrDefault;
        public AbsolutePath Parent => IoService.TryParent(this).ValueOrDefault;
        public PathType Type => IoService.Type(this);
        public AbsolutePath WithoutExtension => IoService.WithoutExtension(this);
    }
}
