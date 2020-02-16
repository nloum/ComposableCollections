using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreIO
{
	/// <summary>
	/// From here: http://stackoverflow.com/a/3879231
	/// </summary>
	public class ConcatenatedStream : Stream
	{
		readonly Queue<Stream> _streams;

		public ConcatenatedStream(params Stream[] streams)
		{
            this._streams = new Queue<Stream>(streams);
		}

		public ConcatenatedStream(IEnumerable<Stream> streams)
		{
			this._streams = new Queue<Stream>(streams);
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_streams.Count == 0)
				return 0;

			int bytesRead = _streams.Peek().Read(buffer, offset, count);
			if (bytesRead == 0)
			{
				_streams.Dequeue().Dispose();
				bytesRead += Read(buffer, offset + bytesRead, count - bytesRead);
			}
			return bytesRead;
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void Flush()
		{
			throw new NotImplementedException();
		}

		public override long Length
		{
			get { throw new NotImplementedException(); }
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	}


}
