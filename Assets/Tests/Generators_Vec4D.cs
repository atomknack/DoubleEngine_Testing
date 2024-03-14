using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DoubleEngine;
using Newtonsoft.Json;

public partial class TestGenerators
{

    public class Vec4D_Random_50 : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            for (var i = 0; i < Vec3DTestData.d_vectors.Count; i++)
                yield return new object[] { RandVector4D(null,-1000, 1000) };
        }
    }
}
