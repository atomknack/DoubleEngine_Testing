using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DoubleEngine;
using Newtonsoft.Json;
using VectorCore;

public partial class TestGenerators
{
    public class Vec3D_2EulerRamdomDegreesAngles : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var rand = new System.Random();
            for (var i = 0; i < 50; i++)
            {
                yield return new object[] { 
                    new Vec3D((double)rand.Next(-400, 400), (double)rand.Next(-400, 400), (double)rand.Next(-400, 400)),
                    new Vec3D((double)rand.Next(-400, 400), (double)rand.Next(-400, 400), (double)rand.Next(-400, 400))
                };
            }
        }
    }
    public static class QuaternionTestData
    {
        public static List<double> angles = new List<double>(new double[]{0, 1, 42.005, -23.007, 0.001d,
            0.005,0.0005, 87, 2,3, 7, 290, 14.5,51, 103.103103103, 2000, -0.00001d,
            -0.005,-0.0005, -1, -2,-3, -700, -100, -14.5,-51,42, -103.103103103, -2010,});
    }
    public class Vec4DAngles : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var rand = new System.Random();
            var count = QuaternionTestData.angles.Count;
            for (var i = 0; i < 50; i++)
            {
                double qx = QuaternionTestData.angles[rand.Next(0, count)];
                double qy = QuaternionTestData.angles[rand.Next(0, count)];
                double qz = QuaternionTestData.angles[rand.Next(0, count)];
                double qw = QuaternionTestData.angles[rand.Next(0, count)];
                yield return new object[] { new Vec4D(qx, qy, qz, qw) };
            }
        }
    }

    public class VectorAngles : IEnumerable
    {
        public IEnumerator GetEnumerator() => GetEnumerator(0);

        public IEnumerator GetEnumerator(int seed)
        {
            var rand = new System.Random(seed);
            var count = QuaternionTestData.angles.Count;
            for (var i = 0; i < 50; i++)
            {
                double qx = QuaternionTestData.angles[rand.Next(0, count)];
                double qy = QuaternionTestData.angles[rand.Next(0, count)];
                double qz = QuaternionTestData.angles[rand.Next(0, count)];
                yield return new object[] { new Vec3D(qx,qy,qz)};
            }
        }
    }

    public class VectorPairs : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var rand = new System.Random();
            var seed = rand.Next(1000);
            IEnumerator vecEn1 = new VectorAngles().GetEnumerator(seed+2000);
            IEnumerator vecEn2 = new VectorAngles().GetEnumerator(seed+10000);

            while (vecEn1.MoveNext() && vecEn2.MoveNext())
            {
                yield return new object[] { ((object[])vecEn1.Current)[0], ((object[])vecEn2.Current)[0] };
            }
        }
    }

    public class VectorTriples : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var rand = new System.Random();
            var seed = rand.Next(1000);
            IEnumerator vecEn1 = new VectorAngles().GetEnumerator(seed + 2000);
            IEnumerator vecEn2 = new VectorAngles().GetEnumerator(seed + 10000);
            IEnumerator vecEn3 = new VectorAngles().GetEnumerator(seed + 100000);

            while (vecEn1.MoveNext() && vecEn2.MoveNext() && vecEn3.MoveNext())
            {
                yield return new object[] { 
                    ((object[])vecEn1.Current)[0], 
                    ((object[])vecEn2.Current)[0],
                    ((object[])vecEn3.Current)[0],
                };
            }
        }
    }
}