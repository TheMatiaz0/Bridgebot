using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
    public static class RectangleExtension
    {
       
        public static bool Intersects(this Rectangle rect, Circle circle)
            => circle.Intersect(rect);
        public static bool Intersects(this Rectangle rect, Line line)
            => line.Intersect(rect);
    }
}
