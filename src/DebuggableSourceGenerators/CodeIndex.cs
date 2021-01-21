namespace DebuggableSourceGenerators
{
    public class CodeIndex
    {
        private readonly CodeIndexBuilder _codeIndexBuilder;

        public CodeIndex(CodeIndexBuilder codeIndexBuilder)
        {
            _codeIndexBuilder = codeIndexBuilder;
        }
        
        public IType GetType(string typeName, int arity = 0)
        {
            return _codeIndexBuilder.GetType(typeName, arity).Value;
        }

        public IType GetType(string namespaceName, string typeName, int arity = 0)
        {
            return _codeIndexBuilder.GetType(namespaceName, typeName, arity).Value;
        }
    }
}