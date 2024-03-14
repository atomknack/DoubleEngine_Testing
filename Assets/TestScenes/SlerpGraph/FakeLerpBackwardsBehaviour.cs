using DoubleEngine;
using DoubleEngine.UHelpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLerpBackwardsBehaviour : FunctionBehavior
{
    public Vector3 start;
    public Vector3 end;
    public override Vector3 VirtualFuction(float value)
    {
        return FakeLerpBehaviour.FakeSlerpUnclampedInPlace(end.ToVec3D(), start.ToVec3D(), (double)(1-value)).ToVector3();
        //return (Vector3)Vec3D.SlerpUnclamped((Vec3D)start, (Vec3D)end, (double)value);
    }
}
