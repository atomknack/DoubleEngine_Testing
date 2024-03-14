using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.UHelpers;
using VectorCore;

namespace Collision2D_Tests
{

    [TestFixture]
    public class PointInsideTriangle_Tests
    {
        GameObject go = null;
        PolygonCollider2D collider = null;

        [OneTimeSetUp]
        public void OneTimeInit()
        {
            var go = new GameObject();
            go.AddComponent<PolygonCollider2D>();
            collider = go.GetComponent<PolygonCollider2D>();
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            UnityEngine.GameObject.DestroyImmediate(collider);
            UnityEngine.GameObject.DestroyImmediate(go);
        }

        [TearDown]
        public void EveryTestCleanup()
        {
            collider.points = null;
        }

        public static TriVec2D[] triVec2D = new TriVec2D[] {
            new TriVec2D(new Vec2D(0,0), new Vec2D(3,5), new Vec2D(5,1)),
            new TriVec2D(new Vec2D(1,1), new Vec2D(2,-2), new Vec2D(-1,-1)),
            new TriVec2D(new Vec2D(3,-2), new Vec2D(-3, -3), new Vec2D(-4,5)),
            new TriVec2D(new Vec2D(0,2), new Vec2D(2, 0), new Vec2D(-2,-2)),
            new TriVec2D(new Vec2D(-1,2), new Vec2D(3, 5), new Vec2D(0,-7)),
        };

        public static TestCaseData[] PointInsideTriangle = {
            new TestCaseData( 0, new Vec2D(2,2) ),
            new TestCaseData( 1, new Vec2D(1,0.5) ),
            new TestCaseData( 2, new Vec2D(-1,0) ),
            new TestCaseData( 2, new Vec2D(-2, 0) ),
            new TestCaseData( 2, new Vec2D(-3, 0) ),
            new TestCaseData( 2, new Vec2D(-2.9, -2.9) ),
            new TestCaseData( 3, new Vec2D(1,0) ),
            new TestCaseData( 4, new Vec2D(0,0) ),
            new TestCaseData( 0, new Vec2D(0.01,0.01) ),
            new TestCaseData( 1, new Vec2D(-0.9995,-0.9999) ),
            new TestCaseData( 1, new Vec2D(1.19588, 0.39919) ), // not in but should be somewhere close
        };
        public static IEnumerable PointInsideTriangle_UnityTestCaseData => PointInsideTriangle.Select(x => x.Returns(null));
        public static IEnumerable PointInsideTriangle_UnityTestCaseDataVector2 => 
            PointInsideTriangle.Select(x => new TestCaseData(x.OriginalArguments[0], ((Vec2D)x.OriginalArguments[1]).ToVector2()).Returns(null));

        public static TestCaseData[] PointOutsideTriangle = {
            new TestCaseData( 1, new Vec2D(1.2d, 0.40001d) ), // not in but should be somewhere close
        };
        public static IEnumerable PointOutsideTriangle_UnityTestCaseDataVector2 =>
            PointOutsideTriangle.Select(x => new TestCaseData(x.OriginalArguments[0], ((Vec2D)x.OriginalArguments[1]).ToVector2()).Returns(null));

        [TestCaseSource(nameof(PointInsideTriangle))]
        public IEnumerator TriVec2D_PointInsideTriangle_Test(int triIndex, Vec2D point)
        //public void TriVec2D_PointInsideTriangle_Test(int triIndex, Vec2D point)
        {
            TriVec2D tri = triVec2D[triIndex];

            //if (CollisionDiscrete2D.RaycastToTri(RayVec2D.FromEdgeVec2D(new Vec2D(1.2, 20), new Vec2D(1.2, -20)), tri.v0, tri.v1, tri.v2, out double triRaycastDistance, out Vec2D triRaycastCollisionPoint))
            //    TestContext.WriteLine($"{triRaycastCollisionPoint.ToString("F5")}");

            //TestContext.WriteLine($"from{lineStart:F5}to{lineEnd:F5} point:{point:F5}; projected:{projected:F5}");
            Assert.True(CollisionDiscrete2D.OverlapPoint(tri, point));
            Assert.True(CollisionDiscrete2D.OverlapPoint(tri.v0, tri.v1, tri.v2, point));
            var span = new Span<Vec2D>(tri.ToVec2DArray());
            TestContext.WriteLine($"v0:{span[0]:F5}, v1{span[1]:F5}, v2:{span[2]:F5}");
            Assert.True(CollisionDiscrete2D.PointInsidePolygon(span, point));

            yield return null;
        }

        public static IEnumerable<TestCaseData> TriIndexAndPoint_Random_50()
            {
                var rand = TestGenerators.rand;
                for (var triIndex = 0; triIndex < triVec2D.Length; triIndex++)
                    for (var i = 0; i < 10; i++)
                    {
                        //int triIndex = rand.Next(0, triVec2D.Length);
                        TriVec2D tri = triVec2D[triIndex];
                        Vec2D min = tri.Min() - Vec2D.one;
                        Vec2D max = tri.Max() + Vec2D.one;
                        yield return new TestCaseData(triIndex, TestGenerators.RandVec2D(rand, min, max).ToVector2()).Returns(null);
                    } 
            }

        [UnityTest]
        [Category("Flaky tolerated")]
        [TestCaseSource("PointInsideTriangle_UnityTestCaseDataVector2")]
        [TestCaseSource("PointOutsideTriangle_UnityTestCaseDataVector2")]
        [TestCaseSource(nameof(TriIndexAndPoint_Random_50))]
        public IEnumerator TriVec2D_PointTriangle_Tests(int triIndex, Vector2 point)
        {
            /// additional cleanup because of Unity
            collider.points = new[]{ Vector2.zero};
            Physics2D.OverlapPoint(Vector2.zero);
            collider.points = null;
            ///

            TriVec2D tri = triVec2D[triIndex];//.ToTriVector2().ToTriVec2D();
            collider.points = new Vector2[] { tri.v0.ToVector2(), tri.v1.ToVector2(), tri.v2.ToVector2() };
            Collider2D unityCollision = Physics2D.OverlapPoint(point);
            bool unityInside = unityCollision != null;
            TestContext.WriteLine($"PointInside:{unityInside}, triIndex:{triIndex}, point:{point.ToString("F5")}");
            Assert.AreEqual(unityInside, CollisionDiscrete2D.OverlapPoint(tri, point.ToVec2D()));

            collider.points = null;

            yield return null;
        }

        [UnityTest, TestCaseSource("PointInsideTriangle_UnityTestCaseData")]
        public IEnumerator RaycastFromDifferenDirections(int triIndex, Vec2D pointOrig)
        {
            Vec2D[] raySources = { 
                new Vec2D(-100, pointOrig.y), 
                new Vec2D(100, pointOrig.y), 
                new Vec2D(pointOrig.x, -100), 
                new Vec2D(pointOrig.x, 100),
                new Vec2D(105, 130),
                new Vec2D(5, -180),
                new Vec2D(-35, 20),
                new Vec2D(359, 209),
            };
            foreach (var raySource in raySources) { 
            Vec2D point = pointOrig.ToVector2().ToVec2D();
            RayVec2D rayVec2D = RayVec2D.FromEdgeVec2D(raySource, point+Vec2D.one);
                TriVec2D tri = triVec2D[triIndex];//.ToTriVector2().ToTriVec2D();
            double distance = 100_000;
            Vec2D dPoint = new Vec2D();
            bool haveDoubleEngineCollision = false;
            (Vec2D eS, Vec2D eE)[] edges = { (tri.v0, tri.v1), (tri.v1, tri.v2), (tri.v2, tri.v0) };
            foreach(var edge in edges)
            {
                TestContext.WriteLine($"edgeStart:{edge.eS.ToString("F5")}, edgeEnd:{edge.eE.ToString("F5")}");
                if (CollisionDiscrete2D.RaycastToEdge(rayVec2D, edge.eS, edge.eE, out double tempDistanceEdge, out Vec2D tempCollisionPoint))
                {
                    TestContext.WriteLine($"distance:{distance}, distanceEdge:{tempDistanceEdge.ToString("F8")}, Point:{tempCollisionPoint.ToString("F8")}");
                    if (tempDistanceEdge < distance)
                    {
                        distance = tempDistanceEdge;
                        dPoint = tempCollisionPoint;
                    }
                    haveDoubleEngineCollision = true;
                }
            }
            TestContext.WriteLine($"distance:{distance}, {dPoint}");

            if(CollisionDiscrete2D.RaycastToTri(rayVec2D, tri.v0, tri.v1, tri.v2, out double triRaycastDistance, out Vec2D triRaycastCollisionPoint))
            {
                Assert.AreEqual(distance, triRaycastDistance);
                Assert.AreEqual(dPoint, triRaycastCollisionPoint);
            }
            else
                Assert.IsFalse(haveDoubleEngineCollision);


            collider.points = new Vector2[]{ tri.v0.ToVector2(), tri.v1.ToVector2(), tri.v2.ToVector2()}; 
            RaycastHit2D hit = Physics2D.Raycast(rayVec2D.origin.ToVector2(), rayVec2D.directionNormalized.ToVector2());
            if (hit.collider != null)
            {
                TestContext.WriteLine($"have unity collision");
                Vector2 unityPoint = hit.point;
                Assert.AreEqual(unityPoint.x, dPoint.x, 0.00005d);
                Assert.AreEqual(unityPoint.y, dPoint.y, 0.00005d);
            }
            else
                Assert.AreEqual(false, haveDoubleEngineCollision);

            }

            yield return null;
        }


        ///Unity Tests

        [UnityTest]
        public IEnumerator TestUnity()
        {
            Assert.Pass(); //Pass
            yield return null;
        }

        [UnityTest, TestCaseSource(nameof(CaseSource))]
        public IEnumerator SourceTestUnity(int a, float b, bool c)
        {
            var go = new GameObject();
            go.AddComponent<PolygonCollider2D>();
            PolygonCollider2D collider = go.GetComponent<PolygonCollider2D>();
            //collider.points = 
            var originalPosition = go.transform.position.y;

            go.transform.Translate(Vector3.one);// yield return new WaitForFixedUpdate();

            Assert.AreNotEqual(originalPosition, go.transform.position.y);

            Assert.AreEqual(4, a / b); //Fail


            collider.points = null;
            UnityEngine.GameObject.DestroyImmediate(collider);
            UnityEngine.GameObject.DestroyImmediate(go);

            yield return null;
        }

        [Test, TestCaseSource(nameof(CaseSource))]
        public IEnumerator SourceTest(int a, float b, bool c)
        {
            Assert.AreEqual(4, a/b); //Pass
            yield return null;
        }

        static TestCaseData[] CaseSource =
        {
            new TestCaseData(12, 3.0f, false).Returns(null),
            new TestCaseData( 12, 3.0f, false).Returns(null),
            //new TestCaseData( 10, 3.0f, false).Returns(null),
        };
    }
}
