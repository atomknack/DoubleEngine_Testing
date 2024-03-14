using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using DoubleEngine;
using CollectionLike;
using CollectionLike.Enumerables;
using System;
using FluentAssertions_Extensions;
using VectorCore;

namespace Enumerables
{

    [TestFixture]
    public class AssembleIndices_Tests
    {
        Vec2D[] vertices = { new Vec2D(0, 0), new Vec2D(1, 0), new Vec2D(0, 2), new Vec2D(3, -5) };
        public static TestCaseData[] assembledVec2D = {
            new TestCaseData( new Vec2D[]{ new Vec2D(1, 0), new Vec2D(0, 2), }, new int[]{1,2}),
            new TestCaseData( new Vec2D[]{ new Vec2D(0, 0), new Vec2D(0, 2), new Vec2D(3, -5) }, new int[] { 0, 2, 3 } ),
            new TestCaseData( new Vec2D[]{ }, new int[]{ }),
            new TestCaseData( new Vec2D[]{ 
                new Vec2D(3,-5),new Vec2D(3,-5), new Vec2D(3,-5),new Vec2D(3,-5),
                new Vec2D(3,-5),new Vec2D(3,-5), new Vec2D(3,-5),new Vec2D(3,-5),
                }, new int[] { 3,3,3,3,  3,3,3,3 } ),
        };

        [TestCaseSource("assembledVec2D")]
        public void AssembledVec2D(Vec2D[] expected, int[] actualIndices)
        {
            Assert.AreEqual(expected, vertices.AssembleIndices(actualIndices));
            Span<Vec2D> buffer = new Vec2D[actualIndices.Length];
            vertices.AsReadOnlySpan().AssembleIndicesToBuffer(actualIndices.AsReadOnlySpan(), buffer);

            buffer.ShouldEqual(expected);
        }

        float[] floats = { 0.5f, 1.1f, 0.2f, 99.3f, -89.4f };
        public static TestCaseData[] assembledFloats = {
            new TestCaseData( new float[]{ 1.1f, 0.2f, }, new int[]{1,2}),
            new TestCaseData( new float[]{ 0.5f, 0.2f, 99.3f }, new int[] { 0, 2, 3 } ),
            new TestCaseData( new float[]{ }, new int[]{ }),
            new TestCaseData( new float[]{ 
                99.3f,99.3f,99.3f,99.3f,  
                99.3f,99.3f,99.3f,99.3f,
                }, new int[] { 3,3,3,3,  3,3,3,3 } ),
        };
        [TestCaseSource("assembledFloats")]
        public void AssembledFloats(float[] expected, int[] actualIndices)
        {
            Assert.AreEqual(expected, floats.AssembleIndices<float>(actualIndices));
            Span<float> buffer = stackalloc float[actualIndices.Length];
            floats.AsReadOnlySpan().AssembleIndicesToBuffer<float>(actualIndices.AsReadOnlySpan(), buffer);

            buffer.ShouldEqual(expected);
        }
    }
}

