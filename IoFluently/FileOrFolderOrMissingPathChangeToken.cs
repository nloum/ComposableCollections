using System;
using Microsoft.Extensions.Primitives;
using UtilityDisposables;

namespace IoFluently
{
    public class FileOrFolderOrMissingPathChangeToken : IChangeToken {
        private readonly FileOrFolderOrMissingPath _fileOrFolderOrMissingPath;
        private int _numActiveChangeCallbacks = -1;

        public FileOrFolderOrMissingPathChangeToken( FileOrFolderOrMissingPath fileOrFolderOrMissingPath ) {
            _fileOrFolderOrMissingPath = fileOrFolderOrMissingPath;
            RegisterChangeCallback( _ => HasChanged = true, null );
        }

        public IDisposable RegisterChangeCallback( Action<object> callback, object state ) {
            _numActiveChangeCallbacks++;
            //var disposable = _absolutePath.IoService.ObserveChanges(_absolutePath.ExpectFolder()).Subscribe( _ => callback( state ) );
            return new AnonymousDisposable( () => {
                //disposable.Dispose();
                _numActiveChangeCallbacks--;
            } );
        }

        public bool HasChanged { get; private set; }
        public bool ActiveChangeCallbacks => _numActiveChangeCallbacks > 0;
    }
}