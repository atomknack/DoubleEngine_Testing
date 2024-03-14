using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Collections.Pooled;
using UnityEngine;
using DoubleEngine;
using FluentAssertions;
using VectorCore;

namespace AtomTests
{
    public partial class Poly_Tests
    {
        public static Vec2D[][] polyToProcess = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(5,5), new Vec2D(5,-5) },
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(2, 2), new Vec2D(5,5), new Vec2D(5, 2), new Vec2D(5, -2), new Vec2D(5,-5) },
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1, 1), new Vec2D(1,1), new Vec2D(1, -1)},
            new Vec2D[]{ new Vec2D(0, -1), new Vec2D(-1,-1), new Vec2D(-1, 1), new Vec2D(1,1), new Vec2D(1, -1), new Vec2D(0.5, -1.0003),},
        };
        public static Vec2D[][] polyProcessed = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(5,5), new Vec2D(5,-5) },
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(5,5), new Vec2D(5,-5) },
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1, 1), new Vec2D(1,1), new Vec2D(1, -1)},
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1, 1), new Vec2D(1,1), new Vec2D(1, -1)},
        };

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RemoveMiddlePointsThatLieOnSameLineOfPoly_Test(int polyIndex)
        {
            PooledList<Vec2D> poly = new PooledList<Vec2D>(polyToProcess[polyIndex]);
            poly.RemoveMiddlePointsThatLieOnSameLineOfPoly();
            Assert.AreEqual(polyProcessed[polyIndex].Length, poly.Count);
            Assert.AreEqual(polyProcessed[polyIndex], poly);
        }

        public static Vec2D[][] polyCannotBeSimplified = new Vec2D[][] {
        new Vec2D[]{ new Vec2D(0, -1), new Vec2D(-1,-1), new Vec2D(-1, 1), new Vec2D(1,1), new Vec2D(1, -1), new Vec2D(0.7, -1.00035),}, //partial, last point cannot be removed, but first is removed (but probably shouldnt) - need to check epsilons at PointBelongsToEdge
        new Vec2D[]{ new Vec2D(0,0), new Vec2D(2, 2.0005), new Vec2D(5,5), new Vec2D(5,-5) }, //ok cannot remove new Vec2D(2, 2.0005)
        };

        [TestCase(0, 5)]
        [TestCase(1, 4)]
        public void CannotSimplifyPoly_Test(int polyIndex, int length)
        {
            PooledList<Vec2D> poly = new PooledList<Vec2D>(polyCannotBeSimplified[polyIndex]);
            poly.RemoveMiddlePointsThatLieOnSameLineOfPoly();
            TestContext.WriteLine(String.Join(", ", poly));
            Assert.AreEqual(length, poly.Count);
        }

        [Test]
        public void Passing_Null_ShouldThrow()
        {
            Action act = ()=>PolyHelpers.RemoveMiddlePointsThatLieOnSameLineOfPoly(null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void Passing_LessThan3Vertices_ShouldThrow()
        {
            PooledList<Vec2D> list = new PooledList<Vec2D>();
            RemoveMiddleShouldThrow(list);
            list.Add(Vec2D.zero);
            RemoveMiddleShouldThrow(list);
            list.Add(Vec2D.one);
            RemoveMiddleShouldThrow(list);

            static void RemoveMiddleShouldThrow(PooledList<Vec2D> list)
            {
                Action act = () => PolyHelpers.RemoveMiddlePointsThatLieOnSameLineOfPoly(list);
                act.Should().ThrowExactly<ArgumentException>();
            }
        }

    }
}
