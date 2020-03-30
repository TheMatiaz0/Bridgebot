using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Drawer with fixed position.
    /// </summary>
    public sealed class FixedBatch : IDisposable
    {
        private Texture2D BaseSquareTexture { get; set; }
        public SpriteBatch SpriteBatch { get; }

        public FixedBatch(GraphicsDevice device)
        {
            SpriteBatch = new SpriteBatch(device);
            BaseSquareTexture = SpriteCreator.MakeRectangle(new Point(1, 1), Color.White).Value;

        }

        public void Begin(Matrix? transformMatrix = null,Effect effect = null)
            => SpriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix
                ,effect:effect);
        /// <summary>
        /// Draws texture into the screen.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>

        public void Draw(Texture2D texture, Vector2 pos, Vector2 scale, Color color, Rotation rotation, SpriteEffects spriteEffect)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));
            Vector2 rpos = ToPrimitivePosition(pos);

            Vector2 realyOffset = new Vector2(texture.Width, texture.Height) / 2;
            SpriteBatch.Draw(texture, rpos, null, color, rotation.Radians, realyOffset,
                new Vector2((scale.X * Screen.VirtualScale.X) / texture.Width, (scale.Y * Screen.VirtualScale.Y) / texture.Height), spriteEffect, 1f);
        }
        public void Draw(Texture2D texture, Vect2 pos, Vect2 scale, Color color, Rotation rotation, SpriteEffects spriteEffect)
            => Draw(texture, pos.ToBasicVector2(), scale.ToBasicVector2(), color, rotation, spriteEffect);
        /// <summary>
        /// Draws string into the screen
        /// </summary>      
        internal void DrawString(SpriteFont font, string text, Vector2 pos, Color color, Vector2 scale, TextRenderOptions textRender = TextRenderOptions.LeftUp)
        {
            if (font == null)
                throw new ArgumentNullException(nameof(font));
           
            Vector2 size = font.MeasureString("A");

            scale = scale / size;

            text = text ?? String.Empty;
            SpriteBatch.DrawString(font, text, ToPrimitivePosition(pos) + GetBottom(font, textRender, text, scale * Screen.VirtualScale.ToBasicVector2()), color, 0, new Vector2(0, 0), scale: scale * Screen.VirtualScale.ToBasicVector2(), SpriteEffects.None, 0f);
        }
        public void DrawString(SpriteFont font, string text, Vect2 pos, Color color, Vect2 scale, TextRenderOptions textRender = TextRenderOptions.LeftUp)
            => DrawString(font, text, pos.ToBasicVector2(), color, scale.ToBasicVector2(), textRender);
        public void DrawLine(Vect2 start, Vect2 end, Color color, int scale)
            => DrawLine(start.ToBasicVector2(), end.ToBasicVector2(), color, scale);
        public void DrawLine(Vector2 start, Vector2 end, Color color, int scale)
        {

            start = ToPrimitivePosition(start);
            end = ToPrimitivePosition(end);
           
            Vector2 edge = end - start;
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            SpriteBatch.Draw(BaseSquareTexture,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), 
                    Math.Max(1, (int)(scale * ((Screen.VirtualScale.X + Screen.VirtualScale.Y) / 2)))), //width of line, change this to make thicker line
                null,
                color,
                angle,     
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
            

        }
    
   
        private Vector2 GetBottom(SpriteFont font, TextRenderOptions textRender, string text, Vector2 scale)
        {


            Vect2 measure = Vect2.FromBasic(font.MeasureString(text) * scale / 2);
            return textRender switch
            {
                TextRenderOptions.LeftUp => default,
                TextRenderOptions.Center => new Vector2(-measure.X, -measure.Y),
                _ => throw new ArgumentException("", paramName: nameof(textRender)),
            };
        }
        public Vect2 GetBottom(SpriteFont font, TextRenderOptions textRender, string text, Vect2 scale)
            => Vect2.FromBasic(GetBottom(font, textRender, text, scale.ToBasicVector2()));
        /// <summary>
        /// Primitive position is used to render into screen in SpriteBatch (Fixed batch use normal position).
        /// In primitive position y is revert and Vector.Zero is equal upper-left
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vect2 ToPrimitivePosition(Vect2 pos) 
            => new Vect2(pos.X * Screen.VirtualScale.X + GameHeart.Actual.BaseGame.GraphicsDevice.Viewport.Width / 2,
                -pos.Y * Screen.VirtualScale.Y +GameHeart.Actual.BaseGame.GraphicsDevice.Viewport.Height / 2);
        public static Vect2 ToHighLvPosition(Vect2 pos)
            => new Vect2((pos.X - (GameHeart.Actual.BaseGame.GraphicsDevice.Viewport.Width / 2)) / Screen.VirtualScale.X,
                -(pos.Y - GameHeart.Actual.BaseGame.GraphicsDevice.Viewport.Height / 2) / Screen.VirtualScale.Y);
        public static Vect2 FixY(Vect2 dir)
            => new Vect2(dir.X, -dir.Y);
        internal static Vector2 ToPrimitivePosition(Vector2 pos)
            => ToPrimitivePosition(Vect2.FromBasic(pos)).ToBasicVector2();
        public void End()
        {
            SpriteBatch.End();
        }
        public void Dispose()
        {
            SpriteBatch.Dispose();
        }

    }
}
