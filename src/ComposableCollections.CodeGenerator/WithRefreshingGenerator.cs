using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
	public class WithRefreshingGenerator : IGenerator<WithRefreshingGeneratorSettings>
    {
	    private WithRefreshingGeneratorSettings _settings;

	    public void Initialize(WithRefreshingGeneratorSettings settings)
	    {
		    _settings = settings;
	    }

	    public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel)
	    {
		    var textWriter = new StringWriter();
		    
		    var partial = _settings.Partial ? "partial " : "";
		    textWriter.WriteLine($@"using System;
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

 			        namespace {_settings.Namespace}
 		        {{
         public static {partial}class {_settings.Class}
         {{");
		    textWriter.WriteLine("#region WithRefreshing that always refreshes the value");
		    
		    foreach (var iface in _settings.Interfaces)
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
		    
		    foreach (var iface in _settings.Interfaces)
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
		    
		    foreach (var iface in _settings.Interfaces)
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
		    textWriter.WriteLine("}\n}");

		    var results = ImmutableDictionary<string, string>.Empty.Add("WithRefreshingExtensions.g.cs", textWriter.ToString());
		    return results;
	    }
    }
}