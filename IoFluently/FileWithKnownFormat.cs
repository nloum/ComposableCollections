using System;
using System.Threading.Tasks;

namespace IoFluently
{
    internal class FileWithKnownFormatAsync<TReader> : IFileWithKnownFormatAsync<TReader>
    {
        public AbsolutePath Path { get; }
        
        private Func<AbsolutePath, Task<TReader>> _read;

        public FileWithKnownFormatAsync(AbsolutePath path, Func<AbsolutePath, Task<TReader>> read)
        {
            Path = path;
            _read = read;
        }

        public Task<TReader> Read()
        {
            return _read(Path);
        }
    }
    
    internal class FileWithKnownFormatSync<TReader> : IFileWithKnownFormatSync<TReader>
    {
        public AbsolutePath Path { get; }
        
        private Func<AbsolutePath, TReader> _read;

        public FileWithKnownFormatSync(AbsolutePath path, Func<AbsolutePath, TReader> read)
        {
            Path = path;
            _read = read;
        }

        public TReader Read()
        {
            return _read(Path);
        }
    }

    internal class FileWithKnownFormatAsync<TReader, TWriter> : FileWithKnownFormatAsync<TReader>, IFileWithKnownFormatAsync<TReader, TWriter>
    {
        private Func<AbsolutePath, TWriter, Task> _write;

        public FileWithKnownFormatAsync(AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Func<AbsolutePath, TWriter, Task> write) : base(path, read)
        {
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(Path, writer);
        }
    }
    
    internal class FileWithKnownFormatAsyncSync<TReader, TWriter> : FileWithKnownFormatAsync<TReader>, IFileWithKnownFormatAsyncSync<TReader, TWriter>
    {
        private Action<AbsolutePath, TWriter> _write;

        public FileWithKnownFormatAsyncSync(AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Action<AbsolutePath, TWriter> write) : base(path, read)
        {
            _write = write;
        }

        public void Write(TWriter writer)
        {
            _write(Path, writer);
        }
    }
    
    internal class FileWithKnownFormatSync<TReader, TWriter> : FileWithKnownFormatSync<TReader>, IFileWithKnownFormatSync<TReader, TWriter>
    {
        private Action<AbsolutePath, TWriter> _write;

        public FileWithKnownFormatSync(AbsolutePath path, Func<AbsolutePath, TReader> read,
            Action<AbsolutePath, TWriter> write) : base(path, read)
        {
            _write = write;
        }

        public void Write(TWriter writer)
        {
            _write(Path, writer);
        }
    }
    
    internal class FileWithKnownFormatSyncAsync<TReader, TWriter> : FileWithKnownFormatSync<TReader>, IFileWithKnownFormatSyncAsync<TReader, TWriter>
    {
        private Func<AbsolutePath, TWriter, Task> _write;

        public FileWithKnownFormatSyncAsync(AbsolutePath path, Func<AbsolutePath, TReader> read,
            Func<AbsolutePath, TWriter, Task> write) : base(path, read)
        {
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(Path, writer);
        }
    }
    
    public interface IFileWithKnownFormatAsync<TReader>
    {
        AbsolutePath Path { get; }
        Task<TReader> Read();
    }
    
    public interface IFileWithKnownFormatSync<out TReader>
    {
        AbsolutePath Path { get; }
        TReader Read();
    }

    public interface IFileWithKnownFormatAsync<TReader, in TWriter> : IFileWithKnownFormatAsync<TReader>
    {
        Task Write(TWriter writer);
    }
    
    public interface IFileWithKnownFormatAsyncSync<TReader, in TWriter> : IFileWithKnownFormatAsync<TReader>
    {
        void Write(TWriter writer);
    }
    
    public interface IFileWithKnownFormatSync<out TReader, in TWriter> : IFileWithKnownFormatSync<TReader>
    {
        void Write(TWriter writer);
    }
    
    public interface IFileWithKnownFormatSyncAsync<out TReader, in TWriter> : IFileWithKnownFormatSync<TReader>
    {
        Task Write(TWriter writer);
    }
}