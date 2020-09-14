using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
    public class WithDefaultValueGenerator : IGenerator<WithDefaultValueGeneratorSettings>
    {
	    private WithDefaultValueGeneratorSettings _settings;

	    public void Initialize(WithDefaultValueGeneratorSettings settings)
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
		    textWriter.WriteLine("#region WithDefaultValue that always returns a value");
		    
		    foreach (var iface in _settings.Interfaces)
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
		    
		    foreach (var iface in _settings.Interfaces)
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
		    
		    foreach (var iface in _settings.Interfaces)
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
		    textWriter.WriteLine("}\n}");

		    var results = ImmutableDictionary<string, string>.Empty.Add("WithMappingExtensions.g.cs", textWriter.ToString());
		    return results;
	    }
    }
}