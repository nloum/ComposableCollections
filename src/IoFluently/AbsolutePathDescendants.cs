using System.Collections.Generic;
using System.Linq;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren
    {
        internal AbsolutePathDescendants(AbsolutePath path, string pattern, IIoService ioService) : base(path, pattern, true, ioService)
        {
        }
        
        public IEnumerable<TreeTraversal<string, AbsolutePath>> Traverse()
        {
            return _path.TraverseTree(x =>
            {
                var children = GetChildren(x);
                var childrenNames = children.Select(child => child.Name);
                return childrenNames;
            }, (AbsolutePath node, string name, out AbsolutePath child) =>
            {
                child = node / name;
                var result = child.Exists();
                return result;
            });
        }
        
        public override IEnumerator<AbsolutePath> GetEnumerator()
        {
            return Traverse().Skip(1).Where(x => x.Type != TreeTraversalType.ExitBranch).Select(x => x.Value).GetEnumerator();
        }
    }
}