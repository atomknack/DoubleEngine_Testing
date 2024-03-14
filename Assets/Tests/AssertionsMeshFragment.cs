using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using System;
using System.Linq;
using VectorCore;

namespace FluentAssertions_Extensions
{
    public static partial class Assertions
    {
        public static void AssertNoTriangleOverlapPointThatNotItsCorner(ReadOnlySpan<Vec2D> vertices, List<IndexedTri> iTris)
        {
            foreach (var iTri in iTris)
            {
                for (var i = 0; i < iTris.Count; i++)
                {
                    var v0 = iTris[i].v0;
                    if (iTri.IsNotCorner(v0))
                        Assert.IsFalse(CollisionDiscrete2D.OverlapPoint(new TriVec2D(vertices, iTri), vertices[v0]));
                    var v1 = iTris[i].v1;
                    if (iTri.IsNotCorner(v1))
                        Assert.IsFalse(CollisionDiscrete2D.OverlapPoint(new TriVec2D(vertices, iTri), vertices[v1]));
                    var v2 = iTris[i].v2;
                    if (iTri.IsNotCorner(v2))
                        Assert.IsFalse(CollisionDiscrete2D.OverlapPoint(new TriVec2D(vertices, iTri), vertices[v2]));
                }

            }
        }

        //failing this test means some edges are intersecting or some triangles are invisible
        public static void AssertIfTrianglesHave2SameVerticesTheyHaveCommonOppositeEdge(List<IndexedTri> iTris)
        {
            for (int i = 0; i < iTris.Count; i++)
                for (int j = 0; j < iTris.Count; j++)
                {
                    var first = iTris[i];
                    var other = iTris[j];
                    if (first.NumberOfSameVertices(other) == 2)
                        Assert.IsTrue(first.HaveCommonOppositeEdge(other), $"first iTri:{first}, other iTri: {other}");
                }


        }

        //failing this test means some triangles are duplicates or double sided
        public static void AssertNoDifferentTrianglesHaveAllSameVertices(List<IndexedTri> iTris)
        {
            for (int i = 0; i < iTris.Count; i++)
                Assert.AreEqual(1, iTris.Count(tri => tri.NumberOfSameVertices(iTris[i]) == 3));
        }

        //failing this test means some triangles are not visible
        public static void AssertAllTrianglesClockwise(ReadOnlySpan<Vec2D> vertices, List<IndexedTri> iTris)
        {
            TriVec2D[] triangles = iTris.ToTriVec2DArray(vertices);
            for (int i = 0; i < triangles.Length; i++)
                Assert.IsTrue(triangles[i].IsTriangleClockwise(), $"Triangle n:{i} is Not Clockwise ({triangles[i]}, {iTris[i]}), vertices:{vertices.ToArray()}");
        }
        //failing this test means some triangles are not visible
        public static void AssertNoCounterClockwiseTriangles(ReadOnlySpan<Vec2D> vertices, List<IndexedTri> iTris)
        {
            TriVec2D[] triangles = iTris.ToTriVec2DArray(vertices);
            for (int i = 0; i < triangles.Length; i++)
                Assert.IsFalse(TriVec2D.IsTriangleCounterClockwise(triangles[i].v0, triangles[i].v1, triangles[i].v2), $"Triangle n:{i} is Counter Clockwise ({triangles[i]}, {iTris[i]}), vertices:{vertices.ToArray()}");
        }
        //failing this test means some triangles are not visible
        public static void AssertNoLineSizeTriangles(ReadOnlySpan<Vec2D> vertices, List<IndexedTri> iTris)
        {
            TriVec2D[] triangles = iTris.ToTriVec2DArray(vertices);
            for (int i = 0; i < triangles.Length; i++)
                Assert.IsTrue(
                    triangles[i].IsTriangleClockwise() ||
                    TriVec2D.IsTriangleCounterClockwise(triangles[i].v0, triangles[i].v1, triangles[i].v2)
                    , $"Triangle n:{i} is Counter Clockwise ({triangles[i]}, {iTris[i]}), vertices:{vertices.ToArray()}");
        }

    }
}