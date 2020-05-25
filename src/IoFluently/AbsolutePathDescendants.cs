using System.Collections.Generic;
using System.Linq;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren
    {
        public AbsolutePathDescendants(AbsolutePath path, string pattern, IIoService ioService) : base(path, pattern, true, ioService)
        {
        }
        
        public IEnumerable<TreeTraversal<string, AbsolutePath>> Traverse()
        {
            return _path.TraverseTree(x => GetChildren(x).Select(child => child.Name), (AbsolutePath node, string name, out AbsolutePath child) =>
            {
                child = node.TryDescendant(name).Value;
                return child.Exists();
            });
        }
        
        public override IEnumerator<AbsolutePath> GetEnumerator()
        {
            return Traverse().Skip(1).Where(x => x.Type != TreeTraversalType.ExitBranch).Select(x => x.Value).GetEnumerator();
        }
    }
}