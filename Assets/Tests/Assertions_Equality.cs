using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using FluentAssertions;

namespace FluentAssertions_Extensions
{
    public static partial class Assertions
    {

        public static void AreEqual(ThreeDimensionalCell cell, ThreeDimensionalCell other)
        {
            Assert.AreEqual(cell.GetHashCode(), other.GetHashCode());
            Assert.AreEqual(cell.ToBytesArray(), other.ToBytesArray());
            Assert.True(cell == other);
            NUnit.Framework.Assert.AreEqual(cell, other);
        }
        public static void AreNotEqual(ThreeDimensionalCell cell, ThreeDimensionalCell other)
        {
            Assert.AreNotEqual(cell.GetHashCode(), other.GetHashCode());
            Assert.AreNotEqual(cell.ToBytesArray(), other.ToBytesArray());
            Assert.False(cell == other);
            NUnit.Framework.Assert.AreNotEqual(cell, other);
        }

        public static void AreEqual(IThreeDimensionalGrid expected, IThreeDimensionalGrid actual)
        {
            var dimensions = actual.GetDimensions();
            Assert.AreEqual(expected.GetDimensions(), dimensions);
            for (int xi = 0; xi < dimensions.x; ++xi)
                for (int yi = 0; yi < dimensions.y; ++yi)
                    for (int zi = 0; zi < dimensions.z; ++zi)
                        Assert.AreEqual(expected.GetCell(xi, yi, zi), actual.GetCell(xi, yi, zi));
        }
    }
}