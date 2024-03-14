using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySlerpUnclamped : FunctionBehavior
{
    public Vector3 start;
    public Vector3 end;
    public override Vector3 VirtualFuction(float value)
    {
        return Vector3.SlerpUnclamped(start, end, value);
    }
}
