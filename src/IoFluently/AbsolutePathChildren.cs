using System.Collections.Generic;

namespace IoFluently
{
    public class AbsolutePathChildren : AbsolutePathDescendantsOrChildren
    {
        internal AbsolutePathChildren(AbsolutePath path, string pattern, IIoService ioService) : base(path, pattern, false, ioService)
        {
        }

        public override IEnumerator<AbsolutePath> GetEnumerator()
        {
            return GetChildren(_path).GetEnumerator();
        }
    }
}