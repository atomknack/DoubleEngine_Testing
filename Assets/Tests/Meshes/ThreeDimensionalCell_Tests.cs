using DoubleEngine.Atom;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtomTests
{
    [TestFixture]
    public class ThreeDimensionalCell_Tests
    {
        [TestCase(0,0,0,0,0,0)]
        [TestCase(99, 68, 8, 99, 68, 8)]
        [TestCase(-99, 68, 2, -99, 68, 2)]
        [TestCase(1, 1, 1, 1, 1, 1)]
        [TestCase(1, 2, 3, 1, 2, 3)]
        public void AreEqual(short meshID, byte Grid6SidesCachedIndex, byte material, short otherMeshID, byte otherGridSidesIndex, byte otherMaterial)
        {
            Assert.AreEqual(ThreeDimensionalCell.Create(meshID, Grid6SidesCachedIndex, material),
                ThreeDimensionalCell.Create(otherMeshID, otherGridSidesIndex, otherMaterial));
        }


        [TestCase(0, 0, 0, 0, 0, 1)]
        [TestCase(0, 0, 0, 0, 1, 0)]
        [TestCase(0, 0, 0, 1, 0, 0)]
        [TestCase(0, 0, 1, 0, 0, 0)]
        [TestCase(0, 1, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, -1, 0, 0)]
        [TestCase(-1, 0, 0, 1, 0, 0)]
        [TestCase(0, 0, 0, -1, 0, 0)]
        [TestCase(-1, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 3, 2, 1)]
        [TestCase(1, 2, 3, 0, 0, 0)]
        public void AreNotEqual(short meshID, byte Grid6SidesCachedIndex, byte material, short otherMeshID, byte otherGridSidesIndex, byte otherMaterial)
        {
            Assert.AreNotEqual(ThreeDimensionalCell.Create(meshID, Grid6SidesCachedIndex, material),
                ThreeDimensionalCell.Create(otherMeshID, otherGridSidesIndex, otherMaterial));
        }


    }

}