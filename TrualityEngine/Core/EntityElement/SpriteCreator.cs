
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace TrualityEngine.Core
{
    /// <summary>
    ///  Creating simple <see cref="Asset{Texture2D}"/>
    /// </summary>
    public static class SpriteCreator
    {


       
        public delegate Color CreatorColor(int x, int y);
        public static Sprite MakeRectangle(Point size, Color color)
            => Create(
                (x, y) => color, size);
        public static Sprite MakeGridRectangle(Point size, Color colorA, Color colorB)
            => Create(
                (x, y) => ((x + y) % 2 == 0) ? colorA : colorB, size);
        public static Sprite MakeLineRectangleX(Point size, Color colorA, Color colorB)
            => Create(
                (x, y) => x % 2 == 0 ? colorA : colorB, size);
        public static Sprite MakeLineRectangleY(Point size, Color colorA, Color colorB)
             => Create(
                (x, y) => y % 2 == 0 ? colorA : colorB, size);
        public static Sprite MakeGradientX(Point size, IEvaluate<Color> gradient)
            => Create(
                (x, y) => gradient.Evaluate(new Percent(x, size.X)), size);
        public static Sprite MakeGradientY(Point size, IEvaluate<Color> gradient)
          => Create(
              (x, y) => gradient.Evaluate(new Percent(y, size.Y)), size);
        public static Sprite MakeGradient(Point size, IEvaluate<Color> gradient)
         => Create(
             (x, y) => gradient.Evaluate(new Percent(y + x, size.Y + size.X)), size);
        public static Sprite AddBorder(Sprite sprite, Color borderColor, int size)
        {
            Color[,] orignal = sprite.Value.To2DimensionArray();
            return Create(
                (x, y) =>
                {
                    if (x <size-1|| x > sprite.Value.Width-size
                    || y < size-1 || y > sprite.Value.Height-size)
                        return borderColor;
                    else
                        return orignal[x, y];
                }, new Point( sprite.Value.Width,sprite.Value.Height));
        }
        public static Sprite MakeHalfToHalfY(Point size, Color colorA, Color colorB, int? yLock = null)
        {
            int yMax = yLock ?? size.Y / 2;
            return Create(
                   (x, y) => y < yMax ? colorA : colorB, size);
        }
        public static Sprite MakeHalfToHalfX(Point size, Color colorA, Color colorB, int? xLock = null)
        {
            int xMax = xLock ?? size.X / 2;
            return Create(
               (x, y) => (x < xMax) ? colorA : colorB, size);
        }

        public static Sprite MakeQuarters(Point size, Color colorA, Color colorB, Color colorC, Color colorD, Point? locker = null)
        {

            Point max = locker ?? (size.ToVector2() / 2).ToPoint();
            return Create(
                (x, y) =>
                {
                    if (x < max.X && y < max.Y)
                        return colorA;
                    else if (x >= max.X && y < max.Y)
                        return colorB;
                    else if (x < max.X && y >= max.Y)
                        return colorC;
                    else
                        return colorD;

                }, size);
        }

        public static Sprite Create(CreatorColor creator, Point size)
        {

            if (GameHeart.Actual == null)
                return Sprite.CreateAnonymous(null);

            Texture2D texture2D = new Texture2D(GameHeart.Actual.BaseGame.GraphicsDevice, size.X, size.Y);
            Color[] colors = new Color[size.X * size.Y];
            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                    colors[x + (size.X * y)] = creator(x, y);
            texture2D.SetData(colors);

            return Sprite.CreateAnonymous(texture2D);

        }

        public static Texture2D CreateFrom2DimensionArray(Color[,] colors)
        {
            return Create((x, y) => colors[x, y], new Point(colors.GetLength(0), colors.GetLength(1))).Value;
        }





    }
}
