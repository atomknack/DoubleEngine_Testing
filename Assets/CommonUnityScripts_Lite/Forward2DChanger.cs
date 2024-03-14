using System;
using UnityEngine;

public class Forward2DChanger 
{
    private int _x, _z;
    private float _threshold;

    public (int x, int z) Forward() => (_x, _z);
    public (int x, int z) Right() => (_z, -_x);
    public (int x, int z) Left() => (-_z, _x);
    public (int x, int z) Backward() => (-_x, -_z);

    public bool TryChangeDirection(float x, float z)
    {
        const float zeroishEpsilon = 0.00001f;
        if (Mathf.Abs(x)<zeroishEpsilon && Mathf.Abs(z) < zeroishEpsilon) 
            return false;

        var normalized = new Vector2(x, z).normalized;
        x = normalized.x;
        z = normalized.y;
        if (Mathf.Abs((_x - x) + (_z - z)) < _threshold)
            return false;

        if(Mathf.Abs(x) > Mathf.Abs(z))
        {
            _x = Math.Sign(x);
            _z = 0;
            return true;
        }

        _x = 0;
        _z = Math.Sign(z);
        return true;
    }
    public Forward2DChanger(): this(0.55f) {}
    public Forward2DChanger(float threshold)
    {
        _x = 0;
        _z = 1;
        _threshold = threshold;
    }
}
