using System;
using System.IO;

namespace IoFluently
{
    /// <summary>
    /// Converts the StreamWriter specified in the constructor into an IObserver<string> that produces a new value every
    /// time the StreamWriter is written to.
    /// </summary>
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