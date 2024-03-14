using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using DoubleEngine;
using VectorCore;

namespace AtomTests.Meshes
{
    public class Clockwise2D_Tests 
    {
        public static TriVec2D[] triVec2D_Clockwise = new TriVec2D[] {
            new TriVec2D(new Vec2D(0,0), new Vec2D(3,5), new Vec2D(5,1)),
            new TriVec2D(new Vec2D(1,1), new Vec2D(2,-2), new Vec2D(-1,-1)),
            new TriVec2D(new Vec2D(3,-2), new Vec2D(-3, -3), new Vec2D(-4,5)),
            new TriVec2D(new Vec2D(0,2), new Vec2D(2, 0), new Vec2D(-2,-2)),
            new TriVec2D(new Vec2D(-1,2), new Vec2D(3, 5), new Vec2D(0,-7)),
        };

        [TestCaseSource("triVec2D_Clockwise")]
        public void TriVec2D_Clockwise(TriVec2D tri)
        {
            Assert.IsTrue(tri.IsTriangleClockwise());
            Vec2D[] poly = new Vec2D[] {tri.v0, tri.v1, tri.v2};
            Assert.IsTrue(PolyHelpers.IsPolyClockwise(new int[] {0,1,2},poly));
        }


        public static Vec2D[][] polys_Clockwise = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(3,5), new Vec2D(5,1) },
            new Vec2D[]{ new Vec2D(1,1), new Vec2D(2,-2), new Vec2D(1, -1), new Vec2D(-1,-3),  new Vec2D(-0.7,1)},
            new Vec2D[]{new Vec2D(0,3),new Vec2D(0.5, 1.5),new Vec2D(3,1.5),new Vec2D(1,1),new Vec2D(2,-1),new Vec2D(0,0),new Vec2D(-2,-1),new Vec2D(-1,1),new Vec2D(-3,1.5),new Vec2D(-0.5,1.5)},
            new Vec2D[]{ new Vec2D(0,2), new Vec2D(2, 0), new Vec2D(-2,-2) },
            new Vec2D[]{ new Vec2D(-1,2), new Vec2D(3, 5), new Vec2D(0,-7) },
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1,1), new Vec2D(1, 1), new Vec2D(1,-1)},
        };

        [TestCaseSource("polys_Clockwise")]
        public void Poly_Clockwise(Vec2D[] poly)
        {
            Assert.IsTrue(PolyHelpers.IsPolyClockwise(Enumerable.Range(0, poly.Length).ToArray(), poly));
        }

        public static TriVec2D[] triVec2D_CounterClockwise = new TriVec2D[] {
            new TriVec2D(new Vec2D(3,5), new Vec2D(0,0), new Vec2D(5,1)),
            new TriVec2D(new Vec2D(2,-2), new Vec2D(1,1), new Vec2D(-1,-1)),
            new TriVec2D(new Vec2D(-3, -3), new Vec2D(3,-2), new Vec2D(-4,5)),
            new TriVec2D(new Vec2D(2, 0), new Vec2D(0,2), new Vec2D(-2,-2)),
            new TriVec2D(new Vec2D(3, 5), new Vec2D(-1,2), new Vec2D(0,-7)),
            new TriVec2D(new Vec2D(0.42, -0.43), new Vec2D(0.41, 0.17), new Vec2D(0.12, -0.12)),
        };

        [TestCaseSource("triVec2D_CounterClockwise")]
        public void TriVec2D_CounterClockwise(TriVec2D tri)
        {
            Assert.IsFalse(tri.IsTriangleClockwise());
            Assert.IsTrue(TriVec2D.IsTriangleCounterClockwise(tri.v0, tri.v1, tri.v2));
            Vec2D[] poly = new Vec2D[] { tri.v0, tri.v1, tri.v2 };
            Assert.IsFalse(PolyHelpers.IsPolyClockwise(new int[] { 0, 1, 2 }, poly));
        }

        public static Vec2D[][] polys_CounterClockwise = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(-0.7,1), new Vec2D(-1, -3), new Vec2D(1, -1), new Vec2D(2, -2), new Vec2D(1,1),},
        };

        [TestCaseSource("polys_CounterClockwise")]
        public void Poly_CounterClockwise(Vec2D[] poly)
        {
            Assert.IsFalse(PolyHelpers.IsPolyClockwise(Enumerable.Range(0, poly.Length).ToArray(), poly));
        }
    }
}