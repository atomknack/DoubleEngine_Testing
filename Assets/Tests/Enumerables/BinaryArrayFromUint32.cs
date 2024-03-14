using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DoubleEngine;
using NUnit.Framework;

namespace Enumerables
{

    [TestFixture]
    public static class BinaryArrayFromUint
    {
        [Test]
        public static void Num12345678()
        {
            int[] expected = { 
                0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,
                1,1,1,0,0,0,0,1 
            };
            uint num = 225;
            TestContext.WriteLine(String.Join(",", expected));
            TestContext.WriteLine(String.Join(",", num.ToBinaryAsArray32()));
            Assert.AreEqual(32, expected.Length);
            Assert.AreEqual(32, num.ToBinaryAsArray32().Length);
            Assert.AreEqual(expected, num.ToBinaryAsArray32());
        }

        [Test]
        [TestCase(0,   "00000000000000000000000000000000")]
        [TestCase(225, "00000000000000000000000011100001")]
        [TestCase(256, "00000000000000000000000100000000")]
        [TestCase(512, "00000000000000000000001000000000")]
        [TestCase(513, "00000000000000000000001000000001")]
        public static void NumString(int singnedValue, string binaryString)
        {
            UInt32 value = (UInt32)singnedValue;
            Assert.AreEqual(binaryString, value.ToBinaryAsString32());
        }

        [Test]
        [TestCase(0)]
        [TestCase(225)]
        [TestCase(256)]
        [TestCase(34997322)]
        [TestCase(1499799422)]
        public static void BinaryArrayFromUint32(int singnedValue)
        {
            UInt32 value = (UInt32)singnedValue;
            string expectedString = Convert.ToString(value, 2).PadLeft(32, '0');
            int[] intExpected = expectedString.Select(x => Int32.Parse(x.ToString())).ToArray();
            Assert.AreEqual(32, intExpected.Length);

            int[] intStr = value.ToBinaryAsString32().Select(x=> Int32.Parse(x.ToString())).ToArray();
            int[] intArr = value.ToBinaryAsArray32();

            Assert.AreEqual(32, intStr.Length);
            Assert.AreEqual(32, intArr.Length);
            Assert.AreEqual(intExpected, intArr);
            Assert.AreEqual(intArr, intStr);
        }

    }
}