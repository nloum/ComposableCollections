using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren, IFileProvider
    {
        internal AbsolutePathDescendants(AbsolutePath path, string pattern, IIoService ioService) : base(path, pattern, true, ioService)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter(_path / subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(_path / subpath);
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public IEnumerable<TreeTraversal<string, AbsolutePath>> Traverse()
        {
            return _path.TraverseTree(x =>
            {
                var children = x.IoService.EnumerateChildren(x);
                var childrenNames = children.Select(child => child.Name);
                return childrenNames;
            }, (AbsolutePath node, string name, out AbsolutePath child) =>
            {
                child = node / name;
                if (IoService.CanEmptyDirectoriesExist)
                {
                    var result = child.IoService.Exists(child);
                    return result;
                }

                return true;
            });
        }
        
        public override IEnumerator<AbsolutePath> GetEnumerator()
        {
            return Traverse().Skip(1).Where(x => x.Type != TreeTraversalType.ExitBranch).Select(x => x.Value).GetEnumerator();
        }
    }
}