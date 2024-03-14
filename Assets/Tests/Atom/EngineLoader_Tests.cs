using AtomTests;
using DoubleEngine.Atom.Loaders;
using DoubleEngine.Atom;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FluentAssertions;
using System;
using System.IO;
using UnityEngine.TestTools;
using DoubleEngine.UHelpers;

namespace AtomTests
{
    public partial class EngineLoader_Tests
    {
        /*
        [Test]
        public void LoadFromResourcesByU()
        {
            DoubleEngineLoaderFromResources loader = new DoubleEngineLoaderFromResources("forResourcesTest_");
            EngineLoader.SetLoader(loader);
            FlatNodes.Reload();
            FlatNodes.AllDefaultNodes.Count().Should().BeGreaterThan(7);
            UThreeDimensionalCellMeshes.Reload();
            ThreeDimensionalCellMeshes.GetCount().Should().BeGreaterThan(14);
        } */
        [Test]
        public void LoadFlatNodesFormLayeredBuilderExample()
        {
            string path = LayeredBuilder_Tests.foulderPath;
            //Debug.Log(path);
            LoaderFromFiles loader = new LoaderFromFiles(path);
            //Debug.Log(loader.FlatNodesPath);
            loader.FlatNodesPath.FilePathToTest().Should().Exist();
            //Debug.Log(loader.TDCellMeshesPath);
            loader.TDCellMeshesPath.FilePathToTest().Should().Exist();

            EngineLoader.SetLoader(loader);
            FlatNodes.Reload();
            FlatNodes.AllDefaultNodes.Count().Should().BeGreaterThan(2);
            UThreeDimensionalCellMeshes.Reload();
            ThreeDimensionalCellMeshes.GetCount().Should().BeGreaterThan(2);
        }

        [Test]
        public void LoadMinimalFlatNodesAnd3DCubeMeshes()
        {
            string path = $"{Application.dataPath}/Tests/Atom/EngineLoadMinimal/";
            //Debug.Log(path);
            LoaderFromFiles loader = new LoaderFromFiles(path);
            //Debug.Log(loader.FlatNodesPath);
            loader.FlatNodesPath.FilePathToTest().Should().Exist();
            //Debug.Log(loader.TDCellMeshesPath);
            loader.TDCellMeshesPath.FilePathToTest().Should().Exist();

            EngineLoader.SetLoader(loader);
            FlatNodes.Reload();
            FlatNodes.AllDefaultNodes.Count().Should().Be(2);
            UThreeDimensionalCellMeshes.Reload();
            ThreeDimensionalCellMeshes.GetCount().Should().Be(1);
        }

        [Test]
        public void LoadNoFilesFlatNodesAnd3DCubeMeshes()
        {
            string path = $"{Application.dataPath}/Tests/Atom/EngineLoadNoFiles/";

            //Debug.Log(path);

            LoaderFromFiles loader = new LoaderFromFiles(path);
            //Debug.Log(loader.FlatNodesPath);
            loader.FlatNodesPath.FilePathToTest().Should().NotExist();
            //Debug.Log(loader.TDCellMeshesPath);
            loader.TDCellMeshesPath.FilePathToTest().Should().NotExist();

            EngineLoader.SetLoader(loader);

            Action flatNodesReload = () => FlatNodes.Reload();
            //LogAssert.Expect(LogType.Log, "Cannot Load FlatNodes file");
            flatNodesReload.Should().Throw<Exception>();

            Action tdcmReload = () => UThreeDimensionalCellMeshes.Reload();
            //LogAssert.Expect(LogType.Log, "Cannot Load ThreeDimensionalCellMeshes file");
            tdcmReload.Should().Throw<Exception>();
        }
    }
}