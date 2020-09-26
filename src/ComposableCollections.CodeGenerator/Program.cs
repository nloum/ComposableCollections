using System;
using Autofac;
using CommandLine;

namespace ComposableCollections.CodeGenerator
{
	class Program
	{
		static void Main(string[] args)
        {
	        var parser = new Parser(with => with.EnableDashDash = true);
	        var parserResult = parser.ParseArguments<CommandLineOptions>(args);
	        Console.WriteLine(parserResult);
	        parserResult
		        .WithNotParsed(errors =>
		        {
			        foreach(var error in errors)
			        {
				        Console.WriteLine(error);
			        }
		        })
		        .WithParsed(options =>
		        {
			        var containerBuilder = new ContainerBuilder();
			        containerBuilder.RegisterModule<AutofacModule>();
			        var container = containerBuilder.Build();

			        var commandLineService = container.Resolve<ICommandLineService>();
			        commandLineService.Run(options);
		        });
	        
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
	        var anonymousImplementationGeneratorSettings = new AnonymousImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Interfaces",
		        AllowedArguments = new List<string>()
		        {
			        "IComposableReadOnlyDictionary",
			        "IComposableDictionary"
		        },
		        InterfacesToImplement = new List<string>()
		        {
			        "IDisposableDictionary",
			        "IDisposableQueryableDictionary",
			        "IDisposableQueryableReadOnlyDictionary",
			        "IDisposableReadOnlyDictionary",
			        "IQueryableDictionary",
			        "IQueryableReadOnlyDictionary",
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
	        var getOrDefaultDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "DictionaryGetOrDefaultDecorator",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "GetOrDefaultDictionaryDecorator" },
		        }
	        };
	        var getOrDefaultExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClasses = new List<string>() { "DictionaryGetOrDefaultDecorator" },
		        ExtensionMethodName = "WithDefaultValue"
	        };
	        var getOrRefreshDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "DictionaryGetOrRefreshDecorator",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "GetOrRefreshDictionaryDecorator" },
		        }
	        };
	        var getOrRefreshExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClasses = new List<string>() { "DictionaryGetOrRefreshDecorator" },
		        ExtensionMethodName = "WithRefreshing"
	        };
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
	        var withReadWriteLockQueryableDecoratorSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Decorators",
		        BaseClass = "ReadWriteLockQueryableDictionaryDecorator",
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "ReadWriteLockDictionaryDecorator" },
		        }
	        };
	        var withReadWriteLockExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClasses = new List<string>() { "ReadWriteLockDictionaryDecorator" },
		        ExtensionMethodName = "WithReadWriteLock"
	        };
	        var withMappingKeysAndValuesAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "MappingKeysAndValuesDictionaryAdapter",
		        AllowDifferentTypeParameters = true,
		        ClassNameBlacklist = new List<string>()
		        {
			        "WithBuiltInKey",
			        "Queryable",
			        "ReadCached",
			        "ReadWriteCached",
		        },
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "MappingKeysAndValuesDictionaryAdapter" },
		        }
	        };
	        var withMappingKeysAndValuesReadOnlyAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "MappingKeysAndValuesReadOnlyDictionaryAdapter",
		        AllowDifferentTypeParameters = true,
		        ClassNameBlacklist = new List<string>()
		        {
			        "WithBuiltInKey",
			        "Queryable",
			        "ReadCached",
			        "ReadWriteCached",
		        },
		        ClassNameWhitelist = new List<string>()
		        {
				    "ReadOnly"
		        },
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "MappingKeysAndValuesDictionaryAdapter" },
		        }
	        };
	        var withMappingValuesAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "MappingValuesDictionaryAdapter",
		        AllowDifferentTypeParameters = true,
		        ClassNameBlacklist = new List<string>()
		        {
			        "WithBuiltInKey",
			        "Queryable",
			        "ReadCached",
			        "ReadWriteCached",
		        },
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "MappingValuesDictionaryAdapter" },
		        }
	        };
	        var withMappingValuesReadOnlyAdapterSubClassesGeneratorSettings = new SubclassCombinationImplementationGeneratorSettings()
	        {
		        Namespace = "ComposableCollections.Dictionary.Adapters",
		        BaseClass = "MappingValuesReadOnlyDictionaryAdapter",
		        AllowDifferentTypeParameters = true,
		        ClassNameBlacklist = new List<string>()
		        {
			        "WithBuiltInKey",
			        "Queryable",
			        "ReadCached",
			        "ReadWriteCached",
		        },
		        ClassNameWhitelist = new List<string>()
		        {
				    "ReadOnly"
		        },
		        ClassNameModifiers = new List<ClassNameBuilder>
		        {
			        new ClassNameBuilder() { Search = "Dictionary", Replacement = "MappingValuesDictionaryAdapter" },
		        }
	        };
	        var withMappingKeysAndValuesExtensionMethodsGeneratorSettings = new ConstructorToExtensionMethodGeneratorSettings()
	        {
		        Namespace = "ComposableCollections",
		        BaseClasses = new List<string>()
		        {
			        "MappingKeysAndValuesDictionaryAdapter",
			        "MappingKeysAndValuesReadOnlyDictionaryAdapter",
			        "MappingValuesDictionaryAdapter",
			        "MappingValuesReadOnlyDictionaryAdapter",
		        },
		        ExtensionMethodName = "WithMapping"
	        };

        }
    }
}