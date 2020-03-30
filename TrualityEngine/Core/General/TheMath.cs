using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TrualityEngine.Core;
namespace TrualityEngine.Core
{
    /// <summary>
    /// TheMath class has float version math methods and some complementary math methods.
    /// </summary>
    public static class TheMath
    {

        public const float PI = (float)Math.PI;
        public const float TwoPi = (float)MathHelper.TwoPi;
        public const float PiOver2 = (float)MathHelper.PiOver2;
        public const float PiOver4 = (float)MathHelper.PiOver4;
        public const float E = (float)Math.E;
        public const float Rad2Deg = 360 / (PI * 2);
        public const float Log10E = MathHelper.Log10E;
        public const float Log2E = MathHelper.Log2E;
        public static float ToRadiansFromDegrees(float degrees)
        {
            return MathHelper.ToRadians(degrees);
        }
        public static float ToDegreesFromRadians(float radians)
        {
            return MathHelper.ToDegrees(radians);
        }
        /// <summary>
        /// Converting 0-1 percent notation to 0-255 byte.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"
        public static byte FloatPercentToByte(float value)
        {
            if (value > 1 || value < 0)
                throw new ArgumentException("Value must be in range 0-1", nameof(value));
            return (byte)(value * 255.0);
        }
        /// <summary>
        /// Converting 0-255 byte percent notation to 0-1 float.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"
        public static float BytePercentToFloat(byte value)
        {

            return (float)(value / 255.0f);
        }
        public static Vect2 Abs(Vect2 vect)
        {

            return new Vect2(Math.Abs(vect.X), Math.Abs(vect.Y));
        }

        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }
        public static float Cos(float value)
        {

            return (float)Math.Cos(value);

        }
        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }
        public static float Tanh(float value)
        {

            return (float)Math.Tanh(value);

        }
        public static float Log(float value)
        {
            return (float)Math.Log(value);
        }
        public static float Pow(float value, float power = 2)
        {
            return (float)Math.Pow(value, power);
        }

        public static float Cosh(float value)
        {
            return (float)Math.Cosh(value);
        }
        public static float Sinh(float value)
        {
            return (float)Math.Sinh(value);
        }

        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling(value);
        }
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }
        public static Vect2 Max(Vect2 a, Vect2 b)
        {
            return Vector2.Max(a, b);
        }
        public static Vect2 Min(Vect2 a, Vect2 b)
        {
            return Vector2.Min(a, b);
        }




    }
}
