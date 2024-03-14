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
//using static Assertions;
using System.CodeDom.Compiler;
using System.CodeDom;

namespace AtomTests.Meshes
{
    public class SingleEdges2D_Tests
    {
        public static IEnumerable<TestCaseData> OneSubpoly()=>
            HelperFetchArrayFromJsonFile(Application.dataPath + "/Tests/Meshes/2D/Meshes2D_With_one_subpoly");
        public static IEnumerable<TestCaseData> TwoSubpoly() =>
            HelperFetchArrayFromJsonFile(Application.dataPath + "/Tests/Meshes/2D/Meshes2D_With_two_subpoly");
        public static IEnumerable<TestCaseData> MoreSubpoly() =>
            HelperFetchArrayFromJsonFile(Application.dataPath + "/Tests/Meshes/2D/Meshes2D_With_more_subpoly");

        public static IEnumerable<TestCaseData> HelperFetchArrayFromJsonFile(string filenameWithoutJsonExtension)
        {
            MeshFragmentVec2D[] meshes = JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D[]>( filenameWithoutJsonExtension + ".json");
            EdgeIndexed[][] singleEdges = JsonHelpers.LoadFromJsonFile<EdgeIndexed[][]>(filenameWithoutJsonExtension + ".singleEdges");
            for (int i = 0; i < meshes.Length; i++)
                yield return new TestCaseData(meshes[i], singleEdges[i]);
        }
        /*
        private static EdgeIndexed[][] HelperSingleEdgesFromMeshes(MeshFragmentVec2D[] meshes)
        {
            EdgeIndexed[][] edges = new EdgeIndexed[meshes.Length][];
            for (int i = 0; i < meshes.Length; i++)
            {
                if (meshes[i].JoinedClosestVertices().Vertices.Length != meshes[i].Vertices.Length)
                    throw new Exception();
                edges[i] = EdgeIndexed.SingleEdgesFromTriangles(meshes[i].Triangles);
            }
            return edges;
        }*/

        [TestCaseSource(nameof(OneSubpoly))]
        [TestCaseSource(nameof(TwoSubpoly))]
        [TestCaseSource(nameof(MoreSubpoly))]
        public void SingleEdgesFromOneSubpolyMesh(MeshFragmentVec2D fragment, EdgeIndexed[] singleEdgesExpected)
        {
            //var joined = fragment.JoinedClosestVertices();
            var singleEdges = EdgeIndexed.SingleEdgesFromTriangles(fragment.Triangles);
            //TestContext.WriteLine(ToLiteral(JsonHelpers.ArrayToJsonString(singleEdgesExpected)));
            //TestContext.WriteLine(ToLiteral(JsonHelpers.ArrayToJsonString(singleEdges)));
            Assert.AreEqual(singleEdgesExpected, singleEdges);
            //TestTriangulated(joined.Vertices, triangulated);

        }

        
        [TestCase("20220318_mesh2d_id20_2poly1hole")]
        [TestCase("20220318_mesh2d_id23_4poly3hole")]
        [TestCase("20220318_mesh2d_id24_1poly2hole")]
        [TestCase("20220318_mesh2d_id21_5poly4hole")]

        [TestCase("20220320_mesh2d_id25_atypical")]
        [TestCase("20220320_mesh2d_id26_atypical")]
        [TestCase("20220320_mesh2d_id27_atypical")]
        [TestCase("20220320_mesh2d_id28_atypical")]
        [TestCase("20220320_mesh2d_id29_atypical")]
        [TestCase("20220320_mesh2d_id30_atypical")]
        [TestCase("20220321_mesh2d_id32_atypical_NotFinished")]
        [TestCase("20220321_mesh2d_id36_like32_atypical")]
        [TestCase("20220321_mesh2d_id34_atypical_NotFinished")]
        [TestCase("20220321_mesh2d_id39_like34_atypical")]
        [TestCase("20220321_mesh2d_id37_atypical")]
        [TestCase("20220321_mesh2d_id40_atypical")]
        public void SingleEdgesFromMesh(string fileName)
    {
            string fullNameWithoutExtension = Application.dataPath + "/Tests/Meshes/2D/mesh2D_testcases/" + fileName;
            MeshFragmentVec2D mesh = JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
                    fullNameWithoutExtension + ".json");

            //if (mesh.JoinedClosestVertices().Vertices.Length != mesh.Vertices.Length)
            //    throw new Exception();
            EdgeIndexed[] expectedSingleEdges = JsonHelpers.LoadFromJsonFile<EdgeIndexed[]>(fullNameWithoutExtension + ".singleEdges");

            var singleEdges = EdgeIndexed.SingleEdgesFromTriangles(mesh.Triangles);
            Assert.AreEqual(expectedSingleEdges, singleEdges);
            //JsonHelpers.SaveToJsonFile(singleEdges, fullNameWithoutExtension + ".singleEdges");
        }


    }
}