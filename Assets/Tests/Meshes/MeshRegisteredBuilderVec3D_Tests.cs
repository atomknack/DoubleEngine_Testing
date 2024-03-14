using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using DoubleEngine;
using DoubleEngine.Atom;
using System.IO;
using Newtonsoft.Json;
using static FluentAssertions_Extensions.Assertions;

namespace AtomTests.Meshes
{
    [TestFixture]
    public class MeshRegisteredBuilderVec3D_Tests
{
        static MeshRegisteredBuilderVec3D singleBuilder = new MeshRegisteredBuilderVec3D();
        [TestCaseSource(typeof(AtomTests.FlatNodes_Mesh_Tests), nameof(AtomTests.FlatNodes_Mesh_Tests.MeshFragmets))]
        public void SingleStaticBuilderMeshFragmetAfterRegister_EqualTo_JoinedMeshFragment(MeshFragmentVec3D mesh)
        {
            //prerequisite:
            var joined = mesh.JoinedClosestVertices();
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(mesh, joined);
            AssertMeshSizeEqual(mesh, joined);

            //test:
            MeshRegisteredBuilderVec3D builder = singleBuilder;
            builder.Clear();
            builder.AddMeshFragment(mesh);
            var afterRegister = builder.BuildMeshFragmentVec3D();
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(joined, afterRegister);
            TestContext.WriteLine($"Vertices: {joined.Vertices.Length}, Triangles: {joined.Triangles.Length}");
            AssertMeshSizeEqual(joined, afterRegister);
            AssertMeshesContainedVerticesInSameCoordinates(joined, afterRegister);
            AssertMeshesHaveEqualishTriangles(joined, afterRegister);
        }


        [TestCaseSource(typeof(AtomTests.FlatNodes_Mesh_Tests), nameof(AtomTests.FlatNodes_Mesh_Tests.MeshFragmets))]
        public void MeshFragmetAfterRegister_EqualTo_JoinedMeshFragment(MeshFragmentVec3D mesh)
        {
            //prerequisite:
            var joined = mesh.JoinedClosestVertices();
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(mesh, joined);
            
            //test:
            MeshRegisteredBuilderVec3D builder = new MeshRegisteredBuilderVec3D();
            builder.AddMeshFragment(mesh);
            var afterRegister = builder.BuildMeshFragmentVec3D();
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(joined, afterRegister);
            AssertMeshesHaveEqualishTriangles(joined, afterRegister);
        }

        [TestCase("/Tests/Meshes/defaultCube8vertices.json")]
        [TestCase("/Tests/Meshes/defaultCubeWithCuts.json")]
        public void RegisterDoesNotChangeJoinedMesh(string path)
        {
            //prerequisites:
            MeshFragmentVec3D mesh =
                JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(
                    Application.dataPath + path);
            AssertMeshHasAllCubeVertices(mesh);
            MeshRegisteredBuilderVec3D builder = new MeshRegisteredBuilderVec3D();
            builder.AddMeshFragment(mesh);
            var afterRegister = builder.BuildMeshFragmentVec3D();

            //test:
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(mesh, afterRegister);
            AssertMeshSizeEqual(mesh, afterRegister);
            AssertMeshesContainedVerticesInSameCoordinates(mesh, afterRegister);
            AssertMeshesHaveEqualishTriangles(mesh, afterRegister);

            AssertMeshHasAllCubeVertices(afterRegister);
        }

        [TestCase("/Tests/Meshes/defaultCube_NotJoined.json")]
        [TestCase("/Tests/Meshes/defaultCubeWithCuts_NotJoined.json")]
        public void RegisterDoesJoinMesh(string path)
        {
            //prerequisites:
            MeshFragmentVec3D mesh =
                JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(
                    Application.dataPath + path);
            AssertMeshHasAllCubeVertices(mesh);
            MeshRegisteredBuilderVec3D builder = new MeshRegisteredBuilderVec3D();
            builder.AddMeshFragment(mesh);
            var afterRegister = builder.BuildMeshFragmentVec3D();
            var justJoined = mesh.JoinedClosestVertices();

            //test:
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(justJoined, afterRegister);

            Assert.AreEqual(justJoined.Vertices.Length, afterRegister.Vertices.Length);
            Assert.AreEqual(justJoined.Triangles.Length, afterRegister.Triangles.Length);
            AssertMeshesContainedVerticesInSameCoordinates(mesh, afterRegister);
            AssertMeshHasAllCubeVertices(afterRegister);

            Assert.AreEqual(mesh.Triangles.Length, afterRegister.Triangles.Length);
            Assert.IsTrue(afterRegister.Vertices.Length>0);
            Assert.IsTrue(mesh.Vertices.Length > afterRegister.Vertices.Length);
            AssertMeshesHaveEqualishTriangles(mesh, afterRegister);
        }

        [TestCase("/Tests/Meshes/defaultCube8vertices.json")]
        public void DefaultCubeHas8VerticesAnd12Triangles(string path)
        {
            //prerequisites:
            MeshFragmentVec3D mesh =
                JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(
                    Application.dataPath + path);
            AssertMeshHasAllCubeVertices(mesh);
            MeshRegisteredBuilderVec3D builder = new MeshRegisteredBuilderVec3D();
            builder.AddMeshFragment(mesh);
            var afterRegister = builder.BuildMeshFragmentVec3D();

            Assert.AreEqual(8, afterRegister.Vertices.Length);
            Assert.AreEqual(12*3, afterRegister.Triangles.Length); //6 sides * 2 triangles per side * 3 vertice in each triangle
        }


    }

}