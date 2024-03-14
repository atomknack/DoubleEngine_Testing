using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using VectorCore;

namespace AtomTests
{
[TestFixture]
public partial class Tests2d_Vec2D_tests
    {
    public static IEnumerable Vec2D_LinesEdgesIntersect = new[] {
        new[]{new Vec2D(0,1), new Vec2D(1,1), new Vec2D(0.5f,0.5f), new Vec2D(0.8f,2)},
        new[]{new Vec2D(0,1), new Vec2D(10,1), new Vec2D(0.5f,0.5f), new Vec2D(0.8f,2)},
        new[]{new Vec2D(0,0), new Vec2D(2,2), new Vec2D(2,0), new Vec2D(0,2)},
        new[]{new Vec2D(-1,1), new Vec2D(2.9999999f, 2.9999999f), new Vec2D(3,3), new Vec2D(3,-90)},
        };

    [TestCaseSource("Vec2D_LinesEdgesIntersect")]
    public void Vec2D_EdgesIntersect_tests(Vec2D aStart, Vec2D aEnd, Vec2D otherEdgeStart, Vec2D otherEdgeEnd)
    {
        Assert.True(CollisionDiscrete2D.EdgesIntersectAndNotParallel_NoSameVerticeCheck(aStart, aEnd, otherEdgeStart, otherEdgeEnd, out var intesection));
        TestContext.WriteLine($"{intesection}");
    }

}
}