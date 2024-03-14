/*using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DoubleEngine;
using NUnit.Framework;

[TestFixture]
public class SliceTests
{
    public const int N = 100;
    public static Random r;
    public static int[][] randomsArray = new int[N][];
    public static int[] emptyArray = new int[0];
    private static IEnumerable vec3DArrays = new object[] {
        new Vec3D[0],
        new TestCaseData(new Vec3D[1] { Vec3D.up, }),
        new[] { Vec3D.up, Vec3D.left },
        new[] { new Vec3D(0, 0, 90d), new Vec3D(0, 0, 0) },
    };
    static SliceTests()
    {
        const int MaxLength = 1000;
        const int MaxItemValue = 500000;
        r = new Random(42);
        for (int i = 0; i < N; i++)
        {
            int randArrLen = r.Next(0, MaxLength);
            if (randArrLen == 1)
                randArrLen = 2; // fix for NUnit bug with 1 element array conversion: System.ArgumentException : Object of type 'System.Int32' cannot be converted to type 'System.Int32[]'.
            int[] randArr = randomsArray[i] = new int[randArrLen];
            for (int ra= 0; ra< randArr.Length; ra++)
                randArr[ra]= r.Next(0, MaxItemValue);
        }
    }
    public string Debug_SpanToString<T>(Span<T> a)
    {
        List<string> temp = new();
        for (int i = 0; i < a.Length; i++)
            temp.Add(a[i].ToString());
        return String.Join(", ", temp);
    }
    public void AssertSpansAreEqual<T>(Span<T> a, Span<T> b)
        {
        Assert.AreEqual(a.Length, b.Length);
        for(int i = 0; i < a.Length; i++)
            Assert.AreEqual(a[i], b[i]);
        }


    [TestCaseSource("vec3DArrays")]
    public void vec3DArrays_SimpleTest(Vec3D[] arr)
    {
        Slice s = new Slice(0, arr.Length);
        AssertSpansAreEqual<Vec3D>(s.GetSpan(arr), arr.AsSpan());
    }

    [Test, TestCaseSource("randomsArray")]
    [TestCaseSource("emptyArray")]
    public void intArrays_SimpleTest(int[] arr)
    {
        Slice s = new Slice(0, arr.Length);
        Span<int> sliceSpan = s.GetSpan(arr);
        Span<int> pureSpan = arr.AsSpan();
        AssertSpansAreEqual<int>(sliceSpan, pureSpan);
        Memory<int> memory = arr.AsMemory<int>();
        AssertSpansAreEqual<int>(sliceSpan, memory.Span);
    }

    [Test, TestCaseSource("randomsArray")]
    [TestCaseSource("emptyArray")]
    public void SliceIntArrays_SimpleTest(int[] arr)
    {
        Slice s = new Slice(0, arr.Length);
        Span<int> sliceSpan = s.GetSpan(arr);
        Span<int> pureSpan = arr.AsSpan();
        AssertSpansAreEqual<int>(sliceSpan, pureSpan);
        Memory<int> memory = arr.AsMemory<int>();
        AssertSpansAreEqual<int>(sliceSpan, memory.Span);
        int maxIterations = 8;
        int i = 0;
        while (pureSpan.Length > 0 && i<maxIterations)
        {
            int newLength = r.Next(0, pureSpan.Length);
            int newStart = r.Next(0, pureSpan.Length - newLength);
            TestContext.WriteLine($"{newLength} {newStart}");
            s = s.SliceThis(newStart, newLength);
            sliceSpan = s.GetSpan(arr);
            pureSpan = pureSpan.Slice(newStart, newLength);
            AssertSpansAreEqual<int>(sliceSpan, pureSpan);
            memory = memory.Slice(newStart, newLength);
            AssertSpansAreEqual<int>(sliceSpan, memory.Span);
            i++;
        }
        TestContext.WriteLine(Debug_SpanToString(pureSpan));
    }

}
*/