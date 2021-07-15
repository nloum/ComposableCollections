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
    public partial class FilePath
    {
        IEnumerable<AbsolutePath> IFileOrFolderOrMissingPath.Ancestors => Ancestors.Select(folder => folder.ExpectFileOrFolderOrMissingPath());

        FolderPath IFile.Parent => Parent.Value.ExpectFolder();
    }

    public partial class FolderPath
    {
        IEnumerable<AbsolutePath> IFileOrFolderOrMissingPath.Ancestors => Ancestors.Select(ancestor => ancestor.ExpectFileOrFolderOrMissingPath());

        IMaybe<AbsolutePath> IFileOrFolderOrMissingPath.Parent => Parent.Select(x => x.ExpectFileOrFolderOrMissingPath());
    }

    public partial class MissingPath
    {
        IEnumerable<AbsolutePath> IFileOrFolderOrMissingPath.Ancestors => Ancestors.Select(ancestor => ancestor.ExpectFileOrFolderOrMissingPath());
    }
    
    public partial class FileOrMissingPathBase {
        IEnumerable<AbsolutePath> IFileOrFolderOrMissingPath.Ancestors => Value.Ancestors.Select(ancestor => ancestor.ExpectFileOrFolderOrMissingPath());
    }
}