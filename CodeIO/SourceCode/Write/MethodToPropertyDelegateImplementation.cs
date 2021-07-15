using CodeIO.LoadedTypes.Read;

namespace CodeIO.SourceCode.Write
{
    public class MethodToPropertyDelegateImplementation : PropertyImplementation
    {
        public string MethodName { get; init; }
        public IMethod DelegatesTo { get; init; }
        public string Body { get; init; }
        public IInterface ExplicitImplementationOf { get; init; }

        public override void WriteImplementationInClass(SourceCodeWriter sourceCodeWriter, string name)
        {
            sourceCodeWriter.WriteLine($"public {((IReflectionType)DelegatesTo.ReturnType).Type.ConvertToCSharpTypeName()} {name} => IoService.{DelegatesTo.Name}(this);");
        }

        public override void WriteDeclarationInInterface(SourceCodeWriter sourceCodeWriter, string name)
        {
            sourceCodeWriter.WriteLine($"public {((IReflectionType)DelegatesTo.ReturnType).Type.ConvertToCSharpTypeName()} {name} {{ get; }}");
        }
    }
}