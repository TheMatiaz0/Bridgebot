using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public static class Texture2DExtension
    {
        public static Color[,] To2DimensionArray(this Texture2D texture2D)
        {
            Color[,] pixels = new Color[texture2D.Width, texture2D.Height];
            Color[] momentColor = new Color[texture2D.Width * texture2D.Height];
            texture2D.GetData(momentColor);
            for (int x = 0; x < texture2D.Width; x++)
                for (int y = 0; y < texture2D.Height; y++)
                    pixels[x, y] = momentColor[x + texture2D.Width * y];
            return pixels;
        }
    }
}
