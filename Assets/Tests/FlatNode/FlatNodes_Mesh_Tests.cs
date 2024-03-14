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

namespace AtomTests
{



[TestFixture]
public class FlatNodes_Mesh_Tests
{
        private static List<FlatNode> flatNodes;
        public static IEnumerable<FlatNode> AllDefaultNodes { get => flatNodes; }
        public static IEnumerable<MeshFragmentVec3D> MeshFragmets { get => flatNodes.Select(x => x.form.To3D(-0.5)); }
        static FlatNodes_Mesh_Tests()
        {
            string fileName = Application.dataPath + "/Tests/FlatNode/flatnodesData_readonlyFileTest.json";
            string flatNodesSource = File.ReadAllText(fileName);
            flatNodes = (List<FlatNode>)JsonConvert.DeserializeObject(flatNodesSource, typeof(List<FlatNode>));
        }

    /*public IEnumerable<MeshFragmentVec3D> DefaultMeshFragmets() => FlatNodes.AllDefaultNodes.Select(x => x.form);
    [OneTimeSetUp]
    public void Init() {
        TestContext.WriteLine("Setup");
        TestContext.WriteLine(Application.dataPath);
        FlatNodes.LoadFromJsonFile(Application.dataPath + "/Tests/FlatNode/flatnodesData_readonlyFileTest.json");
    }*/

    [Test]
    public void FlatNode_FlatNodeCount()
    {
        Assert.AreEqual(6, AllDefaultNodes.Count());
        Assert.AreEqual(79, MeshFragmets.Tris().Count());
    }

    [Test]
    public void FlatNode_AllTrianglesFacesXY_IsClockwise()
    {
        TestContext.WriteLine($"{MeshFragmets.Tris().Count()}");
        foreach (var face in MeshFragmets.Tris().ConvertFaces_Vec3DToVec2D())//.ConvertFaces_Vector3ToVector2())
        {
            TestContext.WriteLine(face);
            Assert.IsTrue(face.IsTriangleClockwise());
        }
    }

    [Test]
    public void Faces2DFrom3D_NotChangeCount()
    {
        int facesCount = MeshFragmets.Tris().Count();
        Assert.AreEqual(MeshFragmets.Tris().ConvertFaces_Vec3DToVec2D().Count(), facesCount);
    }

        [TestCaseSource("MeshFragmets")]
        public void FlatNode_MeshFragmetsHaveJoinedVertices(MeshFragmentVec3D mesh)
    {
            MeshFragmentVec3D joined = mesh.JoinedClosestVertices();
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(mesh, joined);
            AssertNoDuplicateVertices(mesh.Vertices);
    }
}

}