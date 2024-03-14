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
using VectorCore;

namespace AtomTests
{
    [TestFixture]
    public class ThreeDimensionalChunker_Tests
    {
        public static IEnumerable Vec3Is_Chunk8LocalNoOffset = new[] {
        new[]{new Vec3I(0,0,0), new Vec3I(0,0,0), new Vec3I(0,0,0)},
        new[]{new Vec3I(3,2,5), new Vec3I(0,0,0), new Vec3I(0,0,0) },
        new[]{new Vec3I(3,9,5), new Vec3I(0,8,0), new Vec3I(0,8,0)},
        new[]{new Vec3I(3,7,5), new Vec3I(0,0,0), new Vec3I(0,0,0)},
        };
        public static IEnumerable Vec3Is_Chunk8LocalOffset8_8_8 = new[] {
        new[]{new Vec3I(0,0,0), new Vec3I(8, 8,8), new Vec3I(0,0,0)},
        new[]{new Vec3I(3,2,5), new Vec3I(8, 8,8), new Vec3I(0,0,0)},
        new[]{new Vec3I(3,9,5), new Vec3I(8,16,8), new Vec3I(0,8,0)},
        new[]{new Vec3I(3,7,5), new Vec3I(8, 8,8), new Vec3I(0,0,0)},
        };
        public static IEnumerable Vec3Is_Chunk8LocalOffset10_20_30 = new[] {
        new[]{new Vec3I(0,0,0), new Vec3I(8, 16, 24), new Vec3I(-2,-4,-6)},
        new[]{new Vec3I(3,2,5), new Vec3I(8, 16, 32), new Vec3I(-2,-4,2)},
        new[]{new Vec3I(3,9,5), new Vec3I(8, 24, 32), new Vec3I(-2,4,2)},
        new[]{new Vec3I(3,7,5), new Vec3I(8, 24, 32), new Vec3I(-2,4,2)},
        };
        public static IEnumerable Vec3Is_Chunk8LocalOffset12_5_17 = new[] {
        new[]{new Vec3I(0,0,0), new Vec3I(8,0,0), new Vec3I(0,0,0)},
        new[]{new Vec3I(3,2,5), new Vec3I(0,0,0), new Vec3I(0,0,0) },
        new[]{new Vec3I(3,9,5), new Vec3I(0,0,0), new Vec3I(0,0,0)},
        new[]{new Vec3I(3,7,5), new Vec3I(0,0,0), new Vec3I(0,0,0)},
        };
        ThreeDimensionalChunker overlord;
        ThreeDimensionalChunker.Worker worker;
        IThreeDimensionalGrid grid_30_40_45;


        [TestCaseSource("Vec3Is_Chunk8LocalNoOffset")]
        public void Vec3Is_Chunk8LocalNoOffset_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I _)
        {
            SetUpOverlordWorkerNoOffset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.IsTrue(success);
            Assert.AreEqual(expectedChunk, chunk);
        }
        [TestCaseSource("Vec3Is_Chunk8LocalNoOffset")]
        public void Vec3Is_Chunk8LocalToGlobalNoOffset_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I expectedGlobalOffset)
        {
            SetUpOverlordWorkerNoOffset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.IsTrue(success);
            var global = worker.TESTING_GlobalOffset(chunk);
            var global2 = worker.TESTING_GlobalOffset(expectedChunk);
            Assert.AreEqual(expectedGlobalOffset, global);
            Assert.AreEqual(expectedGlobalOffset, global2);
        }
        [TestCaseSource("Vec3Is_Chunk8LocalOffset8_8_8")]
        public void Vec3Is_Chunk8LocalOffset_8_8_8_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I _)
        {
            SetUpOverlordWorker_8_8_8_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.IsTrue(success);
            Assert.AreEqual(expectedChunk, chunk);
        }
        [TestCaseSource("Vec3Is_Chunk8LocalOffset8_8_8")]
        public void Vec3Is_Chunk8LocalToGlobalOffset_8_8_8_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I expectedGlobalOffset)
        {
            SetUpOverlordWorker_8_8_8_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.IsTrue(success);
            var global = worker.TESTING_GlobalOffset(chunk);
            var global2 = worker.TESTING_GlobalOffset(expectedChunk);
            Assert.AreEqual(expectedGlobalOffset, global);
            Assert.AreEqual(expectedGlobalOffset, global2);
        }
        [TestCaseSource("Vec3Is_Chunk8LocalOffset10_20_30")]
        public void Vec3Is_Chunk8LocalOffset_10_20_30_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I _)
        {
            SetUpOverlordWorker_10_20_30_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.IsTrue(success);
            Assert.AreEqual(expectedChunk, chunk);
        }
        [TestCaseSource("Vec3Is_Chunk8LocalOffset10_20_30")]
        public void Vec3Is_Chunk8LocalToGlobalOffset_10_20_30_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I expectedGlobalOffset)
        {
            SetUpOverlordWorker_10_20_30_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.IsTrue(success);
            var global = worker.TESTING_GlobalOffset(chunk);
            var global2 = worker.TESTING_GlobalOffset(expectedChunk);
            Assert.AreEqual(expectedGlobalOffset, global);
            Assert.AreEqual(expectedGlobalOffset, global2);
        }

        public static IEnumerable Vec3Is_Chunk8NoOffset_False = new[] { //grid_30_40_45
            new[]{new Vec3I(-1,0,0)}, new[]{new Vec3I(0,-1,0)}, new[]{new Vec3I(0,0,-1)},new[]{new Vec3I(-1,-1,-1)},new[]{new Vec3I(-10,-10,-10)},
            new[]{new Vec3I( 30, 39, 44)}, new[]{new Vec3I( 29, 40, 44)}, new[]{new Vec3I( 29, 39, 45)},new[]{new Vec3I( 100, 100, 100)},
        };
        [TestCaseSource("Vec3Is_Chunk8NoOffset_False")]
        public void Vec3Is_Chunk8NoOffset_False_tests(Vec3I cellCoord)
        {
            SetUpOverlordWorkerNoOffset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            TestContext.WriteLine(chunk);
            Assert.IsFalse(success);
        }
        public static IEnumerable Vec3Is_Chunk8Offset8_8_8_False = new[] { //grid_30_40_45
            new[]{new Vec3I(-9,-8,-8)}, new[]{new Vec3I(-8,-9,-8)}, new[]{new Vec3I(-8,-8,-9)},new[]{new Vec3I(-9,-9,-9)},new[]{new Vec3I(-100,-100,-100)},
            new[]{new Vec3I(22,31,36)}, new[]{new Vec3I(21,32,36)}, new[]{new Vec3I(21,31,37)},new[]{new Vec3I(22,32,37)},new[]{new Vec3I(100,100,100)},
        };
        [TestCaseSource("Vec3Is_Chunk8Offset8_8_8_False")]
        public void Vec3Is_Chunk8Offset_8_8_8_False_tests(Vec3I cellCoord)
        {
            SetUpOverlordWorker_8_8_8_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            TestContext.WriteLine(chunk);
            Assert.IsFalse(success);
        }
        public static IEnumerable Vec3Is_Chunk8Offset10_20_30_False = new[] { //grid_30_40_45
            new[]{new Vec3I(-11,-20,-30)}, new[]{new Vec3I(-10,-21,-30)}, new[]{new Vec3I(-10,-20,-31)},new[]{new Vec3I(-11,-21,-31)},new[]{new Vec3I(-100,-100,-100)},
            new[]{new Vec3I(20,19,14)}, new[]{new Vec3I(19,20,14)}, new[]{new Vec3I(19,19,15)},new[]{new Vec3I(20,20,15)},new[]{new Vec3I(100,100,100)},
        };
        [TestCaseSource("Vec3Is_Chunk8Offset10_20_30_False")]
        public void Vec3Is_Chunk8Offset_10_20_30_False_tests(Vec3I cellCoord)
        {
            SetUpOverlordWorker_10_20_30_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            TestContext.WriteLine(chunk);
            Assert.IsFalse(success);
        }
        public static IEnumerable Vec3Is_Chunk8NoOffset_True = new[] { new[]{new Vec3I(0,0,0)},  new[]{new Vec3I( 29, 39, 44)},  };
        [TestCaseSource("Vec3Is_Chunk8NoOffset_True")]
        public void Vec3Is_Chunk8NoOffset_True_tests(Vec3I cellCoord)
        {
            SetUpOverlordWorkerNoOffset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            TestContext.WriteLine(chunk);
            Assert.IsTrue(success);
        }
        public static IEnumerable Vec3Is_Chunk8Offset8_8_8_True = new[] { new[]{new Vec3I(-8,-8,-8)}, new[]{new Vec3I( 21, 31, 36)}, };
        [TestCaseSource("Vec3Is_Chunk8Offset8_8_8_True")]
        public void Vec3Is_Chunk8Offset_8_8_8_True_tests(Vec3I cellCoord)
        {
            SetUpOverlordWorker_8_8_8_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            TestContext.WriteLine(chunk);
            Assert.IsTrue(success);
        }
        public static IEnumerable Vec3Is_Chunk8Offset10_20_30_True = new[] { new[]{new Vec3I(-10,-20,-30)}, new[]{new Vec3I( 19, 19, 14)}, };
        [TestCaseSource("Vec3Is_Chunk8Offset10_20_30_True")]
        public void Vec3Is_Chunk8Offset_10_20_30_True_tests(Vec3I cellCoord)
        {
            SetUpOverlordWorker_10_20_30_Offset();
            bool success = overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            TestContext.WriteLine(chunk);
            Assert.IsTrue(success);
        }

        /*
        [TestCaseSource("Vec3Is_Chunk8LocalOffset12_5_17")]
        public void Vec3Is_Chunk8LocalOffset_12_5_17_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I _)
        {
            SetUpOverlordWorker_12_5_17_Offset();
            overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            Assert.AreEqual(expectedChunk, chunk);
        }
        [TestCaseSource("Vec3Is_Chunk8LocalOffset12_5_17")]
        public void Vec3Is_Chunk8LocalToGlobalOffset_12_5_17_tests(Vec3I cellCoord, Vec3I expectedChunk, Vec3I expectedGlobalOffset)
        {
            SetUpOverlordWorker_12_5_17_Offset();
            overlord.TESTING_TryGetChunkCoordFromWordCoord(cellCoord, out var chunk);
            var global = worker.TESTING_GlobalOffset(chunk);
            var global2 = worker.TESTING_GlobalOffset(expectedChunk);
            Assert.AreEqual(expectedGlobalOffset, global);
            Assert.AreEqual(expectedGlobalOffset, global2);
        }
        */
        [OneTimeSetUp]
        public void Init()
        {
            grid_30_40_45 = ThreeDimensionalGrid.Create(30, 40, 45);
        }
        public void SetUpOverlordWorkerNoOffset()
        {
            overlord = new ThreeDimensionalChunker(8, grid_30_40_45, Vec3I.zero);
            worker = (ThreeDimensionalChunker.Worker)overlord.CreateSubordinateWorker();
        }
        public void SetUpOverlordWorker_8_8_8_Offset()
        {
            overlord = new ThreeDimensionalChunker(8, grid_30_40_45, new Vec3I(8, 8, 8));
            worker = (ThreeDimensionalChunker.Worker)overlord.CreateSubordinateWorker();
        }
        public void SetUpOverlordWorker_10_20_30_Offset()
        {
            overlord = new ThreeDimensionalChunker(8, grid_30_40_45, new Vec3I(10, 20, 30));
            worker = (ThreeDimensionalChunker.Worker)overlord.CreateSubordinateWorker();
        }
        public void SetUpOverlordWorker_12_5_17_Offset()
        {
            overlord = new ThreeDimensionalChunker(8, grid_30_40_45, new Vec3I(12, 5, 17));
            worker = (ThreeDimensionalChunker.Worker)overlord.CreateSubordinateWorker();
        }
    }
}
