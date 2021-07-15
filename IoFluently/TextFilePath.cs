using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleMonads;
using UnitsNet;

namespace IoFluently
{
    public static class TextFileExtensions
    {
        public static TextFilePath ExpectTextFile(this IFileOrFolderOrMissingPath path)
        {
            return new TextFilePath(path);
        }

        public static TextFileOrMissingPath ExpectTextFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new TextFileOrMissingPath(new TextFilePath(path));
            }
            
            return new TextFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
        
        public static TextFileOrMissingPath ExpectMissingTextFile(this IFileOrFolderOrMissingPath path)
        {
            return new((IMissingPath)new MissingPath(path));
        }
    }
    
    public class TextFilePath : FilePath
    {
        public TextFilePath(IFileOrFolderOrMissingPath path) : base(path)
        {
        }
        
        public StreamReader OpenReader(FileOptions fileOptions = FileOptions.SequentialScan, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, Information? bufferSize = default)
        {
            var stream = this.Open(FileMode.Open, FileAccess.Read, FileShare.Read,
                FileOptions.SequentialScan, bufferSize);
            if (encoding == null)
            {
                return new StreamReader(stream, detectEncodingFromByteOrderMarks);
            }

            return new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks);
        }

        public Encoding Encoding
        {
            get
            {
                using(var stream = this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.SequentialScan)) {
                    using (var reader = new StreamReader(stream, detectEncodingFromByteOrderMarks: true))
                    {
                        var line = reader.ReadLine();
                        return reader.CurrentEncoding;
                    }
                }
            }
        }

        public IMaybe<string> NewLine
        {
            get
            {
                using var stream = this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.SequentialScan);
                while (true)
                {
                    var data = stream.ReadByte();
                    if (data == -1)
                    {
                        return Maybe<string>.Nothing();
                    }

                    if (data == '\r')
                    {
                        return "\r\n".ToMaybe();
                    }

                    if (data == '\n')
                    {
                        return "\n".ToMaybe();
                    }
                }
            }
        }

        public IEnumerable<Line> ReadLines(Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, Information? bufferSize = default, ulong startingByteOffset = 0)
        {
            if (detectEncodingFromByteOrderMarks)
            {
                encoding ??= Encoding;
            }
            
            if (encoding == null && !detectEncodingFromByteOrderMarks)
            {
                throw new ArgumentException(nameof(detectEncodingFromByteOrderMarks));
            }

            using var stream = this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.SequentialScan, bufferSize);
            if (startingByteOffset > (ulong) stream.Length)
            {
                startingByteOffset = (ulong) stream.Length;
            }
            
            if (startingByteOffset != 0)
            {
                stream.Seek((long)startingByteOffset, SeekOrigin.Begin);
            }

            ulong lineNumber = 1;

            var lastLine = new List<Line>();
            
            foreach (var buffer in new StreamStringEnumerator(stream, startingByteOffset, (ulong) encoding.GetMaxCharCount((int)startingByteOffset), FileSystem.GetBufferSizeOrDefaultInBytes(bufferSize), encoding))
            {
                var innerEnumerator = new LineSplitEnumerator(buffer.Value,
                    buffer.ByteOffsetOfStart, buffer.CharOffsetOfStart,
                    lineNumber, encoding);

                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                lastLine.Add(innerEnumerator.Current);
                
                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                do
                {
                    if (lastLine.Count == 1)
                    {
                        yield return lastLine[0];
                        lastLine.Clear();
                    }
                    else
                    {
                        yield return lastLine.Combine();
                        lastLine.Clear();
                    }
                    lastLine.Add(innerEnumerator.Current);
                } while (innerEnumerator.MoveNext());
            }
            
            if (lastLine.Count == 1)
            {
                yield return lastLine[0];
                lastLine.Clear();
            }
            else if (lastLine.Count > 1)
            {
                yield return lastLine.Combine();
                lastLine.Clear();
            }
        }
        
        public IEnumerable<Line> ReadLinesBackwards(Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            FileShare fileShare = FileShare.Read, Information? bufferSize = default,
            ulong startingByteOffset = UInt64.MaxValue)
        {
            if (detectEncodingFromByteOrderMarks)
            {
                encoding ??= Encoding;
            }

            if (encoding == null && !detectEncodingFromByteOrderMarks)
            {
                throw new ArgumentException(nameof(detectEncodingFromByteOrderMarks));
            }
            
            using var stream = this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, FileOptions.RandomAccess, bufferSize);
            if (startingByteOffset > (ulong) stream.Length)
            {
                startingByteOffset = (ulong) stream.Length;
            }
            
            if (startingByteOffset != 0)
            {
                stream.Seek((long)startingByteOffset, SeekOrigin.Begin);
            }

            ulong lineNumber = 1;

            var lastLine = new List<Line>();
            
            foreach (var buffer in new ReverseStreamStringEnumerator(stream, startingByteOffset, (ulong) encoding.GetMaxCharCount((int)startingByteOffset), FileSystem.GetBufferSizeOrDefaultInBytes(bufferSize), encoding))
            {
                var innerEnumerator = new ReverseLineSplitEnumerator(buffer.Value,
                    buffer.ByteOffsetOfStart, buffer.CharOffsetOfStart,
                    lineNumber, encoding);

                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                lastLine.Add(innerEnumerator.Current);
                
                if (!innerEnumerator.MoveNext())
                {
                    continue;
                }
                
                do
                {
                    if (lastLine.Count == 1)
                    {
                        yield return lastLine[0];
                        lastLine.Clear();
                    }
                    else
                    {
                        yield return lastLine.Combine();
                        lastLine.Clear();
                    }
                    lastLine.Add(innerEnumerator.Current);
                } while (innerEnumerator.MoveNext());
            }
            
            if (lastLine.Count == 1)
            {
                yield return lastLine[0];
                lastLine.Clear();
            }
            else if (lastLine.Count > 1)
            {
                yield return lastLine.Combine();
                lastLine.Clear();
            }
        }
        
        public string ReadAllText(FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            Information? bufferSize = default)
        {
            using var reader = this.OpenReader(FileOptions.SequentialScan, encoding, detectEncodingFromByteOrderMarks, bufferSize);
            return reader.ReadToEnd();
        }
        
        public StreamWriter OpenWriter(FileOptions fileOptions = FileOptions.WriteThrough, Encoding encoding = null, Information? bufferSize = default)
        {
            return this.ExpectTextFileOrMissingPath()
                .OpenWriter(fileOptions, encoding, bufferSize);
        }

        public virtual TextFilePath WriteAllLines(IEnumerable<string> lines, string newline = null, Encoding encoding = null, Information? bufferSize = default)
        {
            return this.ExpectTextFileOrMissingPath()
                .WriteAllLines(lines, newline, encoding, bufferSize);
        }

        public TextFilePath WriteAllText(string text, Encoding encoding = null)
        {
            return this.ExpectTextFileOrMissingPath()
                .WriteAllText(text, encoding);
        }
    }

    public class TextFileOrMissingPath : FileOrMissingPathBase
    {
        public TextFileOrMissingPath(TextFilePath item1) : base((IFilePath)item1)
        {
        }

        public TextFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public TextFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFilePath, IMissingPath> other)
            : base(other.Collapse(
                textFile => new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IMissingPath>((IFilePath)textFile),
                missingPath => new SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IMissingPath>((IMissingPath)missingPath)))
        {
        }

        public TextFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public StreamWriter OpenWriter(FileOptions fileOptions = FileOptions.WriteThrough, Encoding encoding = null, Information? bufferSize = default,
             bool createRecursively = true)
        {
            var stream = Value .FileSystem.Open(Value.ExpectTextFileOrMissingPath(), FileMode.Create, FileAccess.Write, FileShare.None,
                fileOptions, bufferSize, createRecursively);
            if (encoding == null)
            {
                return new StreamWriter(stream);
            }
            else
            {
                return new StreamWriter(stream, encoding);
            }
        }

        public virtual TextFilePath WriteAllLines(IEnumerable<string> lines, string newline = null, Encoding encoding = null, Information? bufferSize = default,  bool createRecursively = true)
        {
            newline ??= Environment.NewLine;
            
            var stream = Value .FileSystem.Open(Value.ExpectTextFile(), FileMode.Create, FileAccess.ReadWrite, FileShare.Write, FileOptions.WriteThrough, bufferSize, createRecursively);
            foreach (var line in lines)
            {
                var bytes = Encoding.Default.GetBytes(line + newline);
                stream.Write(bytes, 0, bytes.Length);
            }

            return new TextFilePath(Value );
        }

        public TextFilePath WriteAllText(string text, Encoding encoding = null,  bool createRecursively = true)
        {
            encoding ??= Encoding.Default;
            using var writer = OpenWriter(FileOptions.None, encoding, Information.FromBytes(Math.Max(encoding.GetByteCount(text), 1)), createRecursively);
            writer.Write(text);

            return new TextFilePath(Value );
        }
    }
}