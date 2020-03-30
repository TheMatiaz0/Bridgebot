using Microsoft.Xna.Framework;
using System.Linq;

namespace TrualityEngine.Core
{
    public static class ColorExtension
    {
        public static byte[] ToByteArray(this Color color)
        {
            return color.ToVector4().ToArray().Select(item => (byte)(item * 255)).ToArray();
        }
        public static Color Remove(this Color color, Color other)
        {
            return new Color(color.R - other.R, color.G - other.G, color.B - other.B, 255);
        }
       
    }

}
