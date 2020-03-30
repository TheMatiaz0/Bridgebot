 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents one frame of <see cref="Animation"/>
    /// </summary>
    public class AnimFrame
    {
        public Sprite Sprite { get; set; }
        /// <summary>
        /// Multiplies scale
        /// </summary>
        public Vect2 Scale { get; set; }
        /// <summary>
        /// Lerp color
        /// </summary>
        public Color? Color { get; set; }
        public float? Alpha { get; set; }
        /// <summary>
        /// Adds offset
        /// </summary>
        public Vect2 Offset { get; set; }
        public float Ammount { get; set; }
        public Rotation Rotate { get; set; }
        /// <summary>
        /// Action will run if frame starts
        /// </summary>
        public Action<AnimFrame, AnimationRuntime,SpriteEntity> Action { get; set; } = null;
        public AnimFrame(Sprite sprite, Vect2? scale = null, Color? color = null,
            Vect2? offset = null, Action<AnimFrame, AnimationRuntime, SpriteEntity> action = null, Rotation rotate = new Rotation(), float lerpPower = 1, float? alpha=null)
        {
            Sprite = sprite;
            Scale = scale ?? new Vect2(1, 1);
            Color = color;
            Offset = offset ?? Vect2.Zero;
            Action = action;
            Ammount = lerpPower;
            Rotate = rotate;
            Alpha = alpha;
        }
        public static AnimFrame[] FromSprites(params Sprite[] sprites)
        {
            return sprites.Select(sprite => new AnimFrame(sprite)).ToArray();
        }
        public static AnimFrame[] FromSprites(params string[] spritesFileName)
        {
            Sprite[] sprites = new Sprite[spritesFileName.Length];
            int index = 0;
            foreach (string fileName in spritesFileName)
                sprites[index++] = Sprite.Get(fileName);   
            return FromSprites(sprites);
        }
       
    }
}
