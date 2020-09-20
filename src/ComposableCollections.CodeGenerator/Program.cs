using System.Collections.Generic;
using System.Collections.Immutable;
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
		        PreExistingInterfaces = new List<string>
		        {
			        "IComposableDictionary",
			        "IComposableReadOnlyDictionary",
			        "IDisposableReadOnlyDictionary",
			        "IQueryableReadOnlyDictionary",
			        "IReadCachedDictionary",
			        "IReadCachedReadOnlyDictionary",
			        "IReadWriteCachedDictionary",
			        "IWriteCachedDictionary",
		        },
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

	        var anonymousImplementationGenerator = new AnonymousImplementationGenerator();
	        var anonymousImplementationGeneratorSettings = new AnonymousImplementationGeneratorSettings()
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
	        
	        var withDefaultValueSettings = new WithDefaultValueGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        Class = "WithDefaultValueExtensions",
		        Partial = false,
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
	        var withDefaultValueGenerator = new WithDefaultValueGenerator();
	        withDefaultValueGenerator.Initialize(withDefaultValueSettings);
	        var withDefaultValueFiles = withDefaultValueGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));

	        var withMappingSettings = new WithMappingGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        Class = "WithMappingExtensions",
		        Partial = false,
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
	        
	        var withReadWriteLockSettings = new WithReadWriteLockGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        Class = "WithReadWriteLockExtensions",
		        Partial = false,
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
	        var withReadWriteLockGenerator = new WithReadWriteLockGenerator();
	        withReadWriteLockGenerator.Initialize(withReadWriteLockSettings);
	        var withReadWriteLockFiles = withReadWriteLockGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));

	        var withRefreshingSettings = new WithRefreshingGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        Class = "WithRefreshingExtensions",
		        Partial = false,
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
	        var withRefreshingGenerator = new WithRefreshingGenerator();
	        withRefreshingGenerator.Initialize(withRefreshingSettings);
	        var withRefreshingFiles = withRefreshingGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));

	        var withWriteCachingSettings = new WithWriteCachingGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        Class = "WithWriteCachingExtensions",
		        Partial = false,
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
	        var withWriteCachingGenerator = new WithWriteCachingGenerator();
	        withWriteCachingGenerator.Initialize(withWriteCachingSettings);
	        var withWriteCachingFiles = withWriteCachingGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));

	        var withReadCachingGenerator = new ExtensionMethodVariationGenerator();
	        withReadCachingGenerator.Initialize(new ExtensionMethodVariationGeneratorSettings()
	        {
		        ExtensionMethodName = "WithReadCaching"
	        });
	        var withReadCachingFiles = withReadCachingGenerator.Generate(syntaxTrees, syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withReadCachingFiles)
	        {
		        (repoRoot / "src" / "ComposableCollections" / result.Key).WriteText(result.Value);
	        }
	        
	        foreach (var result in combinedInterfaces)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Interfaces" / result.Key).WriteText(result.Value);
	        }

	        foreach (var result in anonymousImplementations)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Anonymous" / result.Key).WriteText(result.Value);
	        }

	        foreach (var result in decoratorBases)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Base" / result.Key).WriteText(result.Value);
	        }

	        foreach (var result in withDefaultValueFiles.Concat(withMappingFiles)
		        .Concat(withReadWriteLockFiles).Concat(withRefreshingFiles).Concat(withWriteCachingFiles))
	        {
		        (repoRoot / "src" / "ComposableCollections" / result.Key).WriteText(result.Value);
	        }
	        
	        var concurrentWriteCachedDictionaryAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "ConcurrentWriteCachedDictionaryAdapter",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "WriteCached", Replacement = "ConcurrentWriteCached" },
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "DictionaryAdapter" },
		        }
	        };
	        var concurrentWriteCachedDictionaryAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        concurrentWriteCachedDictionaryAdapterSubClassesGenerator.Initialize(concurrentWriteCachedDictionaryAdapterSubClassesGeneratorSettings);
	        var concurrentWriteCachedDictionarySubclasses =
		        concurrentWriteCachedDictionaryAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in concurrentWriteCachedDictionarySubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Adapters" / result.Key).WriteText(result.Value);
	        }
        }
    }
}