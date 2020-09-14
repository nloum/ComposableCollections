using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
	public class WithMappingGenerator : IGenerator<WithMappingGeneratorSettings>
    {
	    private WithMappingGeneratorSettings _settings;

	    public void Initialize(WithMappingGeneratorSettings settings)
	    {
		    _settings = settings;
	    }

	    public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel)
	    {
		    var textWriter = new StringWriter();
		    
		    textWriter.WriteLine(@"using System;
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
		    textWriter.WriteLine("#region WithMapping - different key types");
		    
		    foreach (var iface in _settings.Interfaces)
		    {
			    var parameters = new List<string>();
			    if (iface.Contains("Queryable"))
			    {
				    parameters.Add("Expression<Func<TValue1, TValue2>> convertToValue2");
			    }
			    else
			    {
				    parameters.Add("Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2");
			    }
			    parameters.Add("Func<TKey1, TKey2> convertToKey2");
			    if (!iface.Contains("ReadOnly"))
			    {
				    parameters.Add("Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1");
			    }
			    parameters.Add("Func<TKey2, TKey1> convertToKey1");
			    
			    textWriter.WriteLine(
				    $"public static {iface}<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this {iface}<TKey1, TValue1> source, {string.Join(", ", parameters)}) {{");
			    if (iface.Contains("Queryable"))
			    {
				    textWriter.WriteLine("var convertToValue2Compiled = convertToValue2.Compile();");
			    }
			    if (iface.Contains("ReadOnly"))
			    {
				    if (iface.Contains("Queryable"))
				    {
					    textWriter.WriteLine("var mappedSource = new MappingReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToKey2, convertToKey1);");
				    }
				    else
				    {
					    textWriter.WriteLine("var mappedSource = new MappingReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToKey2, convertToKey1);");
				    }
			    }
			    else
			    {
				    if (iface.Contains("Queryable"))
				    {
					    textWriter.WriteLine(
						    "var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToValue1, convertToKey2, convertToKey1);");
				    } else {
					    textWriter.WriteLine(
						    "var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToValue1, convertToKey2, convertToKey1);");
				    }
			    }

			    if (iface.Contains("Cached"))
			    {
				    textWriter.WriteLine("var cachedMapSource = new ConcurrentCachedWriteDictionaryAdapter<TKey2, TValue2>(mappedSource);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("mappedSource");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("cachedMapSource.AsBypassCache");
				    arguments.Add("cachedMapSource.AsNeverFlush");
				    arguments.Add("() => {  cachedMapSource.FlushCache(); source.FlushCache(); }");
				    arguments.Add("cachedMapSource.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values.Select(convertToValue2)");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return mappedSource;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey2, TValue2>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }

 		    textWriter.WriteLine("#endregion\n");
		    textWriter.WriteLine("#region WithMapping - one key type");
		    
		    foreach (var iface in _settings.Interfaces)
		    {
			    var parameters = new List<string>();
			    var arguments = new List<string>();
			    if (iface.Contains("Queryable"))
			    {
				    parameters.Add("Expression<Func<TValue1, TValue2>> convertToValue2");
				    //arguments.Add("(key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2Compiled(key, value))");
				    arguments.Add("convertToValue2");
			    }
			    else
			    {
				    parameters.Add("Func<TKey, TValue1, TValue2> convertToValue2");
				    arguments.Add("(key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value))");
			    }

			    arguments.Add("x => x");

			    if (!iface.Contains("ReadOnly"))
			    {
				    parameters.Add("Func<TKey, TValue2, TValue1> convertToValue1");
				    arguments.Add("(key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value))");
			    }
			    
			    arguments.Add("x => x");

			    textWriter.WriteLine(
				    $"public static {iface}<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this {iface}<TKey, TValue1> source, {string.Join(", ", parameters)}) {{");
			    textWriter.WriteLine(
				    $"return source.WithMapping<TKey, TValue1, TKey, TValue2>({string.Join(", ", arguments)});");
			    textWriter.WriteLine("}");
		    }

		    textWriter.WriteLine("#endregion\n");
		    
		    textWriter.WriteLine("#region WithMapping - transactional different key types");
		    
		    foreach (var iface in _settings.Interfaces)
		    {
			    if (!iface.Contains("Disposable") || iface.Contains("ReadOnly"))
			    {
				    continue;
			    }
			    
			    var parameters = new List<string>();
			    var readOnlyArguments = new List<string>();
			    var readWriteArguments = new List<string>();
			    if (iface.Contains("Queryable"))
			    {
				    parameters.Add("Expression<Func<TValue1, TValue2>> convertToValue2");
			    }
			    else
			    {
				    parameters.Add("Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2");
			    }
			    readOnlyArguments.Add("convertToValue2");
			    readWriteArguments.Add("convertToValue2");

			    parameters.Add("Func<TKey1, TKey2> convertToKey2");
			    readOnlyArguments.Add("convertToKey2");
			    readWriteArguments.Add("convertToKey2");
			    
			    parameters.Add("Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1");
			    readWriteArguments.Add("convertToValue1");
			    
			    parameters.Add("Func<TKey2, TKey1> convertToKey1");
			    readOnlyArguments.Add("convertToKey1");
			    readWriteArguments.Add("convertToKey1");

			    var readOnlyInterface = iface.Replace("Dictionary", "ReadOnlyDictionary").Replace("Cached", "");
			    
			    textWriter.WriteLine(
				    $"public static IReadWriteFactory<{readOnlyInterface}<TKey2, TValue2>, {iface}<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadWriteFactory<{readOnlyInterface}<TKey1, TValue1>, {iface}<TKey1, TValue1>> source, {string.Join(", ", parameters)}) {{");

			    textWriter.WriteLine(
				    $"return new AnonymousReadWriteFactory<{readOnlyInterface}<TKey2, TValue2>, {iface}<TKey2, TValue2>>(");
			    textWriter.WriteLine(
				    $"() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>({string.Join(", ", readOnlyArguments)}),");
			    textWriter.WriteLine(
				    $"() => source.CreateWriter().WithMapping<TKey1, TValue1, TKey2, TValue2>({string.Join(", ", readWriteArguments)}));");
			    
			    textWriter.WriteLine("}");
		    }

		    textWriter.WriteLine("#endregion");
		    
		    textWriter.WriteLine("#region WithMapping - transactional same key type");
		    
		    foreach (var iface in _settings.Interfaces)
		    {
			    if (!iface.Contains("Disposable") || iface.Contains("ReadOnly"))
			    {
				    continue;
			    }
			    
			    var parameters = new List<string>();
			    var readOnlyArguments = new List<string>();
			    var readWriteArguments = new List<string>();
			    if (iface.Contains("Queryable"))
			    {
				    parameters.Add("Expression<Func<TValue1, TValue2>> convertToValue2");
			    }
			    else
			    {
				    parameters.Add("Func<TKey, TValue1, TValue2> convertToValue2");
			    }
			    readOnlyArguments.Add("convertToValue2");
			    readWriteArguments.Add("convertToValue2");

			    parameters.Add("Func<TKey, TValue2, TValue1> convertToValue1");
			    readWriteArguments.Add("convertToValue1");

			    var readOnlyInterface = iface.Replace("Dictionary", "ReadOnlyDictionary").Replace("Cached", "");

			    textWriter.WriteLine(
				    $"public static IReadWriteFactory<{readOnlyInterface}<TKey, TValue2>, {iface}<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadWriteFactory<{readOnlyInterface}<TKey, TValue1>, {iface}<TKey, TValue1>> source, {string.Join(", ", parameters)}) {{");

			    textWriter.WriteLine(
				    $"return new AnonymousReadWriteFactory<{readOnlyInterface}<TKey, TValue2>, {iface}<TKey, TValue2>>(");
			    textWriter.WriteLine(
				    $"() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>({string.Join(", ", readOnlyArguments)}),");
			    textWriter.WriteLine(
				    $"() => source.CreateWriter().WithMapping<TKey, TValue1, TValue2>({string.Join(", ", readWriteArguments)}));");
			    
			    textWriter.WriteLine("}");
		    }

		    textWriter.WriteLine("#endregion");
		    
		    textWriter.WriteLine("#region WithMapping - read-only transactional different key types");
		    
		    foreach (var iface in _settings.Interfaces)
		    {
			    if (!iface.Contains("Disposable") || iface.Contains("ReadOnly") || iface.Contains("Cached"))
			    {
				    continue;
			    }
			    
			    var parameters = new List<string>();
			    var readOnlyArguments = new List<string>();
			    var readWriteArguments = new List<string>();
			    if (iface.Contains("Queryable"))
			    {
				    parameters.Add("Expression<Func<TValue1, TValue2>> convertToValue2");
			    }
			    else
			    {
				    parameters.Add("Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2");
			    }
			    readOnlyArguments.Add("convertToValue2");
			    readWriteArguments.Add("convertToValue2");

			    parameters.Add("Func<TKey1, TKey2> convertToKey2");
			    readOnlyArguments.Add("convertToKey2");
			    readWriteArguments.Add("convertToKey2");
			    
			    parameters.Add("Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1");
			    readWriteArguments.Add("convertToValue1");
			    
			    parameters.Add("Func<TKey2, TKey1> convertToKey1");
			    readOnlyArguments.Add("convertToKey1");
			    readWriteArguments.Add("convertToKey1");

			    var readOnlyInterface = iface.Replace("Dictionary", "ReadOnlyDictionary").Replace("CachedWrite", "");
			    
			    textWriter.WriteLine(
				    $"public static IReadOnlyFactory<{readOnlyInterface}<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadOnlyFactory<{readOnlyInterface}<TKey1, TValue1>> source, {string.Join(", ", parameters)}) {{");

			    textWriter.WriteLine(
				    $"return new AnonymousReadOnlyFactory<{readOnlyInterface}<TKey2, TValue2>>(");
			    textWriter.WriteLine(
				    $"() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>({string.Join(", ", readOnlyArguments)}));");
			    
			    textWriter.WriteLine("}");
		    }

		    textWriter.WriteLine("#endregion");
		    
		    textWriter.WriteLine("#region WithMapping - read-only transactional same key type");
		    
		    foreach (var iface in _settings.Interfaces)
		    {
			    if (!iface.Contains("Disposable") || iface.Contains("ReadOnly") || iface.Contains("Cached"))
			    {
				    continue;
			    }
			    
			    var parameters = new List<string>();
			    var readOnlyArguments = new List<string>();
			    var readWriteArguments = new List<string>();
			    if (iface.Contains("Queryable"))
			    {
				    parameters.Add("Expression<Func<TValue1, TValue2>> convertToValue2");
			    }
			    else
			    {
				    parameters.Add("Func<TKey, TValue1, TValue2> convertToValue2");
			    }
			    readOnlyArguments.Add("convertToValue2");
			    readWriteArguments.Add("convertToValue2");

			    parameters.Add("Func<TKey, TValue2, TValue1> convertToValue1");
			    readWriteArguments.Add("convertToValue1");

			    var readOnlyInterface = iface.Replace("Dictionary", "ReadOnlyDictionary").Replace("Cached", "");

			    textWriter.WriteLine(
				    $"public static IReadOnlyFactory<{readOnlyInterface}<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadOnlyFactory<{readOnlyInterface}<TKey, TValue1>> source, {string.Join(", ", parameters)}) {{");

			    textWriter.WriteLine(
				    $"return new AnonymousReadOnlyFactory<{readOnlyInterface}<TKey, TValue2>>(");
			    textWriter.WriteLine(
				    $"() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>({string.Join(", ", readOnlyArguments)}));");
			    
			    textWriter.WriteLine("}");
		    }

		    textWriter.WriteLine("#endregion");
		    
		    textWriter.WriteLine("}\n}");

		    var results = ImmutableDictionary<string, string>.Empty.Add("WithMappingExtensions.g.cs", textWriter.ToString());
		    return results;
	    }
    }
}