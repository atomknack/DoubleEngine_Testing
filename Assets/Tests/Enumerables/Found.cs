using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DoubleEngine;
using CollectionLike;
using CollectionLike.Enumerables;
using NUnit.Framework;

namespace Enumerables
{

    [TestFixture]
    public class Found_Tests
    {
        public static int[] arr = { 2, 3, 4, 5, 6, 7,-99, 8 , 5};
        public static int[] emplyArr = {};


        [TestCase(nameof(arr), 5, true, 3)]
        [TestCase(nameof(arr), 2, true, 0)]
        [TestCase(nameof(arr), -99, true, 6)]
        [TestCase(nameof(arr), 8, true, 7)]
        public static void Found_Test(string nameOfStaticArray, int search, bool expectedIsFound, int expectedIndex)
        {
            FoundFromStartIndex_Test(nameOfStaticArray, search, expectedIsFound, expectedIndex, 0);
        }

        [TestCase(nameof(arr), 2, true, -1, 0)]
        [TestCase(nameof(arr), 2, true, 0, 1)]
        public static void FoundFromStartIndex_Test_Throws(
            string nameOfStaticArray, int search, bool expectedIsFound, int expectedIndex, int startIndex
            )
        {
            Assert.That(
                () => FoundFromStartIndex_Test(nameOfStaticArray, search, expectedIsFound, expectedIndex, startIndex),
                Throws.TypeOf<AssertionException>()
                );
        }

        [TestCase(nameof(emplyArr), 8, false, -19999, 1)]
        public static void FoundNotThrowsIfStartIndexIsBiggerThanLastElementIndex(
            string nameOfStaticArray, int search, bool expectedIsFound, int expectedIndex, int startIndex
            )
        {
#pragma warning disable CS0618 // using array.Found and not version for spat to test
            int[] array = (int[])(typeof(Found_Tests).GetField(nameOfStaticArray).GetValue(null));
            Assert.That(
                () => Assert.IsFalse(array.Found(x => x == search, out int outFound, out int outIndexOfFound, startIndex)),
                Throws.Nothing
                );
            Assert.That(
            () => Assert.AreEqual(-1, Array.IndexOf(array, search, startIndex)),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                );
#pragma warning restore CS0618 // using array.Found and not version for spat to test
        }

        //-19999 means that this value dont used in this case
        [TestCase(nameof(arr), 5, true, 3, 3)]
        [TestCase(nameof(arr), 5, true, 8, 7)]
        [TestCase(nameof(arr), 5, true, 8, 8)]
        [TestCase(nameof(arr), 2, true, 0, 0)]
        [TestCase(nameof(arr), 2, false, -19999, 1)]
        [TestCase(nameof(arr), -99, true, 6, 6)]
        [TestCase(nameof(arr), -99, false, -19999, 7)]
        [TestCase(nameof(arr), 8, true, 7, 7)]
        [TestCase(nameof(emplyArr), 8, false, -19999, 0)]
        public static void FoundFromStartIndex_Test(string nameOfStaticArray, int search, bool expectedIsFound, int expectedIndex, int startIndex)
        {
            int[] array = (int[])(typeof(Found_Tests).GetField(nameOfStaticArray).GetValue(null));
            FoundFromStartIndex(array, search, expectedIsFound, expectedIndex, startIndex);
        }

        public static void FoundFromStartIndex(int[] array, int search, bool expectedIsFound, int expectedIndex, int startIndex)
        {
#pragma warning disable CS0618 // using array.Found and not version for spat to test
            bool actualIsFound = array.Found(x => x == search, out int outFound, out int outIndexOfFound, startIndex);
#pragma warning restore CS0618 // using array.Found and not version for spat to test
            if (expectedIsFound)
            {
                Assert.AreEqual(expectedIndex, outIndexOfFound);
                Assert.AreEqual(Array.IndexOf(array, search, startIndex), outIndexOfFound);
            }
            else
            {
                TestContext.WriteLine("value not found");
                Assert.AreEqual(-1, Array.IndexOf(array, search, startIndex));
            }
            Assert.AreEqual(expectedIsFound, actualIsFound);
        }
    }
}
