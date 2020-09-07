using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace IoFluently
{
    public class StreamReaderObservableAdapter : IObservable<string>
    {
        private readonly StreamReader _reader;
        private readonly char[] _terminators = null;
        private readonly int? _size = null;

        public StreamReaderObservableAdapter(StreamReader reader, char[] terminators)
        {
            _reader = reader;
            _terminators = terminators;
        }

        public StreamReaderObservableAdapter(StreamReader reader, int? size)
        {
            _reader = reader;
            _size = size;
        }

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