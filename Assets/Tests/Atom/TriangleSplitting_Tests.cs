using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Collections.Pooled;
using UnityEngine;
using DoubleEngine;
using Newtonsoft.Json;
using CollectionLike;
using FluentAssertions;
using FluentAssertions_Extensions;
using CollectionLike.Pooled;
using VectorCore;

namespace AtomTests
{
    public class TriangleSplitting_Tests 
    {
        public static IEnumerable<TestCaseData> toSplitTestCases = new TestCaseData[] {
            new TestCaseData(
                new Vec3D[]{ new Vec3D(-1,-1,0), new Vec3D(-1, 1, 0), new Vec3D(1, 1, 0), new Vec3D(1, -1, 0), new Vec3D(0, -1, 0) , new Vec3D(0, 1, 0) , new Vec3D(-1, 0, 0), new Vec3D(1, 0, 0) },
                new int[] {0,1,2, 0,2,3},
                6*3)
       };

    [TestCaseSource(nameof(toSplitTestCases))]
    public void SplitTriangles_Test(Vec3D[] vertices, int[] triangles, int expectedTrianglesLengthAfterSplit)
        {
            using PooledList<int> buffer = Expendables.CreateList<int>(triangles.Length*2);// new List<int>();
            Span<int> allVerticesIndexes = stackalloc int[vertices.Length];
            allVerticesIndexes.FillAsRange();
            for (int i = 0; i < triangles.Length; i += 3)
            {
                TestContext.WriteLine($"triangle{i}: {triangles[i]}, {triangles[i + 1]}, {triangles[i + 2]}");
                TestContext.WriteLine($"triangle{i}Coords: {vertices[triangles[i]]}, {vertices[triangles[i + 1]]}, {vertices[triangles[i + 2]]}");
                buffer.AddTriangleAndSplitIfNeeded(triangles[i], triangles[i + 1], triangles[i + 2], allVerticesIndexes, vertices);
            }
            TestContext.WriteLine(JsonConvert.SerializeObject(MeshFragmentVec3D.CreateMeshFragment(vertices, buffer.ToArray())));
            Assert.AreEqual(expectedTrianglesLengthAfterSplit, buffer.Count);

            ReadOnlySpan<IndexedTri> faces = triangles.AsReadOnlySpan().CastToIndexedTri_ReadOnlySpan();
            using PooledList<IndexedTri> facesBuffer = Expendables.CreateList<IndexedTri>(triangles.Length);
            for (int i = 0; i < faces.Length; i++)
                facesBuffer.AddTriangleAndSplitIfNeeded(faces[i], allVerticesIndexes, vertices);

            //Assert.AreEqual(buffer.Count, facesBuffer.Count * 3);
            //Assert.AreEqual(buffer.AsReadOnlySpan(), facesBuffer.AsReadOnlySpan().CastToInt_ReadOnlySpan());

            buffer.Count.Should().Be(facesBuffer.Count * 3);
            buffer.AsReadOnlySpan().ShouldEqual(facesBuffer.AsReadOnlySpan().CastToInt_ReadOnlySpan());
        }
    }
}
