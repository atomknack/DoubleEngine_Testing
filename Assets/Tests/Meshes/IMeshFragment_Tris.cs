using DoubleEngine;
using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AtomTests.Meshes
{
    public class IMeshFragment_Tris
    {
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/01.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/02.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/03.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/04.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/05.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/06.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/07.mesh3d12")]
        [TestCase(@"/Tests/Meshes/coloredOnlyMeshes/08.mesh3d12")]
        public static void Tris_Test(string fileName)
        {
            string fullPath = Application.dataPath + fileName;
            TestContext.WriteLine(fullPath);
            MeshFragmentVec3D mesh = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3DWithMaterials>(fullPath).fragment;
            TriVec3D[] tris = mesh.Tris();
            var vertices = mesh.Vertices;
            var faces = mesh.Faces;

            tris.Length.Should().Be(mesh.Faces.Length);
            for(int i = 0; i < tris.Length; i++)
            {
                tris[i].Should().Be(new TriVec3D(vertices[faces[i].v0], vertices[faces[i].v1], vertices[faces[i].v2]));
            }
        }
    }
}
