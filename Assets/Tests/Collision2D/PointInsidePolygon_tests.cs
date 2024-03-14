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
    public class PointInsidePolygon_Tests
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
        public void EveryTestCleanup() => collider.points = null;
        

        public static Vec2D[][] polysForTests = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(3,5), new Vec2D(5,1) },
            new Vec2D[]{ new Vec2D(1,1), new Vec2D(2,-2), new Vec2D(1, -1), new Vec2D(-1,-3),  new Vec2D(-0.7,1)},
            new Vec2D[]{new Vec2D(0,3),new Vec2D(0.5, 1.5),new Vec2D(3,1.5),new Vec2D(1,1),new Vec2D(2,-1),new Vec2D(0,0),new Vec2D(-2,-1),new Vec2D(-1,1),new Vec2D(-3,1.5),new Vec2D(-0.5,1.5)},
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1,1), new Vec2D(1, 1), new Vec2D(1,-1)},
        };

        public static TestCaseData[] pointInsidePolygon_TestCaseData = {
            new TestCaseData( 0, new Vec2D(2.6676580000, 2.1595930000) ),
            new TestCaseData( 0, new Vec2D(3.7542450000, 1.6706290000) ),
            new TestCaseData( 0, new Vec2D(2.8034800000, 3.7623090000) ),
            new TestCaseData( 0, new Vec2D(1.0921050000, 0.7198639000) ),
            new TestCaseData( 1, new Vec2D(-0.29329341650009200000, -1.34465217590332000000) ),
            new TestCaseData( 1, new Vec2D(0.2500000000, -0.2852292000) ),
            new TestCaseData( 1, new Vec2D(-0.6735992000, -2.1595930000) ),
            new TestCaseData( 1, new Vec2D(1.6082340000, -1.3989810000) ),
            new TestCaseData( 1, new Vec2D(0.9291170000, 0.8828526000) ),
        };
        public static IEnumerable pointInsidePolygon_UnityTestCaseData => pointInsidePolygon_TestCaseData.Select(x => x.Returns(null));

        [UnityTest]
        [TestCaseSource("pointInsidePolygon_UnityTestCaseData")]
        public IEnumerator pointInsidePolygon_Test(int polyIndex, Vec2D point)
        {
            (bool unityInside, bool doubleEngineCollision) = pointInPolygon(polyIndex, point);
            Assert.IsTrue(doubleEngineCollision);
            Assert.AreEqual(unityInside, doubleEngineCollision);
            yield return null;
        }

        public static IEnumerable pointOutsidePolygon_UnityTestCaseData = new TestCaseData[]{
            new TestCaseData( 0, new Vec2D(0.7661294000, 2.7300510000) ),
            new TestCaseData( 0, new Vec2D(-2.3306450000, -1.5619690000) ),
            new TestCaseData( 0, new Vec2D(3.0479630000, 6.9677420000) ),
            new TestCaseData( 0, new Vec2D(7.2313240000, 2.5670620000) ),
            new TestCaseData( 0, new Vec2D(5.3841260000, -2.3225810000) ),
            new TestCaseData( 0, new Vec2D(7.7474540000, -0.5568762000) ),
            new TestCaseData( 1, new Vec2D(1.0921050000, -1.5076400000) ),
            new TestCaseData( 1, new Vec2D(1.5267410000, 0.5025463000) ),
            new TestCaseData( 1, new Vec2D(-0.9724114000, 0.6112051000) ),
            new TestCaseData( 1, new Vec2D(0.3586596000, 1.2359930000) ),
            new TestCaseData( 1, new Vec2D(-0.4834468000, -2.7300510000) ),
            new TestCaseData( 1, new Vec2D(-2.4664690000, -6.3429540000) ),
        }.Select(x => x.Returns(null));
        [UnityTest]
        [TestCaseSource("pointOutsidePolygon_UnityTestCaseData")]
        public IEnumerator pointOutsidePolygon_Test(int polyIndex, Vec2D point)
        {
            (bool unityInside, bool doubleEngineCollision) = pointInPolygon(polyIndex, point);
            Assert.IsFalse(doubleEngineCollision);
            Assert.AreEqual(unityInside, doubleEngineCollision);
            yield return null;
        }

        public static IEnumerable<TestCaseData> PolyIndexAndPoint_RandomPointsForEachPoly()
        {
            var rand = TestGenerators.rand;
            for (var polyIndex = 0; polyIndex < polysForTests.Length; polyIndex++)
            {
                Vec2D[] poly = polysForTests[polyIndex];
                Vec2D min = TestGenerators.MinComponentsVec2D(poly) - Vec2D.one;
                Vec2D max = TestGenerators.MaxComponentsVec2D(poly) + Vec2D.one;
                for (var i = 0; i < 20; i++)
                    yield return new TestCaseData(polyIndex, TestGenerators.RandVec2D(rand, min, max)).Returns(null);
            }
        }

        [UnityTest]
        [TestCaseSource(nameof(PolyIndexAndPoint_RandomPointsForEachPoly))]
        public IEnumerator pointPolygon_DifferentSystemsEqual_Test(int polyIndex, Vec2D point)
        {
            (bool unityInside, bool doubleEngineCollision) = pointInPolygon(polyIndex, point);
            Assert.AreEqual(unityInside, doubleEngineCollision);
            yield return null;
        }

        private (bool unityCollision, bool customDoubleCollision) pointInPolygon(int polyIndex, Vec2D point)
        {
            /// additional cleanup because of Unity
            collider.points = new[] { Vector2.zero };
            Physics2D.OverlapPoint(Vector2.zero);
            collider.points = null;
            ///

            Vec2D[] poly = polysForTests[polyIndex];
            collider.points = poly.ToArrayVector2();
            Collider2D unityCollision = Physics2D.OverlapPoint(point.ToVector2());
            bool unityInside = unityCollision != null;
            TestContext.WriteLine($"PointInside:{unityInside}, triIndex:{polyIndex}, point:{point.ToString("F5")}");
            bool doubleEngineCollision = CollisionDiscrete2D.PointInsidePolygon(poly, point);

            collider.points = null;

            return (unityCollision, doubleEngineCollision);
        }
    }
}
