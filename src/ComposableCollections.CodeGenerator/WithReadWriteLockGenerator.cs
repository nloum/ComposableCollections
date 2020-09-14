using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
    public class WithReadWriteLockGenerator : IGenerator<WithReadWriteLockGeneratorSettings>
    {
	    private WithReadWriteLockGeneratorSettings _settings;

	    public void Initialize(WithReadWriteLockGeneratorSettings settings)
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
		    textWriter.WriteLine("#region WithReadWriteLock");
		    
		    foreach (var iface in _settings.Interfaces)
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
		    textWriter.WriteLine("}\n}");

		    var results = ImmutableDictionary<string, string>.Empty.Add("WithMappingExtensions.g.cs", textWriter.ToString());
		    return results;
	    }
    }
}