using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using MoreCollections;
using SimpleMonads;

namespace MoreIO
{
    public static class PathTextExt
    {
        public static IMaybe<StreamWriter> CreateText(this PathSpec pathSpec)
        {
            try
            {
                return new Maybe<StreamWriter>(pathSpec.AsFileInfo().CreateText());
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<StreamWriter>.Nothing;
            }
            catch (IOException)
            {
                return Maybe<StreamWriter>.Nothing;
            }
            catch (SecurityException)
            {
                return Maybe<StreamWriter>.Nothing;
            }
        }

        public static IEnumerable<string> ReadLines(this PathSpec pathSpec, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeFileStream = pathSpec.Open(fileMode, fileAccess, fileShare);
            if (maybeFileStream.HasValue)
            {
                using (maybeFileStream.Value)
                {
                    return maybeFileStream.Value.ReadLines(encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
                }
            }
            return EnumerableUtility.EmptyArray<string>();
        }

        public static IMaybe<string> ReadText(this PathSpec pathSpec, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            return pathSpec.Open(fileMode, fileAccess, fileShare).Select(
                fs =>
                {
                    using (fs)
                    {
                        return fs.ReadText(encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
                    }
                });
        }

        public static void WriteText(this PathSpec pathSpec, IEnumerable<string> lines, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeFileStream = pathSpec.Open(fileMode, fileAccess, fileShare);
            if (maybeFileStream.HasValue)
            {
                using (maybeFileStream.Value)
                {
                    maybeFileStream.Value.WriteLines(lines, encoding, bufferSize, leaveOpen);
                }
            }
        }

        public static void WriteText(this PathSpec pathSpec, string text, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            var maybeFileStream = pathSpec.Open(fileMode, fileAccess, fileShare);
            if (maybeFileStream.HasValue)
            {
                using (maybeFileStream.Value)
                {
                    maybeFileStream.Value.WriteText(text, encoding, bufferSize, leaveOpen);
                }
            }
        }

        public static IEnumerable<string> ReadLines(this Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static IEnumerable<string> ReadLinesBackwards(this Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            string content = string.Empty;
            // Seek file pointer to end
            stream.Seek(0, SeekOrigin.End);

            byte[] buffer = new byte[bufferSize];

            //loop now and read backwards
            while (stream.Position > 0)
            {
                buffer.Initialize();

                int bytesRead;

                if (stream.Position - bufferSize >= 0)
                {
                    stream.Seek(-bufferSize, SeekOrigin.Current);
                    bytesRead = stream.Read(buffer, 0, bufferSize);
                    stream.Seek(-bufferSize, SeekOrigin.Current);
                }
                else
                {
                    var finalBufferSize = stream.Position;
                    stream.Seek(0, SeekOrigin.Begin);
                    bytesRead = stream.Read(buffer, 0, (int)finalBufferSize);
                    stream.Seek(0, SeekOrigin.Begin);
                }

                var strBuffer = encoding.GetString(buffer, 0, bytesRead);

                // lines is equal to what we just read, with the leftover content from last iteration appended to it.
                var lines = (strBuffer + content).Split('\n');

                // Loop through lines backwards, ignoring the first element, and yield each value
                for (var i = lines.Length - 1; i > 0; i--)
                {
                    yield return lines[i].Trim('\r');
                }

                // Leftover content is part of a line defined on the line(s) that we'll read next iteration of this while loop
                // so we must save leftover content for later
                content = lines[0];
            }
        }

        public static string ReadText(this Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                return sr.ReadText();
            }
        }

        private static string ReadText(this StreamReader streamReader)
        {
            return streamReader.ReadToEnd();
        }

        public static void WriteLines(this Stream stream, IEnumerable<string> lines, Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sw = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                sw.WriteLines(lines);
            }
        }

        private static void WriteLines(this StreamWriter streamWriter, IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                streamWriter.WriteLine(line);
            }
        }

        public static void WriteText(this Stream stream, string text, Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (var sw = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                sw.WriteText(text);
            }
        }

        private static void WriteText(this StreamWriter streamWriter, string text)
        {
            streamWriter.Write(text);
        }
    }
}
