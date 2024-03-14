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

namespace AtomTests
{

public partial class EngineLoader_Tests
{
        [TestCase("magicLanJello_cellMeshesData")]
        [TestCase("magicLanJello_flatnodesData")]
        [TestCase("forResourcesTest_cellMeshesData")]
        [TestCase("forResourcesTest_flatnodesData")]
        public void EncoderEnc3FromResourcesTest(string resourceName)
        {
            TextAsset textAsset = Resources.Load(resourceName) as TextAsset;
            TextAsset enc3 = Resources.Load(resourceName + ".enc3") as TextAsset;
            EncoderEnc3Test(enc3.bytes, textAsset.text);
        }
        [TestCase("magicLanJello_cellMeshesData")]
        [TestCase("magicLanJello_flatnodesData")]
        [TestCase("forResourcesTest_cellMeshesData")]
        [TestCase("forResourcesTest_flatnodesData")]
        public void DecoderEnc3AsStringFromResourcesTest(string resourceName)
        {
            TextAsset textAsset = Resources.Load(resourceName) as TextAsset;
            TextAsset enc3 = Resources.Load(resourceName + ".enc3") as TextAsset;
            DecoderEnc3Test(textAsset.text, enc3.bytes);
        }

        public void EncoderEnc3Test(byte[] expected, string text)
        {
            byte[] actual = EncodersTB.EncodeAsENC3(text);
            actual.Should().Equal(expected);
        }
        public void DecoderEnc3Test(string expected, byte[] bytes)
        {
            string actual = EncodersTB.DecodeAsENC3(bytes);
            actual.Should().Be(expected);
        }
        [TestCase("forResourcesTest_cellMeshesData")]
        [TestCase("forResourcesTest_flatnodesData")]
        public void EncoderEnc2FromResourcesTest(string resourceName)
        {
            TextAsset textAsset = Resources.Load(resourceName) as TextAsset;
            TextAsset enc2 = Resources.Load(resourceName + ".enc2") as TextAsset;
            EncoderEnc2Test(enc2.bytes, textAsset.text);
        }
        [TestCase("forResourcesTest_cellMeshesData")]
        [TestCase("forResourcesTest_flatnodesData")]
        public void DecoderEnc2AsStringFromResourcesTest(string resourceName)
        {
            TextAsset textAsset = Resources.Load(resourceName) as TextAsset;
            TextAsset enc2 = Resources.Load(resourceName + ".enc2") as TextAsset;
            DecoderEnc2Test(textAsset.text, enc2.bytes);
        }

        public void EncoderEnc2Test(byte[] expected, string text)
        {
            byte[] actual = EncodersTB.EncodeAsENC2(text);
            actual.Should().Equal(expected);
        }
        public void DecoderEnc2Test(string expected, byte[] bytes)
        {
            string actual = EncodersTB.DecodeAsENC2(bytes);
            actual.Should().Be(expected);
        }

    }

}