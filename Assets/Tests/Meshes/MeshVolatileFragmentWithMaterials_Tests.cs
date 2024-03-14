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
using FluentAssertions;

using static FluentAssertions_Extensions.Assertions;
using Collections.Pooled;
using FluentAssertions_Extensions;
using CollectionLike.Pooled;
using VectorCore;

namespace AtomTests.Meshes
{

    public class MeshVolatileFragmentWithMaterials_Tests
    {
        public readonly static string coloredFoulderPath = $"{Application.dataPath}/Tests/Meshes/coloredOnlyMeshes/";
        public readonly static IEnumerable<string> coloredFilenames = Enumerable.Range(1, 15).Select(s => $"{coloredFoulderPath}{s:00}");
        //JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(fileName + ".mesh3d12");

        [TestCaseSource(nameof(coloredFilenames))]
        public void MeshVolatileFragmentCreateFromMeshFragment_Tests(string fileName)
        {
            var mesh = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3DWithMaterials>(fileName + ".mesh3d12");
            mesh.ShouldNotBeEmpty();

            MeshVolatileFragmentWithMaterials violatileMesh = MeshVolatileFragmentWithMaterials.TESTING_CreateFromWithoutCopy(
                PooledArrayStruct<Vec3D>.CreateAsCopyFromSpan(mesh.Vertices),
                PooledArrayStruct<IndexedTri>.CreateAsCopyFromSpan(mesh.Faces),
                PooledArrayStruct<byte>.CreateAsCopyFromSpan(mesh.FaceMaterials));

            //AssertMeshesHaveEqualishTriangles(mesh, violatileMesh);
            //Assert.AreContainsEqualElementsOnSamePlaces(mesh, violatileMesh);
            violatileMesh.ShouldEqual(mesh);
        }

        [TestCaseSource(nameof(coloredFilenames))]
        public void MeshVolatileFragment_CreateByCopyingFrom_Tests(string fileName)
        {
            MeshFragmentVec3DWithMaterials mesh = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3DWithMaterials>(fileName + ".mesh3d12");
            mesh.ShouldNotBeEmpty();
            MeshVolatileFragmentWithMaterials violatileMesh = MeshVolatileFragmentWithMaterials.CreateByCopyingFrom(mesh);

            //AssertMeshesHaveEqualishTriangles(mesh, violatileMesh);
            //Assert.AreContainsEqualElementsOnSamePlaces(mesh, violatileMesh);
            violatileMesh.ShouldEqual(mesh);
        }



        [TestCaseSource(nameof(coloredFilenames))]
        public void MeshVolatileFragmentTranslate_Tests(string fileName)
        {
            var mesh_1 = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3DWithMaterials>(fileName + ".mesh3d12");

            MeshVolatileFragmentWithMaterials violatileMesh = MeshVolatileFragmentWithMaterials.TESTING_CreateFromWithoutCopy(
                PooledArrayStruct<Vec3D>.CreateAsCopyFromSpan(mesh_1.Vertices),
                PooledArrayStruct<IndexedTri>.CreateAsCopyFromSpan(mesh_1.Faces),
                PooledArrayStruct<byte>.CreateAsCopyFromSpan(mesh_1.FaceMaterials));

            //Assert.AreContainsEqualElementsOnSamePlaces(mesh1, violatileMesh);
            //AssertMeshesHaveEqualishTriangles(mesh, violatileMesh);
            violatileMesh.ShouldEqual(mesh_1);

            Vec3D translation = new Vec3D(10, -5, 3);

            var mesh_2 = MeshFragmentVec3DWithMaterials.Create( mesh_1.fragment.Translated(translation), mesh_1.faceMaterials);

            violatileMesh.Translate(translation);

            //Assert.AreContainsEqualElementsOnSamePlaces(mesh2, violatileMesh);
            //AssertMeshesHaveEqualishTriangles(mesh, violatileMesh);
            violatileMesh.ShouldEqual(mesh_2);

        }
    }
}