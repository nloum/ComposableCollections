using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class AbsolutePathChildren : AbsolutePathDescendantsOrChildren
    {
        public AbsolutePathChildren(Folder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<AbsolutePath> GetEnumerator()
        {
            return _path.IoService.Children(_path, _pattern).Select(x => new AbsolutePath(x)).GetEnumerator();
        }
    }
}