using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleMonads.CodeGeneration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var maxArity = 16;

            for (var i = 2; i <= maxArity; i++)
            {
                File.Delete($"IEither{i}.cs");
                File.Delete($"Either{i}.cs");
                File.Delete($"EitherExtensions{i}.cs");

                using (var fs = File.OpenWrite($"IEither{i}.cs"))
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine("namespace SimpleMonads {");
                    GenerateEither(writer, i, maxArity, CodePart.Interface);
                    writer.WriteLine("}");
                }

                using (var fs = File.OpenWrite($"Either{i}.cs"))
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine("using System;\n\nnamespace SimpleMonads {");
                    GenerateEither(writer, i, maxArity, CodePart.Class);
                    writer.WriteLine("}");
                }

                using (var fs = File.OpenWrite($"Either{i}Extensions.cs"))
                using (var writer = new StreamWriter(fs))
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
            var constructors = new List<string>();

            for (var i = 1; i <= arity; i++)
            {
                var argName = "T" + i;
                genericArgNames.Add(argName);
                genericArgDefinitions.Add($"out {argName}");
                interfaceProperties.Add($"IMaybe<{argName}> Item{i} {{ get; }}");
                classProperties.Add($"public IMaybe<{argName}> Item{i} {{ get; }} = Utility.Nothing<{argName}>();");
                constructors.Add($"public Either({argName} item{i}) {{\n" +
                                 $"Item{i} = item{i}.ToMaybe();\n" +
                                 "}");
            }

            var genericArgNamesA = genericArgNames.Select(x => x + "A").ToList();
            var genericArgNamesB = genericArgNames.Select(x => x + "B").ToList();

            if (part == CodePart.Interface)
            {
                writer.WriteLine($"public interface IEither<{string.Join(", ", genericArgDefinitions)}>\n{{");
                writer.WriteLine(string.Join("\n", interfaceProperties));
                for (var i = arity + 1; i <= maxArity; i++) GenerateOrDeclaration(writer, arity, i);
                writer.WriteLine("}");
            }

            if (part == CodePart.Class)
            {
                writer.WriteLine(
                    $"public sealed class Either<{string.Join(", ", genericArgNames)}> : IEither<{string.Join(", ", genericArgNames)}>\n{{");
                writer.WriteLine(string.Join("\n", constructors));
                writer.WriteLine(string.Join("\n", classProperties));
                for (var i = arity + 1; i <= maxArity; i++) GenerateOrImplementation(writer, arity, i);
                writer.WriteLine("}");
            }

            if (part == CodePart.ExtensionMethods)
            {
                writer.WriteLine($"public static class Either{arity}Extensions\n{{");

                for (var i = 1; i <= arity; i++) GeneratePartialSelect(writer, arity, i);
                GenerateFullSelect(writer, arity, genericArgNamesA, genericArgNamesB);

                writer.WriteLine("}");
            }
        }

        private static void GenerateOrDeclaration(TextWriter writer, int arity, int biggerArityArgs)
        {
            var arityArgs = Enumerable.Range(1, arity).Select(i => $"T{i}");
            var additionalArityArgs = Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"T{i}");
            var maxArityArgs = Enumerable.Range(1, biggerArityArgs).Select(i => $"T{i}");
            writer.WriteLine(
                $"IEither<{string.Join(", ", maxArityArgs)}> Or<{string.Join(", ", additionalArityArgs)}>();");
        }

        private static void GenerateOrImplementation(TextWriter writer, int arity, int biggerArityArgs)
        {
            var arityArgs = Enumerable.Range(1, arity).Select(i => $"T{i}");
            var additionalArityArgs = Enumerable.Range(arity + 1, biggerArityArgs - arity).Select(i => $"T{i}");
            var maxArityArgs = Enumerable.Range(1, biggerArityArgs).Select(i => $"T{i}");
            writer.WriteLine(
                $"public IEither<{string.Join(", ", maxArityArgs)}> Or<{string.Join(", ", additionalArityArgs)}>()\n{{");

            for (var i = 1; i <= arity; i++)
            {
                writer.WriteLine($"if (Item{i}.HasValue) {{");
                writer.WriteLine($"return new Either<{string.Join(", ", maxArityArgs)}>(Item{i}.Value);");
                writer.WriteLine("}");
            }

            writer.WriteLine("throw new System.InvalidOperationException(\"The either has no values\");\n}");
        }

        private static void GeneratePartialSelect(TextWriter writer, int arity, int i)
        {
            writer.Write("public static IEither<");
            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                    writer.Write($"T{j}B");
                else
                    writer.Write($"T{j}");

                if (j < arity) writer.Write(", ");
            }

            writer.Write($"> Select{i}<");

            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                    writer.Write($"T{j}A, T{j}B");
                else
                    writer.Write($"T{j}");

                if (j < arity) writer.Write(", ");
            }

            writer.Write(">(IEither<");

            for (var j = 1; j <= arity; j++)
            {
                if (j == i)
                    writer.Write($"T{j}A");
                else
                    writer.Write($"T{j}");

                if (j < arity) writer.Write(", ");
            }

            writer.Write($"> either, Func<T{i}A, T{i}B> selector)\n{{\n");

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

            writer.WriteLine($"public static IEither<{string.Join(", ", genericArgNamesB)}> " +
                             $"Select<{string.Join(", ", genericArgNamesA)}, {string.Join(", ", genericArgNamesB)}>(" +
                             $"this IEither<{string.Join(", ", genericArgNamesA)}> input, {string.Join(", ", selectors)}) {{\n" +
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