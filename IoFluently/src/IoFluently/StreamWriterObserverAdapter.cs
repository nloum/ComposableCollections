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

        /// <summary>
        /// Completes the observable and flushes and closes the stream.
        /// </summary>
        public void OnCompleted()
        {
            _streamWriter.Flush();
            _streamWriter.Close();
            _streamWriter.Dispose();
        }

        /// <summary>
        /// Indicates an error happened in the Reactive Extensions code that produces the text that is going to be written
        /// to the stream.
        /// </summary>
        /// <param name="error">The exception that was produced by the upstream Reactive Extensions code</param>
        /// <exception cref="Exception">The exception passed in</exception>
        public void OnError(Exception error)
        {
            throw error;
        }

        /// <summary>
        /// Causes the specified value to be written to the stream.
        /// </summary>
        /// <param name="value">The value to be written to the stream</param>
        public void OnNext(string value)
        {
            _streamWriter.Write(value);
        }
    }
}