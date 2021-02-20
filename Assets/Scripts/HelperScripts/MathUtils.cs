using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    /// <summary>
    /// Linearly interpolates the value between two points, given the value of the two points and the relative position (0-1) between them.
    /// </summary>
    public static float Lerp(float s, float e, float t)
    {
        return s + (e - s) * t;
    }


    /// <summary>
    /// Bilinearly interpolates the value inside a square, given the four corner values of the sqaure and the relative position (0-1) inside the square.
    /// </summary>
    public static float Blerp(float c00, float c10, float c01, float c11, float tx, float ty)
    {
        return Lerp(Lerp(c00, c10, tx), Lerp(c01, c11, tx), ty);
    }
}
