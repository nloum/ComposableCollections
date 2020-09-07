using System;
using System.IO;

namespace IoFluently
{
    public class StreamWriterObserverAdapter : IObserver<string>
    {
        private readonly StreamWriter _streamWriter;

        public StreamWriterObserverAdapter(StreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }

        public void OnCompleted()
        {
            _streamWriter.Flush();
            _streamWriter.Close();
            _streamWriter.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(string value)
        {
            _streamWriter.Write(value);
        }
    }
}