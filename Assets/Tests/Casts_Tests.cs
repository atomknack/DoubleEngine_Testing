using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using DoubleEngine;
using DoubleEngine.Atom;
using System.IO;
using Newtonsoft.Json;
using FluentAssertions_Extensions;

public class Casts_Tests
{
    public static int[][] ints = new int[][] {
            new int[] { 1, 2, 3 },
            new int[]{ 0,1,2,3,4,5,6,7,8 },
        };
    public static IndexedTri[][] indexedTris = new IndexedTri[][] {
        new IndexedTri[] {new IndexedTri(1,2,3)},
        new IndexedTri[] {new IndexedTri(0,1,2), new IndexedTri(3, 4, 5), new IndexedTri(6,7, 8)},
    };
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    public static void CastFromIntToIndexedTris_Test(int intsIndex, int indexedTrisIndex)
    {
        ReadOnlySpan<IndexedTri> expected = indexedTris[indexedTrisIndex].AsSpan();
        ReadOnlySpan<IndexedTri> actual = ints[intsIndex].AsSpan().CastToIndexedTri_ReadOnlySpan();
        actual.ShouldEqual(expected);
    }
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    public static void CastFromIndexedTrisToInt_Test(int indexedTrisIndex, int intsIndex)
    {
        ReadOnlySpan<int> expected = ints[intsIndex].AsSpan();
        ReadOnlySpan<int> actual = indexedTris[indexedTrisIndex].AsSpan().CastToInt_ReadOnlySpan();
        actual.ShouldEqual(expected);
    }

    public static int[][] throwsArgumentInts = new int[][] {
            new int[] { 1, 2, 3 , 4 },
        };
    [TestCaseSource("throwsArgumentInts")]
    public static void CastThrowsArgumentException(int[] values)
    {
        Assert.Throws<ArgumentException>(() => values.AsSpan().CastToIndexedTri_ReadOnlySpan());
    }
}
