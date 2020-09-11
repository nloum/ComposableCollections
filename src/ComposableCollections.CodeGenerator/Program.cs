using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Humanizer;
using IoFluently;
using ReactiveProcesses;

namespace ComposableCollections.CodeGenerator
{
    class Program
    {
	    private static void GenerateWithReadWriteLockExtensionMethods(TextWriter textWriter)
	    {
		     
        static void Main(string[] args)
        {
	        var ioService = new IoService(new ReactiveProcessFactory());
	        var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").IsFolder());

	        var withBuiltInKeyAdapterClasses = repoRoot / "src" / "ComposableCollections" / "Dictionary" /
	                                           "WithBuiltInKey" / "Adapters.g.cs";
	        using (var streamWriter = withBuiltInKeyAdapterClasses.OpenWriter())
	        {
		        GenerateWithBuiltInKeyAdapterClasses(streamWriter);
	        }
 	        
	        var dictionaryExtensionsFilePath = repoRoot / "src" / "ComposableCollections" / "DictionaryExtensions.g.cs";
	        using (var streamWriter = dictionaryExtensionsFilePath.OpenWriter())
	        {
		        streamWriter.WriteLine(@"using System;
		        using System.Collections.Generic;
		        using System.Linq;
		        using System.Linq.Expressions;
		        using ComposableCollections.Common;
		        using ComposableCollections.Dictionary;
		        using ComposableCollections.Dictionary.Adapters;
		        using ComposableCollections.Dictionary.Decorators;
		        using ComposableCollections.Dictionary.Interfaces;
		        using ComposableCollections.Dictionary.Sources;
		        using ComposableCollections.Dictionary.Transactional;
		        using ComposableCollections.Dictionary.WithBuiltInKey;
		        using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
		        using UtilityDisposables;

			        namespace ComposableCollections
		        {
        public static partial class DictionaryExtensions
        {");
		        GenerateWithReadWriteLockExtensionMethods(streamWriter);
		        GenerateWithDefaultValueExtensionMethods(streamWriter);
		        GenerateWithWriteCachingExtensionMethods(streamWriter);
		        GenerateWithRefreshingExtensionMethods(streamWriter);
		        GenerateWithMappingExtensionMethods(streamWriter);
		        streamWriter.WriteLine("}\n}");
	        }
        }

    }
}