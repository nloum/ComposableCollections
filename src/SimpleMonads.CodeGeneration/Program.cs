using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
                    writer.WriteLine("namespace SimpleMonads {");
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
            var subTypesOfConstructors = new List<string>();
            var eitherConstructors = new List<string>();

            for (var i = 1; i <= arity; i++)
            {
                var argName = "T" + i;
                genericArgNames.Add(argName);
                genericArgDefinitions.Add($"out {argName}");
                interfaceProperties.Add($"IMaybe<{argName}> Item{i} {{ get; }}");
                classProperties.Add($"public IMaybe<{argName}> Item{i} {{ get; }} = Utility.Nothing<{argName}>();");
                subTypesOfConstructors.Add($"public Either({argName} item{i}) {{\n" +
                                           $"Item{i} = item{i}.ToMaybe();\n" +
                                           "}");
                eitherConstructors.Add($"public Either({argName} item{i}) : base(item{i}) {{ }}\n");
            }

            var genericArgNamesA = genericArgNames.Select(x => x + "A").ToList();
            var genericArgNamesB = genericArgNames.Select(x => x + "B").ToList();
            var genericArgNamesString = string.Join(", ", genericArgNames);
            var baseConstraints = string.Join(" ", genericArgNames.Select(arg => $"where {arg} : TBase"));

            if (part == CodePart.Interface)
            {
                writer.WriteLine("public partial class SubTypesOf<TBase> {");
                writer.WriteLine($"public interface IEither{arity} : IEither {{\n" +
                                 "TBase Value { get; }" +
                                 $"\n}}");
                writer.WriteLine($"public interface IEither<{string.Join(", ", genericArgDefinitions)}> : IEither{arity} {baseConstraints} \n{{");
                writer.WriteLine(string.Join("\n", interfaceProperties));
                for (var i = arity + 1; i <= maxArity; i++) GenerateOrDeclaration(writer, arity, i);
                writer.WriteLine($"public interface ICast<out TBase> : IEither<{genericArgNamesString}> {{");
                writer.WriteLine("TBase Value { get; }");
                writer.WriteLine("}");
                writer.WriteLine("}");
                writer.WriteLine("}");
                writer.WriteLine($"public interface IEither<{string.Join(", ", genericArgDefinitions)}> : SubTypesOf<object>.IEither<{genericArgNamesString}> \n{{");
                writer.WriteLine("}");
            }

            if (part == CodePart.Class)
            {
                //GenerateCastClass(writer, arity, genericArgNames);
                writer.WriteLine("public partial class SubTypesOf<TBase> {");
                writer.WriteLine(
                    $"public class Either<{string.Join(", ", genericArgNames)}> : IEither<{genericArgNamesString}>, IEquatable<IEither<{genericArgNamesString}>> {baseConstraints}\n{{");
                writer.WriteLine(string.Join("\n", subTypesOfConstructors));
                writer.WriteLine(string.Join("\n", classProperties));
                GenerateValue(writer, arity);
                for (var i = arity + 1; i <= maxArity; i++) GenerateOrImplementation(writer, arity, i);
                GenerateEqualityMembers(writer, arity);
                GenerateToString(writer, arity);
                GenerateSubTypesOfImplicitOperators(writer, arity, genericArgNames);
                //GenerateCastMethod(writer, arity, genericArgNames);
                writer.WriteLine("}");
                writer.WriteLine("}");
                writer.WriteLine(
                    $"public class Either<{genericArgNamesString}> : SubTypesOf<object>.Either<{genericArgNamesString}>, IEither<{genericArgNamesString}>\n{{");
                writer.WriteLine(string.Join("\n", eitherConstructors));
                GenerateEitherImplicitOperators(writer, arity, genericArgNames);
                writer.WriteLine("}");
            }

            if (part == CodePart.ExtensionMethods)
            {
                writer.WriteLine($"public static class Either{arity}Extensions\n{{");

                for (var i = 1; i <= arity; i++)
                {
                    GeneratePartialSelect(writer, arity, i);
                }
                GenerateFullSelect(writer, arity, genericArgNamesA, genericArgNamesB);
                GenerateFullForEach(writer, arity, genericArgNames);

                writer.WriteLine("}");
            }
        }

        private static void GenerateCastClass(TextWriter writer, int arity, List<string> genericArgNames)
        {
            var baseConstraints = string.Join(" ", genericArgNames.Select(arg => $"where {arg} : TBase"));
            var genericArgNamesString = string.Join(", ", genericArgNames);
            writer.WriteLine($"internal class CastImpl<TBase, {genericArgNamesString}> : Either<{genericArgNamesString}>, IEither<{genericArgNamesString}>.ICast<TBase> {{");
            foreach(var genericArgName in genericArgNames)
            {
                writer.WriteLine($"public CastImpl({genericArgName} item) : base(item) {{ }}");
            }
            writer.WriteLine("public new TBase Value => (TBase)base.Value;");
            writer.WriteLine("}");
        }
        
        private static void GenerateCastMethod(TextWriter writer, int arity, List<string> genericArgNames)
        {
            var genericArgNamesString = string.Join(", ", genericArgNames);
            writer.WriteLine($"public IEither<{genericArgNamesString}>.ICast<TBase> Cast<TBase>() {{");
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"if (Item{i}.HasValue) {{");
                writer.WriteLine($"return new CastImpl<TBase, {genericArgNamesString}>(Item{i}.Value);");
                writer.WriteLine("}");
            }
            writer.WriteLine("throw new InvalidOperationException(\"None of the Either items has a value, which violates a core assumption of this class. Did you override the Either class and break this assumption?\");");
            writer.WriteLine("}");
        }

        private static void GenerateEitherImplicitOperators(TextWriter writer, int arity, List<string> genericArgNames)
        {
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"public static implicit operator Either<{string.Join(", ", genericArgNames)}>(T{i} t{i}) {{");
                writer.WriteLine($"return new(t{i});");
                writer.WriteLine("}");
            }
        }

        private static void GenerateSubTypesOfImplicitOperators(TextWriter writer, int arity, List<string> genericArgNames)
        {
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"public static implicit operator Either<{string.Join(", ", genericArgNames)}>(T{i} t{i}) {{");
                writer.WriteLine($"return new(t{i});");
                writer.WriteLine("}");
            }
            
            writer.WriteLine($"public static implicit operator TBase(Either<{string.Join(", ", genericArgNames)}> either) {{");
            writer.WriteLine($"return either.Value;");
            writer.WriteLine("}");

            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"public static implicit operator T{i}(Either<{string.Join(", ", genericArgNames)}> either) {{");
                writer.WriteLine($"return either.Item{i}.Value;");
                writer.WriteLine("}");
            }
            
            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"public static implicit operator Maybe<T{i}>(Either<{string.Join(", ", genericArgNames)}> either) {{");
                writer.WriteLine($"return (Maybe<T{i}>)either.Item{i};");
                writer.WriteLine("}");
            }
        }

        private static void GenerateValue(TextWriter writer, int arity)
        {
            var selectArguments = string.Join(", ", Enumerable.Repeat(0, arity).Select(_ => "x => (TBase)x"));
            writer.Write("public TBase Value => Item1.Cast<TBase>()");
            for (var i = 2; i <= arity - 1; i++)
            {
                writer.Write($".Otherwise(Item{i}.Cast<TBase>()");
            }
            writer.Write($".Otherwise(() => Item{arity}.Value");
            for (var i = 2; i <= arity; i++)
            {
                writer.Write(")");
            }
            writer.WriteLine(";");
        }

        private static void GenerateEqualityMembers(TextWriter writer, int arity)
        {
            var genericParameters = string.Join(", ", Enumerable.Repeat(0, arity).Select((_, i) => $"T{i + 1}"));
            writer.WriteLine($"public bool Equals(SubTypesOf<TBase>.IEither<{genericParameters}> other) {{");
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
                writer.WriteLine($"if (Item{i+1}.HasValue) {{");
                writer.WriteLine("return $\"{Utility.ConvertToCSharpTypeName(typeof(Either<" + genericParameters + ">))}({Utility.ConvertToCSharpTypeName(typeof(T" + (i + 1) + "))} Item" + (i+1) + ": {Item" + (i+1) + ".Value})\";");
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
                $"SubTypesOf<TBase>.IEither<{string.Join(", ", maxArityArgs)}> Or<{string.Join(", ", additionalArityArgs)}>() {additionalArityArgsConstraints};");
        }

        private static void GenerateOrImplementation(TextWriter writer, int arity, int biggerArityArgs)
        {
            var arityArgs = Enumerable.Range(1, arity).Select(i => $"T{i}");
            var additionalArityArgs = Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"T{i}");
            var additionalArityArgsConstraints = string.Join(" ", Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"where T{i} : TBase"));
            var maxArityArgs = Enumerable.Range(1, biggerArityArgs).Select(i => $"T{i}");
            writer.WriteLine(
                $"public SubTypesOf<TBase>.IEither<{string.Join(", ", maxArityArgs)}> Or<{string.Join(", ", additionalArityArgs)}>() {additionalArityArgsConstraints}\n{{");

            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"if (Item{i}.HasValue) {{");
                writer.WriteLine($"return new SubTypesOf<TBase>.Either<{string.Join(", ", maxArityArgs)}>(Item{i}.Value);");
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

                writer.Write($"if (either.Item{j}.HasValue) {{\nreturn new Either<");
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
                    writer.Write($"selector(either.Item{j}.Value)");
                else
                    writer.Write($"either.Item{j}.Value");

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
                    $"if (input.Item{i}.HasValue) {{\nreturn new Either<{string.Join(", ", genericArgNamesB)}>(\n");

                body.Append($"selector{i}(input.Item{i}.Value)");

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
                    $"if (input.Item{i}.HasValue) {{\n");

                body.Append($"action{i}(input.Item{i}.Value);\n}}\n");

                actions.Add($"Action<T{i}> action{i}");
            }

            body.Append("else {\nthrow new InvalidOperationException();\n}\n");
            body.Append("return input;\n");

            var baseConstraints = string.Join(" ", genericArgNames.Select(arg => $"where {arg} : TBase"));

            writer.WriteLine($"public static SubTypesOf<TBase>.IEither<{string.Join(", ", genericArgNames)}> " +
                             $"ForEach<TBase, {string.Join(", ", genericArgNames)}>(" +
                             $"this SubTypesOf<TBase>.IEither<{string.Join(", ", genericArgNames)}> input, {string.Join(", ", actions)}) {baseConstraints} {{\n" +
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