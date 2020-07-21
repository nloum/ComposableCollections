using System;
using System.Threading.Tasks;

namespace IoFluently
{
    internal class PathWithKnownFormatAsync<TReader> : IPathWithKnownFormatAsync<TReader>
    {
        public AbsolutePath Path { get; }
        
        private Func<Task<TReader>> _read;

        public PathWithKnownFormatAsync(AbsolutePath path, Func<Task<TReader>> read)
        {
            Path = path;
            _read = read;
        }

        public Task<TReader> Read()
        {
            return _read();
        }

        protected bool Equals(PathWithKnownFormatAsync<TReader> other)
        {
            return Equals(Path, other.Path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathWithKnownFormatAsync<TReader>) obj);
        }

        public override int GetHashCode()
        {
            return (Path != null ? Path.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and cannot be written to";
        }
    }
    
    internal class PathWithKnownFormatSync<TReader> : IPathWithKnownFormatSync<TReader>
    {
        public AbsolutePath Path { get; }
        
        private Func<TReader> _read;

        public PathWithKnownFormatSync(AbsolutePath path, Func<TReader> read)
        {
            Path = path;
            _read = read;
        }

        public TReader Read()
        {
            return _read();
        }

        protected bool Equals(PathWithKnownFormatSync<TReader> other)
        {
            return Equals(Path, other.Path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathWithKnownFormatSync<TReader>) obj);
        }

        public override int GetHashCode()
        {
            return (Path != null ? Path.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"{Path}, read synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and cannot be written to";
        }
    }

    internal class PathWithKnownFormatAsync<TReader, TWriter> : PathWithKnownFormatAsync<TReader>, IPathWithKnownFormatAsync<TReader, TWriter>
    {
        private readonly AbsolutePath _path;
        private Func<TWriter, Task> _write;

        public PathWithKnownFormatAsync(AbsolutePath path, Func<Task<TReader>> read,
            Func<TWriter, Task> write) : base(path, read)
        {
            _path = path;
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(writer);
        }

        public AbsolutePath Path { get; }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatAsyncSync<TReader, TWriter> : PathWithKnownFormatAsync<TReader>, IPathWithKnownFormatAsyncSync<TReader, TWriter>
    {
        private readonly AbsolutePath _path;
        private Action<TWriter> _write;

        public PathWithKnownFormatAsyncSync(AbsolutePath path, Func<Task<TReader>> read,
            Action<TWriter> write) : base(path, read)
        {
            _path = path;
            _write = write;
        }

        public void Write(TWriter writer)
        {
            _write(writer);
        }

        protected bool Equals(PathWithKnownFormatAsyncSync<TReader, TWriter> other)
        {
            return base.Equals(other) && Equals(_path, other._path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathWithKnownFormatAsyncSync<TReader, TWriter>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (_path != null ? _path.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatSync<TReader, TWriter> : PathWithKnownFormatSync<TReader>, IPathWithKnownFormatSync<TReader, TWriter>
    {
        private readonly AbsolutePath _path;
        private Action<TWriter> _write;

        public PathWithKnownFormatSync(AbsolutePath path, Func<TReader> read,
            Action<TWriter> write) : base(path, read)
        {
            _path = path;
            _write = write;
        }

        public void Write(TWriter writer)
        {
            _write(writer);
        }

        protected bool Equals(PathWithKnownFormatSync<TReader, TWriter> other)
        {
            return base.Equals(other) && Equals(_path, other._path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathWithKnownFormatSync<TReader, TWriter>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (_path != null ? _path.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{Path}, read synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatSyncAsync<TReader, TWriter> : PathWithKnownFormatSync<TReader>, IPathWithKnownFormatSyncAsync<TReader, TWriter>
    {
        private readonly AbsolutePath _path;
        private Func<TWriter, Task> _write;

        public PathWithKnownFormatSyncAsync(AbsolutePath path, Func<TReader> read,
            Func<TWriter, Task> write) : base(path, read)
        {
            _path = path;
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(writer);
        }

        protected bool Equals(PathWithKnownFormatSyncAsync<TReader, TWriter> other)
        {
            return base.Equals(other) && Equals(_path, other._path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathWithKnownFormatSyncAsync<TReader, TWriter>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (_path != null ? _path.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{Path}, read synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    public interface IPathWithKnownFormatAsync<TReader>
    {
        AbsolutePath Path { get; }
        Task<TReader> Read();
    }
    
    public interface IPathWithKnownFormatSync<out TReader>
    {
        AbsolutePath Path { get; }
        TReader Read();
    }

    public interface IPathWithKnownFormatAsync<TReader, in TWriter> : IPathWithKnownFormatAsync<TReader>
    {
        Task Write(TWriter writer);
    }
    
    public interface IPathWithKnownFormatAsyncSync<TReader, in TWriter> : IPathWithKnownFormatAsync<TReader>
    {
        void Write(TWriter writer);
    }
    
    public interface IPathWithKnownFormatSync<out TReader, in TWriter> : IPathWithKnownFormatSync<TReader>
    {
        void Write(TWriter writer);
    }
    
    public interface IPathWithKnownFormatSyncAsync<out TReader, in TWriter> : IPathWithKnownFormatSync<TReader>
    {
        Task Write(TWriter writer);
    }
}