using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace ComposableCollections.CodeGenerator
{
    public class DelegateMemberService
    {
        public void DelegateMember(ISymbol member, string delegateToField, string explicitInterfaceImplementation, bool explicitImplementation, StringBuilder sourceCodeBuilder, List<string> usings)
        {
            if (member.Kind == SymbolKind.Method)
            {
                var methodDeclaration = member as IMethodSymbol;

                // Skip properties because they're handling explicitly below
                if (methodDeclaration.Name.StartsWith("get_") ||
                    methodDeclaration.Name.StartsWith("set_"))
                {
                    return;
                }

                var methodParams = string.Join(", ", methodDeclaration.Parameters.Select(Utilities.ConvertToParameter));
                var methodParamNames = string.Join(", ", methodDeclaration.Parameters.Select(Utilities.ConvertToArgument));

                var methodTypeParameters =
                    Utilities.ConvertToTypeParameters(methodDeclaration.TypeParameters);
                
                var returnType = methodDeclaration.ReturnsVoid
                    ? "void"
                    : methodDeclaration.ReturnType.ToString();

                if (!methodDeclaration.ReturnsVoid)
                {
                    usings.AddRange(Utilities.GetUsings(methodDeclaration.ReturnType));
                }

                foreach (var param in methodDeclaration.Parameters)
                {
                    usings.AddRange(Utilities.GetUsings(param.Type));
                }
                foreach (var typeArg in methodDeclaration.TypeArguments)
                {
                    usings.AddRange(Utilities.GetUsings(typeArg));
                }

                if (explicitImplementation)
                {
                    // Explicit interface implementation
                    
                    sourceCodeBuilder.AppendLine($"{returnType} {explicitInterfaceImplementation}.{member.Name}{methodTypeParameters}({methodParams}) {{");
                }
                else
                {
                    sourceCodeBuilder.AppendLine($"public virtual {returnType} {member.Name}{methodTypeParameters}({methodParams}) {{");
                }
                
                if (!methodDeclaration.ReturnsVoid)
                {
                    sourceCodeBuilder.Append("return ");
                }

                sourceCodeBuilder.AppendLine($"{delegateToField}.{methodDeclaration.Name}({methodParamNames});");
                sourceCodeBuilder.AppendLine("}");
            }
            else if (member.Kind == SymbolKind.Property)
            {
                var propertyDeclaration = member as IPropertySymbol;

                foreach (var param in propertyDeclaration.Parameters)
                {
                    usings.AddRange(Utilities.GetUsings(param.Type));
                }
                if (propertyDeclaration.IsIndexer)
                {
                    var propertyParams = string.Join(", ", propertyDeclaration.Parameters.Select(Utilities.ConvertToParameter));
                    var propertyParamNames = string.Join(", ", propertyDeclaration.Parameters.Select(Utilities.ConvertToArgument));

                    usings.AddRange(Utilities.GetUsings(propertyDeclaration.Type));

                    if (explicitImplementation)
                    {
                        sourceCodeBuilder.Append(
                            $"{propertyDeclaration.Type} {explicitInterfaceImplementation}.this[{propertyParams}]");
                    }
                    else
                    {
                        sourceCodeBuilder.Append(
                            $"public virtual {propertyDeclaration.Type} this[{propertyParams}]");
                    }
                    
                    if (propertyDeclaration.GetMethod != null && propertyDeclaration.SetMethod == null)
                    {
                        sourceCodeBuilder.AppendLine($" => {delegateToField}[{propertyParamNames}];");
                    }
                    else
                    {
                        sourceCodeBuilder.AppendLine(" {");

                        if (propertyDeclaration.GetMethod != null)
                        {
                            sourceCodeBuilder.AppendLine($"get => {delegateToField}[{propertyParamNames}];");
                        }

                        if (propertyDeclaration.SetMethod != null)
                        {
                            sourceCodeBuilder.AppendLine($"set => {delegateToField}[{propertyParamNames}] = value;");
                        }
                        sourceCodeBuilder.AppendLine("}");
                    }
                }
                else
                {
                    usings.AddRange(Utilities.GetUsings(propertyDeclaration.Type));
                    if (explicitImplementation)
                    {
                        sourceCodeBuilder.Append(
                            $"{propertyDeclaration.Type} {explicitInterfaceImplementation}.{propertyDeclaration.Name}");
                    }
                    else
                    {
                        sourceCodeBuilder.Append(
                            $"public virtual {propertyDeclaration.Type} {propertyDeclaration.Name}");
                    }
                    if (propertyDeclaration.GetMethod != null && propertyDeclaration.SetMethod == null)
                    {
                        sourceCodeBuilder.AppendLine($" => {delegateToField}.{propertyDeclaration.Name};");
                    }
                    else
                    {
                        sourceCodeBuilder.AppendLine(
                            $"public virtual {propertyDeclaration.Type} {propertyDeclaration.Name} {{");

                        if (propertyDeclaration.GetMethod != null)
                        {
                            sourceCodeBuilder.AppendLine($"get => {delegateToField}.{propertyDeclaration.Name};");
                        }

                        if (propertyDeclaration.SetMethod != null)
                        {
                            sourceCodeBuilder.AppendLine($"get => {delegateToField}.{propertyDeclaration.Name} = value;");
                        }
                        sourceCodeBuilder.AppendLine("}");
                    }
                }
            }
        }
    }
}