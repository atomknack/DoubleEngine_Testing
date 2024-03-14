using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using CollectionLike;
using CollectionLike.Enumerables;
using System.Linq;
using System;

namespace Enumerables
{

[TestFixture]
public class CyclicShifts
    {
        public static int[][] arrays_ForShift = new int[][] {
            new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8 },
            new int[]{ -99, 1, 80, -9, 234, 5, },
        };

        public static TestCaseData[] leftShifts = {
            new TestCaseData( 0, 0, new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8 } ),
            new TestCaseData( 0, 2, new int[]{ 2, 3, 4, 5, 6, 7, 8, 0, 1 } ),
            new TestCaseData( 0, 5, new int[]{ 5, 6, 7, 8, 0, 1, 2, 3, 4, } ),
            new TestCaseData( 1, 0, new int[]{ -99, 1, 80, -9, 234, 5, } ),
            new TestCaseData( 1, 2, new int[]{ 80, -9, 234, 5, -99, 1, } ),
        };

    [TestCaseSource("leftShifts")]
        public void CyclicLeftShifts_Test(int arrayIndex, int shiftLeft, int[] expected)
        {
            int[] arrayForShift = arrays_ForShift[arrayIndex];
            int[] clone = (int[])arrayForShift.Clone();
            Assert.AreEqual(clone, arrayForShift);

            Assert.AreEqual(expected, arrayForShift.RotateLeft(shiftLeft));
            Assert.AreEqual(clone, arrayForShift);

            int[] shiftBySteps = arrayForShift;
            for (int i = 0; i < shiftLeft; i++)
                shiftBySteps = shiftBySteps.CyclicLeftShift().ToArray();
            Assert.AreEqual(expected, shiftBySteps);
            Assert.AreEqual(clone, arrayForShift);

            List<int> shiftingList = new List<int>(arrayForShift);
            shiftingList.RotateLeftInplace(shiftLeft);
            TestContext.WriteLine($"initial:{arrayForShift.ItemsToString()}; expected: {expected.ItemsToString()}; listShift: {shiftingList.ItemsToString()}");
            Assert.AreEqual(expected, shiftingList);
        }

        [Test]
    public void CyclicLeftShift_Test()
    {
        int[] initial = { 1, 2, 3, 4, 5 };
        int[] leftShifted = { 2, 3, 4, 5, 1 };
        TestContext.WriteLine($"{leftShifted.ItemsToString()}; {initial.CyclicLeftShift().ItemsToString()}");
        Assert.AreEqual(leftShifted, initial.CyclicLeftShift());
    }
    [Test]
    public void CyclicRightShift_Test()
    {
        int[] initial = { 1, 2, 3, 4, 5 };
        int[] rightShifted = { 5, 1, 2, 3, 4,};
        TestContext.WriteLine($"{rightShifted.ItemsToString()}; {initial.CyclicRightShift().ItemsToString()}");
        Assert.AreEqual(rightShifted, initial.CyclicRightShift());
    }

    [Test]
    public void CyclicShiftEmptyOfNull_Test()
    {
        int[] initial = { };
        int[] shifted = { };
        TestContext.WriteLine($"{initial.ItemsToString()}; {shifted.CyclicLeftShift().ItemsToString()}");
        Assert.AreEqual(shifted, initial.CyclicLeftShift());
        Assert.AreEqual(shifted, initial.CyclicRightShift());
        Assert.AreNotEqual(null, initial.CyclicLeftShift());
        Assert.AreNotEqual(null, initial.CyclicRightShift());
        initial = null;
        shifted = null;
        TestContext.WriteLine($"{initial.ItemsToString()}; {shifted.CyclicLeftShift().ItemsToString()}");
        Assert.AreEqual(shifted, initial.CyclicLeftShift());
        Assert.AreEqual(shifted, initial.CyclicRightShift());
        Assert.AreNotEqual(new { }, initial.CyclicLeftShift());
        Assert.AreNotEqual(new { }, initial.CyclicRightShift());
    }
}
}