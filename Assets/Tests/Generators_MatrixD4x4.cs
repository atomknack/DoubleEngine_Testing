using System.Collections;
using System.Collections.Generic;
using DoubleEngine;
using DoubleEngine.UHelpers;
using UnityEngine;
using VectorCore;

public partial class TestGenerators
{
    public class Matrices : IEnumerable<object>
    {
        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            var rand = new System.Random();
            var count = QuaternionTestData.angles.Count;
            for (var i = 0; i < 50; i++)
            {
                Vec3D direction = RandVector3D(rand, - 365, 365);
                MatrixD4x4 mD1 = MatrixD4x4.FromRotation(QuaternionD.EulerDegrees(direction));
                Vec3D translation = RandVector3D(rand, - 50, 50);
                MatrixD4x4 mDT1 = MatrixD4x4.FromTranslation(translation);
                yield return new object[] { mD1* mDT1 };
            }
        }
            public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<object>)this).GetEnumerator();
        }
    }
}