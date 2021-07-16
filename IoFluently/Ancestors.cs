using System.Collections;
using System.Collections.Generic;
using ComposableCollections;

namespace IoFluently
{
    public abstract class Ancestors<TFileOrFolderOrMissingPath> : IReadOnlyList<TFileOrFolderOrMissingPath>
    {
        private IFileOrFolderOrMissingPath _path;

        protected Ancestors(IFileOrFolderOrMissingPath path)
        {
            _path = path;
        }

        protected abstract TFileOrFolderOrMissingPath Expect(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TFileOrFolderOrMissingPath> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        public int Count => _path.Components.Count - 1;

        public TFileOrFolderOrMissingPath this[int index] => Expect(new FileOrFolderOrMissingPath(_path.Components.Take(Count - index), _path.IsCaseSensitive, _path.DirectorySeparator, _path.FileSystem));
    }
}