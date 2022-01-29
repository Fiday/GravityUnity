using System;
using UnityEngine;

namespace _OpenXR.Scripts
{
    public static class Gravity
    {
        private static int _gravitationExponent = -9;

        public static readonly float GravityConstant = (float) (6.67f * Math.Pow(10, _gravitationExponent));

        public static float OrbitVelocity(float mass, float distance)
        {
            return MathF.Sqrt((GravityConstant * mass) / distance);
        }
    }
}