using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using DoubleEngine;
using CollectionLike;
using CollectionLike.Enumerables;
using Newtonsoft.Json;
using VectorCore;

namespace AtomTests.Meshes
{
    public class SubtractTests
    {
        public static Vec2D[][] polys = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1,1), new Vec2D(1, 1), new Vec2D(1,-1)}, //0
            new Vec2D[]{ new Vec2D(-1,-2), new Vec2D(-1,-1), new Vec2D(1, -1), new Vec2D(1,-2)}, //1
            new Vec2D[]{ new Vec2D(-0.8,-1), new Vec2D(-1,1), new Vec2D(1, 1), new Vec2D(0.8, -1)}, //2
            new Vec2D[]{ new Vec2D(-0.8,-1), new Vec2D(0.8, -1), new Vec2D(0.8, -2), new Vec2D(-0.8, -2)}, //3
            new Vec2D[]{ new Vec2D(0.8, -1), new Vec2D(-0.8,-1), new Vec2D(-0.8, 0.8), new Vec2D(0.8, 0.8) }, //4
        };

        [TestCase(0, 2, 6)]
        [TestCase(0, 3, 12)]
        [TestCase(0, 4, 18)]
        public void Subtact_Tests(int polyIndexBase, int polyIndexSubtract, int trianglesArrayLength)
        {
            //List<IndexedTri> triangulated = TriangulatePoly(polys[polyIndex], expectedNumberOfTriangles);
            MeshFragmentVec2D polyBase = MeshFragmentVec2D.FromPolyCopy(polys[polyIndexBase]);
            TestContext.WriteLine($"{polyBase.Vertices.Length} {polyBase.Triangles.Length}");
            MeshFragmentVec2D polySubtract = MeshFragmentVec2D.FromPolyCopy(polys[polyIndexSubtract].CyclicLeftShift().ToArray());
            TestContext.WriteLine($"{polySubtract.Vertices.Length} {polySubtract.Triangles.Length}");
            //
            TestContext.WriteLine(JsonConvert.SerializeObject(polyBase));
            TestContext.WriteLine(JsonConvert.SerializeObject(polySubtract));

            MeshFragmentVec2D afterSubtraction = MeshFragmentVec2D.Subtract(polyBase, polySubtract);
            TestContext.WriteLine(JsonConvert.SerializeObject(afterSubtraction));
            Assert.AreEqual(trianglesArrayLength, afterSubtraction.Triangles.Length);
        }

        public static TestCaseData[] emptyResult = {
            new TestCaseData( 0, 0),
        };

        [TestCaseSource("emptyResult")]
        public void EmptyResult(int polyIndexBase, int polyIndexSubtract)
        {
            //List<IndexedTri> triangulated = TriangulatePoly(polys[polyIndex], expectedNumberOfTriangles);
            MeshFragmentVec2D polyBase = MeshFragmentVec2D.FromPolyCopy(polys[polyIndexBase]);
            TestContext.WriteLine($"{polyBase.Vertices.Length} {polyBase.Triangles.Length}");
            MeshFragmentVec2D polySubtract = MeshFragmentVec2D.FromPolyCopy(polys[polyIndexSubtract].CyclicLeftShift().ToArray());
            TestContext.WriteLine($"{polySubtract.Vertices.Length} {polySubtract.Triangles.Length}");
            //
            TestContext.WriteLine($"{String.Join(", ", polyBase.Vertices.ToArray())}");
            TestContext.WriteLine($"{String.Join(", ", polySubtract.Vertices.ToArray())}");
            TestContext.WriteLine($"{String.Join(", ", polyBase.Triangles.ToArray())}");
            TestContext.WriteLine($"{String.Join(", ", polySubtract.Triangles.ToArray())}");

            MeshFragmentVec2D afterSubtraction = MeshFragmentVec2D.Subtract(polyBase, polySubtract);
            Assert.AreEqual(MeshFragmentVec2D.Empty, afterSubtraction);
        }

        public static TestCaseData[] notChanged = {
            new TestCaseData( 0, 1),
        };

        [TestCaseSource("notChanged")]
        public void NotChanged(int polyIndexBase, int polyIndexSubtract)
        {
            //List<IndexedTri> triangulated = TriangulatePoly(polys[polyIndex], expectedNumberOfTriangles);
            MeshFragmentVec2D polyBase = MeshFragmentVec2D.FromPolyCopy(polys[polyIndexBase]);
            TestContext.WriteLine($"{polyBase.Vertices.Length} {polyBase.Triangles.Length}");
            MeshFragmentVec2D polySubtract = MeshFragmentVec2D.FromPolyCopy(polys[polyIndexSubtract]);
            TestContext.WriteLine($"{polySubtract.Vertices.Length} {polySubtract.Triangles.Length}");

            MeshFragmentVec2D afterSubtraction = MeshFragmentVec2D.Subtract(polyBase, polySubtract);
            List<IndexedTri> triangulated = afterSubtraction.Triangles.ToIndexedTriList();
            Assert.AreEqual(polyBase.Vertices.Length, afterSubtraction.Vertices.Length);
            Assert.AreEqual(polyBase.Triangles.Length, afterSubtraction.Triangles.Length);

            Triangulate2D_Tests.TestTriangulated(afterSubtraction.Vertices, triangulated);

            TestContext.WriteLine($"{String.Join(", ", polyBase.Vertices.ToArray())}");
            TestContext.WriteLine($"{String.Join(", ", afterSubtraction.Vertices.ToArray())}");
            TestContext.WriteLine($"{String.Join(", ", polyBase.Triangles.ToArray())}");
            TestContext.WriteLine($"{String.Join(", ", afterSubtraction.Triangles.ToArray())}");
        }

    }
}