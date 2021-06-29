using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TreeLinq
{
	public abstract class Path<TNodeName> : IReadOnlyList<TNodeName>, IComparable<Path<TNodeName>>, IComparable
		where TNodeName : IComparable {
		public ImmutableList<TNodeName> Components { get; }
		
		public TNodeName Name => Count == 0 ? default( TNodeName ) : this[this.Count - 1];

		public bool IsRoot => Count == 0;

		protected Path( params TNodeName[] components ) {
			Components = ImmutableList<TNodeName>.Empty.AddRange( components );
		}

		protected Path( IEnumerable<TNodeName> components ) {
			Components = components.ToImmutableList();
		}

		public Path( ImmutableList<TNodeName> components ) {
			Components = components;
		}

		public abstract bool IsAbsolute { get; }
		public abstract bool IsRelative { get; }
		
		public static bool operator ==( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			if ( object.ReferenceEquals(path1, path2) ) {
				return true;
			}

			if ( object.ReferenceEquals(null, path1) || object.ReferenceEquals( null, path2 ) ) {
				return false;
			}

			return path1.Equals( path2 );
		}

		public static bool operator !=( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			return !(path1 == path2);
		}

		public static bool operator >( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			return path1.CompareTo( path2 ) > 0;
		}

		public static bool operator <( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			return path1.CompareTo( path2 ) < 0;
		}

		public static bool operator >=( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			return path1.CompareTo( path2 ) >= 0;
		}

		public static bool operator <=( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			return path1.CompareTo( path2 ) <= 0;
		}

		private IEnumerable<TNodeName> Elements() {
			for ( var i = 0; i < Components.Count; i++ ) {
				yield return Components[i];
			}
		}

		public IEnumerator<TNodeName> GetEnumerator() => Elements().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int Count => Components.Count;

		public TNodeName this[int index] => Components[index];

		public RelativeTreePath<TNodeName> SkipAncestors( int skipAncestors ) {
			return new RelativeTreePath<TNodeName>( Components.Skip(skipAncestors) );
		}

		public TNodeName Last => this[this.Count - 1];

		public bool StartsWith( Path<TNodeName> other ) {
			if ( Count < other.Count ) {
				return false;
			}

			for ( var i = 0; i < other.Count; i++ ) {
				if ( !this[i].Equals( other[i] ) ) {
					return false;
				}
			}

			return true;
		}

		public bool Equals( Path<TNodeName> other ) {
			if ( Count != other.Count ) {
				return false;
			}

			var matchingElements = this.Zip( other, ( myElement, otherElement ) => new { myElement, otherElement } );
			foreach ( var matchingElement in matchingElements ) {
				if ( !matchingElement.myElement.Equals( matchingElement.otherElement ) ) {
					return false;
				}
			}

			return true;
		}

		public int CompareTo( Path<TNodeName> other ) {
			var matchingElements = this.Zip( other, ( myElement, otherElement ) => new { myElement, otherElement } );
			foreach ( var matchingElement in matchingElements ) {
				var comparisonResult = matchingElement.myElement.CompareTo( matchingElement.otherElement );
				if ( comparisonResult != 0 ) {
					return comparisonResult;
				}
			}

			return 0;
		}

		public override bool Equals( object obj ) {
			if ( ReferenceEquals( null, obj ) ) {
				return false;
			}

			if ( ReferenceEquals( this, obj ) ) {
				return true;
			}

			if ( obj.GetType() != this.GetType() ) {
				return false;
			}

			return Equals( (Path<TNodeName>)obj );
		}

		public override int GetHashCode() {
			unchecked {
				int hash = 19;

				foreach ( var pathElement in this ) {
					hash = hash * 31 + pathElement.GetHashCode();
				}

				return hash;
			}
		}
		
		public override string ToString() {
			return string.Join( "/", this.Select( x => x.ToString() ) );
		}

		public int CompareTo( object obj ) {
			if ( obj is Path<TNodeName> path ) {
				return CompareTo( path );
			}

			return ToString().CompareTo( obj.ToString() );
		}
	}
}