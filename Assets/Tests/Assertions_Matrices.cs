using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DoubleEngine;
using Newtonsoft.Json;
using static TestGenerators;
using DoubleEngine.Atom;
using FluentAssertions;
using DoubleEngine.UHelpers;
using VectorCore;

namespace FluentAssertions_Extensions
{
    public static partial class Assertions
    {
        public static void AssertMatricesAreEqualByTransfomingRandomTriangleAndItNormal(in MatrixD4x4 expected, in MatrixD4x4 actual, double epsion = 0.0000_0000_0000_01d)
        {
                (var v0, var v1, var v2) = (NextVec3D(rand, -100, 100), NextVec3D(rand, -100, 100), NextVec3D(rand, -100, 100));
                (var expectedV0, var expectedV1, var expectedV2) = (expected.MultiplyPoint(v0), expected.MultiplyPoint(v1), expected.MultiplyPoint(v2));
                (var actualV0, var actualV1, var actualV2) = (actual.MultiplyPoint(v0), actual.MultiplyPoint(v1), actual.MultiplyPoint(v2));
                AssertVec3DsAreEqual(expectedV0, actualV0, epsion);
                AssertVec3DsAreEqual(expectedV1, actualV1, epsion);
                AssertVec3DsAreEqual(expectedV2, actualV2, epsion);
                Vec3D expectedNormal = Vec3D.NormalFromTriangle(expectedV0, expectedV1, expectedV2);
                //AssertVec3DsAreEqual(expectedNormal, expected.MultiplyPoint3x4(Vec3D.NormalFromTriangle(v0, v1, v2)).Normalized(), epsion);
                Vec3D actualPoint = Vec3D.NormalFromTriangle(actualV0, actualV1, actualV2);
                AssertVec3DsAreEqual(expectedNormal, actualPoint, epsion);
                //TestContext.WriteLine($"{epsion} {expectedPoint} {actualPoint}");
        }

        public static void AssertMatricesAreEqualByTransfomingNRandomPoints(Matrix4x4 u, in MatrixD4x4 mD, int nPoints, string format = "F6", string comment = "")
        {
            if (nPoints <= 0)
                throw new ArgumentOutOfRangeException(nameof(nPoints));
            for (int i = 0; i < nPoints; i++)
            {
                var point = NextVec3D(rand, -100, 100);
                Vector3 uTransformed = u.MultiplyPoint(point.ToVector3());
                double epsion = 0.0000005d;
                epsion = epsion + (epsion*uTransformed.magnitude);
                Vec3D dTransformed = mD.MultiplyPoint(point);
                //TestContext.WriteLine($"{epsion} {uTransformed} {dTransformed}");
                AssertVector3sAreEqual(uTransformed, dTransformed, epsion);
            }
        }
        public static void AssertMatricesAreEqualByTransfomingNRandomPoints(in MatrixD4x4 expected, in MatrixD4x4 actual, int nPoints, string format = "F6", string comment = "")
        {
            if (nPoints <= 0)
                throw new ArgumentOutOfRangeException(nameof(nPoints));
            for (int i = 0; i < nPoints; i++)
            {
                var point = NextVec3D(rand, -100, 100);
                Vec3D expectedPoint = expected.MultiplyPoint(point);
                Vec3D actualPoint = actual.MultiplyPoint(point);
                double epsion = 0.0000_0000_0000_01d;
                //TestContext.WriteLine($"{epsion} {expectedPoint} {actualPoint}");
                AssertVec3DsAreEqual(expectedPoint, actualPoint, epsion);
            }
        }

        public static void AssertMatricesAreEqual(Matrix4x4 u, MatrixD4x4 qD, double epsilon, string format = "F6", string comment = "")
        {
            Assert.AreEqual(u.m00, qD.m00, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m01, qD.m01, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m02, qD.m02, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m03, qD.m03, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");

            Assert.AreEqual(u.m10, qD.m10, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m11, qD.m11, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m12, qD.m12, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m13, qD.m13, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");

            Assert.AreEqual(u.m20, qD.m20, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m21, qD.m21, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m22, qD.m22, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m23, qD.m23, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");

            Assert.AreEqual(u.m30, qD.m30, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m31, qD.m31, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m32, qD.m32, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");
            Assert.AreEqual(u.m33, qD.m33, epsilon); // , $"{comment} \n{UMatrToString(u,format)}, \n{DMatrToString(qD,format)}");

        }

        /*
        public static void AreEqual(ThreeDimensionalCell cell, ThreeDimensionalCell other)
        {
            Assert.AreEqual(cell.GetHashCode(), other.GetHashCode());
            Assert.AreEqual(cell.ToBytesArray(), other.ToBytesArray());
            Assert.True(cell == other);
            NUnit.Framework.Assert.AreEqual(cell, other);
        }
        public static void AreNotEqual(ThreeDimensionalCell cell, ThreeDimensionalCell other)
        {
            Assert.AreNotEqual(cell.GetHashCode(), other.GetHashCode());
            Assert.AreNotEqual(cell.ToBytesArray(), other.ToBytesArray());
            Assert.False(cell == other);
            NUnit.Framework.Assert.AreNotEqual(cell, other);
        }

        public static void AreEqual(IThreeDimensionalGrid expected, IThreeDimensionalGrid actual)
        {
            var dimensions = actual.GetDimensions();
            Assert.AreEqual(expected.GetDimensions(), dimensions);
            for (int xi = 0; xi < dimensions.x; ++xi)
                for (int yi = 0; yi < dimensions.y; ++yi)
                    for (int zi = 0; zi < dimensions.z; ++zi)
                        Assert.AreEqual(expected.GetCell(xi, yi, zi), actual.GetCell(xi, yi, zi));
        }
        */
    }
}