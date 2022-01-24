using System;
using UnityEngine;

namespace _OpenXR.Scripts
{
    public static class Gravity
    {
        [SerializeField] private static int _gravitaionExponent = -11;
        
        public static readonly float GravityConstant = (float) (6.67f * Math.Pow(10, _gravitaionExponent));
        
        public static float OrbitVelocity(float mass, float distance)
        {
            return MathF.Sqrt((GravityConstant * mass) / distance);
        }
    }
}