using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Humanizer;

namespace ComposableCollections.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dictionaryInterfacePrefixes = new List<string>
            {
                "ReadOnly",
                "Cached",
                "Disposable",
                "Queryable",
            };
            dictionaryInterfacePrefixes.Sort();

            var dictionaryInterfacePostfixes = new List<string>
            {
                //"WithBuiltInKey",
            };
            dictionaryInterfacePostfixes.Sort();

            //var results = GenerateTypeNames("Dictionary", dictionaryInterfacePrefixes, dictionaryInterfacePostfixes);

            // foreach (var result in results)
            // {
            //     Console.WriteLine(result);
            // }
            
            GenerateCombinationInterfaces(dictionaryInterfacePrefixes, "Dictionary");
            //GenerateCombinationClasses(dictionaryInterfacePrefixes, "Dictionary");
        }

        private static void GenerateCombinationClasses(IEnumerable<string> baseInterfaces, string basicName)
        {
	        var combinationInterfaces =
		        CalcCombinationsOfOneFromEach(baseInterfaces.Select(x => new[] {x, string.Empty}));

	        foreach (var combinationInterface in combinationInterfaces)
	        {
		        var validCombinationInterface = combinationInterface.Where(x => !string.IsNullOrEmpty(x)).ToImmutableList();
		        if (validCombinationInterface.Count < 2)
		        {
			        continue;
		        }
		        
		        var className = $"{string.Join(string.Empty, combinationInterface)}{basicName}";
		        Console.WriteLine(
			        $"public class {className} : I{className}<TKey, TValue> {{ ");
		        foreach (var wrapped in validCombinationInterface)
		        {
			        Console.WriteLine($"private readonly I{wrapped}Dictionary<TKey, TValue> _{wrapped.Camelize()}Dictionary;");
		        }
		        Console.WriteLine($"public {className}({string.Join(", ", validCombinationInterface.Select(x => $"I{x}{basicName}<TKey, TValue> {x.Camelize()}Dictionary"))}) {{");
		        foreach (var wrapped in validCombinationInterface)
		        {
			        Console.WriteLine($"_{wrapped.Camelize()}Dictionary = {wrapped.Camelize()}Dictionary;");
		        }
		        Console.WriteLine("}\n}");
	        }
        }

        private static void GenerateCombinationInterfaces(IEnumerable<string> baseInterfaces, string basicName)
        {
	        var combinationInterfaces =
		        CalcCombinationsOfOneFromEach(baseInterfaces.Select(x => new[] {x, string.Empty}));

	        foreach (var combinationInterface in combinationInterfaces)
	        {
		        if (combinationInterface.Count(x => !string.IsNullOrEmpty(x)) == 0)
		        {
			        continue;
		        }
		        
		        var interfaceName = $"I{string.Join(string.Empty, combinationInterface)}{basicName}";
		        Console.WriteLine(
			        $"public interface {interfaceName}<TKey, TValue> : {string.Join(", ", combinationInterface.Select(x => string.IsNullOrEmpty(x) ? string.Empty : $"I{x}{basicName}<TKey, TValue>").Where(x => !string.IsNullOrEmpty(x)))} {{ }}");
	        }
        }

        private static IEnumerable<string> GenerateTypeNames(string basicName, IEnumerable<string> prefixes,
            IEnumerable<string> postfixes)
        {
	        var prefixCombinations =
		        CalcCombinationsOfOneFromEach(prefixes.Select(prefix => new[] {prefix, string.Empty}));
	        return prefixCombinations
		        .SelectMany(prefixes => CalcCombinationsOfOneFromEach(postfixes.Select(postfix => new []{postfix, string.Empty})).Select(postfixes => new { prefixes, postfixes}))
		        .Select(x => $"{string.Join(string.Empty, x.prefixes)}{basicName}{string.Join(string.Empty, x.postfixes)}");
        }
        
        /// <summary>
		/// If you pass in new[]{0, 1} and new[]{0, 1} you'll get new[]{0, 0}, new[]{0, 1}, new[]{1, 0}, new[]{1, 1} out.
		/// </summary>
		/// <typeparam name="TItem">The type of item in the enumerables.</typeparam>
		/// <param name="enumerables">The enumerables that will be used to calculate combinations</param>
		/// <returns>A lazily-computed enumerable where each return item is a combination</returns>
	    public static IEnumerable<ImmutableList<TItem>> CalcCombinationsOfOneFromEach<TItem>(
		    IEnumerable<IEnumerable<TItem>> enumerables ) {
		    var enumerators = enumerables.Select( enumerable => Tuple.Create( enumerable, enumerable.GetEnumerator() ) ).ToList();

		    foreach ( var enumerator in enumerators ) {
			    if ( !enumerator.Item2.MoveNext() ) {
				    throw new ArgumentException( "An empty enumerable was specified" );
			    }
			}

		    yield return enumerators.Select( t => t.Item2.Current ).ToImmutableList();

			while (true) {
				var i = 0;
				while(true) {
					if ( i == enumerators.Count ) {
						yield break;
					}

					if ( !enumerators[i].Item2.MoveNext() ) {
						enumerators[i] = Tuple.Create( enumerators[i].Item1,
							enumerators[i].Item1.GetEnumerator() );
						enumerators[i].Item2.MoveNext();
					} else {
						break;
					}

					i++;
				}

				yield return enumerators.Select( t => t.Item2.Current ).ToImmutableList();
			}
	    }

        public static IEnumerable<IReadOnlyList<T>> CalcCombination<T>(IReadOnlyList<T> list, int sizeOfCombination, int indexInList = 0)
        {
            for (var i = indexInList; i < list.Count; i++)
            {
                var item = list[i];
                var itemArray = new[] { item };
                if (sizeOfCombination == 1) {
	                yield return itemArray;
                }
                else
                {
                    foreach (var subsequentList in CalcCombination<T>(list, sizeOfCombination - 1, i + 1))
                    {
                        yield return itemArray.Concat(subsequentList).ToList();
                    }
                }
            }
        }
    }
}