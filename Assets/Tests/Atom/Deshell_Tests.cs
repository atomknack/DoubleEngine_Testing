using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Collections.Pooled;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using Newtonsoft.Json;
using FluentAssertions_Extensions;
using VectorCore;

namespace AtomTests
{
    public class Deshell_Tests
    {
        public static MeshFragmentVec3D[] fragments = new MeshFragmentVec3D[] { 
            MeshFragmentVec3D.CreateMeshFragment( //0
                    new Vec3D[]{ new Vec3D(-0.5,-0.5,-0.5), new Vec3D(0.5, 0.5, 0.5),new Vec3D(0.5, -0.5, -0.5)},
                    new int[] {0,1,2}),
            MeshFragmentVec3D.CreateMeshFragment( //0
                    new Vec3D[]{ new Vec3D(-0.5,-0.5,-0.5), new Vec3D(0.5, 0.5, 0.5),new Vec3D(0.5, -0.5, -0.5), new Vec3D(-0.5, -0.5, 0.5)},
                    new int[] {0,1,2, 2,0,3}),
        };

        [Test]
        public void DeshellDefaultCube()
        {
            string defaultCubeJson = "{\"vertices\":[{\"x\":-0.5,\"y\":0.5,\"z\":0.5},{\"x\":-0.5,\"y\":-0.5,\"z\":0.5},{\"x\":0.5,\"y\":0.5,\"z\":0.5},{\"x\":-0.5,\"y\":-0.5,\"z\":-0.5},{\"x\":0.5,\"y\":-0.5,\"z\":-0.5},{\"x\":0.5,\"y\":0.5,\"z\":-0.5},{\"x\":0.5,\"y\":-0.5,\"z\":0.5},{\"x\":-0.5,\"y\":0.5,\"z\":-0.5}],\"triangles\":[0,1,2,3,4,1,4,5,6,5,4,7,7,3,0,5,7,2,1,6,2,4,6,1,5,2,6,4,3,7,3,1,0,7,0,2]}";
            MeshFragmentVec3D defaultCube = (MeshFragmentVec3D)JsonConvert.DeserializeObject(defaultCubeJson, typeof(MeshFragmentVec3D));
            //preconditions
            Assert.AreEqual(8, defaultCube.Vertices.Length);
            Assert.AreEqual(36, defaultCube.Triangles.Length);
            MeshFragmentVec3D deshelled = Grid6Sides.DeshelledMeshFragmentVec3D(defaultCube);
            Assert.AreEqual(0, deshelled.Vertices.Length);
            Assert.AreEqual(0, deshelled.Triangles.Length);
            Assert.AreEqual(MeshFragmentVec3D.Empty, deshelled);
        }

        public static IEnumerable<TestCaseData> intactAfterDeshell_testcases = new TestCaseData[]
        {
            new TestCaseData(fragments[0])
        };

        [Obsolete("Remove redundand")]
        [TestCaseSource(nameof(intactAfterDeshell_testcases))]
        public void IntactAfterDeshell_Tests(MeshFragmentVec3D mesh)
        {
            MeshFragmentVec3D deshelled = Grid6Sides.DeshelledMeshFragmentVec3D(mesh);

            deshelled.ShouldEqual(mesh);

            //TODO remove after next successfull run
            deshelled.Vertices.ShouldEqual(mesh.Vertices);
            deshelled.Triangles.ShouldEqual(mesh.Triangles);
        }

        [TestCase(1, 0)]
        public void DeshellSuccess_Tests(int beforeDeshellingMeshIndex, int afterDeshellingMeshIndex)
        {
            MeshFragmentVec3D beforeDeshelling = fragments[beforeDeshellingMeshIndex];
            MeshFragmentVec3D deshelled = Grid6Sides.DeshelledMeshFragmentVec3D(beforeDeshelling);
            MeshFragmentVec3D expected = fragments[afterDeshellingMeshIndex];

            deshelled.ShouldEqual(expected);

            //TODO remove after next successfull run
            deshelled.Vertices.ShouldEqual(expected.Vertices);
            deshelled.Triangles.ShouldEqual(expected.Triangles);
        }

        [TestCase(1, 1)]
        public void DeshelNotEqualSelfAfter_Tests(int beforeDeshellingMeshIndex, int afterDeshellingMeshIndex)
        {
            Assert.Throws<AssertionException>(() => DeshellSuccess_Tests(beforeDeshellingMeshIndex, afterDeshellingMeshIndex));
        }
    }
}