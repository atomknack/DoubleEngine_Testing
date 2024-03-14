using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.UHelpers;
using FluentAssertions;
using VectorCore;

namespace AtomTests
{
public partial class Tests2d_Vec2D_tests
    {
    public static IEnumerable Vec2DArrays = new[] {
        new object[] { new[]{new Vec2D(0,1), new Vec2D(1,1), new Vec2D(0.5f,0.5f), new Vec2D(0.8f, 2), new Vec2D(-0.9f, 0.9f) }, 2 },
        new object[] { new[]{new Vec2D(0,0), new Vec2D(2,2), new Vec2D(2,0), new Vec2D(0, 2) }, 0 },
        new object[] { new[]{new Vec2D(-1,1), new Vec2D(2.9999999f, 2.9999999f), new Vec2D(-90, 3), new Vec2D(3, -90) }, 3 },
        new object[] { new[]{new Vec2D(-1,1), new Vec2D(3, -90), new Vec2D(2.9999999f, 2.9999999f), new Vec2D(3, 3), new Vec2D(3, -90) }, 1 },
        new object[] { new[]{new Vec2D(-1,1), new Vec2D(4, -90), new Vec2D(2.9999999f, 2.9999999f), new Vec2D(3,3), new Vec2D(3,-90)}, 4 },
        };

    [TestCaseSource("Vec2DArrays")]
    public void MinXWhereMinY_Tests(Vec2D[] vectors, int expected)
    {
        List<Vec2D> vectorsList = new List<Vec2D>(vectors);
        ReadOnlySpan<Vec2D> vectorsSpan = new ReadOnlySpan<Vec2D>(vectors);

        //int vectorsMinArrayIndex = vectors.MinXWhereMinY();
        vectorsList.MinXWhereMinY().Should().Be(expected);
        vectorsSpan.MinXWhereMinY().Should().Be(expected);
        //TestContext.WriteLine($"{vectorsMinListIndex} {vectorsMinSpanIndex}");
        //TestContext.WriteLine($"MinXWhereMinY {vectorsMinArrayIndex} of {vectors.ToArrayVec2D().ArrayToJsonString()}");
        //Assert.AreEqual(expected, vectorsMinListIndex);
        //Assert.AreEqual(expected, vectorsMinSpanIndex);


    }
}
}