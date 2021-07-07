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
    public class StreamCloseDecorator : Stream
    {
        private readonly Stream _stream;
        private readonly Action _onClose;
        private bool _hasBeenClosed = false;

        /// <summary>
        /// Create a StreamCloseDisposeDecorator object.
        /// </summary>
        /// <param name="stream">The stream to be decorated</param>
        /// <param name="onClose">The callback that is called when this Stream object is closed</param>
        /// <param name="onDispose">The callback that is called when this Stream object is disposed</param>
        public StreamCloseDecorator(Stream stream, Action onClose)
        {
            _stream = stream;
            _onClose = onClose;
        }

        /// <inheritdoc />
        public override object InitializeLifetimeService()
        {
            return _stream.InitializeLifetimeService();
        }

        /// <inheritdoc />
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginRead(buffer, offset, count, callback, state);
        }

        /// <inheritdoc />
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <inheritdoc />
        public override void Close()
        {
            // Ensure that _onClose only gets called once
            if (!_hasBeenClosed)
            {
                _hasBeenClosed = true;
                _onClose();
            }
            _stream.Close();
        }

        /// <inheritdoc />
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return _stream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        /// <inheritdoc />
        public override int EndRead(IAsyncResult asyncResult)
        {
            return _stream.EndRead(asyncResult);
        }

        /// <inheritdoc />
        public override void EndWrite(IAsyncResult asyncResult)
        {
            _stream.EndWrite(asyncResult);
        }

        /// <inheritdoc />
        public override void Flush()
        {
            _stream.Flush();
        }

        /// <inheritdoc />
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return _stream.FlushAsync(cancellationToken);
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        /// <inheritdoc />
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        /// <inheritdoc />
        public override int ReadByte()
        {
            return _stream.ReadByte();
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        /// <inheritdoc />
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        /// <inheritdoc />
        public override void WriteByte(byte value)
        {
            _stream.WriteByte(value);
        }

        /// <inheritdoc />
        public override bool CanRead => _stream.CanRead;

        /// <inheritdoc />
        public override bool CanSeek => _stream.CanSeek;

        /// <inheritdoc />
        public override bool CanTimeout => _stream.CanTimeout;

        /// <inheritdoc />
        public override bool CanWrite => _stream.CanWrite;

        /// <inheritdoc />
        public override long Length => _stream.Length;

        /// <inheritdoc />
        public override long Position
        {
            get => _stream.Position;
            set => _stream.Position = value;
        }

        /// <inheritdoc />
        public override int ReadTimeout
        {
            get => _stream.ReadTimeout;
            set => _stream.ReadTimeout = value;
        }

        /// <inheritdoc />
        public override int WriteTimeout
        {
            get => _stream.WriteTimeout;
            set => _stream.WriteTimeout = value;
        }
    }
}