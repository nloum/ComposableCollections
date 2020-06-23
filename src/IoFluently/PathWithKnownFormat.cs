using System;
using System.Threading.Tasks;

namespace IoFluently
{
    internal class PathWithKnownFormatAsync<TReader> : IPathWithKnownFormatAsync<TReader>
    {
        public AbsolutePath Path { get; }
        
        private Func<AbsolutePath, Task<TReader>> _read;

        public PathWithKnownFormatAsync(AbsolutePath path, Func<AbsolutePath, Task<TReader>> read)
        {
            Path = path;
            _read = read;
        }

        public Task<TReader> Read()
        {
            return _read(Path);
        }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and cannot be written to";
        }
    }
    
    internal class PathWithKnownFormatSync<TReader> : IPathWithKnownFormatSync<TReader>
    {
        public AbsolutePath Path { get; }
        
        private Func<AbsolutePath, TReader> _read;

        public PathWithKnownFormatSync(AbsolutePath path, Func<AbsolutePath, TReader> read)
        {
            Path = path;
            _read = read;
        }

        public TReader Read()
        {
            return _read(Path);
        }
        
        public override string ToString()
        {
            return $"{Path}, read synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and cannot be written to";
        }
    }

    internal class PathWithKnownFormatAsync<TReader, TWriter> : PathWithKnownFormatAsync<TReader>, IPathWithKnownFormatAsync<TReader, TWriter>
    {
        private Func<AbsolutePath, TWriter, Task> _write;

        public PathWithKnownFormatAsync(AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Func<AbsolutePath, TWriter, Task> write) : base(path, read)
        {
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(Path, writer);
        }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatAsyncSync<TReader, TWriter> : PathWithKnownFormatAsync<TReader>, IPathWithKnownFormatAsyncSync<TReader, TWriter>
    {
        private Action<AbsolutePath, TWriter> _write;

        public PathWithKnownFormatAsyncSync(AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Action<AbsolutePath, TWriter> write) : base(path, read)
        {
            _write = write;
        }

        public void Write(TWriter writer)
        {
            _write(Path, writer);
        }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatSync<TReader, TWriter> : PathWithKnownFormatSync<TReader>, IPathWithKnownFormatSync<TReader, TWriter>
    {
        private Action<AbsolutePath, TWriter> _write;

        public PathWithKnownFormatSync(AbsolutePath path, Func<AbsolutePath, TReader> read,
            Action<AbsolutePath, TWriter> write) : base(path, read)
        {
            _write = write;
        }

        public void Write(TWriter writer)
        {
            _write(Path, writer);
        }

        public override string ToString()
        {
            return $"{Path}, read synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to synchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatSyncAsync<TReader, TWriter> : PathWithKnownFormatSync<TReader>, IPathWithKnownFormatSyncAsync<TReader, TWriter>
    {
        private Func<AbsolutePath, TWriter, Task> _write;

        public PathWithKnownFormatSyncAsync(AbsolutePath path, Func<AbsolutePath, TReader> read,
            Func<AbsolutePath, TWriter, Task> write) : base(path, read)
        {
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(Path, writer);
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