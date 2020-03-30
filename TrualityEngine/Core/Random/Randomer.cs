using Microsoft.Xna.Framework;
using System;

namespace TrualityEngine.Core
{
    public class Randomer
    {
        public int? Seed { get; }
        private readonly Random random;
        public static readonly Randomer Base = new Randomer();
        public Randomer(int seed)
        {
            Seed = seed;
            random = new Random(seed);

        }
        public Randomer()
        {
            Seed = null;
            random = new Random();
        }
        public int NextInt(int min = int.MinValue, int max = int.MaxValue)
        {
            return random.Next(min, max);
        }
        public double NextDouble(double min = double.MinValue, double max = double.MaxValue)
        {
            return random.NextDouble() * (max - min) + min;

        }
        public float NextFloat(float min = float.MinValue, float max = float.MaxValue)
        {
            return (float)(NextDouble(min, max));
        }
        public Color NextColor(Color min, Color max)
        {

            byte[] minV = min.ToByteArray();
            byte[] maxV = max.ToByteArray();
            const int rgbLenght = 4;
            byte[] rgba = new byte[rgbLenght];
            for (int x = 0; x < rgbLenght; x++)
            {
                rgba[x] = (byte)NextInt((int)minV[x], (int)maxV[x] + 1);
            }
            return new Color(rgba[0], rgba[1], rgba[2], rgba[3]);
        }
        public Color NextColor(bool alphaCanBeDifferent = false)
            => NextColor(new Color(0, 0, 0, alphaCanBeDifferent ? 0 : 255), new Color(255, 255, 255, 255));
        public Vect2 NextVector2(Vect2 min, Vect2 max)
        {
            float x = NextFloat(min.X, max.X);
            float y = NextFloat(min.Y, max.Y);
            return new Vect2(x, y);
        }
        public Vect2 NextVector2()
        {
            return NextVector2(new Vect2(float.MinValue / 1000, float.MinValue / 1000), new Vect2(float.MaxValue / 1000, float.MaxValue / 1000));
        }
        public Direction NextDirection()
        {
            return NextVector2(-Vect2.One, Vect2.One);
        }

    }
}
