 
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
namespace TrualityEngine.Core
{
    public enum LastFrameMode
    {
        FrameBeforeAnim,
        ByProperty,      
        LastAnimFrame,
    }
    /// <summary>
    /// Represents animation
    /// </summary>
    public class Animation
    {


      
        /// <summary>
        /// If <see cref="LastFrame"/> is equal <see cref="LastFrameMode.ByProperty"/>, frame after anim will be this
        /// </summary>
        public Sprite SpriteAfterAnimation { get; set; }
        public LastFrameMode LastFrame { get; set; } = LastFrameMode.LastAnimFrame;
        public bool RemberLastProperty { get; set; } = true;
        public AnimFrame[] Frames { get; }

        /// <summary>
        /// Default equals 1f
        /// </summary>
        public float Speed { get; set; } = 1f;
       
        /// <exception cref="ArgumentNullException"></exception>
     
        public Animation(IEnumerable<AnimFrame> frames, float speed=1f, Sprite spriteAfterAnim = null)
        {
           
            Speed = speed;
            Frames = frames?.ToArray() ?? throw new ArgumentNullException(nameof(frames));
            SpriteAfterAnimation = spriteAfterAnim ?? throw new ArgumentNullException(nameof(spriteAfterAnim));
        }
        public Animation(params AnimFrame[] frames):this((IEnumerable<AnimFrame>)frames)
        {

        }
        public AnimationRuntime CreateRuntime(Sprite before)
             => new AnimationRuntime(this, before);

    }
}
