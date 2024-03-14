using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using NUnit.Framework;
using UnityEngine;

namespace Enumerables
{

    [TestFixture]
    public class ImmutableArray_Tests
    {
        public static TestCaseData[] intArrays = {
            new TestCaseData( new int[]{0, 1, 2, 3, 4, 5, 6}),
            new TestCaseData( new int[]{1, 3, 8, 0 }),
            new TestCaseData( new int[]{-92, 8,0,0 }),
            new TestCaseData( new int[]{0, 0, 0 }),
            new TestCaseData( new int[]{89, -78, 1 }),
            new TestCaseData( new int[]{5, 2, 3, 4, 5 }),
        };

        [TestCaseSource("intArrays")]
        public void ImmutableArray_from_Array(int[] intArray)
        {
            ImmutableArray<int> immutable = intArray.ToImmutableArray();
            Assert.AreEqual(intArray.Length, immutable.Length);
            Assert.IsTrue(immutable.Length > 2);
            Assert.AreEqual(intArray, immutable);
        }
    }
}
