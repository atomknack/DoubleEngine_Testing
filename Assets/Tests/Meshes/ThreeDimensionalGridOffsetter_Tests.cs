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
using FluentAssertions;
using VectorCore;

namespace AtomTests
{
    public class ThreeDimensionalGridOffsetter_Tests
    {
        public static TestCaseData[] Vec3I_Offsetter_Offset_Vector_VectorIn_VectorOut = new[] {
            new TestCaseData(new Vec3I(0,0,0),  new Vec3I(0,0,0), new Vec3I(0,0,0), new Vec3I(0,0,0)),
            new TestCaseData(new Vec3I(0,0,0), new Vec3I(1,2,3), new Vec3I(1,2,3), new Vec3I(1,2,3)),
            new TestCaseData(new Vec3I(1,1,1), new Vec3I(1,2,3), new Vec3I(2,3,4), new Vec3I(0,1,2)),
            new TestCaseData(new Vec3I(-1,-1,-1), new Vec3I(1,2,3), new Vec3I(0,1,2), new Vec3I(2,3,4)),
            new TestCaseData(new Vec3I(3,2,5),  new Vec3I(10,20,30), new Vec3I(13,22,35), new Vec3I(7,18,25)),
            new TestCaseData(new Vec3I(3,-9,5), new Vec3I(-10,-20,-30), new Vec3I(-7,-29,-25), new Vec3I(-13,-11,-35)),
            new TestCaseData(new Vec3I(1,2,3),  new Vec3I(1,2,3),   new Vec3I(2,4,6), new Vec3I(0,0,0)),
            new TestCaseData(new Vec3I(-3,-2,-1), new Vec3I(1,2,3), new Vec3I(-2,0,2), new Vec3I(4,4,4)),
            };

        IThreeDimensionalGrid grid_30_40_45;
        ThreeDimensionalOneElementTestingGrid oneElementGrid;
        IThreeDimensionalGrid iOneElementTestGrid;

        [OneTimeSetUp]
        public void Init()
        {
            grid_30_40_45 = ThreeDimensionalGrid.Create(30, 40, 45);
            oneElementGrid = new ThreeDimensionalOneElementTestingGrid();
            iOneElementTestGrid = oneElementGrid;
        }

        [Test]
        [TestCaseSource(nameof(Vec3I_Offsetter_Offset_Vector_VectorIn_VectorOut))]
        public void SetOffset_in_and_out_tests( Vec3I offset, Vec3I vector, Vec3I expectedIn, Vec3I expectedOut )
        {
            ThreeDimensionalGridOffsetter offsetter = ThreeDimensionalGridOffsetter.Create(grid_30_40_45);
            offsetter.SetOffset(offset);

            offsetter.ProjectCoordinateInsideGrid(vector).Should().Be(expectedIn);
            offsetter.ProjectCoordinateFromGridToOutside(vector).Should().Be(expectedOut);
        }
        [Test]
        [TestCaseSource(nameof(Vec3I_Offsetter_Offset_Vector_VectorIn_VectorOut))]
        public void SetOffset_OneElementGrid_tests(Vec3I offset, Vec3I vector, Vec3I expectedIn, Vec3I _)
        {
            iOneElementTestGrid.Clear();
            oneElementGrid.OnlyCell.Should().Be(ThreeDimensionalCell.Empty);
            oneElementGrid.OnlyCellPos.Should().Be(Vec3I.zero);

            ThreeDimensionalGridOffsetter offsetter = ThreeDimensionalGridOffsetter.Create(iOneElementTestGrid);
            offsetter.SetOffset(offset);

            var cell = ThreeDimensionalCell.Create(1,2,3);
            offsetter.UpdateCell(vector.x, vector.y, vector.z, cell);

            oneElementGrid.OnlyCellPos.Should().Be(expectedIn);

            var cells = iOneElementTestGrid.GetAllMeaningfullCells().ToArray();

            cells.Length.Should().Be(1);
            cells[0].cell.Should().Be(cell);
            cells[0].pos.Should().Be(expectedIn);

            cells = offsetter.GetAllMeaningfullCells().ToArray();

            cells.Length.Should().Be(1);
            cells[0].cell.Should().Be(cell);
            cells[0].pos.Should().Be(vector);
        }

        [Test]
        [TestCaseSource(nameof(Vec3I_Offsetter_Offset_Vector_VectorIn_VectorOut))]
        public void SetInOnlyOffset_tests(Vec3I offset, Vec3I vector, Vec3I expectedIn, Vec3I _)
        {
            ThreeDimensionalGridOffsetter offsetter = ThreeDimensionalGridOffsetter.Create(grid_30_40_45);
            offsetter.SetInOffset(offset);

            offsetter.ProjectCoordinateInsideGrid(vector).Should().Be(expectedIn);
            offsetter.ProjectCoordinateFromGridToOutside(vector).Should().Be(vector);
        }

        [Test]
        [TestCaseSource(nameof(Vec3I_Offsetter_Offset_Vector_VectorIn_VectorOut))]
        public void SetOutOnlyOffset_tests(Vec3I offset, Vec3I vector, Vec3I expectedIn, Vec3I _)
        {
            ThreeDimensionalGridOffsetter offsetter = ThreeDimensionalGridOffsetter.Create(grid_30_40_45);
            offsetter.SetOutOffset(offset);

            offsetter.ProjectCoordinateInsideGrid(vector).Should().Be(vector);
            offsetter.ProjectCoordinateFromGridToOutside(vector).Should().Be(expectedIn);
        }

        [TestCaseSource(nameof(Vec3I_Offsetter_Offset_Vector_VectorIn_VectorOut))]
        public void CreateClone_Test(Vec3I offset, Vec3I vector, Vec3I expectedIn, Vec3I expectedOut)
        {
            ThreeDimensionalGridOffsetter offsetter = ThreeDimensionalGridOffsetter.Create(grid_30_40_45);
            offsetter.SetOffset(offset);

            ThreeDimensionalGridOffsetter clone = ThreeDimensionalGridOffsetter.CreateClone(offsetter);

            System.Object.ReferenceEquals(clone, offsetter).Should().BeFalse();

            offsetter.SetOffset(offset + 1000);

            Vec3I offsetterInside2 = offsetter.ProjectCoordinateInsideGrid(vector);
            Vec3I offsetterOutside2 = offsetter.ProjectCoordinateFromGridToOutside(vector);
            offsetterInside2.Should().NotBe(expectedIn);
            offsetterOutside2.Should().NotBe(expectedOut);

            clone.ProjectCoordinateInsideGrid(vector).Should().Be(expectedIn);
            clone.ProjectCoordinateFromGridToOutside(vector).Should().Be(expectedOut);


            clone.SetOffset(offset + 3000);
            clone.ProjectCoordinateInsideGrid(vector).Should().Be(expectedIn + 3000);
            clone.ProjectCoordinateFromGridToOutside(vector).Should().Be(expectedOut - 3000);

            offsetter.ProjectCoordinateInsideGrid(vector).Should().Be(offsetterInside2);
            offsetter.ProjectCoordinateFromGridToOutside(vector).Should().Be(offsetterOutside2);
        }
    }
}
