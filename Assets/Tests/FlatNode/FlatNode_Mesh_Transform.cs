using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using System;
using FluentAssertions_Extensions;
using VectorCore;

namespace AtomTests
{


    [TestFixture]
    public static class FlatNode_Mesh_Transform
    {
        class Vec3DComparerApprox : IEqualityComparer<Vec3D>
        {
            double epsilon = 0.00000007d;
            public bool Equals(Vec3D v1, Vec3D v2) =>
                (Math.Abs(v1.x - v2.x) < epsilon) && (Math.Abs(v1.y - v2.y) < epsilon) && (Math.Abs(v1.z - v2.z) < epsilon);
            public int GetHashCode(Vec3D obj) => throw new System.NotImplementedException();
        }
        private struct TestMeshTransformCombination
        {
            public readonly MeshFragmentVec3D mesh;
            public readonly FlatNodeTransform wasTransformedBy;

            public TestMeshTransformCombination(MeshFragmentVec3D mesh, FlatNodeTransform wasTransformedBy)
            {
                this.mesh = mesh;
                this.wasTransformedBy = wasTransformedBy;
            }
        }

        private static Vec3DComparerApprox comparerVec3D = new Vec3DComparerApprox();
        private static string FilePath(string fileName) => JsonHelpers.ApplicationDataPath + "/Tests/FlatNode/" + fileName + ".json";

        [OneTimeSetUp]
        public static void Init()
        {
            DoubleEngine.__GlobalStatic.Init(Application.dataPath, Debug.Log);
        }

        [TestCase("meshFragment_OneTriangle", 3, 1 * 3)]
        [TestCase("meshFragment_FourTriangles", 5, 4 * 3)]
        //[DefaultFloatingPointTolerance(1)]
        public static void FlatNode_Mesh_transformedByAllFlatNodesTransforms(
            string fileName,
            int expectedVerticesLength,
            int expectedTrianglesLength)
        {
            MeshFragmentVec3D loadedMesh = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(FilePath(fileName));
            TestMeshTransformCombination[] allTransformed = JsonHelpers.LoadFromJsonFile<TestMeshTransformCombination[]>(
                FilePath(fileName + "_AllTransformations"));
            Assert.AreEqual(8, allTransformed.Length);

            foreach (var mc in allTransformed)
            {
                MeshFragmentVec2D t2d = FlatNode.ApplyTransformationToFragment(loadedMesh.To2D(), mc.wasTransformedBy);
                MeshFragmentVec3D t = t2d.To3D(-0.5);
                Assert.AreEqual(expectedVerticesLength, t.Vertices.Length);
                Assert.That(mc.mesh.Vertices.ToArray(), Is.EqualTo(t.Vertices.ToArray()).Using(comparerVec3D));
                Assert.AreEqual(expectedTrianglesLength, t.Triangles.Length);

                //Assert.AreEqual(mc.mesh.Triangles, t.Triangles);

                t.Triangles.ShouldEqual(mc.mesh.Triangles);
            }
        }

        [TestCase("meshFragment_OneTriangle", 3, 1 * 3)]
        [TestCase("meshFragment_FourTriangles", 5, 4 * 3)]
        public static void FlatNode_Mesh_Load_CorrectNumberOfVerticesAndTriangles(string fileName, int vertices, int triangles)
        {
            //string jsonfilePath = JsonHelpers.ApplicationDataPath + "/Tests/FlatNode/meshFragment_OneTriangle.json";
            string jsonfilePath = FilePath(fileName);

            MeshFragmentVec3D loadedMesh = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(jsonfilePath);
            //JsonHelpers.SaveToJsonFile(loadedMesh, jsonfilePath);

            Assert.AreEqual(vertices, loadedMesh.Vertices.Length);
            Assert.AreEqual(triangles, loadedMesh.Triangles.Length);
            /* 
            /// use generator only when manual testing shows correct transformation

            List<TestMeshTransformCombination> transformedMeshes = new();
            FlatNodeTransform[] allTransforms = FlatNodeTransform.allFlatNodeTransforms;
            for (int i = 0; i < allTransforms.Length; i++)
                transformedMeshes.Add( new TestMeshTransformCombination(
                    FlatNode.ApplyTransformationToFragment(loadedMesh, allTransforms[i]), allTransforms[i]
                    ));

            Assert.AreEqual(8, transformedMeshes.Count);

            foreach (var m in transformedMeshes)
            {
                Assert.AreEqual(vertices, m.mesh.vertices.Length);
                Assert.AreEqual(triangles, m.mesh.triangles.Length);
            }

            JsonHelpers.SaveToJsonFile(transformedMeshes, FilePath(fileName + "_AllTransformations"));
            */
        }
    }


}