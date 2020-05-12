using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace IoFluently
{
    public static class FileWithKnownFormatExtensions
    {
        public static IFileWithKnownFormatSync<T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, T> read)
        {
            return new FileWithKnownFormatSync<T>(path, read);
        }

        public static IFileWithKnownFormatAsync<T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, Task<T>> read)
        {
            return new FileWithKnownFormatAsync<T>(path, read);
        }
        
        public static IFileWithKnownFormatSync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, T> read,
            Action<AbsolutePath, T> write)
        {
            return new FileWithKnownFormatSync<T, T>(path, read, write);
        }

        public static IFileWithKnownFormatSyncAsync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, T> read,
            Func<AbsolutePath, T, Task> write)
        {
            return new FileWithKnownFormatSyncAsync<T, T>(path, read, write);
        }

        public static IFileWithKnownFormatAsyncSync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, Task<T>> read,
            Action<AbsolutePath, T> write)
        {
            return new FileWithKnownFormatAsyncSync<T, T>(path, read, write);
        }

        public static IFileWithKnownFormatAsync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, Task<T>> read,
            Func<AbsolutePath, T, Task> write)
        {
            return new FileWithKnownFormatAsync<T, T>(path, read, write);
        }
        
        public static IFileWithKnownFormatSync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, TReader> read,
            Action<AbsolutePath, TWriter> write)
        {
            return new FileWithKnownFormatSync<TReader, TWriter>(path, read, write);
        }

        public static IFileWithKnownFormatSyncAsync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, TReader> read,
            Func<AbsolutePath, TWriter, Task> write)
        {
            return new FileWithKnownFormatSyncAsync<TReader, TWriter>(path, read, write);
        }

        public static IFileWithKnownFormatAsyncSync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Action<AbsolutePath, TWriter> write)
        {
            return new FileWithKnownFormatAsyncSync<TReader, TWriter>(path, read, write);
        }

        public static IFileWithKnownFormatAsync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Func<AbsolutePath, TWriter, Task> write)
        {
            return new FileWithKnownFormatAsync<TReader, TWriter>(path, read, write);
        }

        public static IFileWithKnownFormatSync<Text, Text> AsTextFile(this AbsolutePath path)
        {
            return path.AsFile(absPath => new Text(absPath.ReadLines()),
                (absPath, text) => absPath.WriteAllLines(text.Lines));
        }

        public static IFileWithKnownFormatSync<XmlDocument> AsXmlFile(this AbsolutePath path)
        {
            return path.AsFile(absPath =>
            {
                var doc = new XmlDocument();
                doc.Load(absPath.ToString());
                return doc;
            }, (absPath, doc) =>
            {
                using (var stream = absPath.Open(FileMode.Create, FileAccess.Write))
                {
                    doc.Save(stream);
                }
            });
        }

        public static Text AsText(this IEnumerable<string> lines)
        {
            return new Text(lines);
        }
    }
}