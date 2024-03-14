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
    public class MeshFragment_JoinClosestVertices
    {
        [TestCase("/Tests/Meshes/defaultCube_NotJoined.json", "/Tests/Meshes/defaultCube8vertices.json")]
        [TestCase("/Tests/Meshes/defaultCubeWithCuts_NotJoined.json", "/Tests/Meshes/defaultCubeWithCuts.json")]
        public void JoinClosestVertices_Test(string notJoinedMeshPath, string joinedPath)
        {
            MeshFragmentVec3D meshNotJoined = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(Application.dataPath + notJoinedMeshPath);
            MeshFragmentVec3D meshJoined = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(Application.dataPath + joinedPath);
            JoinClosestVertices_Test(meshNotJoined, meshJoined);
        }
        public void JoinClosestVertices_Test(MeshFragmentVec3D notJoinedMesh, MeshFragmentVec3D expected)
        {
            //TestContext.WriteLine($"{notJoinedMesh.Vertices.Length} {expected.Vertices.Length}");
            //TestContext.WriteLine($"{notJoinedMesh.Triangles.Length} {expected.Triangles.Length}");
            MeshFragmentVec3D joined = notJoinedMesh.JoinedClosestVertices();
            Assert.AreEqual(expected.Vertices.Length, joined.Vertices.Length);
            Assert.AreEqual(expected.Triangles.Length, joined.Triangles.Length);
            AssertMeshesContainedVerticesInSameCoordinates(expected, joined);
            AssertMeshesHaveEqualishTriangles(expected, joined);
        }
    }
}