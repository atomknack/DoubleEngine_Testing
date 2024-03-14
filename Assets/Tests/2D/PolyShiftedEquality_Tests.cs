using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Collections.Pooled;
using UnityEngine;
using DoubleEngine;

namespace AtomTests
{
    public partial class Poly_Tests
    {
        public static int[][] polys1 = new int[][] {
            new int[]{ 0, 1, 2 },//0
            new int[]{3,4, 8, 2, 0}, //1
            new int[]{5,4, 8, 2, 0}, //2
        };

        public static int[][] polys2 = new int[][] {
            new int[]{ 0, 1, 2 }, //0
            new int[]{ 2, 0, 1 }, //1
            new int[]{ 2, 1, 0 }, //2
            new int[]{ 0, 3, 4, 8, 2}, //3
            new int[]{ 5,4, 8, 2, -19}, //4

        };

        [TestCase(0,0)]
        [TestCase(0,1)]
        [TestCase(1,3)]
        public void PolysShiftedEqual(int poly1Index, int poly2Index)
        {
            Assert.IsTrue(InternalTesting.Polys.PolyShiftedEqual<int>(polys1[poly1Index].AsSpan(), polys2[poly2Index].AsSpan()));
        }

        [TestCase(0, 2)]
        [TestCase(0, 3)]
        [TestCase(2, 3)]
        [TestCase(2, 4)]
        public void PolysShiftedNotEqual(int poly1Index, int poly2Index)
        {
            Assert.IsFalse(InternalTesting.Polys.PolyShiftedEqual<int>(polys1[poly1Index].AsSpan(), polys2[poly2Index].AsSpan()));
        }
    }
}