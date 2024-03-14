using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using DoubleEngine;
using System.Collections.Immutable;
using VectorCore;

public class ImmutableArrayTests
{
    public static Vec2D[][] polys = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(3,5), new Vec2D(5,1) },
            new Vec2D[]{ new Vec2D(1,1), new Vec2D(2,-2), new Vec2D(1, -1), new Vec2D(-1,-3),  new Vec2D(-0.7,1)},
            new Vec2D[]{ new Vec2D(0,3),new Vec2D(0.5, 1.5),new Vec2D(3,1.5),new Vec2D(1,1),new Vec2D(2,-1),new Vec2D(0,0),new Vec2D(-2,-1),new Vec2D(-1,1),new Vec2D(-3,1.5),new Vec2D(-0.5,1.5)},
            new Vec2D[]{ new Vec2D(0,2), new Vec2D(2, 0), new Vec2D(-2,-2) },
            new Vec2D[]{ new Vec2D(-1,2), new Vec2D(3, 5), new Vec2D(0,-7) },
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1,1), new Vec2D(1, 1), new Vec2D(1,-1)},
        };

    public static int[][] ints = new int[][] {
            new int[]{ 0, 3,5,1 },
            new int[]{ 11,2,-20,10, -15, -1,-3,-8,1},
            new int[]{ 0,3,5, 15,3,47,19,72,-10,900},
        };
    [TestCaseSource("ints")]
    public void ImmutableIntFromArray(int[] arr)
    {
        ImmutableArray<int> immArr = arr.ToImmutableArray();
        Assert.AreEqual(arr, immArr);
    }
    public static IEnumerable Ints_List => ints.Select(x => new List<int>(x));
    [TestCaseSource("Ints_List")]
    public void ImmutableIntFromList(List<int> listArr)
    {
        ImmutableArray<int> immArr = listArr.ToImmutableArray();
        Assert.AreEqual(listArr, immArr);
    }

    int Mutiply(int what, int by) => what*by;
    [TestCaseSource("ints")]
    public void ImmutableInt_CreateRange_MultiplyFuction(int[] arr)
    {
        ImmutableArray<int> immArr = arr.ToImmutableArray();
        ImmutableArray<int> immResult = ImmutableArray.CreateRange(immArr,Mutiply,3);
        int[] arr2 = new int[arr.Length];
        for(int i = 0; i < arr.Length; i++)
            arr2[i] = arr[i]*3;
        Assert.AreEqual(arr2, immResult);
    }
}
