using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IoFluently
{
    /// <summary>
    /// A Stream class that takes another Stream object in its constructor and calls the onClose and onDispose callbacks
    /// when Close and Dispose are called on this object. This makes it easy to know when the file is closed.
    /// </summary>
    public class StreamCloseDisposeDecorator : Stream
    {
        private readonly Stream _stream;
        private readonly Action _onClose;
        private readonly Action _onDispose;

        /// <summary>
        /// Create a StreamCloseDisposeDecorator object.
        /// </summary>
        /// <param name="stream">The stream to be decorated</param>
        /// <param name="onClose">The callback that is called when this Stream object is closed</param>
        /// <param name="onDispose">The callback that is called when this Stream object is disposed</param>
        public StreamCloseDisposeDecorator(Stream stream, Action onClose, Action onDispose)
        {
            _stream = stream;
            _onClose = onClose;
            _onDispose = onDispose;
        }

        public object GetLifetimeService()
        {
            return _stream.GetLifetimeService();
        }

        public object InitializeLifetimeService()
        {
            return _stream.InitializeLifetimeService();
        }

        public IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginRead(buffer, offset, count, callback, state);
        }

        public IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginWrite(buffer, offset, count, callback, state);
        }

        public void Close()
        {
            _stream.Close();
            _onClose();
        }

        public void CopyTo(Stream destination)
        {
            _stream.CopyTo(destination);
        }

        public void CopyTo(Stream destination, int bufferSize)
        {
            _stream.CopyTo(destination, bufferSize);
        }

        public Task CopyToAsync(Stream destination)
        {
            return _stream.CopyToAsync(destination);
        }

        public Task CopyToAsync(Stream destination, int bufferSize)
        {
            return _stream.CopyToAsync(destination, bufferSize);
        }

        public Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return _stream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        public void Dispose()
        {
            _stream.Dispose();
            _onDispose();
        }

        public int EndRead(IAsyncResult asyncResult)
        {
            return _stream.EndRead(asyncResult);
        }

        public void EndWrite(IAsyncResult asyncResult)
        {
            _stream.EndWrite(asyncResult);
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public Task FlushAsync()
        {
            return _stream.FlushAsync();
        }

        public Task FlushAsync(CancellationToken cancellationToken)
        {
            return _stream.FlushAsync(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return _stream.ReadAsync(buffer, offset, count);
        }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public int ReadByte()
        {
            return _stream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count)
        {
            return _stream.WriteAsync(buffer, offset, count);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public void WriteByte(byte value)
        {
            _stream.WriteByte(value);
        }

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public bool CanTimeout => _stream.CanTimeout;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position
        {
            get => _stream.Position;
            set => _stream.Position = value;
        }

        public int ReadTimeout
        {
            get => _stream.ReadTimeout;
            set => _stream.ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => _stream.WriteTimeout;
            set => _stream.WriteTimeout = value;
        }
    }
}