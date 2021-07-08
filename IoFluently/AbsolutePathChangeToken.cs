using System;
using Microsoft.Extensions.Primitives;
using UtilityDisposables;

namespace IoFluently
{
    public class AbsolutePathChangeToken : IChangeToken {
        private readonly FileOrFolderOrMissingPath _fileOrFolderOrMissingPath;
        private int _numActiveChangeCallbacks = -1;

        public AbsolutePathChangeToken( FileOrFolderOrMissingPath fileOrFolderOrMissingPath ) {
            _fileOrFolderOrMissingPath = fileOrFolderOrMissingPath;
            RegisterChangeCallback( _ => HasChanged = true, null );
        }

        public IDisposable RegisterChangeCallback( Action<object> callback, object state ) {
            _numActiveChangeCallbacks++;
            var disposable = _fileOrFolderOrMissingPath.IoService.ObserveChanges(_fileOrFolderOrMissingPath).Subscribe( _ => callback( state ) );
            return new AnonymousDisposable( () => {
                disposable.Dispose();
                _numActiveChangeCallbacks--;
            } );
        }

        public bool HasChanged { get; private set; }
        public bool ActiveChangeCallbacks => _numActiveChangeCallbacks > 0;
    }
}