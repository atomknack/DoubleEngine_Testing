using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using DoubleEngine;
using DoubleEngine.Atom;
using VectorCore;

namespace Collision2D_Tests
{


[TestFixture]
public class CoincidentIntersector_Tests 
{
    public static IEnumerable Edge2D_Intersects = new[] {
        new object[]{new EdgeVec2D(new Vec2D(-1,-1), new Vec2D(1,1)), new EdgeVec2D(new Vec2D(-1, 1), new Vec2D(1, -1)), new Vec2D(0,0)},
        new object[]{new EdgeVec2D(new Vec2D(0,0), new Vec2D(100,100)), new EdgeVec2D(new Vec2D(100, 0), new Vec2D(0, 100)), new Vec2D(50,50)},
        new object[]{new EdgeVec2D(new Vec2D(5,17), new Vec2D(100,100)), new EdgeVec2D(new Vec2D(100, 29), new Vec2D(8, 100)), new Vec2D(56.85001043042900d, 62.300535428690600d) },
        new object[]{new EdgeVec2D(new Vec2D(0,10), new Vec2D(10,0)), new EdgeVec2D(new Vec2D(5, 2), new Vec2D(5, 6)), new Vec2D(5, 5) },
        };

    public static IEnumerable Edge2D_CrossAtVertice_Intersects = new[] {
        new object[]{new EdgeVec2D(new Vec2D(0,0), new Vec2D(25,25)), new EdgeVec2D(new Vec2D(25, 25), new Vec2D(100, 75)), new Vec2D(25,25)},
        };
    public static IEnumerable Edge2D_Coincident_TouchAtVertice_Intersects = new[] {
        new object[]{new EdgeVec2D(new Vec2D(0,0), new Vec2D(25,25)), new EdgeVec2D(new Vec2D(25, 25), new Vec2D(100, 100)), new Vec2D(25,25)},
        new object[]{new EdgeVec2D(new Vec2D(-1,-1), new Vec2D(-1,10)), new EdgeVec2D(new Vec2D(-1, 5), new Vec2D(5, 5)), new Vec2D(-1,5)},
        new object[]{new EdgeVec2D(new Vec2D(-1,-1), new Vec2D(-1,10)), new EdgeVec2D(new Vec2D(5, 5), new Vec2D(-1, 5)), new Vec2D(-1,5)},
        };


            [TestCaseSource("Edge2D_Intersects")]
    [TestCaseSource("Edge2D_CrossAtVertice_Intersects")]
    [TestCaseSource("Edge2D_Coincident_TouchAtVertice_Intersects")]
        public void LinesIntersects_SinglePoint(EdgeVec2D a, EdgeVec2D b, Vec2D intersection)
        {
            Vec2D foundIntersection;
            Vec2D? secondEndOfIntersection;
            //TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
            Assert.True(CollisionDiscrete2D.EdgeIntersection(a,b, out foundIntersection, out secondEndOfIntersection));
            Assert.AreEqual(intersection, foundIntersection,$"{intersection.ToString("F20")} : {foundIntersection.ToString("F20")}");
            Assert.IsNull(secondEndOfIntersection);

            Vec2D foundCoincidentIntersection;
            Vec2D? secondCoincidentEndOfIntersection;
            Assert.True(CoincidentIntersector2D.Intersection(a, b, out foundCoincidentIntersection, out secondCoincidentEndOfIntersection));
            Assert.AreEqual(intersection, foundCoincidentIntersection, $"{intersection.ToString("F20")} : {foundCoincidentIntersection.ToString("F20")}");
            Assert.IsNull(secondCoincidentEndOfIntersection);
        }


    public static IEnumerable<TestCaseData> Edge2D_Parallel_NotIntersect = new TestCaseData[] {
        new TestCaseData(new EdgeVec2D(new Vec2D(0,0), new Vec2D(0,100)), new EdgeVec2D(new Vec2D(100, 0), new Vec2D(100, 100))).SetName("Parallel {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(0,0), new Vec2D(0,100)), new EdgeVec2D(new Vec2D(0.1, 0), new Vec2D(0.1, 100))).SetName("Parallel {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(0,0), new Vec2D(0,100)), new EdgeVec2D(new Vec2D(0.00001, 0), new Vec2D(0.00001, 100))).SetName("Parallel {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(0,0), new Vec2D(0,100)), new EdgeVec2D(new Vec2D(0.0000001, 0), new Vec2D(0.000001, 100))).SetName("Parallel {a}"),
      //new TestCaseData(new EdgeVec2D(new Vec2D(0,0), new Vec2D(0,100)), new EdgeVec2D(new Vec2D(0.0000001, 0), new Vec2D(0.0000001, 100))).SetName("Parallel {c}{a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(-10,10), new Vec2D(0,0)), new EdgeVec2D(new Vec2D(-11, 10), new Vec2D(-2, -1))).SetName("Parallel {a}"),
        };
    public static IEnumerable<TestCaseData> Edge2D_LinesCross_Edges_NotIntersect = new TestCaseData[] {
        new TestCaseData(new EdgeVec2D(new Vec2D(50,50), new Vec2D(100,100)), new EdgeVec2D(new Vec2D(0, 25), new Vec2D(25, 0))).SetName("LinesCross {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(100,0), new Vec2D(0,100)), new EdgeVec2D(new Vec2D(51, 50), new Vec2D(100, 100))).SetName("LinesCross {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(10,20), new Vec2D(30,20)), new EdgeVec2D(new Vec2D(30.01, -1000), new Vec2D(30.01, 1000))).SetName("LinesCross {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(0,10), new Vec2D(10,0)), new EdgeVec2D(new Vec2D(5, 2), new Vec2D(5, 4.9999999))).SetName("LinesCross {a}"),
        };
    public static IEnumerable<TestCaseData> Edge2D_Coincident_NotOverlap_NotIntersect = new TestCaseData[] {
        new TestCaseData(new EdgeVec2D(new Vec2D(0,0), new Vec2D(25,25)), new EdgeVec2D(new Vec2D(26, 26), new Vec2D(100, 100))).SetName("NotOverlap {a}"),
        new TestCaseData(new EdgeVec2D(new Vec2D(-100,-100), new Vec2D(-1,-1)), new EdgeVec2D(new Vec2D(100, 100), new Vec2D(1, 1))).SetName("NotOverlap {a}"),
        };

        [TestCaseSource("Edge2D_Intersects")]
        [TestCaseSource("Edge2D_CrossAtVertice_Intersects")]
        public void EdgesNotParallel(EdgeVec2D a, EdgeVec2D b, Vec2D intersection) => EdgesNotParallel(a, b);
        
        [TestCaseSource("Edge2D_LinesCross_Edges_NotIntersect")]
        public void EdgesNotParallel(EdgeVec2D a, EdgeVec2D b)
        {
            Assert.IsFalse(CoincidentIntersector2D.EdgesParallel(a, b));
        }


        [TestCaseSource("Edge2D_Parallel_NotIntersect")]
    [TestCaseSource("Edge2D_LinesCross_Edges_NotIntersect")]
    [TestCaseSource("Edge2D_Coincident_NotOverlap_NotIntersect")]
    public void LinesNoIntersection(EdgeVec2D a, EdgeVec2D b)
    {
        Vec2D foundIntersection;
        Vec2D? secondEndOfIntersection;
        //TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
        Assert.False(CollisionDiscrete2D.EdgeIntersection(a, b, out foundIntersection, out secondEndOfIntersection), 
            $"{foundIntersection.ToString("F9")}; {secondEndOfIntersection.HasValue:F9}; {(secondEndOfIntersection ?? Vec2D.zero).ToString("F9")}");
        TestContext.WriteLine(foundIntersection.ToString());
        Assert.AreEqual(Vec2D.zero, foundIntersection);
        Assert.IsNull(secondEndOfIntersection);

        Vec2D foundCoincidentIntersection;
        Vec2D? secondCoincidentEndOfIntersection;
        Assert.False(CoincidentIntersector2D.Intersection(a, b, out foundCoincidentIntersection, out secondCoincidentEndOfIntersection));
        Assert.AreEqual(Vec2D.zero, foundCoincidentIntersection, $"{Vec2D.zero.ToString("F20")} : {foundCoincidentIntersection.ToString("F20")}");
        Assert.IsNull(secondCoincidentEndOfIntersection);
    }

    public static IEnumerable Edge2D_Coincident_Overlap_SameDirection_Intersects = new[] {
        new object[]{new EdgeVec2D(new Vec2D(0,0), new Vec2D(75,75)), new EdgeVec2D(new Vec2D(25, 25), new Vec2D(100, 100)), new Vec2D(25,25), new Vec2D(75,75)},
        new object[]{new EdgeVec2D(new Vec2D(-25,10), new Vec2D(-25,75)), new EdgeVec2D(new Vec2D(-25, 10), new Vec2D(-25, 25)), new Vec2D(-25,10), new Vec2D(-25,25)},
        };
    [TestCaseSource("Edge2D_Coincident_Overlap_SameDirection_Intersects")]

    public void LinesIntersects_SameDirection_CoincidentOverlap(EdgeVec2D a, EdgeVec2D b, Vec2D intersectionStart, Vec2D intersectionEnd)
    {
        //TestContext.WriteLine($"{a}, {b}, {intersectionStart}, {intersectionEnd}");
        Vec2D foundIntersection;
        Vec2D? secondEndOfIntersection;
        //TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
        Assert.True(CollisionDiscrete2D.EdgeIntersection(a, b, out foundIntersection, out secondEndOfIntersection));
        Assert.AreEqual(intersectionStart, foundIntersection, $"{intersectionStart.ToString("F20")} : {foundIntersection.ToString("F20")}");
        Assert.AreEqual(intersectionEnd, secondEndOfIntersection.Value, $"{intersectionEnd.ToString("F20")} : {secondEndOfIntersection.Value.ToString("F20")}");
        Assert.True(EdgeVec2D.SameDirection(a, b));
        Assert.True(EdgeVec2D.SameDirection(a, new EdgeVec2D(foundIntersection,secondEndOfIntersection.Value)));
    }
    public static IEnumerable Edge2D_Coincident_Overlap_OppositeDirection_Intersects = new[] {
        new object[]{new EdgeVec2D(new Vec2D(0,0), new Vec2D(75,75)), new EdgeVec2D(new Vec2D(100, 100), new Vec2D(25, 25)), new Vec2D(25,25), new Vec2D(75,75)},
        new object[]{new EdgeVec2D(new Vec2D(-25,75), new Vec2D(-25,10)), new EdgeVec2D(new Vec2D(-25, 10), new Vec2D(-25, 25)), new Vec2D(-25,25), new Vec2D(-25,10)},
        };
    [TestCaseSource("Edge2D_Coincident_Overlap_OppositeDirection_Intersects")]
    public void LinesIntersects_Opposite_CoincidentOverlap(EdgeVec2D a, EdgeVec2D b, Vec2D intersectionStart, Vec2D intersectionEnd)
    {
        Vec2D foundIntersection;
        Vec2D? secondEndOfIntersection;
        //TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
        Assert.True(CollisionDiscrete2D.EdgeIntersection(a, b, out foundIntersection, out secondEndOfIntersection));
        Assert.AreEqual(intersectionStart, foundIntersection, $"{intersectionStart.ToString("F20")} : {foundIntersection.ToString("F20")}");
        Assert.AreEqual(intersectionEnd, secondEndOfIntersection.Value, $"{intersectionEnd.ToString("F20")} : {secondEndOfIntersection.Value.ToString("F20")}");
        Assert.False(EdgeVec2D.SameDirection(a, b));
        Assert.True(EdgeVec2D.SameDirection(a, new EdgeVec2D(foundIntersection, secondEndOfIntersection.Value)));
    }

}

}