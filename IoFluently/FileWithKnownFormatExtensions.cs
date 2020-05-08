using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoFluently
{
    public static class FileWithKnownFormatExtensions
    {
        public static FileWithKnownFormatSync<T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, T> read)
        {
            return new FileWithKnownFormatSync<T>(path, read);
        }

        public static FileWithKnownFormatAsync<T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, Task<T>> read)
        {
            return new FileWithKnownFormatAsync<T>(path, read);
        }
        
        public static FileWithKnownFormatSync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, T> read,
            Action<AbsolutePath, T> write)
        {
            return new FileWithKnownFormatSync<T, T>(path, read, write);
        }

        public static FileWithKnownFormatSyncAsync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, T> read,
            Func<AbsolutePath, T, Task> write)
        {
            return new FileWithKnownFormatSyncAsync<T, T>(path, read, write);
        }

        public static FileWithKnownFormatAsyncSync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, Task<T>> read,
            Action<AbsolutePath, T> write)
        {
            return new FileWithKnownFormatAsyncSync<T, T>(path, read, write);
        }

        public static FileWithKnownFormatAsync<T, T> AsFile<T>(this AbsolutePath path, Func<AbsolutePath, Task<T>> read,
            Func<AbsolutePath, T, Task> write)
        {
            return new FileWithKnownFormatAsync<T, T>(path, read, write);
        }
        
        public static FileWithKnownFormatSync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, TReader> read,
            Action<AbsolutePath, TWriter> write)
        {
            return new FileWithKnownFormatSync<TReader, TWriter>(path, read, write);
        }

        public static FileWithKnownFormatSyncAsync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, TReader> read,
            Func<AbsolutePath, TWriter, Task> write)
        {
            return new FileWithKnownFormatSyncAsync<TReader, TWriter>(path, read, write);
        }

        public static FileWithKnownFormatAsyncSync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Action<AbsolutePath, TWriter> write)
        {
            return new FileWithKnownFormatAsyncSync<TReader, TWriter>(path, read, write);
        }

        public static FileWithKnownFormatAsync<TReader, TWriter> AsFile<TReader, TWriter>(this AbsolutePath path, Func<AbsolutePath, Task<TReader>> read,
            Func<AbsolutePath, TWriter, Task> write)
        {
            return new FileWithKnownFormatAsync<TReader, TWriter>(path, read, write);
        }

        public static FileWithKnownFormatSync<Text, Text> AsTextFile(this AbsolutePath path)
        {
            return path.AsFile(absPath => new Text(absPath.ReadLines()),
                (absPath, text) => absPath.WriteAllLines(text.Lines));
        }

        public static Text AsText(this IEnumerable<string> lines)
        {
            return new Text(lines);
        }
    }
}