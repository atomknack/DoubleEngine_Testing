using System;
using DoubleEngine;
using VectorCore;

public class Decompose_Tests
{

}
public static class Decompose_Extensions
{
    //public static bool TryNormalize(out MatrixD4x4 @out, MatrixD4x4 mat)
    public static bool TryNormalize(this MatrixD4x4 mat, out MatrixD4x4 @out)
    {
        const double epsilon = 0.00000001;
        @out = new MatrixD4x4();
        var lastElement = mat.m33;
        if (MathU.Abs(lastElement - 1d) < epsilon)
        {
            @out = mat;
            return true;
        }

        if (MathU.Abs(lastElement) < epsilon)
        {
            @out = new MatrixD4x4();
            return false;
        }

        double scale = 1 / lastElement;
        @out = mat * scale;
        //for (var i = 0; i < 16; i++)
        //    out[i] = mat[i] * scale
        return true;
    }
}


