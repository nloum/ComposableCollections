using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GenericNumbers.NumbersUtility;

namespace GenericNumbers
{
    /// <summary>
    /// Methods that are meant to be used using the new C# 6.0 syntax: using static GenericNumbers.Utility;
    /// </summary>
    public static class NumbersUtility
    {
        /// <summary>
        /// Converts a <see cref="Type"/> to its C# name. E.g., if you pass in typeof(<see cref="IEnumerable{int}"/>) this
        /// will return "IEnumerable<int>".
        /// This is useful mainly in exception error messages.
        /// </summary>
        /// <param name="type">The type to get a C# description of.</param>
        /// <returns>The string representation of a human-friendly type name</returns>
        internal static string ConvertToCSharpTypeName( Type type, bool includeNamespaces = false ) {
            if ( type == typeof(long) ) {
                return "long";
            }

            if ( type == typeof( int ) ) {
                return "int";
            }

            if ( type == typeof(short) ) {
                return "short";
            }

            if ( type == typeof( ulong ) ) {
                return "ulong";
            }

            if ( type == typeof( uint ) ) {
                return "uint";
            }

            if ( type == typeof( ushort ) ) {
                return "ushort";
            }

            if ( type == typeof( byte ) ) {
                return "byte";
            }

            if ( type == typeof( sbyte ) ) {
                return "sbyte";
            }

            if ( type == typeof( string ) ) {
                return "string";
            }

            if ( type == typeof( void ) ) {
                return "void";
            }

            var baseName = new StringBuilder();
            var typeName = includeNamespaces ? type.Namespace + "." + type.Name : type.Name;
            if ( type.Name.Contains( "`" ) ) {
                baseName.Append( typeName.Substring( 0, typeName.IndexOf( "`" ) ) );
            } else {
                baseName.Append( typeName );
            }

            if ( type.GenericTypeArguments.Any() ) {
                baseName.Append( "<" );
                for ( var i = 0; i < type.GenericTypeArguments.Length; i++) {
                    baseName.Append( ConvertToCSharpTypeName( type.GenericTypeArguments[i], includeNamespaces ) );
                    if ( i + 1 < type.GenericTypeArguments.Length ) {
                        baseName.Append( ", " );
                    }
                }

                baseName.Append(">");
            }

            return baseName.ToString();
        }
        
        /// <summary>
        /// Creates a number range
        /// </summary>
        public static INumberRange<T> Range<T>(T lowerBound, T upperBound, bool isLowerBoundStrict = false, bool isUpperBoundStrict = true)
        {
            return NumberRange<T>.Create(lowerBound, upperBound, isLowerBoundStrict, isUpperBoundStrict);
        }
    }

    public static class NumbersUtility<T>
    {
        static NumbersUtility()
        {
            try
            {
                Epsilon = 0.Convert().To<T>();
                Zero = 0.Convert().To<T>();
                One = 1.Convert().To<T>();
                NegativeOne = (-1).Convert().To<T>();
            }
            catch (Exception)
            {
            }
            NumbersUtility<double>.Epsilon = 0.0000001;
            NumbersUtility<float>.Epsilon = 0.0000001f;

            NumbersUtility<long>.IsWholeNumber = true;
            NumbersUtility<ulong>.IsWholeNumber = true;
            NumbersUtility<int>.IsWholeNumber = true;
            NumbersUtility<uint>.IsWholeNumber = true;
            NumbersUtility<short>.IsWholeNumber = true;
            NumbersUtility<ushort>.IsWholeNumber = true;
            NumbersUtility<byte>.IsWholeNumber = true;
            NumbersUtility<double>.Sine = Math.Sin;
            NumbersUtility<float>.Sine = f => (float)Math.Sin(f);
            NumbersUtility<double>.Cosine = Math.Cos;
            NumbersUtility<float>.Cosine = f => (float)Math.Cos(f);
            NumbersUtility<double>.Tangent = Math.Tan;
            NumbersUtility<float>.Tangent = f => (float)Math.Tan(f);

            NumbersUtility<double>.ArcSine = Math.Sinh;
            NumbersUtility<float>.ArcSine = f => (float)Math.Sinh(f);
            NumbersUtility<double>.ArcCosine = Math.Cosh;
            NumbersUtility<float>.ArcCosine = f => (float)Math.Cosh(f);
            NumbersUtility<double>.ArcTangent = Math.Tanh;
            NumbersUtility<float>.ArcTangent = f => (float)Math.Tanh(f);

            IsWholeNumber = true;
        }

        public static T Get(int i)
        {
            if (i == 0) return Zero;
            if (i == 1) return One;
            if (i == -1) return NegativeOne;
            return i.Convert().To<T>();
        }

        public static Func<T, T> Sine { get; private set; }
        public static Func<T, T> Cosine { get; private set; }
        public static Func<T, T> Tangent { get; private set; }
        public static Func<T, T> ArcSine { get; private set; }
        public static Func<T, T> ArcCosine { get; private set; }
        public static Func<T, T> ArcTangent { get; private set; }

        public static T Zero { get; }
        public static T One { get; }
        public static T NegativeOne { get; }

        public static bool IsWholeNumber { get; private set; } = false;

        public static T Epsilon { get; set; }

        public static int SturgesFormula(int n)
        {
            return (int)Math.Ceiling(Math.Log(n, 2) + 1);
        }

        public static IEnumerable<INumberRange<double>> Bin(double min, double max, int classIntervalCount)
        {
            double classSize = (max - min) / classIntervalCount;
            for (int i = 0; i < classIntervalCount; i++)
            {
                yield return Range(min + (i * classSize), min + ((i + 1) * classSize));
            }
        }
    }
}
