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
using VectorCore;
//using static Assertions;

namespace AtomTests.Meshes
{
    [TestFixture]
    public class MeshFragmentEquality
    {
        [Test]
        public void MeshFragmentVec3DEmptyEqualsCreatedEmpty()
        {
            var mesh = MeshFragmentVec3D.CreateMeshFragment(new Vec3D[0], new int[0]);
            Assert.True(((object)MeshFragmentVec3D.Empty).Equals((object)mesh));
            Assert.True(MeshFragmentVec3D.Empty.Equals(mesh));
            Assert.True(MeshFragmentVec3D.Empty == mesh);
            Assert.True(((object)mesh).Equals((object)MeshFragmentVec3D.Empty));
            Assert.True(mesh.Equals(MeshFragmentVec3D.Empty));
            Assert.True(mesh == MeshFragmentVec3D.Empty);
        }
        /*[Test]
        public void MeshFragmentVec3DEmptyEqualsMeshWithoutVertices()
        {
            var mesh = MeshFragmentVec3D.CreateMeshFragment(new Vec3D[0], new int[3] {0,0,0});
            Assert.True(((object)MeshFragmentVec3D.Empty).Equals((object)mesh));
            Assert.True(MeshFragmentVec3D.Empty.Equals(mesh));
            Assert.True(MeshFragmentVec3D.Empty == mesh);
            Assert.True(((object)mesh).Equals((object)MeshFragmentVec3D.Empty));
            Assert.True(mesh.Equals(MeshFragmentVec3D.Empty));
            Assert.True(mesh == MeshFragmentVec3D.Empty);
        }*/

        [Test]
        public void MeshFragmentVec3DNotEqualNull()
        {
            Assert.False(((object)MeshFragmentVec3D.Empty).Equals(null));
            Assert.False(MeshFragmentVec3D.Empty.Equals(null));
            Assert.False(MeshFragmentVec3D.Empty == null);
            Assert.False(null == MeshFragmentVec3D.Empty);
        }
        [Test]
        public void MeshFragmentVec2DNotEqualNull()
        {
            Assert.False(((object)MeshFragmentVec2D.Empty).Equals(null));
            Assert.False(MeshFragmentVec2D.Empty.Equals(null));
            Assert.False(MeshFragmentVec2D.Empty == null);
            Assert.False(null == MeshFragmentVec2D.Empty);
        }

        [Test]
        public void MeshFragmentVec3DEmptyEqualsSelf()
        {
            Assert.True(((object)MeshFragmentVec3D.Empty).Equals((object)MeshFragmentVec3D.Empty));
            Assert.True(MeshFragmentVec3D.Empty.Equals(MeshFragmentVec3D.Empty));
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(MeshFragmentVec3D.Empty == MeshFragmentVec3D.Empty);
#pragma warning restore CS1718 // Comparison made to same variable
        }
        [Test]
        public void MeshFragmentVec2DEmptyEqualsSelf()
        {
            Assert.True(((object)MeshFragmentVec2D.Empty).Equals((object)MeshFragmentVec2D.Empty));
            Assert.True(MeshFragmentVec2D.Empty.Equals(MeshFragmentVec2D.Empty));
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(MeshFragmentVec2D.Empty == MeshFragmentVec2D.Empty);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Test]
        public void EmptyMeshFragmentVec3DNotEqualsEmptyMeshFragmentVec2D()
        {
            Assert.False(((object)MeshFragmentVec3D.Empty).Equals((object)MeshFragmentVec2D.Empty));
            Assert.False(((object)MeshFragmentVec2D.Empty).Equals((object)MeshFragmentVec3D.Empty));
            Assert.False(MeshFragmentVec3D.Empty.Equals(MeshFragmentVec2D.Empty));
            Assert.False(MeshFragmentVec2D.Empty.Equals(MeshFragmentVec3D.Empty));
        }
    }
}