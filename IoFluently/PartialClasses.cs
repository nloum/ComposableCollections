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
        IEnumerable<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Ancestors => Ancestors.Select(folder => folder.ExpectFileOrFolderOrMissingPath());

        FolderPath IFilePath.Parent => Parent.Value.ExpectFolder();
    }

    public partial class FolderPath
    {
        IEnumerable<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Ancestors => Ancestors.Select(ancestor => ancestor.ExpectFileOrFolderOrMissingPath());

        IMaybe<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Parent => Parent.Select(x => x.ExpectFileOrFolderOrMissingPath());
    }

    public partial class MissingPath
    {
        IEnumerable<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Ancestors => Ancestors.Select(ancestor => ancestor.ExpectFileOrFolderOrMissingPath());
    }
    
    public partial class FileOrMissingPathBase {
        IEnumerable<FileOrFolderOrMissingPath> IFileOrFolderOrMissingPath.Ancestors => Value.Ancestors.Select(ancestor => ancestor.ExpectFileOrFolderOrMissingPath());
    }
}