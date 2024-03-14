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

using FluentAssertions_Extensions;
using static FluentAssertions_Extensions.Assertions;
using DoubleEngine.Atom.Loaders;
using DoubleEngine.UHelpers;

namespace AtomTests
{
    //[NonParallelizable] all tests as of now - TestManager 1.1.33
    [TestFixture]
    public class LayeredBuilder_Tests
    {
        public readonly static string foulderPath = $"{Application.dataPath}/Tests/Meshes/testGrids/";
        public readonly static IEnumerable<string> filenames = Enumerable.Range(1, 15).Select(s => $"{foulderPath}{s}");
        public readonly static string coloredFoulderPath = $"{Application.dataPath}/Tests/Meshes/coloredTestGrids/";
        public readonly static IEnumerable<string> coloredFilenames = Enumerable.Range(1, 12).Select(s => $"{coloredFoulderPath}color{s}");

        IThreeDimensionalGrid grid;
        ThreeDimensionalBuilder builder;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            EngineLoader.SetLoader(new LoaderFromFiles(foulderPath));
            FlatNodes.Reload();
            UThreeDimensionalCellMeshes.Reload();

            //FlatNodes.LoadFromJsonFile(foulderPath + "flatnodesData.json_");
            //ThreeDimensionalCellMeshes.StaticConstructorInitIntactMeshesFromJson(foulderPath + "cellMeshesData.json_");

            grid = ThreeDimensionalGrid.Create(12, 10, 18);
            builder = ThreeDimensionalBuilder.Create(12, 10, 18);
        }

        [TestCaseSource(nameof(filenames))]
        public void LayeredGrid_Tests(string fileName)
        {
            Assert.That(fileName + ".obj", Does.Exist);
            Assert.That(fileName + ".bg10", Does.Exist);
            Assert.That(fileName + ".mesh3d10", Does.Exist);
            MeshFragmentVec3D existingMesh3d = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(fileName + ".mesh3d10");
            AssertNoDuplicateVertices(existingMesh3d.Vertices, 0.00001d);

            GridLoaderTests.LoadByDeprecatedLoaderThatIsForTestingOnly(grid, fileName + ".bg10");// ForTestingOnly_OldDeprecatedGridLoader.LoadGrid

            builder.Build(grid);
            var builded = MeshFragmentVec3D.CreateMeshFragment(builder);//builder.BuildMesh(grid);
            MeshFragmentVec3D objFragment = MeshFragmentVec3D.DeserializeFromOBJString(File.ReadAllText(fileName + ".obj"));

            CompareBuilded(existingMesh3d, builded, objFragment);
        }

        [TestCaseSource(nameof(filenames))]
        public void Decimator_Tests(string fileName)
        {
            var decimatedObjFilename = fileName + "_decimated.obj";
            var decimatedMesh3d10Filename = fileName + "_decimated.mesh3d10";
            Assert.That(decimatedObjFilename, Does.Exist);
            Assert.That(fileName + ".bg10", Does.Exist);
            Assert.That(decimatedMesh3d10Filename, Does.Exist);
            MeshFragmentVec3D existingMesh3d = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3D>(decimatedMesh3d10Filename);
            AssertNoDuplicateVertices(existingMesh3d.Vertices, 0.00001d);

            GridLoaderTests.LoadByDeprecatedLoaderThatIsForTestingOnly(grid, fileName + ".bg10");
            builder.Build(grid);
            var builded = DecimatorColored.DecimateAndReturnMeshFragmentVec3DWithMaterials(builder);
            MeshFragmentVec3D objFragment = MeshFragmentVec3D.DeserializeFromOBJString(File.ReadAllText(decimatedObjFilename));

            CompareBuilded(existingMesh3d, builded.fragment, objFragment);
        }

        private static void CompareBuilded(MeshFragmentVec3D existingMesh3d, MeshFragmentVec3D builded, MeshFragmentVec3D objFragment)
        {
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(existingMesh3d, builded);
            AssertMeshSizeEqual(existingMesh3d, builded);
            AssertMeshesContainedVerticesInSameCoordinates(existingMesh3d, builded);
            AssertMeshesHaveEqualishTriangles(existingMesh3d, builded);

            AssertNoDuplicateVertices(objFragment.Vertices, 0.0000001d);

            //Assert.AreEqual(existingMesh3d.Vertices, objFragment.Vertices);
            //Assert.AreEqual(existingMesh3d.Faces, objFragment.Faces);
            objFragment.ShouldEqual(existingMesh3d);

            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(objFragment, builded);
            AssertMeshSizeEqual(objFragment, builded);
            AssertMeshesContainedVerticesInSameCoordinates(objFragment, builded, 0.00000001d);
            AssertMeshesHaveEqualishTriangles(objFragment, builded);
        }

        [TestCaseSource(nameof(coloredFilenames))]
        public void ColoredAndDecimated_Tests(string fileName)
        {
            var fullMesh3d12Filename = fileName + ".mesh3d12";
            var decimatedMesh3d10Filename = fileName + "_decimated.mesh3d12";
            Assert.That(fileName + ".bg10", Does.Exist);
            Assert.That(fullMesh3d12Filename, Does.Exist);
            Assert.That(decimatedMesh3d10Filename, Does.Exist);
            MeshFragmentVec3DWithMaterials existingFullMesh3d = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3DWithMaterials>(fullMesh3d12Filename);
            MeshFragmentVec3DWithMaterials existingDecimatedMesh3d = JsonHelpers.LoadFromJsonFile<MeshFragmentVec3DWithMaterials>(decimatedMesh3d10Filename);

            GridLoaderTests.LoadByDeprecatedLoaderThatIsForTestingOnly(grid, fileName + ".bg10");
            //Debug.Log(String.Join(", ", grid.GetAllMeaningfullCells().ToArray()));
            //builder.BuildMesh(grid);
            builder.Build(grid);
            //var buildedColored = builder.BuildMeshWithMaterials(grid);
            var buildedColored = MeshFragmentVec3DWithMaterials.Create(builder);
            CompareBuilded(existingFullMesh3d, buildedColored);
            var buildedDecimated = DecimatorColored.DecimateAndReturnMeshFragmentVec3DWithMaterials(builder);
            CompareBuilded(existingDecimatedMesh3d, buildedDecimated);


        }
        [Obsolete("TODO compare materials")]
        private static void CompareBuilded(MeshFragmentVec3DWithMaterials existingMesh3d, MeshFragmentVec3DWithMaterials builded)
        {
            AssertNoDuplicateVertices(existingMesh3d.fragment.Vertices, 0.00001d);
            AssertMeshVerticesFacesNotZeroCorrectAndEqualSize(existingMesh3d.fragment, builded.fragment);
            AssertMeshSizeEqual(existingMesh3d.fragment, builded.fragment);
            AssertMeshesContainedVerticesInSameCoordinates(existingMesh3d.fragment, builded.fragment);
            AssertMeshesHaveEqualishTriangles(existingMesh3d.fragment, builded.fragment);
        }

    }

}