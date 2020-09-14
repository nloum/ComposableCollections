using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
    public class WithWriteCachingGenerator : IGenerator<WithWriteCachingGeneratorSettings>
    {
	    private WithWriteCachingGeneratorSettings _settings;

	    public void Initialize(WithWriteCachingGeneratorSettings settings)
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
		    textWriter.WriteLine("#region WithWriteCaching");
		    
		    foreach (var iface in _settings.Interfaces)
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
		    textWriter.WriteLine("}\n}");

		    var results = ImmutableDictionary<string, string>.Empty.Add("WithWriteCachingExtensions.g.cs", textWriter.ToString());
		    return results;
	    }
	    
    }
}