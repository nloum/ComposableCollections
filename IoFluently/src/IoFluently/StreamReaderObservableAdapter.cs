using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace IoFluently
{
    /// <summary>
    /// Converts the StreamReader specified in the constructor into an IObservable<string> that reads from the StreamReader
    /// as fast as possible and produces new values every time data is read from the StreamReader.
    /// </summary>
    public class StreamReaderObservableAdapter : IObservable<string>
    {
        private readonly StreamReader _reader;
        private readonly char[] _terminators = null;
        private readonly int? _size = null;

        /// <summary>
        /// Wraps the specified StreamReader in an adapter that provides an IObservable<string> API that wraps the StreamReader.
        /// </summary>
        /// <param name="reader">The StreamReader that will be adapted to an IObservable<string></param>
        /// <param name="terminators">The cahracters that separate the contents of the file into separate events in the
        /// IObservable.</param>
        public StreamReaderObservableAdapter(StreamReader reader, char[] terminators)
        {
            _reader = reader;
            _terminators = terminators;
        }

        /// <summary>
        /// Wraps the specified StreamReader in an adapter that provides an IObservable<string> API that wraps the StreamReader.
        /// </summary>
        /// <param name="reader">The StreamReader that will be adapted to an IObservable<string></param>
        /// <param name="size">The number of bytes in each event in the IObservable. Note that the last event may be smaller
        /// because there might not be enough data to fill the quota.</param>
        public StreamReaderObservableAdapter(StreamReader reader, int? size)
        {
            _reader = reader;
            _size = size;
        }

        /// <summary>
        /// Begin reading from the StreamReader.
        /// </summary>
        /// <param name="observer">An object that will receive events</param>
        /// <returns>A way to end the reading of the StreamReader</returns>
        public IDisposable Subscribe(IObserver<string> observer)
        {
            return GetObservable().Subscribe(observer);
        }

        private IObservable<string> GetObservable()
        {
            return Observable.Create<string>(observer =>
            {
                var buffer = _size == null ? null : new char[_size.Value];
                while (!_reader.EndOfStream)
                {
                    if (_terminators != null)
                    {
                        observer.OnNext(ReadStringUsingTerminators());
                        continue;
                    }
                    if (_size != null)
                    {
                        var numReadBytes = _reader.ReadBlock(buffer, 0, _size.Value);
                        observer.OnNext(new string(buffer, 0, numReadBytes));
                        continue;
                    }

                    observer.OnNext(_reader.Read().ToString());
                }

                Action result = () => { };
                return result;
            });
        }

        private string ReadStringUsingTerminators()
        {
            var stringBuilder = new StringBuilder();
            while (true)
            {
                if (_reader.EndOfStream)
                {
                    return stringBuilder.ToString();
                }
                var c = (char)_reader.Read();
                stringBuilder.Append(c);
                foreach (var terminator in _terminators)
                {
                    if (terminator == c)
                    {
                        return stringBuilder.ToString();
                    }
                }
            }
        }
    }
}