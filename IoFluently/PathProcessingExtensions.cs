using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UtilityDisposables;

namespace IoFluently
{
    /// <summary>
    /// Extension methods for processing paths
    /// </summary>
    public static class PathProcessingExtensions
    {
        /// <summary>
        /// Tells IoFluently how to read data from the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<T> AsPathFormat<T>(this FileOrFolderOrMissingPath path, Func<T> read)
        {
            return new PathWithKnownFormatSync<T>(path, read);
        }

        /// <summary>
        /// Tells IoFluently how to read data asynchronously from the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatAsync<T> AsPathFormat<T>(this FileOrFolderOrMissingPath path, Func<Task<T>> read)
        {
            return new PathWithKnownFormatAsync<T>(path, read);
        }

        /// <summary>
        /// Tells IoFluently how to read data from and write data to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<T, T> AsPathFormat<T>(this FileOrFolderOrMissingPath path, Func<T> read,
            Action<T> write)
        {
            return new PathWithKnownFormatSync<T, T>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently how to read data from and write data asynchronously to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSyncAsync<T, T> AsPathFormat<T>(this FileOrFolderOrMissingPath path, Func<T> read,
            Func<T, Task> write)
        {
            return new PathWithKnownFormatSyncAsync<T, T>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently how to read data asynchronously from and write data to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatAsyncSync<T, T> AsPathFormat<T>(this FileOrFolderOrMissingPath path, Func<Task<T>> read,
            Action<T> write)
        {
            return new PathWithKnownFormatAsyncSync<T, T>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently how to read data asynchronously from and write data asynchronously to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatAsync<T, T> AsPathFormat<T>(this FileOrFolderOrMissingPath path, Func<Task<T>> read,
            Func<T, Task> write)
        {
            return new PathWithKnownFormatAsync<T, T>(path, read, write);
        }
        
        /// <summary>
        /// Tells IoFluently how to read data from and write data to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this FileOrFolderOrMissingPath path, Func<TReader> read,
            Action<TWriter> write)
        {
            return new PathWithKnownFormatSync<TReader, TWriter>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently how to read data from and write data asynchronously to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSyncAsync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this FileOrFolderOrMissingPath path, Func<TReader> read,
            Func<TWriter, Task> write)
        {
            return new PathWithKnownFormatSyncAsync<TReader, TWriter>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently how to read data asynchronously from and write data to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatAsyncSync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this FileOrFolderOrMissingPath path, Func<Task<TReader>> read,
            Action<TWriter> write)
        {
            return new PathWithKnownFormatAsyncSync<TReader, TWriter>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently how to read data asynchronously from and write data asynchronously to the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <param name="write">A function defining how to write to this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatAsync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this FileOrFolderOrMissingPath path, Func<Task<TReader>> read,
            Func<TWriter, Task> write)
        {
            return new PathWithKnownFormatAsync<TReader, TWriter>(path, read, write);
        }

        /// <summary>
        /// Creates an IIoService object for reading from and writing to the specified zip file
        /// </summary>
        /// <param name="absolutePath">The zip file path</param>
        /// <param name="enableOpenFilesTracking">Whether to enable open-files tracking in the zip file</param>
        /// <returns>An object that can be used for reading from and writing to the zip file</returns>
        public static ZipIoService ExpectZipFile(this IHasAbsolutePath absolutePath, bool enableOpenFilesTracking = false)
        {
            return new ZipIoService(absolutePath.Path.ExpectFileOrMissingPath(),  enableOpenFilesTracking);
        }
    }
}
