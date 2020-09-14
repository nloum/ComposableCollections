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
	    private static void GenerateWithReadWriteLockExtensionMethods(TextWriter textWriter)
	    {
		    var interfaces = new List<string>
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
		    };

		    textWriter.WriteLine("#region WithReadWriteLock");
		    
		    foreach (var iface in interfaces)
		    {
			    if (iface.Contains("ReadOnly"))
			    {
				    continue;
			    }

			    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithReadWriteLock<TKey, TValue>(this {iface}<TKey, TValue> source) {{");
			    if (iface.Contains("Queryable"))
			    {
				    textWriter.WriteLine("var decorator = new ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>(source);");
			    }
			    else
			    {
				    textWriter.WriteLine("var decorator = new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("((IQueryableReadOnlyDictionary<TKey, TValue>)decorator).Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
	    }
	    
	    private static void GenerateWithWriteCachingExtensionMethods(TextWriter textWriter)
	    {
		    var interfaces = new List<string>
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
		    };

		    textWriter.WriteLine("#region WithWriteCaching");
		    
		    foreach (var iface in interfaces)
		    {
			    if (iface.Contains("Cached") || iface.Contains("ReadOnly"))
			    {
				    continue;
			    }

			    var cachedInterface = "ICached" + iface.Substring(1).Replace("Composable", "");
			    
			    textWriter.WriteLine($"public static {cachedInterface}<TKey, TValue> WithWriteCaching<TKey, TValue>(this {iface}<TKey, TValue> source) {{");
				textWriter.WriteLine("var adapter = new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);");
			    var arguments = new List<string>();

			    arguments.Add("adapter");
			    arguments.Add("adapter.AsBypassCache");
			    arguments.Add("adapter.AsNeverFlush");
			    arguments.Add("adapter.FlushCache");
			    arguments.Add("adapter.GetWrites");

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return adapter;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {cachedInterface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
	    }
	    
	    private static void GenerateWithDefaultValueExtensionMethods(TextWriter textWriter)
	    {
		    var interfaces = new List<string>
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
		    };

		    textWriter.WriteLine("#region WithDefaultValue that always returns a value");
		    
		    foreach (var iface in interfaces)
		    {
			    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithDefaultValue<TKey, TValue>(this {iface}<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {{");
			    if (iface.Contains("ReadOnly"))
			    {
				    textWriter.WriteLine("var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);");
			    }
			    else
			    {
				    textWriter.WriteLine("var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
		    
		    textWriter.WriteLine("#region WithDefaultValue");
		    
		    foreach (var iface in interfaces)
		    {
			    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithDefaultValue<TKey, TValue>(this {iface}<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {{");
			    if (iface.Contains("ReadOnly"))
			    {
				    textWriter.WriteLine("var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);");
			    }
			    else
			    {
				    textWriter.WriteLine("var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
		    
		    
		    textWriter.WriteLine("#region WithDefaultValue - optional persistence");
		    
		    foreach (var iface in interfaces)
		    {
			    if (iface.Contains("ReadOnly"))
			    {
				    continue;
			    }
			    else
			    {
				    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithDefaultValue<TKey, TValue>(this {iface}<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {{");
				    textWriter.WriteLine("var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
	    }
	    
	    private static void GenerateWithRefreshingExtensionMethods(TextWriter textWriter)
	    {
		    var interfaces = new List<string>
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
		    };

		    textWriter.WriteLine("#region WithRefreshing that always refreshes the value");
		    
		    foreach (var iface in interfaces)
		    {
			    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithRefreshing<TKey, TValue>(this {iface}<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {{");
			    if (iface.Contains("ReadOnly"))
			    {
				    textWriter.WriteLine("var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);");
			    }
			    else
			    {
				    textWriter.WriteLine("var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");

		    textWriter.WriteLine("#region WithRefreshing");
		    
		    foreach (var iface in interfaces)
		    {
			    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithRefreshing<TKey, TValue>(this {iface}<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {{");
			    if (iface.Contains("ReadOnly"))
			    {
				    textWriter.WriteLine("var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);");
			    }
			    else
			    {
				    textWriter.WriteLine("var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
		    
		    
		    textWriter.WriteLine("#region WithRefreshing - optional persistence");
		    
		    foreach (var iface in interfaces)
		    {
			    if (iface.Contains("ReadOnly"))
			    {
				    continue;
			    }
			    else
			    {
				    textWriter.WriteLine($"public static {iface}<TKey, TValue> WithRefreshing<TKey, TValue>(this {iface}<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {{");
				    textWriter.WriteLine("var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);");
			    }
			    var arguments = new List<string>();

			    arguments.Add("decorator");
			    if (iface.Contains("Cached"))
			    {
				    arguments.Add("source.AsBypassCache");
				    arguments.Add("source.AsNeverFlush");
				    arguments.Add("source.FlushCache");
				    arguments.Add("source.GetWrites");
			    }

			    if (iface.Contains("Disposable"))
			    {
				    arguments.Add("source");
			    }
			    if (iface.Contains("Queryable"))
			    {
				    arguments.Add("source.Values");
			    }

			    if (iface.Contains("Composable"))
			    {
				    textWriter.WriteLine($"    return decorator;");
			    }
			    else
			    {
				    textWriter.WriteLine($"    return new {iface.Substring(1)}Adapter<TKey, TValue>({string.Join(", ", arguments)});");
			    }

			    textWriter.WriteLine("}");
		    }
		    
		    textWriter.WriteLine("#endregion");
	    }

	    private static void GenerateWithMappingExtensionMethods(TextWriter textWriter)
	    {
		    var interfaces = new List<string>
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
		    };

		    var interfacesWithBuiltInKeys = interfaces.Select(x => $"{x}WithBuiltInKey").ToList();

		    textWriter.WriteLine("#region WithMapping - different key types");
		    
		    foreach (var iface in interfaces)
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
		    
		    foreach (var iface in interfaces)
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
		    
		    foreach (var iface in interfaces)
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
		    
		    foreach (var iface in interfaces)
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
		    
		    foreach (var iface in interfaces)
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
		    
		    foreach (var iface in interfaces)
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
	    }
	    
        static void Main(string[] args)
        {
	        var ioService = new IoService(new ReactiveProcessFactory());
	        var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").IsFolder());

	        // var withBuiltInKeyAdapterClasses = repoRoot / "src" / "ComposableCollections" / "Dictionary" /
	        //                                    "WithBuiltInKey" / "Adapters.g.cs";
	        // using (var streamWriter = withBuiltInKeyAdapterClasses.OpenWriter())
	        // {
		       //  GenerateWithBuiltInKeyAdapterClasses(streamWriter);
	        // }

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
	        
	        var autoGeneratedInterfacesFolder =
		        (repoRoot / "src" / "ComposableCollections" / "Dictionary" / "AutoGeneratedInterfaces");
	        foreach (var result in combinedInterfaces.Concat(anonymousImplementations).Concat(decoratorBases))
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

        private static void GenerateWithBuiltInKeyAdapterClasses(StreamWriter streamWriter)
        {
	        streamWriter.WriteLine("namespace ComposableCollections.Dictionary.WithBuiltInKey {");

	        var withBuiltInKeyInterfaces = new Dictionary<string, string>();
	        withBuiltInKeyInterfaces.Add("ICachedReadDictionary", "ICachedReadDictionary");
	        
	        streamWriter.WriteLine("}");
        }
    }
}