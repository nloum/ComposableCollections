namespace CodeIO.SourceCode.Write
{
    public class PropertySourceCodeWriter {
        public string Name { get; init; }
        public PropertyImplementation Implementation { get; init; }

        public void WriteImplementationInClass(SourceCodeWriter sourceCodeWriter)
        {
            Implementation.WriteImplementationInClass(sourceCodeWriter, Name);
        }

        public void WriteDeclarationInInterface(SourceCodeWriter sourceCodeWriter)
        {
            Implementation.WriteDeclarationInInterface(sourceCodeWriter, Name);
        }
    }
}