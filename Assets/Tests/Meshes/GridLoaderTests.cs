using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using DoubleEngine.Atom;
using FluentAssertions_Extensions;
using static FluentAssertions_Extensions.Assertions;
using DoubleEngine.Atom.Loaders;
using FluentAssertions;
using DoubleEngine.UHelpers;

namespace AtomTests
{
    [TestFixture]
    public class GridLoaderTests
    {
        IThreeDimensionalGrid gridForOldBinary;
        IThreeDimensionalGrid gridForNewBinary;
        IThreeDimensionalGrid gridForTextNewBinary;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var foulderPath = LayeredBuilder_Tests.foulderPath;
            Debug.Log(foulderPath);

            EngineLoader.SetLoader(new LoaderFromFiles(foulderPath));
            FlatNodes.Reload();
            UThreeDimensionalCellMeshes.Reload();

            //FlatNodes.LoadFromJsonFile(foulderPath + "flatnodesData.json_");
            //ThreeDimensionalCellMeshes.StaticConstructorInitIntactMeshesFromJson(foulderPath + "cellMeshesData.json_");


            gridForOldBinary = ThreeDimensionalGrid_WithSimpleBuilder.Create(12, 10, 18);
            gridForNewBinary = ThreeDimensionalGrid.Create(12, 10, 18);
            gridForTextNewBinary = ThreeDimensionalGrid.Create(12, 10, 18);
        }

        static string tempFilePath2789 = Common.TempTestData_Path + "/tempFileforthisTest2789.txt";
        //static string tempFilePath2790 = Common.TempTestData_Path + "/tempFileforthisTest2790.txt";

        public static void LoadByDeprecatedLoaderThatIsForTestingOnly(IThreeDimensionalGrid grid, string filepath)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            ForTestingOnly_OldDeprecatedGridLoader.LoadGrid(grid, filepath);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [SetUp]
        public void DeleteTempFile()
        {
            File.Delete(tempFilePath2789);
            //File.Delete(tempFilePath2790);
        }

        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.filenames))]
        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
        public void FileExists_Tests(string fileName)
        {
            Assert.That(fileName + ".bg10", Does.Exist);
        }

        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.filenames))]
        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
        public void CanLoadBG10AndBG33(string fileName)
        {
            gridForNewBinary.Clear();
            var path = fileName + ".bg10";
            GridLoaderBinary.LoadGrid(gridForNewBinary, path);
            gridForOldBinary.Clear();
            var pathBG33 = fileName + ".bg33";
            GridLoaders.LoadGrid(gridForOldBinary, pathBG33);
            var cellsBG33 = gridForOldBinary.GetAllMeaningfullCells();
            cellsBG33.Should().HaveCountGreaterThanOrEqualTo(1);
            cellsBG33.Should().Equal(gridForNewBinary.GetAllMeaningfullCells());
        }

    [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
        public void NewLoaderCanLoadThenSave(string fileName)
        {
            gridForNewBinary.Clear();
            var path = fileName + ".bg10";
            GridLoaderBinary.LoadGrid(gridForNewBinary, path);
            GridLoaderBinary.SaveGrid(gridForNewBinary, tempFilePath2789);
            string original = File.ReadAllText(path);
            string saved = File.ReadAllText(tempFilePath2789);
            original.Should().Be(saved);
        }
        [Test]
        public void NewLoaderCanLoadThenSaveTwice()
        {
            NewLoaderCanLoadThenSave("C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/coloredTestGrids/color7");
            NewLoaderCanLoadThenSave("C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/coloredTestGrids/color1");
        }

        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.filenames))]
        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
        public void NewLoaderCanSave(string fileName)
        {
            gridForNewBinary.Clear();
            gridForOldBinary.Clear();
            GridLoaderBinary.LoadGrid(gridForNewBinary, fileName + ".bg10");
            GridLoaderBinary.SaveGrid(gridForNewBinary, tempFilePath2789);
            LoadByDeprecatedLoaderThatIsForTestingOnly(gridForOldBinary, tempFilePath2789);
            /*
            GridLoaderTextStream.SaveGrid(gridForOldBinary, tempFilePath2790);

            var tempCopyName = tempFilePath2790 + "_" + fileName.Split('/').Last();
            File.Delete(tempCopyName);
            File.Copy(tempFilePath2790, tempCopyName); //fileName.Substring(Math.Max(0, fileName.Length - 2))); //fileName.Skip(Math.Max(0, collection.Count() - N)));
            
            */
            AssertGridsAreEqual(gridForOldBinary, gridForNewBinary);
        }

        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.filenames))]
        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
        public void NewTextLoaderCanSaveAndLoad(string fileName)
        {
            gridForNewBinary.Clear();
            gridForTextNewBinary.Clear();
            gridForOldBinary.Clear();
            GridLoaderBinary.LoadGrid(gridForNewBinary, fileName + ".bg10");
            GridLoaderTextStream.SaveGrid(gridForNewBinary, tempFilePath2789);
            GridLoaderTextStream.LoadGrid(gridForTextNewBinary, tempFilePath2789);
            LoadByDeprecatedLoaderThatIsForTestingOnly(gridForOldBinary, fileName + ".bg10");

            var tempCopyName = tempFilePath2789 + "_" + fileName.Split('/').Last();
            File.Delete(tempCopyName);
            File.Copy(tempFilePath2789, tempCopyName); //fileName.Substring(Math.Max(0, fileName.Length - 2))); //fileName.Skip(Math.Max(0, collection.Count() - N)));
            
            AssertGridsAreEqual(gridForOldBinary, gridForTextNewBinary);
        }


        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.filenames))]
        [TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
        public void NewLoaderCanLoad(string fileName)
        {
            //Assert.That(fileName + ".bg10", Does.Exist);
            LoadByDeprecatedLoaderThatIsForTestingOnly(gridForOldBinary, fileName + ".bg10");
            GridLoaderBinary.LoadGrid(gridForNewBinary, fileName + ".bg10");
            AssertGridsAreEqual(gridForOldBinary, gridForNewBinary);
        }


        public void AssertGridsAreEqual(IThreeDimensionalGrid grid, IThreeDimensionalGrid other)
        {
            Assert.AreEqual(grid.GetDimensions(), other.GetDimensions());
            Assert.AreEqual(grid.GetAllMeaningfullCells(), other.GetAllMeaningfullCells());
            Assertions.AreEqual(grid, other);
        }

        
[TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.filenames))]
[TestCaseSource(typeof(LayeredBuilder_Tests), nameof(LayeredBuilder_Tests.coloredFilenames))]
public void CanLoadBG10AndSaveAsBG33(string fileName)
{
    gridForNewBinary.Clear();
    var path = fileName + ".bg10";
    GridLoaderBinary.LoadGrid(gridForNewBinary, path);
    var pathBG33 = fileName + ".bg33";
    GridLoaders.SaveGrid(gridForNewBinary, pathBG33);
}
    }
}