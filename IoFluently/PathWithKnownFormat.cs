using System;
using System.Threading.Tasks;

namespace IoFluently
{
    internal class PathWithKnownFormatAsync<TReader> : IPathWithKnownFormatAsync<TReader>
    {
        public FileOrFolderOrMissingPath Path { get; }
        
        private Func<Task<TReader>> _read;

        public PathWithKnownFormatAsync(FileOrFolderOrMissingPath path, Func<Task<TReader>> read)
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
        public FileOrFolderOrMissingPath Path { get; }
        
        private Func<TReader> _read;

        public PathWithKnownFormatSync(FileOrFolderOrMissingPath path, Func<TReader> read)
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
        private readonly FileOrFolderOrMissingPath _path;
        private Func<TWriter, Task> _write;

        public PathWithKnownFormatAsync(FileOrFolderOrMissingPath path, Func<Task<TReader>> read,
            Func<TWriter, Task> write) : base(path, read)
        {
            _path = path;
            _write = write;
        }

        public Task Write(TWriter writer)
        {
            return _write(writer);
        }

        public FileOrFolderOrMissingPath Path { get; }

        public override string ToString()
        {
            return $"{Path}, read asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TReader))} and written to asynchronously using a {Utility.ConvertToCSharpTypeName(typeof(TWriter))}";
        }
    }
    
    internal class PathWithKnownFormatAsyncSync<TReader, TWriter> : PathWithKnownFormatAsync<TReader>, IPathWithKnownFormatAsyncSync<TReader, TWriter>
    {
        private readonly FileOrFolderOrMissingPath _path;
        private Action<TWriter> _write;

        public PathWithKnownFormatAsyncSync(FileOrFolderOrMissingPath path, Func<Task<TReader>> read,
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
        private readonly FileOrFolderOrMissingPath _path;
        private Action<TWriter> _write;

        public PathWithKnownFormatSync(FileOrFolderOrMissingPath path, Func<TReader> read,
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
        private readonly FileOrFolderOrMissingPath _path;
        private Func<TWriter, Task> _write;

        public PathWithKnownFormatSyncAsync(FileOrFolderOrMissingPath path, Func<TReader> read,
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
    
    /// <summary>
    /// Represents a path (typically a file, but it could be a folder too) that has a known format that can be deserialized
    /// to a .NET object.
    /// This specific interface reads asynchronously.
    /// There are variants of this interface that read and write synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TReader">A type that represents the deserialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to read the file more selectively, such as an Image object, where the image data
    /// is lazily loaded.</typeparam>
    public interface IPathWithKnownFormatAsync<TReader>
    {
        /// <summary>
        /// The path of the file.
        /// </summary>
        FileOrFolderOrMissingPath Path { get; }
        
        /// <summary>
        /// Reads from the file.
        /// </summary>
        /// <returns>The object that has been read</returns>
        Task<TReader> Read();
    }
    
    /// <summary>
    /// Represents a path (typically a file, but it could be a folder too) that has a known format that can be deserialized
    /// to a .NET object.
    /// This specific interface reads synchronously.
    /// There are variants of this interface that read and write synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TReader">A type that represents the deserialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to read the file more selectively, such as an Image object, where the image data
    /// is lazily loaded.</typeparam>
    public interface IPathWithKnownFormatSync<out TReader>
    {
        /// <summary>
        /// The path of the file.
        /// </summary>
        FileOrFolderOrMissingPath Path { get; }
        
        /// <summary>
        /// Reads from the file.
        /// </summary>
        /// <returns>The object that has been read</returns>
        TReader Read();
    }

    /// <summary>
    /// Represents a path (typically a file, but it could be a folder too) that has a known format that can be serialized
    /// from a .NET object.
    /// This specific interface reads asynchronously and writes asynchronously.
    /// There are variants of this interface that read and write synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TReader">A type that represents the deserialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to read the file more selectively, such as an Image object, where the image data
    /// is lazily loaded.</typeparam>
    /// <typeparam name="TWriter">A type that represents the serialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to write the file more selectively, such as an Image object, where the image data
    /// is lazily written.</typeparam>
    public interface IPathWithKnownFormatAsync<TReader, in TWriter> : IPathWithKnownFormatAsync<TReader>
    {
        /// <summary>
        /// Writes to the file.
        /// </summary>
        /// <param name="writer">The object to be written</param>
        /// <returns>A task that completes when the writing of the object is finished</returns>
        Task Write(TWriter writer);
    }
    
    /// <summary>
    /// Represents a path (typically a file, but it could be a folder too) that has a known format that can be serialized
    /// from a .NET object.
    /// This specific interface reads asynchronously and writes synchronously.
    /// There are variants of this interface that read and write synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TReader">A type that represents the deserialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to read the file more selectively, such as an Image object, where the image data
    /// is lazily loaded.</typeparam>
    /// <typeparam name="TWriter">A type that represents the serialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to write the file more selectively, such as an Image object, where the image data
    /// is lazily written.</typeparam>
    public interface IPathWithKnownFormatAsyncSync<TReader, in TWriter> : IPathWithKnownFormatAsync<TReader>
    {
        /// <summary>
        /// Writes to the file.
        /// </summary>
        /// <param name="writer">The object to be written</param>
        void Write(TWriter writer);
    }
    
    /// <summary>
    /// Represents a path (typically a file, but it could be a folder too) that has a known format that can be serialized
    /// from a .NET object.
    /// This specific interface reads synchronously and writes synchronously.
    /// There are variants of this interface that read and write synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TReader">A type that represents the deserialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to read the file more selectively, such as an Image object, where the image data
    /// is lazily loaded.</typeparam>
    /// <typeparam name="TWriter">A type that represents the serialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to write the file more selectively, such as an Image object, where the image data
    /// is lazily written.</typeparam>
    public interface IPathWithKnownFormatSync<out TReader, in TWriter> : IPathWithKnownFormatSync<TReader>
    {
        /// <summary>
        /// Writes to the file.
        /// </summary>
        /// <param name="writer">The object to be written</param>
        void Write(TWriter writer);
    }
    
    /// <summary>
    /// Represents a path (typically a file, but it could be a folder too) that has a known format that can be serialized
    /// from a .NET object.
    /// This specific interface reads synchronously and writes asynchronously.
    /// There are variants of this interface that read and write synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TReader">A type that represents the deserialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to read the file more selectively, such as an Image object, where the image data
    /// is lazily loaded.</typeparam>
    /// <typeparam name="TWriter">A type that represents the serialized file format. This could be the actual contents of
    /// the file, such as a Text object or even a string if this is a text file. Or this could be a more complex object whose
    /// purpose is to allow the developer to write the file more selectively, such as an Image object, where the image data
    /// is lazily written.</typeparam>
    public interface IPathWithKnownFormatSyncAsync<out TReader, in TWriter> : IPathWithKnownFormatSync<TReader>
    {
        /// <summary>
        /// Writes to the file.
        /// </summary>
        /// <param name="writer">The object to be written</param>
        /// <returns>A task that completes when the writing of the object is finished</returns>
        Task Write(TWriter writer);
    }
}