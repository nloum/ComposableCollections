using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TreeLinq
{
	public class Path<TNodeName> : IReadOnlyList<TNodeName>, IComparable<Path<TNodeName>>, IComparable
		where TNodeName : IComparable {
		private readonly ImmutableList<TNodeName> _wrapped;

		public static Path<TNodeName> Empty { get; } = new Path<TNodeName>( ImmutableList<TNodeName>.Empty );

		public TNodeName Name => Count == 0 ? default( TNodeName ) : this[this.Count - 1];

		public bool IsRoot => Count == 0;

		public Path( TNodeName singleElement ) {
			_wrapped = ImmutableList<TNodeName>.Empty.Add( singleElement );
		}

		public Path( IEnumerable<TNodeName> wrapped ) {
			_wrapped = wrapped.ToImmutableList();
		}

		public Path( ImmutableList<TNodeName> wrapped ) {
			_wrapped = wrapped;
		}

		public static Path<TNodeName> operator +( Path<TNodeName> path1, Path<TNodeName> path2 ) {
			return new Path<TNodeName>( path1.Concat( path2 ) );
		}

		public static Path<TNodeName> operator +( Path<TNodeName> path1, TNodeName path2 ) {
			return new Path<TNodeName>( path1.Concat( new[] { path2 } ) );
		}

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
			for ( var i = 0; i < _wrapped.Count; i++ ) {
				yield return _wrapped[i];
			}
		}

		public IEnumerator<TNodeName> GetEnumerator() => Elements().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int Count => _wrapped.Count;

		public TNodeName this[int index] => _wrapped[index];

		public Path<TNodeName> SkipAncestors( int skipAncestors ) {
			return new Path<TNodeName>( _wrapped.Skip(skipAncestors) );
		}

		public Path<TNodeName> SkipDescendants( int skipDescendants ) {
			return new Path<TNodeName>( _wrapped.Take(Count - skipDescendants ) );
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