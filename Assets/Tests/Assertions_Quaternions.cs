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
        public static void AssertQuaternionEqualityByRandomTransforming(QuaternionD expectedQ, QuaternionD actualQ)
        {
            MatrixD4x4 expectedM = MatrixD4x4.FromRotation(expectedQ);
            MatrixD4x4 actualM = MatrixD4x4.FromRotation(actualQ);
            TestContext.WriteLine($"{expectedQ}, {actualQ}");
            TestContext.WriteLine($"{expectedQ.ToQuaternion().eulerAngles}, {actualQ.ToQuaternion().eulerAngles}");
            double epsilon = 0.000000000001d;

            AssertMatricesAreEqualByTransfomingRandomTriangleAndItNormal(expectedM, actualM, epsilon);
            for (int i = 0; i < 10; i++)
                AssertMatricesAreEqualByTransfomingRandomTriangleAndItNormal(expectedM, actualM, epsilon);
        }
    }
}