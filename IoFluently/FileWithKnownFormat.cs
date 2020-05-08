using System;
using System.Threading.Tasks;

namespace IoFluently
{
    public class FileWithKnownFormatAsync<TReader>
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
    
    public class FileWithKnownFormatSync<TReader>
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

    public class FileWithKnownFormatAsync<TReader, TWriter> : FileWithKnownFormatAsync<TReader>
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
    
    public class FileWithKnownFormatAsyncSync<TReader, TWriter> : FileWithKnownFormatAsync<TReader>
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
    
    public class FileWithKnownFormatSync<TReader, TWriter> : FileWithKnownFormatSync<TReader>
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
    
    public class FileWithKnownFormatSyncAsync<TReader, TWriter> : FileWithKnownFormatSync<TReader>
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
}