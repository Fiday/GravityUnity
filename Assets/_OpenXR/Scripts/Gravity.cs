using System;

namespace _OpenXR.Scripts
{
    public static class Gravity
    {
        public static readonly float GravityConstant = (float) (6.67f * Math.Pow(10, -11));
        
        public static float OrbitVelocity(float mass, float distance)
        {
            return MathF.Sqrt((GravityConstant * mass) / distance);
        }
    }
}