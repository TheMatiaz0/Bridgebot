 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Static class for creating simple <see cref="Animation"/> example fade.
    /// </summary>
    public static class AnimationCreator
    {
        public static Animation CreateFade(Texture2D original, float speed, SimpleDirection dir = SimpleDirection.Right)
        {

            Sprite[] animFrames = new Sprite[original.Width];
            Color[,] lastPixels = original.To2DimensionArray();
            (int start, Func<int, int> updater) info = dir switch
            {
                SimpleDirection.Left => (0, z => ++z),
                SimpleDirection.Right => (original.Width - 1, z => --z),
                SimpleDirection.Up => (0, z => ++z),
                SimpleDirection.Down => (original.Height - 1, z => --z),
                _ => throw new ArgumentException("Unexpected enum value", nameof(dir))


            };
            if (dir.IsConnectedWithX())
                Generate(info, i => i < original.Width, m => m < original.Height, (pix, z, m, color) => pix[z, m] = color);
            else
                Generate(info, i => i < original.Height, m => m < original.Width, (pix, z, m, color) => pix[m, z] = color);
            void Generate((int start, Func<int, int> updater) baseForInfo, Func<int, bool> baseForContidions, Func<int, bool> generateContidions, Action<Color[,], int, int, Color> setter)
            {
                for (int z = baseForInfo.start, i = 0; baseForContidions(i); i++, z = baseForInfo.updater(z))
                {
                    animFrames[i] = Sprite.CreateAnonymous(new Texture2D(TheGame.Instance.GraphicsDevice, original.Width, original.Height));
                    _ = new Color[original.Width, original.Height];
                    Color[,] pixels = lastPixels.Clone() as Color[,];
                    for (int m = 0; generateContidions(m); m++)
                        setter(pixels, z, m, new Color(0, 0, 0, 0));
                    animFrames[i] = Sprite.CreateAnonymous(SpriteCreator.CreateFrom2DimensionArray(pixels));
                    lastPixels = pixels.Clone() as Color[,];


                }
            }
            return new Animation(AnimFrame.FromSprites(animFrames), speed);


        }
        private static Animation CreateAlphaChange(Texture2D orignal, float speed, int framesCount,
           float alphaAfter, Func<int, float, float> alphaLoader)
        {

            AnimFrame[] frames = new AnimFrame[framesCount];
            float changeInOneFrame = 1.0f / (frames.Length);

            for (int i = 0; i < frames.Length - 1; i++)
            {
                frames[i] = new AnimFrame(Sprite.CreateAnonymous(orignal), alpha: alphaLoader(i, changeInOneFrame));
            }
            frames[frames.Length - 1] = new AnimFrame(Sprite.CreateAnonymous(orignal), alpha: alphaAfter);

            return new Animation(frames, speed);
        }
        public static Animation CreateDisappearance(Texture2D orignal, float speed = 1f, int framesCount = 80)
            => CreateAlphaChange(orignal, speed, framesCount, 0,
                (int i, float changeInLastFrame) => 1.0f - i * changeInLastFrame);
        public static Animation CreateAppearance(Texture2D orignal, float speed = 1f, int framesCount = 80)
         => CreateAlphaChange(orignal, speed, framesCount, 1,
               (int i, float changeInLastFrame) => 0 + i * changeInLastFrame);


        public static Animation CreateRotate(Texture2D original, Rotation rotation, float speed = 1f, bool upper = true)
        {


            float absAngle = Math.Abs(rotation.Angle);

            AnimFrame[] frames = new AnimFrame[(int)Math.Ceiling(absAngle)];

            for (int i = 0; i < frames.Length - 1; i++)
                frames[i] = new AnimFrame(Sprite.CreateAnonymous(original), rotate: (upper) ? Rotation.FromDegrees(i) : Rotation.FromDegrees(-i));
            frames[frames.Length - 1] = new AnimFrame(Sprite.CreateAnonymous(original), rotate: rotation);
            return new Animation(frames, speed);


        }

        public static Animation CreateSinglePulse(Texture2D original, float multiply, int framesCount = 40, float speed = 1f)
        {
            List<AnimFrame> frames = new List<AnimFrame>();

            double change = Math.PI / framesCount;

            for (double x = -(Math.PI / 2); (x) < Math.PI / 2; x += change)
            {
                double sinusVal = 1 + (Math.Cos(x) * multiply);//cos is op
                frames.Add(new AnimFrame(Sprite.CreateAnonymous(original), scale: new Vect2((float)sinusVal, (float)sinusVal)));
            }
            frames.Add(new AnimFrame(Sprite.CreateAnonymous(original)));
            return new Animation(frames, speed);
        }


    }

}
