using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using VectorCore;

namespace Collision2D_Tests
{

    [TestFixture]
public class EdgeVec2D_PointBelongsToEdge_Tests
{
    public static IEnumerable Vec2D_PointBelongsToLineSegment = new[] {
        new[]{new Vec2D(0,0), new Vec2D(2,2), new Vec2D(1,1)},
        new[]{new Vec2D(1,1), new Vec2D(-2,-2), new Vec2D(0,0)},
        new[]{new Vec2D(1,0), new Vec2D(3, 0), new Vec2D(2,0)},
        new[]{new Vec2D(1,0), new Vec2D(3, 0), new Vec2D(1,0)},
        new[]{new Vec2D(1,2), new Vec2D(3, 0), new Vec2D(2,1)},
        new[]{new Vec2D(-10,0), new Vec2D(0, 10), new Vec2D(-2,8)},
        new[]{new Vec2D(-4,0), new Vec2D(0, 16), new Vec2D(-2,8)},
        };

    [TestCaseSource("Vec2D_PointBelongsToLineSegment")]
    public void Vec2D_PointBelongsToEdge_Test(Vec2D lineStart, Vec2D lineEnd, Vec2D point)
    {
            Vec2D projected = EdgeVec2D.ProjectPointOnEdge(lineStart, lineEnd, point);
            TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
            Assert.True(EdgeVec2D.PointBelongsToEdge(lineStart, lineEnd, point));

            EdgeVec2D lsv3 = new EdgeVec2D(lineStart, lineEnd);
            Assert.True(lsv3.PointBelongsToEdge(point));
            Assert.AreEqual(projected, lsv3.ProjectPointOnEdge(point));

        }

    public static IEnumerable Vec2D_PointNOTBelongsToLineSegment = new[] {
        new[]{new Vec2D(0,0), new Vec2D(2,2), new Vec2D(1,1.001d)},
        new[]{new Vec2D(1,1), new Vec2D(-2.2d,-2), new Vec2D(0,0)},
        new[]{new Vec2D(1,0), new Vec2D(3, 0), new Vec2D(2,0.01d)},
        new[]{new Vec2D(1,0), new Vec2D(3, 0), new Vec2D(3.00001d,0)},
        new[]{new Vec2D(1,0), new Vec2D(3, 0), new Vec2D(0.99999d,0)},
        new[]{new Vec2D(1,2), new Vec2D(3, 0), new Vec2D(2.001d,1)},
        };
    [TestCaseSource("Vec2D_PointNOTBelongsToLineSegment")]
    public void Vec2D_PointNOTBelongsToEdge_Test(Vec2D lineStart, Vec2D lineEnd, Vec2D point)
    {
            Vec2D projected = EdgeVec2D.ProjectPointOnEdge(lineStart, lineEnd, point);
            TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
            Assert.False(EdgeVec2D.PointBelongsToEdge(lineStart, lineEnd, point), new EdgeVec2D(lineStart,lineEnd).PositionOnEdge(point).ToString());

            EdgeVec2D lsv3 = new EdgeVec2D(lineStart, lineEnd);
            Assert.False(lsv3.PointBelongsToEdge(point));
            Assert.AreEqual(projected, lsv3.ProjectPointOnEdge(point));
        }
}
}