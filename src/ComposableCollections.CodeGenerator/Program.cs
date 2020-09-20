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
	        
	        var getOrDefaultDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "DictionaryGetOrDefaultDecorator",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "GetOrDefaultDictionaryDecorator" },
		        }
	        };
	        var getOrDefaultDecoratorAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        getOrDefaultDecoratorAdapterSubClassesGenerator.Initialize(getOrDefaultDecoratorSubClassesGeneratorSettings);
	        var getOrDefaultDecoratorSubclasses =
		        getOrDefaultDecoratorAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in getOrDefaultDecoratorSubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Decorators" / result.Key).WriteText(result.Value);
	        }
	        
	        var getOrDefaultExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClass = "DictionaryGetOrDefaultDecorator",
		        ExtensionMethodName = "WithDefaultValue"
	        };
	        var getOrDefaultExtensionMethodsGenerator = new ConstructorToExtensionMethodGenerator();
	        getOrDefaultExtensionMethodsGenerator.Initialize(getOrDefaultExtensionMethodsGeneratorSettings);
	        var getOrDefaultExtensionMethods =
		        getOrDefaultExtensionMethodsGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in getOrDefaultExtensionMethods)
	        {
		        (repoRoot / "src" / "ComposableCollections" / result.Key).WriteText(result.Value);
	        }
	        
	        var getOrRefreshDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "DictionaryGetOrRefreshDecorator",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "GetOrRefreshDictionaryDecorator" },
		        }
	        };
	        var getOrRefreshDecoratorAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        getOrRefreshDecoratorAdapterSubClassesGenerator.Initialize(getOrRefreshDecoratorSubClassesGeneratorSettings);
	        var getOrRefreshDecoratorSubclasses =
		        getOrRefreshDecoratorAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in getOrRefreshDecoratorSubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Decorators" / result.Key).WriteText(result.Value);
	        }

	        var getOrRefreshExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClass = "DictionaryGetOrRefreshDecorator",
		        ExtensionMethodName = "WithRefreshing"
	        };
	        var getOrRefreshExtensionMethodsGenerator = new ConstructorToExtensionMethodGenerator();
	        getOrRefreshExtensionMethodsGenerator.Initialize(getOrRefreshExtensionMethodsGeneratorSettings);
	        var getOrRefreshExtensionMethods =
		        getOrRefreshExtensionMethodsGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in getOrRefreshExtensionMethods)
	        {
		        (repoRoot / "src" / "ComposableCollections" / result.Key).WriteText(result.Value);
	        }
	        
	        
	        
	        var withReadWriteLockDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "ReadWriteLockDictionaryDecorator",
		        ClassNameBlacklist = new List<string>()
		        {
			        "Queryable"
		        },
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "ReadWriteLockDictionaryDecorator" },
		        }
	        };
	        var withReadWriteLockDecoratorAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        withReadWriteLockDecoratorAdapterSubClassesGenerator.Initialize(withReadWriteLockDecoratorSubClassesGeneratorSettings);
	        var withReadWriteLockDecoratorSubclasses =
		        withReadWriteLockDecoratorAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withReadWriteLockDecoratorSubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Decorators" / result.Key).WriteText(result.Value);
	        }

	        
	        var withReadWriteLockQueryableDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "ReadWriteLockQueryableDictionaryDecorator",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "ReadWriteLockDictionaryDecorator" },
		        }
	        };
	        var withReadWriteLockQueryableDecoratorAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        withReadWriteLockQueryableDecoratorAdapterSubClassesGenerator.Initialize(withReadWriteLockQueryableDecoratorSubClassesGeneratorSettings);
	        var withReadWriteLockQueryableDecoratorSubclasses =
		        withReadWriteLockQueryableDecoratorAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withReadWriteLockQueryableDecoratorSubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Decorators" / result.Key).WriteText(result.Value);
	        }

	        var withReadWriteLockExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClass = "ReadWriteLockDictionaryDecorator",
		        ExtensionMethodName = "WithReadWriteLock"
	        };
	        var withReadWriteLockExtensionMethodsGenerator = new ConstructorToExtensionMethodGenerator();
	        withReadWriteLockExtensionMethodsGenerator.Initialize(withReadWriteLockExtensionMethodsGeneratorSettings);
	        var withReadWriteLockExtensionMethods =
		        withReadWriteLockExtensionMethodsGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withReadWriteLockExtensionMethods)
	        {
		        (repoRoot / "src" / "ComposableCollections" / result.Key).WriteText(result.Value);
	        }
	        
	        
	        
	        
	        
	        var withMappingAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "MappingKeysAndValuesDictionaryAdapter",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "MappingKeysAndValuesDictionaryAdapter" },
		        }
	        };
	        var withMappingAdapterAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        withMappingAdapterAdapterSubClassesGenerator.Initialize(withMappingAdapterSubClassesGeneratorSettings);
	        var withMappingAdapterSubclasses =
		        withMappingAdapterAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withMappingAdapterSubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Adapters" / result.Key).WriteText(result.Value);
	        }

	        var withMappingReadOnlyAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "MappingValuesReadOnlyDictionaryAdapter",
		        ClassNameWhitelist = new List<string>()
		        {
				    "ReadOnly"
		        },
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "MappingValuesDictionaryAdapter" },
		        }
	        };
	        var withMappingReadOnlyAdapterAdapterSubClassesGenerator = new SubclassCombinationImplementationGenerator();
	        withMappingReadOnlyAdapterAdapterSubClassesGenerator.Initialize(withMappingReadOnlyAdapterSubClassesGeneratorSettings);
	        var withMappingReadOnlyAdapterSubclasses =
		        withMappingReadOnlyAdapterAdapterSubClassesGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withMappingReadOnlyAdapterSubclasses)
	        {
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "Adapters" / result.Key).WriteText(result.Value);
	        }

	        var withMappingExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClass = "ReadWriteLockDictionaryAdapter",
		        ExtensionMethodName = "WithMapping"
	        };
	        var withMappingExtensionMethodsGenerator = new ConstructorToExtensionMethodGenerator();
	        withMappingExtensionMethodsGenerator.Initialize(withMappingExtensionMethodsGeneratorSettings);
	        var withMappingExtensionMethods =
		        withMappingExtensionMethodsGenerator.Generate(syntaxTrees,
			        syntaxTree => compilation.GetSemanticModel(syntaxTree));
	        foreach (var result in withMappingExtensionMethods)
	        {
		        (repoRoot / "src" / "ComposableCollections" / result.Key).WriteText(result.Value);
	        }
        }
    }
}