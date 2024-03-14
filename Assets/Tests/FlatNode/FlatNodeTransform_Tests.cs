using System.Collections;
using System.Collections.Generic;
using DoubleEngine;
using DoubleEngine.Atom;
using NUnit.Framework;
using UnityEngine;

namespace AtomTests
{

    [TestFixture]
    public class FlatNodeTransform_Tests
    {
        [OneTimeSetUp]
        public static void Init()
        {
            DoubleEngine.__GlobalStatic.Init(Application.dataPath, Debug.Log);
        }

        private struct TestTransformCombination
        {
            public readonly FlatNodeTransform node;
            public readonly FlatNodeTransform transformedBy;
            public readonly FlatNodeTransform result;
            public TestTransformCombination(FlatNodeTransform node, FlatNodeTransform transformedBy, FlatNodeTransform result)
            {
                this.node = node;
                this.transformedBy = transformedBy;
                this.result = result;
            }
        }

        [Test]
        public void FlatNodeTransformDefault()
        {
            Assert.IsTrue(FlatNodeTransform.Default == new FlatNodeTransform(PerpendicularAngle.a0, false));
            Assert.IsTrue(FlatNodeTransform.Default != FlatNodeTransform.Default.InvertX());
            Assert.IsTrue(FlatNodeTransform.Default != FlatNodeTransform.Default.Rotate(PerpendicularAngle.a90));
            Assert.IsTrue(FlatNodeTransform.Default.Rotate(PerpendicularAngle.a0) == FlatNodeTransform.Default);
            Assert.IsTrue(FlatNodeTransform.Default.Rotate(PerpendicularAngle.a0) == new FlatNodeTransform(PerpendicularAngle.a0, false));

            Assert.IsFalse(FlatNodeTransform.Default == FlatNodeTransform.Default.InvertX());
            Assert.IsFalse(FlatNodeTransform.Default == FlatNodeTransform.Default.Rotate(PerpendicularAngle.a90));
        }


        [Test]
        public void FlatNodeTransform_AllTransformations()
        {
            string jsonfilePath = JsonHelpers.ApplicationDataPath + "/Tests/FlatNode/allFlatNodeTransforms.Json";
            //JsonHelpers.SaveToJsonFile(FlatNodeTransform.allFlatNodeTransforms, jsonfilePath);
            FlatNodeTransform[] allTransforms = FlatNodeTransform.allFlatNodeTransforms;
            FlatNodeTransform[] loadedTransforms = JsonHelpers.LoadFromJsonFile<FlatNodeTransform[]>(jsonfilePath);

            Assert.AreEqual(8, loadedTransforms.Length);
            Assert.AreEqual(loadedTransforms.Length, allTransforms.Length);
            Assert.AreEqual(loadedTransforms, allTransforms);
        }
        [Test]
        public void FlatNodeTransform_AllTransformationsCombinations()
        {
            FlatNodeTransform[] allTransforms = FlatNodeTransform.allFlatNodeTransforms;
            List<TestTransformCombination> allTransformsCombinationsList = new();
            for (int i = 0; i < allTransforms.Length; i++)
                for (int j = 0; j < allTransforms.Length; j++)
                {
                    FlatNodeTransform node = allTransforms[i];
                    FlatNodeTransform by = allTransforms[j];
                    FlatNodeTransform result = node.Transform(by);
                    allTransformsCombinationsList.Add(new TestTransformCombination(node, by, result));
                }
            string jsonCombinationsfilePath = JsonHelpers.ApplicationDataPath + "/Tests/FlatNode/allFlatNodeTransformCombinations.Json";
            //JsonHelpers.SaveToJsonFile(allTransformsCombinationsList, jsonCombinationsfilePath);
            List<TestTransformCombination> loadedTransformCombinations =
                JsonHelpers.LoadFromJsonFile<List<TestTransformCombination>>(jsonCombinationsfilePath);

            Assert.AreEqual(64, loadedTransformCombinations.Count);
            Assert.AreEqual(loadedTransformCombinations.Count, allTransformsCombinationsList.Count);
            Assert.AreEqual(loadedTransformCombinations, allTransformsCombinationsList);
        }


        [Test]
        public void FlatNodeTransform_Rotate()
        {

            Assert.IsTrue(
                new FlatNodeTransform(PerpendicularAngle.a90, false).Rotate(PerpendicularAngle.a90) ==
                new FlatNodeTransform(PerpendicularAngle.a180, false)
                );
            Assert.IsTrue(
                new FlatNodeTransform(PerpendicularAngle.a180, false).Rotate(PerpendicularAngle.a180) ==
                new FlatNodeTransform(PerpendicularAngle.a0, false)
                );
            Assert.IsTrue(
                new FlatNodeTransform(PerpendicularAngle.a180, false).Rotate(PerpendicularAngle.a270) ==
                new FlatNodeTransform(PerpendicularAngle.a90, false)
                );
            Assert.IsTrue(
                new FlatNodeTransform(PerpendicularAngle.a270, false).Rotate(PerpendicularAngle.aNegative180) ==
                new FlatNodeTransform(PerpendicularAngle.a90, false)
                );
            Assert.IsTrue(
                new FlatNodeTransform(PerpendicularAngle.a180, false).Rotate(PerpendicularAngle.aNegative270) ==
                new FlatNodeTransform(PerpendicularAngle.a270, false)
                );
        }

        [Test]
        public void FlatNodeTransform_AllRotations()
        {
            FlatNodeTransform[] allTransforms = FlatNodeTransform.allFlatNodeTransforms;
            for (int i = 0; i < allTransforms.Length; i++)
                foreach (var angle in FlatNodeTransform.allPosibleAngles)
                    Assert.AreEqual(allTransforms[i].Rotate(angle), allTransforms[i].Transform(new FlatNodeTransform(angle, false)));
        }

        [Test]
        public void FlatNodeTransform_AllInvertX()
        {
            FlatNodeTransform[] allTransforms = FlatNodeTransform.allFlatNodeTransforms;
            for (int i = 0; i < allTransforms.Length; i++)
                Assert.AreEqual(allTransforms[i].InvertX(), allTransforms[i].Transform(new FlatNodeTransform(PerpendicularAngle.a0, true)));
        }

        [Test]
        public void FlatNodeTransform_AllInvertY()
        {
            FlatNodeTransform[] allTransforms = FlatNodeTransform.allFlatNodeTransforms;
            for (int i = 0; i < allTransforms.Length; i++)
                Assert.AreEqual(allTransforms[i].InvertY(), allTransforms[i].Transform(new FlatNodeTransform(PerpendicularAngle.a180, true)));
        }
    }

}