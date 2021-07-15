using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UtilityDisposables;

namespace CodeIO.SourceCode.Write
{
    public class SourceCodeWriter
    {
        private readonly TextWriter _textWriter;
        private readonly char _indentationCharacter;
        private readonly int _indentationCharacterCount;
        private int _currentIndentationAmount = 0;

        public SourceCodeWriter(TextWriter textWriter, char indentationCharacter = ' ', int indentationCharacterCount = 4)
        {
            _textWriter = textWriter;
            _indentationCharacter = indentationCharacter;
            _indentationCharacterCount = indentationCharacterCount;
        }

        public IDisposable Braces()
        {
            WriteLine("{");
            _currentIndentationAmount++;
            return new AnonymousDisposable(() =>
            {
                _currentIndentationAmount--;
                WriteLine("}");
            });
        }

        public IDisposable Indent()
        {
            _currentIndentationAmount++;
            return new AnonymousDisposable(() => _currentIndentationAmount--);
        }
        
        protected string GetIndentation()
        {
            return string.Join("", Enumerable.Repeat(_indentationCharacter, _indentationCharacterCount * _currentIndentationAmount));
        }

        public void WriteLine()
        {
            WriteLine("\n");
        }

        public void WriteLine(string value)
        {
            _textWriter.Write(GetIndentation());
            _textWriter.WriteLine(value);
        }

        public Encoding Encoding => _textWriter.Encoding;

        public IFormatProvider FormatProvider => _textWriter.FormatProvider;

        public string NewLine
        {
            get => _textWriter.NewLine;
            set => _textWriter.NewLine = value;
        }
    }
}