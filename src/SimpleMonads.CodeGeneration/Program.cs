using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Humanizer;
using IoFluently;
using ReactiveProcesses;

namespace SimpleMonads.CodeGeneration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ioService = new IoService(new ReactiveProcessFactory());

            var simpleMonadsSourcePath = ioService.CurrentDirectory.Ancestors().First(x => (x / ".git").IsFolder()) / "src" / "SimpleMonads";
            
            var maxArity = 16;

            for (var i = 2; i <= maxArity; i++)
            {
                var interfacePath = simpleMonadsSourcePath / $"IEither{i}.cs";
                var classPath = simpleMonadsSourcePath / $"Either{i}.cs";
                var extensionsPath = simpleMonadsSourcePath / $"Either{i}Extensions.cs";
                interfacePath.DeleteFile();
                classPath.DeleteFile();
                extensionsPath.DeleteFile();

                using (var writer = interfacePath.OpenWriter())
                {
                    writer.WriteLine("using System;\n\nnamespace SimpleMonads {");
                    GenerateEither(writer, i, maxArity, CodePart.Interface);
                    writer.WriteLine("}");
                }

                using (var writer = classPath.OpenWriter())
                {
                    writer.WriteLine("using System;\n\nnamespace SimpleMonads {");
                    GenerateEither(writer, i, maxArity, CodePart.Class);
                    writer.WriteLine("}");
                }

                using (var writer = extensionsPath.OpenWriter())
                {
                    writer.WriteLine("using System;\n\nnamespace SimpleMonads {");
                    GenerateEither(writer, i, maxArity, CodePart.ExtensionMethods);
                    writer.WriteLine("}");
                }
            }
        }

        private static void GenerateEither(TextWriter writer, int arity, int maxArity, CodePart part)
        {
            var genericArgDefinitions = new List<string>();
            var genericArgNames = new List<string>();
            var interfaceProperties = new List<string>();
            var classProperties = new List<string>();
            var baseConstructors = new List<string>();
            var subClassConstructors = new List<string>();
            var collapseArgs = new List<string>();
            
            baseConstructors.Add("protected EitherBase() { }");
            subClassConstructors.Add("protected Either() { }");

            for (var i = 1; i <= arity; i++)
            {
                var argName = "T" + i;
                genericArgNames.Add(argName);
                genericArgDefinitions.Add($"out {argName}");
                interfaceProperties.Add($"{argName}? Item{i} {{ get; }}");
                classProperties.Add($"public {argName}? Item{i} {{ get; init; }} = default;");
                baseConstructors.Add($"public EitherBase({argName} item{i}) {{\n" +
                                           $"Item{i} = item{i};\n" +
                                           "}");
                subClassConstructors.Add($"public Either({argName} item{i}) : base(item{i}) {{ }}\n");
                collapseArgs.Add($"Func<{argName}, TOutput> selector{i}");
            }

            var genericArgNamesA = genericArgNames.Select(x => x + "A").ToList();
            var genericArgNamesB = genericArgNames.Select(x => x + "B").ToList();
            var genericArgNamesString = string.Join(", ", genericArgNames);
            var baseConstraints = string.Join(" ", genericArgNames.Select(arg => $"where {arg} : TBase"));
            var collapseArgsString = string.Join(", ", collapseArgs);
            
            baseConstructors.Add($"public EitherBase(EitherBase<{genericArgNamesString}> other) {{\n" +
                                 string.Join("\n", Enumerable.Range(1, arity).Select(i => $"Item{i} = other.Item{i};")) +
                                 "\n}");

            subClassConstructors.Add($"public Either(Either<{genericArgNamesString}> other) {{\n" +
                                     string.Join("\n", Enumerable.Range(1, arity).Select(i => $"Item{i} = other.Item{i};")) +
                                     "\n}");

            if (part == CodePart.Interface)
            {
                writer.WriteLine($"public interface IEitherBase<{string.Join(", ", genericArgDefinitions)}> : IEither {{");
                writer.WriteLine(string.Join("\n", interfaceProperties));
                for (var i = arity + 1; i <= maxArity; i++) GenerateOrDeclaration(writer, arity, i);
                writer.WriteLine($"TOutput Collapse<TOutput>({collapseArgsString});");
                writer.WriteLine($"ConvertibleTo<TBase>.IEither<{genericArgNamesString}> ConvertTo<TBase>();");
                writer.WriteLine("}");

                writer.WriteLine("public partial class ConvertibleTo<TBase> {");
                writer.WriteLine($"public interface IEither<{string.Join(", ", genericArgDefinitions)}> : SubTypesOf<TBase>.IEither{arity}, IEitherBase<{genericArgNamesString}> \n{{");
                writer.WriteLine("}");
                writer.WriteLine("}");

                writer.WriteLine("public partial class SubTypesOf<TBase> {");
                writer.WriteLine($"public interface IEither{arity} : IEither {{\n" +
                                 "TBase Value { get; }" +
                                 $"\n}}");
                writer.WriteLine($"public interface IEither<{string.Join(", ", genericArgDefinitions)}> : IEither{arity}, IEitherBase<{genericArgNamesString}> {baseConstraints} \n{{");
                writer.WriteLine("}");
                writer.WriteLine("}");
                writer.WriteLine($"public interface IEither<{string.Join(", ", genericArgDefinitions)}> : SubTypesOf<object>.IEither<{genericArgNamesString}> \n{{");
                writer.WriteLine("}");
            }

            if (part == CodePart.Class)
            {
                //GenerateCastClass(writer, arity, genericArgNames);
                writer.WriteLine(
                    $"public class EitherBase<{string.Join(", ", genericArgNames)}> : IEitherBase<{genericArgNamesString}>, IEquatable<IEither<{genericArgNamesString}>>\n{{");
                writer.WriteLine(string.Join("\n", baseConstructors));
                writer.WriteLine(string.Join("\n", classProperties));
                writer.WriteLine($"public TOutput Collapse<TOutput>({collapseArgsString}) {{");
                for (var i = 1; i <= arity; i++)
                {
                    writer.WriteLine($"if (Item{i} != null) return selector{i}(Item{i});");
                }
                writer.WriteLine("throw new InvalidOperationException();");
                writer.WriteLine("}");
                for (var i = arity + 1; i <= maxArity; i++) GenerateOrImplementation(writer, arity, i);
                GenerateEqualityMembers(writer, arity);
                GenerateToString(writer, arity);
                GenerateBaseImplicitOperators(writer, "EitherBase", arity, genericArgNames);
                writer.WriteLine($"public ConvertibleTo<TBase>.IEither<{genericArgNamesString}> ConvertTo<TBase>() {{");
                for (var i = 1; i <= arity; i++)
                {
                    writer.WriteLine($"if (Item{i} != null) {{");
                    writer.WriteLine($"return new ConvertibleTo<TBase>.Either<{genericArgNamesString}>(Item{i});");
                    writer.WriteLine("}");
                }
                writer.WriteLine("throw new InvalidOperationException(\"None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?\");");
                writer.WriteLine("}");
                writer.WriteLine("}");

                writer.WriteLine("public partial class ConvertibleTo<TBase> {");
                writer.WriteLine(
                    $"public class Either<{genericArgNamesString}> : EitherBase<{genericArgNamesString}>, IEither<{genericArgNamesString}>\n{{");
                writer.WriteLine(string.Join("\n", subClassConstructors));
                GenerateSubClassImplicitOperators(writer, "Either", arity, genericArgNames);
                writer.WriteLine($"public static implicit operator TBase(Either<{string.Join(", ", genericArgNames)}> either) {{");
                writer.WriteLine($"return either;");
                writer.WriteLine("}");
                GenerateAbstractValue(writer, arity);
                writer.WriteLine("}");
                writer.WriteLine("}");
                
                writer.WriteLine("public partial class SubTypesOf<TBase> {");
                writer.WriteLine(
                    $"public class Either<{genericArgNamesString}> : ConvertibleTo<TBase>.Either<{genericArgNamesString}>, IEither<{genericArgNamesString}> {baseConstraints}\n{{");
                writer.WriteLine(string.Join("\n", subClassConstructors));
                var typeArgs = genericArgNames.Select(arg => $"{{typeof({arg}).Name}}").Humanize("or");
                writer.WriteLine($"public Either(TBase item) {{\n" +
                                        "if (item == null) throw new ArgumentNullException(\"item\");\n" +
                                         string.Join("\n", Enumerable.Range(1, arity).Select(i => $"if (item is T{i} item{i}) {{\nItem{i} = item{i};\nreturn;\n}}")) +
                                         $"\nthrow new ArgumentException($\"Expected argument to be either a {typeArgs} but instead got a type of {{typeof(TBase).Name}}: {{item.GetType().Name}}\", \"name\");" +
                                         "\n}");
                GenerateValue(writer, arity);
                GenerateSubClassImplicitOperators(writer, "Either", arity, genericArgNames);
                writer.WriteLine("}");
                writer.WriteLine("}");
                
                writer.WriteLine(
                    $"public class Either<{genericArgNamesString}> : SubTypesOf<object>.Either<{genericArgNamesString}>, IEither<{genericArgNamesString}>\n{{");
                writer.WriteLine(string.Join("\n", subClassConstructors));
                GenerateSubClassImplicitOperators(writer, "Either", arity, genericArgNames);
                writer.WriteLine("}");
            }

            if (part == CodePart.ExtensionMethods)
            {
                writer.WriteLine($"public static class Either{arity}Extensions\n{{");

                for (var i = 1; i <= arity; i++)
                {
                    GeneratePartialSelect(writer, arity, i);
                }
                for (var i = 1; i <= arity; i++)
                {
                    GenerateEitherExtensionMethod(writer, arity, i);
                }
                GenerateFullSelect(writer, arity, genericArgNamesA, genericArgNamesB);
                GenerateFullForEach(writer, arity, genericArgNames);
                GenerateSafeCastMethod(writer, arity, genericArgNames);
                
                writer.WriteLine("}");
            }
        }

        private static void GenerateSafeCastMethod(TextWriter writer, int arity, List<string> genericArgNames)
        {
            var args = string.Join(", ", Enumerable.Range(1, arity).Select(i => $"T{i}"));
            var constraints = string.Join(" ", Enumerable.Range(1, arity).Select(i => $"where T{i} : TBase"));
            writer.WriteLine($"public static SubTypesOf<TBase>.IEither<{args}> AsSubTypes<TBase, {args}>(this ConvertibleTo<TBase>.IEither<{args}> either) {constraints} {{");
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"if (either.Item{i} != null) {{");
                writer.WriteLine($"return new SubTypesOf<TBase>.Either<{args}>(either.Item{i});");
                writer.WriteLine("}");
            }
            writer.WriteLine("throw new InvalidOperationException(\"None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?\");");
            writer.WriteLine("}");
        }

        private static void GenerateEitherExtensionMethod(TextWriter writer, int arity, int i)
        {
            var genericArgNames = Enumerable.Range(1, arity).Select(i => $"T{i}").ToImmutableList();
            var genericArgNamesString = string.Join(", ", genericArgNames);
            writer.WriteLine($"public static IEither<{genericArgNamesString}> Either<{genericArgNamesString}>(this T{i} item) {{");
            writer.WriteLine($"return new Either<{genericArgNamesString}>(item);");
            writer.WriteLine("}");
        }
        
        private static void GenerateSubClassImplicitOperators(TextWriter writer, string className, int arity, List<string> genericArgNames)
        {
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"public static implicit operator {className}<{string.Join(", ", genericArgNames)}>(T{i} t{i}) {{");
                writer.WriteLine($"return new(t{i});");
                writer.WriteLine("}");
            }
        }

        private static void GenerateBaseImplicitOperators(TextWriter writer, string className, int arity, List<string> genericArgNames)
        {
            GenerateSubClassImplicitOperators(writer, className, arity, genericArgNames);
            
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"public static implicit operator T{i}({className}<{string.Join(", ", genericArgNames)}> either) {{");
                writer.WriteLine($"return either.Item{i};");
                writer.WriteLine("}");
            }
        }

        private static void GenerateAbstractValue(TextWriter writer, int arity)
        {
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"protected TBase Convert{i}(T{i} item{i}) {{");
                writer.WriteLine($"if (item{i} is TBase @base) {{");
                writer.WriteLine("return @base;");
                writer.WriteLine("}");
                writer.WriteLine($"throw new NotImplementedException($\"Cannot convert from {{typeof(T{i}).Name}} to {{typeof(TBase).Name}}\");");
                writer.WriteLine("}");
            }
            writer.Write("public virtual TBase Value => (TBase)(Item1 != null ? Convert1(Item1) : default)");
            for (var i = 2; i <= arity; i++)
            {
                writer.Write($" ?? (TBase)(Item{i} != null ? Convert{i}(Item{i}) : default)");
            }
            writer.WriteLine(";");
        }

        private static void GenerateValue(TextWriter writer, int arity)
        {
            writer.Write("public virtual TBase Value => Item1");
            for (var i = 2; i <= arity - 1; i++)
            {
                writer.Write($" ?? Item{i}");
            }
            writer.Write($" ?? (TBase)Item{arity}");
            writer.WriteLine(";");
        }

        private static void GenerateEqualityMembers(TextWriter writer, int arity)
        {
            var genericParameters = string.Join(", ", Enumerable.Repeat(0, arity).Select((_, i) => $"T{i + 1}"));
            writer.WriteLine($"public bool Equals(IEither<{genericParameters}> other) {{");
            writer.WriteLine("if (ReferenceEquals(null, other)) return false;");
            writer.WriteLine("if (ReferenceEquals(this, other)) return true;");
            writer.Write("return ");
            for (var i = 0; i < arity; i++)
            {
                writer.Write($"Equals(Item{i+1}, other.Item{i+1})");
                if (i + 1 < arity)
                {
                    writer.Write(" && ");
                }
                else
                {
                    writer.WriteLine(";\n}\n");
                }
            }
            
            writer.WriteLine($"public override bool Equals(object obj) {{\nreturn ReferenceEquals(this, obj) || (obj is IEither<{genericParameters}> other && Equals(other));\n}}\n");

            writer.WriteLine("public override int GetHashCode() {");
            writer.WriteLine("unchecked {");
            writer.WriteLine("int hash = 17;");
            for (var i = 0; i < arity; i++)
            {
                writer.WriteLine($"hash = hash * 23 + Item{i+1}.GetHashCode();");
            }
            writer.WriteLine("return hash;");
            writer.WriteLine("}\n}");
        }

        private static void GenerateToString(TextWriter writer, int arity)
        {
            writer.WriteLine("public override string ToString() {");
            var genericParameters = string.Join(", ", Enumerable.Repeat(0, arity).Select((_, i) => $"T{i + 1}"));
            for (var i = 0; i < arity; i++)
            {
                writer.WriteLine($"if (Item{i+1} != null) {{");
                writer.WriteLine("return $\"{Utility.ConvertToCSharpTypeName(typeof(Either<" + genericParameters + ">))}({Utility.ConvertToCSharpTypeName(typeof(T" + (i + 1) + "))} Item" + (i+1) + ": {Item" + (i+1) + "})\";");
                writer.WriteLine("}");
            }
            writer.WriteLine("throw new InvalidOperationException(\"None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?\");");
            writer.WriteLine("}");
        }

        private static void GenerateOrDeclaration(TextWriter writer, int arity, int biggerArityArgs)
        {
            var arityArgs = Enumerable.Range(1, arity).Select(i => $"T{i}");
            var additionalArityArgs = Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"T{i}");
            var additionalArityArgsConstraints = string.Join(" ", Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"where T{i} : TBase"));
            var maxArityArgs = Enumerable.Range(1, biggerArityArgs).Select(i => $"T{i}");
            writer.WriteLine(
                $"IEither<{string.Join(", ", maxArityArgs)}> Or<{string.Join(", ", additionalArityArgs)}>();");
        }

        private static void GenerateOrImplementation(TextWriter writer, int arity, int biggerArityArgs)
        {
            var arityArgs = Enumerable.Range(1, arity).Select(i => $"T{i}");
            var additionalArityArgs = Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"T{i}");
            var additionalArityArgsConstraints = string.Join(" ", Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"where T{i} : TBase"));
            var maxArityArgs = Enumerable.Range(1, biggerArityArgs).Select(i => $"T{i}");
            writer.WriteLine(
                $"public IEither<{string.Join(", ", maxArityArgs)}> Or<{string.Join(", ", additionalArityArgs)}>()\n{{");

            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"if (Item{i} != null) {{");
                writer.WriteLine($"return new Either<{string.Join(", ", maxArityArgs)}>(Item{i});");
                writer.WriteLine("}");
            }

            writer.WriteLine("throw new System.InvalidOperationException(\"The either has no values\");\n}");
        }

        private static void GeneratePartialSelect(TextWriter writer, int arity, int i)
        {
            writer.Write("public static SubTypesOf<object>.IEither<");
            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                    writer.Write($"T{j}B");
                else
                    writer.Write($"T{j}");

                if (j < arity) writer.Write(", ");
            }

            writer.Write($"> Select{i}<TBase, ");

            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                    writer.Write($"T{j}A, T{j}B");
                else
                    writer.Write($"T{j}");

                if (j < arity) writer.Write(", ");
            }

            writer.Write(">(SubTypesOf<TBase>.IEither<");

            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                    writer.Write($"T{j}A");
                else
                    writer.Write($"T{j}");

                if (j < arity) writer.Write(", ");
            }

            var constraintsList = new List<string>();
            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                     constraintsList.Add($"where T{j}A : TBase");
                else
                    constraintsList.Add($"where T{j} : TBase");
            }

            var constraints = string.Join(" ", constraintsList);

            writer.Write($"> either, Func<T{i}A, T{i}B> selector) {constraints}\n{{\n");

            for (var j = 1; j <= arity; j++)
            {
                if (j > 1) writer.Write("else ");

                writer.Write($"if (either.Item{j} != null) {{\nreturn new Either<");
                for (var k = 1; k <= arity; k++)
                {
                    if (k == i)
                        writer.Write($"T{k}B");
                    else
                        writer.Write($"T{k}");

                    if (k < arity) writer.Write(", ");
                }

                writer.Write(">(");

                if (j == i)
                    writer.Write($"selector(either.Item{j})");
                else
                    writer.Write($"either.Item{j}");

                writer.Write(");\n}\n");
            }

            writer.Write("else {\nthrow new InvalidOperationException();\n}\n");

            writer.WriteLine("}");
        }

        private static void GenerateFullSelect(TextWriter writer, int arity, List<string> genericArgNamesA,
            List<string> genericArgNamesB)
        {
            var body = new StringBuilder();
            var selectors = new List<string>();
            for (var i = 1; i <= arity; i++)
            {
                if (i > 1) body.Append("else ");

                body.Append(
                    $"if (input.Item{i} != null) {{\nreturn new Either<{string.Join(", ", genericArgNamesB)}>(\n");

                body.Append($"selector{i}(input.Item{i})");

                body.Append(");\n}\n");

                selectors.Add($"Func<T{i}A, T{i}B> selector{i}");
            }

            body.Append("else {\nthrow new InvalidOperationException();\n}\n");

            var baseConstraints = string.Join(" ",
                string.Join(" ", genericArgNamesA.Select(arg => $"where {arg} : TBase")));
            
            writer.WriteLine($"public static SubTypesOf<object>.IEither<{string.Join(", ", genericArgNamesB)}> " +
                             $"Select<TBase, {string.Join(", ", genericArgNamesA)}, {string.Join(", ", genericArgNamesB)}>(" +
                             $"this SubTypesOf<TBase>.IEither<{string.Join(", ", genericArgNamesA)}> input, {string.Join(", ", selectors)}) {baseConstraints} {{\n" +
                             body +
                             "}\n");
        }

        private static void GenerateFullForEach(TextWriter writer, int arity, List<string> genericArgNames)
        {
            var body = new StringBuilder();
            var actions = new List<string>();
            for (var i = 1; i <= arity; i++)
            {
                if (i > 1) body.Append("else ");

                body.Append(
                    $"if (input.Item{i} != null) {{\n");

                body.Append($"action{i}(input.Item{i});\n}}\n");

                actions.Add($"Action<T{i}> action{i}");
            }

            body.Append("else {\nthrow new InvalidOperationException();\n}\n");
            body.Append("return input;\n");

            writer.WriteLine($"public static IEitherBase<{string.Join(", ", genericArgNames)}> " +
                             $"ForEach<{string.Join(", ", genericArgNames)}>(" +
                             $"this IEitherBase<{string.Join(", ", genericArgNames)}> input, {string.Join(", ", actions)}) {{\n" +
                             body +
                             "}\n");
        }

        private enum CodePart
        {
            Interface,
            Class,
            ExtensionMethods
        }
    }
}