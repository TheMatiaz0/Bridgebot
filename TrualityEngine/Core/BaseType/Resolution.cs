using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TrualityEngine.Core
{
    public struct Resolution
    {

        private readonly static Resolution[] All =
      {
            _800x480,_800x600,_1024x768,_1280x720,_1280x768,_1366x768,_1360x768,_1600x900,
            _1600x900,_1920x1080,_2048x1024,_2048x1152,_2715x1527,_3840x2160,_4096x2048,_4096x2304

        };

        public static readonly Resolution Empty = new Resolution();
        public static IReadOnlyCollection<Resolution> GetResolutions() => All;


        public static readonly Resolution _800x480 = new Resolution(800, 480);
        public static readonly Resolution _800x600 = new Resolution(800, 600);
        public static readonly Resolution _1024x768 = new Resolution(1024, 768);
        public static readonly Resolution _1280x720 = new Resolution(1280, 720);
        public static readonly Resolution _1280x768 = new Resolution(1280, 768);
        public static readonly Resolution _1366x768 = new Resolution(1366, 768);
        public static readonly Resolution _1360x768 = new Resolution(1360, 768);
        public static readonly Resolution _1600x900 = new Resolution(1600, 900);
        public static readonly Resolution _1920x1080 = new Resolution(1920, 1080);
        public static readonly Resolution _2048x1024 = new Resolution(2048, 1024);
        public static readonly Resolution _2048x1152 = new Resolution(2048, 1152);
        public static readonly Resolution _2715x1527 = new Resolution(2715, 1527);
        public static readonly Resolution _3840x2160 = new Resolution(3840, 2160);
        public static readonly Resolution _4096x2048 = new Resolution(4096, 2048);
        public static readonly Resolution _4096x2304 = new Resolution(4096, 2304);


        public static readonly Resolution _4k16_9 = _4096x2304;
        public static readonly Resolution _4k2_1 = _4096x2048;
        public static readonly Resolution _720p = _1280x720;

        public int Width { get; set; }
        public int Height { get; set; }
        public Resolution(int x, int y)
        {
            Width = x;
            Height = y;
        }
        public Resolution(Point p)
        {
            Width = p.X;
            Height = p.Y;
        }
        public Resolution(Vect2 p)
        {
            Width = (int)p.X;
            Height = (int)p.Y;
        }
        public int[] ToIntArray() => new int[] { Width, Height };
        public Vect2 ToVect2()
        {
            return new Vect2(Width, Height);
        }
        public Point ToPoint()
        {
            return new Point(Width, Height);
        }
        public static Vect2 operator /(Resolution a, Resolution b)
        {
            return new Vect2(a.Width / (float)b.Width, (float)a.Height / b.Height);

        }
        public static Resolution operator *(Resolution a, Resolution b)
        {
            return new Resolution(a.Width * b.Width, a.Height * b.Height);

        }
        public static Resolution operator /(Resolution a, float b)
        {
            return new Resolution((int)(a.Width / b), (int)(a.Height / b));
        }
        public static Resolution operator *(Resolution a, float b)
        {
            return new Resolution((int)(a.Width * b), (int)(a.Height * b));
        }
        public static bool operator ==(Resolution a, Resolution b)
            => a.ToPoint() == b.ToPoint();
        public static bool operator !=(Resolution a, Resolution b)
            => !(a == b);
        public override bool Equals(object obj)
        {
            if (obj is Resolution r)
            {
                return this == r;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return this.ToPoint().GetHashCode();
        }
        public override string ToString()
        {
            return $"{Width}x{Height}";
        }
        public void Deconstruct(out int x, out int y)
        {
            x = Width;
            y = Height;
        }

    }
}
