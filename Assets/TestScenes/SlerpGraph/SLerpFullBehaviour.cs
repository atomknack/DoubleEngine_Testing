using VectorCore;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine.UHelpers;
using DoubleEngine;

public class SLerpFullBehaviour : FunctionBehavior
{
    public Vector3 start;
    public Vector3 end;

    public static Vec3D FakeSlerpUnclampedInPlace(Vec3D from, Vec3D to, double amount)
    {
        double angleTo = Vec3D.AngleRadians(from, to);
        Vec3D cross = Vec3D.Cross(from, to);
        QuaternionD q1 = QuaternionD.AngleAxis(MathU.RadiansToDegrees(angleTo) * amount, cross.Normalized());
        return q1.Rotate(from);
    }
    public override Vector3 VirtualFuction(float value)
    {
        var a = FakeLerpBehaviour.FakeSlerpUnclampedInPlace(start.ToVec3D(), end.ToVec3D(), (double)value).ToVector3();
        var b = FakeLerpBehaviour.FakeSlerpUnclampedInPlace(end.ToVec3D(), start.ToVec3D(), (double)(1 - value)).ToVector3();
        return Vector3.Lerp(a,b,value);
        //return (Vector3)Vec3D.SlerpUnclamped((Vec3D)start, (Vec3D)end, (double)value);
    }
}
