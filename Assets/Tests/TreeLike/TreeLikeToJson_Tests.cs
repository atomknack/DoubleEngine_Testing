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

using DoubleEngine;
using Newtonsoft.Json;
using VectorCore;
using DoubleEngine.TreeLike;
//using System.IdentityModel.Metadata;

namespace TreeLikeTests
{


public class TreeLikeToJson_Tests
{
    [Test]
    public void ToJsonAndBack_Test()
    {
        List<BaseBranch> branches = new();
            branches.Add(new OneOutBranch(10, MeshFragmentVec3D.Empty, 5, 10, TRS3D.DefaultTRS));
            branches.Add(new ThreeOutBranch(23, MeshFragmentVec3D.Empty, 1, 2,TRS3D.DefaultTRS,3, TRS3D.DefaultTRS,4,TRS3D.DefaultTRS));
            branches.Add(new OneOutBranch(33, MeshFragmentVec3D.Empty, 8, 18, TRS3D.DefaultTRS));

            string json = JsonConvert.SerializeObject(branches);
            TestContext.WriteLine(json);
        List<BaseBranch> deserialized = JsonConvert.DeserializeObject<List<BaseBranch>>(json);

            //TestContext.WriteLine($"{branches[0].GetType()} {branches[1].GetType()} {branches[2].GetType()}");

            Assert.AreEqual(branches[0].GetType(), deserialized[0].GetType());
            Assert.AreEqual(branches[1].GetType(), deserialized[1].GetType());
            Assert.AreEqual(branches[1].GetType(), deserialized[1].GetType());
            deserialized.Should().Equal(branches);

            TestContext.WriteLine(JsonConvert.SerializeObject(deserialized));
        }
    }
}