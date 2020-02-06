using System;
using System.Collections.Generic;

using static GenericNumbers.NumbersUtility;

namespace GenericNumbers
{
    /// <summary>
    /// Methods that are meant to be used using the new C# 6.0 syntax: using static GenericNumbers.Utility;
    /// </summary>
    public static class NumbersUtility
    {
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
