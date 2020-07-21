using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using UtilityDisposables;

namespace IoFluently
{
    public static class PathProcessingExtensions
    {
        public static IDisposable TemporaryChanges(this AbsolutePath path)
        {
            var backupPath = path.WithExtension(x => x + ".backup");
            var translation = path.Translate(backupPath);
            translation.Copy(true);

            return new AnonymousDisposable(() => translation.Invert().Move(true));
        }
        
        public static IPathWithKnownFormatSync<T> AsPathFormat<T>(this AbsolutePath path, Func<T> read)
        {
            return new PathWithKnownFormatSync<T>(path, read);
        }

        public static IPathWithKnownFormatAsync<T> AsPathFormat<T>(this AbsolutePath path, Func<Task<T>> read)
        {
            return new PathWithKnownFormatAsync<T>(path, read);
        }
        
        public static IPathWithKnownFormatSync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<T> read,
            Action<T> write)
        {
            return new PathWithKnownFormatSync<T, T>(path, read, write);
        }

        public static IPathWithKnownFormatSyncAsync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<T> read,
            Func<T, Task> write)
        {
            return new PathWithKnownFormatSyncAsync<T, T>(path, read, write);
        }

        public static IPathWithKnownFormatAsyncSync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<Task<T>> read,
            Action<T> write)
        {
            return new PathWithKnownFormatAsyncSync<T, T>(path, read, write);
        }

        public static IPathWithKnownFormatAsync<T, T> AsPathFormat<T>(this AbsolutePath path, Func<Task<T>> read,
            Func<T, Task> write)
        {
            return new PathWithKnownFormatAsync<T, T>(path, read, write);
        }
        
        public static IPathWithKnownFormatSync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<TReader> read,
            Action<TWriter> write)
        {
            return new PathWithKnownFormatSync<TReader, TWriter>(path, read, write);
        }

        public static IPathWithKnownFormatSyncAsync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<TReader> read,
            Func<TWriter, Task> write)
        {
            return new PathWithKnownFormatSyncAsync<TReader, TWriter>(path, read, write);
        }

        public static IPathWithKnownFormatAsyncSync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<Task<TReader>> read,
            Action<TWriter> write)
        {
            return new PathWithKnownFormatAsyncSync<TReader, TWriter>(path, read, write);
        }

        public static IPathWithKnownFormatAsync<TReader, TWriter> AsPathFormat<TReader, TWriter>(this AbsolutePath path, Func<Task<TReader>> read,
            Func<TWriter, Task> write)
        {
            return new PathWithKnownFormatAsync<TReader, TWriter>(path, read, write);
        }

        public static IPathWithKnownFormatSync<Text, Text> AsTextFile(this AbsolutePath path)
        {
            return path.AsPathFormat(() => new Text(path.ReadLines()),
                (text) => path.WriteAllLines(text.Lines));
        }

        public static IPathWithKnownFormatSync<string> AsSmallTextFile(this AbsolutePath path)
        {
            return path.AsPathFormat(() => path.ReadAllText(), (text) => path.WriteAllText(text));
        }
        
        public static IPathWithKnownFormatSync<XmlDocument, XmlDocument> AsXmlFile(this AbsolutePath path)
        {
            return path.AsPathFormat(() =>
            {
                var doc = new XmlDocument();
                doc.Load(path.ToString());
                return doc;
            }, (doc) =>
            {
                using (var stream = path.Open(FileMode.Create, FileAccess.Write))
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