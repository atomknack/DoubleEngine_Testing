using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using System;
using VectorCore;

namespace FluentAssertions_Extensions
{
    public static partial class Assertions
    {
        public static void ShouldContainsAllElementsOf<T>(this ReadOnlySpan<T> span, IEnumerable<T> enumerable)
        {
            foreach (var el in enumerable)
                Assert.True(Assertions.Contains(span, el));
        }
        public static bool Contains<T>(this ReadOnlySpan<T> roSpan, T el)
        {
            for (int i = 0; i < roSpan.Length; i++)
                if (roSpan[i].Equals(el))
                    return true;
            return false;
        }
        public static string UMatrToString(Matrix4x4 m, string format) => $"{m.m00.ToString(format)}, {m.m01.ToString(format)}, {m.m02.ToString(format)}, {m.m03.ToString(format)}, \n{m.m10.ToString(format)}, {m.m11.ToString(format)}, {m.m12.ToString(format)}, {m.m13.ToString(format)}, \n{m.m20.ToString(format)}, {m.m21.ToString(format)}, {m.m22.ToString(format)}, {m.m23.ToString(format)}, \n{m.m30.ToString(format)}, {m.m31.ToString(format)}, {m.m32.ToString(format)}, {m.m33.ToString(format)}\n";
        public static string DMatrToString(MatrixD4x4 m, string format) => $"{m.m00.ToString(format)}, {m.m01.ToString(format)}, {m.m02.ToString(format)}, {m.m03.ToString(format)}, \n{m.m10.ToString(format)}, {m.m11.ToString(format)}, {m.m12.ToString(format)}, {m.m13.ToString(format)}, \n{m.m20.ToString(format)}, {m.m21.ToString(format)}, {m.m22.ToString(format)}, {m.m23.ToString(format)}, \n{m.m30.ToString(format)}, {m.m31.ToString(format)}, {m.m32.ToString(format)}, {m.m33.ToString(format)}\n";

        public static void AssertVector2sAreEqual(Vector2 uTemp, Vec2D dTemp, double errorScale, string format = "F6")
        {
            Assert.AreEqual(uTemp.x, dTemp.x, errorScale, $"xFail {uTemp.ToString(format)}, {dTemp.ToString(format)}");
            Assert.AreEqual(uTemp.y, dTemp.y, errorScale, $"yFail {uTemp.ToString(format)}, {dTemp.ToString(format)}"); ;
        }

        public static void AssertUnityVector3sAreEqual(Vector3 uTemp, Vector3 uTemp2, double errorScale, string format = "F6")
        {
            Assert.AreEqual(uTemp.x, uTemp2.x, errorScale, $"xFail {uTemp.ToString(format)}, {uTemp2.ToString(format)}");
            Assert.AreEqual(uTemp.y, uTemp2.y, errorScale, $"yFail {uTemp.ToString(format)}, {uTemp2.ToString(format)}");
            Assert.AreEqual(uTemp.z, uTemp2.z, errorScale, $"zFail {uTemp.ToString(format)}, {uTemp2.ToString(format)}");
        }
        public static void AssertVector3sAreEqual(Vector3 uTemp, Vec3D dTemp, double errorScale, string format = "F6")
        {
            Assert.AreEqual(uTemp.x, dTemp.x, errorScale, $"xFail {uTemp.ToString(format)}, {dTemp.ToString(format)}");
            Assert.AreEqual(uTemp.y, dTemp.y, errorScale, $"yFail {uTemp.ToString(format)}, {dTemp.ToString(format)}");
            Assert.AreEqual(uTemp.z, dTemp.z, errorScale, $"zFail {uTemp.ToString(format)}, {dTemp.ToString(format)}");
        }
        public static void AssertVector3sAreNotEqual(Vector3 uTemp, Vec3D dTemp, double errorScale, string format = "F6")
        {
            if (Math.Abs(uTemp.x - dTemp.x) > errorScale ||
                 Math.Abs(uTemp.y - dTemp.y) > errorScale ||
                 Math.Abs(uTemp.z - dTemp.z) > errorScale)
                return;
            Assert.Fail($"Are equal, but should not be: {uTemp.ToString(format)}, {dTemp.ToString(format)}");
        }
        public static void AssertVec3DsAreEqual(Vec3D dTemp, Vec3D dTemp2, double errorScale, string format = "F6")
        {
            Assert.AreEqual(dTemp.x, dTemp2.x, errorScale, $"xFail {dTemp.ToString(format)}, {dTemp2.ToString(format)}");
            Assert.AreEqual(dTemp.y, dTemp2.y, errorScale, $"yFail {dTemp.ToString(format)}, {dTemp2.ToString(format)}");
            Assert.AreEqual(dTemp.z, dTemp2.z, errorScale, $"zFail {dTemp.ToString(format)}, {dTemp2.ToString(format)}");
        }


        public static void AssertIsEqualOrFlippedEqual(Quaternion u, QuaternionD d, double epsilon)
        {
            AssertIsEqualOrFlippedEqual(new QuaternionD(u.x, u.y, u.z, u.w), d, epsilon);
        }
        public static void AssertIsEqualOrFlippedEqual(QuaternionD d1, QuaternionD d2, double epsilon)
        {
            Assert.IsTrue(QuaternionD.IsEqualOrFlippedEqual(d1, d2, epsilon), $"{d1.ToString("F6")}, {d2.ToString("F6")}");
        }

        public static void AssertQuaternionDsAreEqual(QuaternionD qD1, QuaternionD qD2, double epsilon, string format = "F6")
        {
            Assert.AreEqual(qD1.x, qD2.x, epsilon, $"xFail {qD1.ToString(format)}, {qD2.ToString(format)}");
            Assert.AreEqual(qD1.y, qD2.y, epsilon, $"yFail {qD1.ToString(format)}, {qD2.ToString(format)}");
            Assert.AreEqual(qD1.z, qD2.z, epsilon, $"zFail {qD1.ToString(format)}, {qD2.ToString(format)}");
            Assert.AreEqual(qD1.w, qD2.w, epsilon, $"wFail {qD1.ToString(format)}, {qD2.ToString(format)}");
        }

        public static void AssertQuaternionsAreEqual(Quaternion u, QuaternionD qD, double epsilon, string format = "F6")
        {
            Assert.AreEqual(u.x, qD.x, epsilon, $"xFail {u.ToString(format)}, {qD.ToString(format)}");
            Assert.AreEqual(u.y, qD.y, epsilon, $"yFail {u.ToString(format)}, {qD.ToString(format)}");
            Assert.AreEqual(u.z, qD.z, epsilon, $"zFail {u.ToString(format)}, {qD.ToString(format)}");
            Assert.AreEqual(u.w, qD.w, epsilon, $"wFail {u.ToString(format)}, {qD.ToString(format)}");
        }


        public static void AssertUnityQuaternionsAreEqual(Quaternion u1, Quaternion u2, double epsilon, string format = "F6")
        {
            Assert.AreEqual(u1.x, u2.x, epsilon, $"xFail {u1.ToString(format)}, {u2.ToString(format)}");
            Assert.AreEqual(u1.y, u2.y, epsilon, $"yFail {u1.ToString(format)}, {u2.ToString(format)}");
            Assert.AreEqual(u1.z, u2.z, epsilon, $"zFail {u1.ToString(format)}, {u2.ToString(format)}");
            Assert.AreEqual(u1.w, u2.w, epsilon, $"wFail {u1.ToString(format)}, {u2.ToString(format)}");
        }

        /*public static void Assert_Transposed_MatricesAreEqual(Matrix4x4 u, MatrixD4x4 qDbase, double epsilon, string format = "F6", string comment = "")
    {
        MatrixD4x4 qD = qDbase.Transposed();
        AssertMatricesAreEqual(u, qD, epsilon, format = "F6", comment);
    }*/

        /*
    public static void AssertQuaternionsAreEqualOrFlippedEqual(Quaternion u, QuaternionD qDbase, double epsilon, string format = "F6")
    {
        QuaternionD qD;
        if ((u.x < 0 && qDbase.x > 0) || (u.x > 0 && qDbase.x < 0))
            qD = QuaternionD.Flipped(qDbase);
        else
            qD = qDbase;
        Assert.AreEqual(u.x, qD.x, epsilon, $"xFail {u.ToString(format)}, {qD.ToString(format)}");
        Assert.AreEqual(u.y, qD.y, epsilon, $"yFail {u.ToString(format)}, {qD.ToString(format)}");
        Assert.AreEqual(u.z, qD.z, epsilon, $"zFail {u.ToString(format)}, {qD.ToString(format)}");
        Assert.AreEqual(u.w, qD.w, epsilon, $"wFail {u.ToString(format)}, {qD.ToString(format)}");
    }
    public static void AssertQuaternionDsAreEqualOrFlippedEqual(QuaternionD qD1, QuaternionD qDbase, double epsilon, string format = "F6")
    {
        QuaternionD qD;
        if ((qD1.x < 0 && qDbase.x > 0) || (qD1.x > 0 && qDbase.x < 0))
            qD = QuaternionD.Flipped(qDbase);
        else
            qD = qDbase;
        Assert.AreEqual(qD1.x, qD.x, epsilon, $"xFail {qD1.ToString(format)}, {qD.ToString(format)}");
        Assert.AreEqual(qD1.y, qD.y, epsilon, $"yFail {qD1.ToString(format)}, {qD.ToString(format)}");
        Assert.AreEqual(qD1.z, qD.z, epsilon, $"zFail {qD1.ToString(format)}, {qD.ToString(format)}");
        Assert.AreEqual(qD1.w, qD.w, epsilon, $"wFail {qD1.ToString(format)}, {qD.ToString(format)}");
    }*/


    }
}