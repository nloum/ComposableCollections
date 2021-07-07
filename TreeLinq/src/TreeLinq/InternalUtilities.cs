using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TreeLinq
{
    public static class InternalUtilities
    {
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
    }
}