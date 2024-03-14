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
        public static void AssertNoDuplicateVertices(ReadOnlySpan<Vec3D> vertices, double epsilon = 0.00001d)
        {
            for (int i = 0; i < vertices.Length; i++)
                for (int j = 0; j < vertices.Length; j++)
                    if (i != j)
                        Assert.False(Vec3D.CloseByEach(vertices[i], vertices[j], epsilon));
        }
        public static void AssertMeshesHaveEqualishTriangles(MeshFragmentVec3D mesh, MeshFragmentVec3D other, double epsilon = 0.000001d)
        {
            AssertMeshContainsEqualishTrianglesFromOther(mesh, other, epsilon);
            AssertMeshContainsEqualishTrianglesFromOther(other, mesh, epsilon);
        }
        [Obsolete("Replace in too slow places")]
        public static void AssertMeshesHaveEqualishTriangles(IMeshFragment<Vec3D> mesh, IMeshFragment<Vec3D> other, double epsilon = 0.000001d)
        {
            AssertMeshContainsEqualishTrianglesFromOther(mesh, other, epsilon);
            AssertMeshContainsEqualishTrianglesFromOther(other, mesh, epsilon);
        }
        public static void AssertMeshContainsEqualishTrianglesFromOther(MeshFragmentVec3D mesh, MeshFragmentVec3D other, double epsilon = 0.000001d)
        {
            foreach (TriVec3D otherTri in other.Tris())
                Assert.IsTrue(mesh.ContainsEqualishTriangle(otherTri));
        }
        public static void AssertMeshContainsEqualishTrianglesFromOther(IMeshFragment<Vec3D> mesh, IMeshFragment<Vec3D> other, double epsilon = 0.000001d)
        {
            foreach (TriVec3D otherTri in other.Tris())
                Assert.IsTrue(mesh.ContainsEqualishTriangle(otherTri), $"there is no triangle like {otherTri} not found in [{string.Join(",", mesh.Tris())}]");
        }
        public static void AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(MeshFragmentVec3D expected, MeshFragmentVec3D actual)
        {
            Assert.IsTrue(actual.Vertices.Length > 0);
            Assert.IsTrue(actual.Triangles.Length > 0);
            Assert.IsTrue(actual.Triangles.Length % 3 == 0);
            Assert.AreEqual(expected.Vertices.Length, actual.Vertices.Length);
            Assert.AreEqual(expected.Triangles.Length, actual.Triangles.Length);
        }

        public static void AssertMeshSizeEqual(MeshFragmentVec3D expected, MeshFragmentVec3D actual)
        {
            Assert.AreEqual(expected.Vertices.Length, actual.Vertices.Length);
            Assert.AreEqual(expected.Triangles.Length, actual.Triangles.Length);
        }

        public static void AssertMeshesContainedVerticesInSameCoordinates(MeshFragmentVec3D a, MeshFragmentVec3D b, double epsilon = 0.000001d)
        {
            foreach (var v in b.Vertices)
                Assert.IsTrue(a.Vertices.In_CompareByEach(v, epsilon));
            foreach (var v in a.Vertices)
                Assert.IsTrue(b.Vertices.In_CompareByEach(v, epsilon));
        }

        public static void AssertMeshHasAllCubeVertices(MeshFragmentVec3D mesh, double cubeSize = 0.5d, double epsilon = 0.000001d)
        {
            double s = cubeSize;
            double negS = -cubeSize;
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(negS, negS, negS), epsilon));
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(negS, s, negS), epsilon));
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(s, s, negS), epsilon));
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(s, negS, negS), epsilon));

            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(negS, negS, s), epsilon));
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(negS, s, s), epsilon));
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(s, s, s), epsilon));
            Assert.IsTrue(mesh.Vertices.In_CompareByEach(new Vec3D(s, negS, s), epsilon));
        }
    }
}