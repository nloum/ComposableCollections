using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using IoFluently;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using ReactiveProcesses;

namespace ComposableCollections.CodeGenerator
{
    class Program
    {
	    static void Main(string[] args)
        {
	        var ioService = new IoService(new ReactiveProcessFactory());
	        var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").IsFolder());

	        var csFiles = (repoRoot / "src" / "ComposableCollections").Descendants()
		        .Where(child => child.HasExtension(".cs"));

	        var syntaxTrees = csFiles.Select(csFile =>
		        CSharpSyntaxTree.ParseText(SourceText.From(csFile.ReadText(), Encoding.UTF8))).ToImmutableList();
	        
	        var compilation = CSharpCompilation.Create("HelloWorld")
		        .AddReferences(MetadataReference.CreateFromFile(
			        typeof(string).Assembly.Location),
			        MetadataReference.CreateFromFile((repoRoot / "src" / "ComposableCollections" / "bin" / "Debug" / "netstandard2.0" / "ComposableCollections.dll").ToString()))
		        .AddSyntaxTrees(syntaxTrees);
	        
	        var combinationInterfacesGenerator = new CombinationInterfacesGenerator();
	        var combinationInterfacesGeneratorSettings = new CombinationInterfacesGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Interfaces",
		        InterfaceNameBlacklistRegexes = new List<string>()
		        {
			        "ReadOnly.*Write",
			        "Write.*ReadOnly",
			        "Cached.*Cached",
		        },
		        InterfaceNameBuilders = new List<InterfaceNameBuilder>()
		        {
			        new InterfaceNameBuilder()
			        {
				        Search = "^ReadOnly$",
				        Replacement = "IComposableReadOnlyDictionary"
			        },
			        new InterfaceNameBuilder()
			        {
				        Search = "^$",
				        Replacement = "IComposableDictionary"
			        },
			        new InterfaceNameBuilder()
			        {
				        Search = "(.+)",
				        Replacement = "I$1Dictionary"
			        },
		        },
		        InterfaceNameModifiers = new List<InterfaceNameModifier>()
		        {
			        new InterfaceNameModifier() { "", "ReadCached" },
			        new InterfaceNameModifier() { "", "WriteCached" },
			        new InterfaceNameModifier() { "", "ReadWriteCached" },
			        new InterfaceNameModifier() { "", "Disposable" },
			        new InterfaceNameModifier() { "", "Queryable" },
			        new InterfaceNameModifier() { "ReadOnly", "" },
		        }
	        };
	        combinationInterfacesGenerator.Initialize(combinationInterfacesGeneratorSettings);
	        var combinedInterfaces = combinationInterfacesGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));

	        var anonymousImplementationGenerator = new DelegateImplementationGenerator();
	        var anonymousImplementationGeneratorSettings = new DelegateImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Interfaces",
		        InterfacesToImplement = new List<string>()
		        {
			        "IDisposableQueryableDictionary",
			        "IDisposableQueryableReadOnlyDictionary",
			        "IReadCachedDisposableDictionary",
			        "IReadCachedDisposableQueryableDictionary",
			        "IReadCachedDisposableQueryableReadOnlyDictionary",
			        "IReadCachedDisposableReadOnlyDictionary",
			        "IReadCachedQueryableDictionary",
			        "IReadCachedQueryableReadOnlyDictionary",
			        "IReadWriteCachedDisposableDictionary",
			        "IReadWriteCachedDisposableQueryableDictionary",
			        "IReadWriteCachedQueryableDictionary",
			        "IWriteCachedDisposableDictionary",
			        "IWriteCachedDisposableQueryableDictionary",
			        "IWriteCachedQueryableDictionary",
		        }
	        };
	        anonymousImplementationGenerator.Initialize(anonymousImplementationGeneratorSettings);
	        var anonymousImplementations = anonymousImplementationGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));

	        var decoratorBaseGenerator = new DecoratorBaseGenerator();
	        var decoratorBaseSettings = new DecoratorBaseGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Interfaces",
		        InterfacesToImplement = new List<string>()
		        {
			        "IDisposableQueryableDictionary",
			        "IDisposableQueryableReadOnlyDictionary",
			        "IReadCachedDisposableDictionary",
			        "IReadCachedDisposableQueryableDictionary",
			        "IReadCachedDisposableQueryableReadOnlyDictionary",
			        "IReadCachedDisposableReadOnlyDictionary",
			        "IReadCachedQueryableDictionary",
			        "IReadCachedQueryableReadOnlyDictionary",
			        "IReadWriteCachedDisposableDictionary",
			        "IReadWriteCachedDisposableQueryableDictionary",
			        "IReadWriteCachedQueryableDictionary",
			        "IWriteCachedDisposableDictionary",
			        "IWriteCachedDisposableQueryableDictionary",
			        "IWriteCachedQueryableDictionary",
		        }
	        };
	        decoratorBaseGenerator.Initialize(decoratorBaseSettings);
	        var decoratorBases = decoratorBaseGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        
	        var withMappingSettings = new WithMappingGeneratorSettings()
	        {
		        Interfaces = new List<string>
		        {
			        "ICachedDictionary",
			        "ICachedDisposableDictionary",
			        "ICachedDisposableQueryableDictionary",
			        "ICachedQueryableDictionary",
			        "IComposableDictionary",
			        "IComposableReadOnlyDictionary",
			        "IDisposableDictionary",
			        "IDisposableQueryableDictionary",
			        "IDisposableQueryableReadOnlyDictionary",
			        "IDisposableReadOnlyDictionary",
			        "IQueryableDictionary",
			        "IQueryableReadOnlyDictionary"
		        }
	        };
	        var withMappingGenerator = new WithMappingGenerator();
	        withMappingGenerator.Initialize(withMappingSettings);
	        var withMappingFiles = withMappingGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        
	        var autoGeneratedInterfacesFolder =
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "AutoGeneratedInterfaces");
	        foreach (var result in combinedInterfaces.Concat(anonymousImplementations).Concat(decoratorBases).Concat(withMappingFiles))
	        {
		        (autoGeneratedInterfacesFolder / result.Key).WriteText(result.Value);
	        }
	        
// 	        var dictionaryExtensionsFilePath = repoRoot / "src" / "ComposableCollections" / "DictionaryExtensions.g.cs";
// 	        using (var streamWriter = dictionaryExtensionsFilePath.OpenWriter())
// 	        {
// 		        streamWriter.WriteLine(@"using System;
// 		        using System.Collections.Generic;
// 		        using System.Linq;
// 		        using System.Linq.Expressions;
// 		        using ComposableCollections.Common;
// 		        using ComposableCollections.Dictionary;
// 		        using ComposableCollections.Dictionary.Adapters;
// 		        using ComposableCollections.Dictionary.Decorators;
// 		        using ComposableCollections.Dictionary.Interfaces;
// 		        using ComposableCollections.Dictionary.Sources;
// 		        using ComposableCollections.Dictionary.Transactional;
// 		        using ComposableCollections.Dictionary.WithBuiltInKey;
// 		        using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
// 		        using UtilityDisposables;
//
// 			        namespace ComposableCollections
// 		        {
//         public static partial class DictionaryExtensions
//         {");
// 		        GenerateWithReadWriteLockExtensionMethods(streamWriter);
// 		        GenerateWithDefaultValueExtensionMethods(streamWriter);
// 		        GenerateWithWriteCachingExtensionMethods(streamWriter);
// 		        GenerateWithRefreshingExtensionMethods(streamWriter);
// 		        GenerateWithMappingExtensionMethods(streamWriter);
// 		        streamWriter.WriteLine("}\n}");
// 	        }
        }
    }
}