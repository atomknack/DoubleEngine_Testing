using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using DoubleEngine;


namespace Dictionaries
{

    [TestFixture]
    public class Remapper_Tests
    {
        RemapperInt remapperForClearingTests;
        [OneTimeSetUp]
        public void OneTimeInit()
        {
            remapperForClearingTests = new RemapperInt();
        }

        public static TestCaseData[] arraysToRemap_Data = new TestCaseData[] {
            new TestCaseData(new int[]{ 0,1,2,2,1,0}, new int[]{0, 3, 5, 5, 3, 0}),
            new TestCaseData(new int[]{ 0,1,2,2,1,3,1,4,3}, new int[]{5, 3, 1, 1, 3, 7, 3, 8, 7}),
            new TestCaseData(new int[]{ 0,1,2,3,4,5,0,1}, new int[]{0, 3, 5, 7, 9, 1, 0, 3}),

        };

        [TestCaseSource(nameof(arraysToRemap_Data))]
        public void RemapArrayOfValues(int[] expected, int[] toRemap)
        {
            RemapperInt remapper = new RemapperInt();
            remapper.AddMany(toRemap);

            Assert.AreEqual(expected, remapper.RemapExistingItems(toRemap));
        }

    }
}
