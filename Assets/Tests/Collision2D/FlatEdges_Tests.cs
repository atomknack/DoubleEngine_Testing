using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using DoubleEngine.UHelpers;
using VectorCore;

namespace AtomTests
{
[TestFixture]
public class Edges_Tests
{
    public static Vector2[] poly = new[] { 
        new Vector2(-1, -1), //0
        new Vector2(-1,  1), //1
        new Vector2( 0,  2), //2
        new Vector2( 1,  1), //3
        new Vector2( 1, -1), //4
        new Vector2( 0,  0), //5
        new Vector2( 0,  6), //6
        new Vector2( 5, -1), //7
        new Vector2( -95, -1), //8
    };
    
    public static IEnumerable IndexedEdgesVectorOnSideCases = new[] {
        new dynamic[]{new EdgeIndexed(0, 3), new Vector2( -0.5f,  2), 5f },
        new dynamic[]{new EdgeIndexed(0, 3), new Vector2( -1f,  2), 6f },
        new dynamic[]{new EdgeIndexed(0, 1), new Vector2( -0.5f,  2), -1f },
        new dynamic[]{new EdgeIndexed(5, 2), new Vector2( -0.5f,  2), 1f },
        new dynamic[]{new EdgeIndexed(0, 3), new Vector2( 2,  2), 0f },
        new dynamic[]{new EdgeIndexed(0, 3), new Vector2( 3,  3), 0f },
        new dynamic[]{new EdgeIndexed(5, 6), new Vector2( -1,  1), 6f },
        new dynamic[]{new EdgeIndexed(6, 5), new Vector2( -1,  1), -6f },
        new dynamic[]{new EdgeIndexed(5, 6), new Vector2( -3,  1), 18f },
        new dynamic[]{new EdgeIndexed(6, 5), new Vector2( -3,  1), -18f },
        new dynamic[]{new EdgeIndexed(3, 6), new Vector2( 0,  0), 6f },
        new dynamic[]{new EdgeIndexed(0, 4), new Vector2( 0,  0), 2f },
        new dynamic[]{new EdgeIndexed(4, 7), new Vector2( 0,  0), 4f },
        new dynamic[]{new EdgeIndexed(4, 7), new Vector2( -9,  -0.9f), 0.400000095f },
        new dynamic[]{new EdgeIndexed(4, 7), new Vector2( -9,  -1), 0f },
        new dynamic[]{new EdgeIndexed(8, 7), new Vector2( 0,  0), 100f },
        new dynamic[]{new EdgeIndexed(8, 7), new Vector2( -8,  -1), 0f },
        };

    // RelationToEdge bigger than zero if point is to the left along the way of edge (from start to end)
    [TestCaseSource("IndexedEdgesVectorOnSideCases")]
    public void RelationToEdge(EdgeIndexed edge, Vector2 point, float side) 
    {
            Vec2D[] polyVec2D = poly.ToArrayVec2D();
            Vector2 polyStart = poly[edge.start];
            Vector2 polyEnd = poly[edge.end];
            Vec2D polyStartVec2D = polyStart.ToVec2D();
            Vec2D polyEndVec2D = polyEnd.ToVec2D();
            Vec2D pointVec2D = point.ToVec2D();
            TestContext.WriteLine($"edge: {polyStart}, {polyEnd}, point: {point}, side: {side}");
            Assert.AreEqual(edge.RelationToEdge(polyVec2D, pointVec2D), side);
            TestContext.WriteLine($"edge - point: {poly[edge.start] - point}, {poly[edge.end] - point}");
            Assert.AreEqual(Vec2D.Cross(polyStartVec2D - pointVec2D, polyEndVec2D - pointVec2D), edge.RelationToEdge(polyVec2D, pointVec2D));

            EdgeVec2D edgeVec2D = new EdgeVec2D(poly[edge.start].ToVec2D(), poly[edge.end].ToVec2D());
            Assert.AreEqual(side, EdgeVec2D.Relation(edgeVec2D, pointVec2D));
            Assert.AreEqual(EdgeVec2D.Relation(edgeVec2D, pointVec2D), Vec2D.Cross(edgeVec2D.start - pointVec2D, edgeVec2D.end - pointVec2D));
    }

}
}