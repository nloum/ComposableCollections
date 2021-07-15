namespace CodeIO.SourceCode.Write
{
    public abstract class PropertyImplementation {
        public abstract void WriteImplementationInClass(SourceCodeWriter sourceCodeWriter, string name);
        public abstract void WriteDeclarationInInterface(SourceCodeWriter sourceCodeWriter, string name);
    }
}