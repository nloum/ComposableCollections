using System;
using System.Collections.Generic;
using System.IO;
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
        /// Backs up the specified path and then when the IDisposable is disposed of, the backup file is restored, overwriting
        /// any changes that were made to the path. This is useful for making temporary changes to the path.
        /// </summary>
        /// <param name="path">The path that temporary changes will be made to.</param>
        /// <returns>An object that, when disposed of, undoes any changes made to the specified path.</returns>
        public static IDisposable TemporaryChanges(this AbsolutePath path)
        {
            var backupPath = path.IoService.TryWithExtension(path, x => x + ".backup").Value;
            var translation = path.Translate(backupPath);
            translation.IoService.Copy(translation, overwrite: true);

            return new AnonymousDisposable(() => translation.IoService.Move(translation.Invert(), overwrite: true));
        }
        
        /// <summary>
        /// Tells IoFluently how to read data from the specified path.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <param name="read">A function defining how to read from this path.</param>
        /// <typeparam name="T">The type of object that represents data read from the file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<T> AsPathFormat<T>(this AbsolutePath path, Func<T> read)
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
        public static IPathWithKnownFormatAsync<T> AsPathFormat<T>(this AbsolutePath path, Func<Task<T>> read)
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
        public static IPathWithKnownFormatSync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<T> read,
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
        public static IPathWithKnownFormatSyncAsync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<T> read,
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
        public static IPathWithKnownFormatAsyncSync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<Task<T>> read,
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
        public static IPathWithKnownFormatAsync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<Task<T>> read,
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
        public static IPathWithKnownFormatSync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<TReader> read,
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
        public static IPathWithKnownFormatSyncAsync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<TReader> read,
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
        public static IPathWithKnownFormatAsyncSync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<Task<TReader>> read,
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
        public static IPathWithKnownFormatAsync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<Task<TReader>> read,
            Func<TWriter, Task> write)
        {
            return new PathWithKnownFormatAsync<TReader, TWriter>(path, read, write);
        }

        /// <summary>
        /// Tells IoFluently that the specified path is a text file.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<Text, Text> AsTextFile(this AbsolutePath path)
        {
            return path.AsPathFormat(() => new Text(path.ReadLines()),
                (text) => path.IoService.WriteAllLines(path, text.Lines));
        }

        /// <summary>
        /// Tells IoFluently that the specified path is a text file that is small enough that it doesn't have to be read
        /// lazily.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<string, string> AsSmallTextFile(this AbsolutePath path)
        {
            return path.AsPathFormat(() => path.ReadAllText(), (text) => path.IoService.WriteText(path, text));
        }
        
        /// <summary>
        /// Tells IoFluently that the specified path is an XML file.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<XmlDocument, XmlDocument> AsXmlFile(this AbsolutePath path)
        {
            return path.AsPathFormat(() =>
            {
                var doc = new XmlDocument();
                doc.Load(path.ToString());
                return doc;
            }, (doc) =>
            {
                using (var stream = path.IoService.TryOpen(path, FileMode.Create, FileAccess.Write, FileShare.None).Value)
                {
                    doc.Save(stream);
                }
            });
        }
        
        /// <summary>
        /// Tells IoFluently that the specified path is an XML file, and should be read from and written to using the
        /// XmlSerializer API.
        /// </summary>
        /// <param name="path">The path to be read from</param>
        /// <typeparam name="TModel">The type that can be used to serialize or deserialize XML from this file</typeparam>
        /// <returns>An object that allows the path to be read from.</returns>
        public static IPathWithKnownFormatSync<TModel, TModel> AsSerializedXmlFile<TModel>(
            this AbsolutePath path, Action<TModel> preSerialize = null, Action<TModel> postDeserialize = null)
        {
            return path.AsPathFormat<TModel, TModel>(() =>
            {
                var serializer = new XmlSerializer(typeof(TModel));
                using (var reader = path.TryOpenReader().Value)
                {
                    var result = (TModel) serializer.Deserialize(reader);
                    postDeserialize?.Invoke(result);
                    return result;
                }
            }, (model) =>
            {
                var serializer = new XmlSerializer(typeof(TModel));
                using (var writer = path.IoService.TryOpenWriter(path).Value)
                {
                    preSerialize?.Invoke(model);
                    serializer.Serialize(writer, model);
                }
            });
        }

        /// <summary>
        /// Creates an IIoService object for reading from and writing to the specified zip file
        /// </summary>
        /// <param name="absolutePath">The zip file path</param>
        /// <param name="enableOpenFilesTracking">Whether to enable open-files tracking in the zip file</param>
        /// <returns>An object that can be used for reading from and writing to the zip file</returns>
        public static ZipIoService AsZipFile(this AbsolutePath absolutePath, bool enableOpenFilesTracking = false)
        {
            return new ZipIoService(absolutePath, absolutePath.IoService.GetNewlineCharacter(), enableOpenFilesTracking);
        }

        /// <summary>
        /// Creates an IIoService object for reading from and writing to the specified zip file
        /// </summary>
        /// <param name="absolutePath">The zip file path</param>
        /// <param name="newline">The newline character to be used when writing text to a file inside the zip file</param>
        /// <param name="enableOpenFilesTracking">Whether to enable open-files tracking in the zip file</param>
        /// <returns>An object that can be used for reading from and writing to the zip file</returns>
        public static ZipIoService AsZipFile(this AbsolutePath absolutePath, string newline, bool enableOpenFilesTracking = false)
        {
            return new ZipIoService(absolutePath, newline, enableOpenFilesTracking);
        }

        /// <summary>
        /// Converts a lazy IEnumerable of lines of text into a Text object, just like the Text object returned by AsTextFile().Read().
        /// </summary>
        /// <param name="lines">The lines of text</param>
        /// <returns>An object that represents text.</returns>
        public static Text AsText(this IEnumerable<string> lines)
        {
            return new Text(lines);
        }
    }
}
