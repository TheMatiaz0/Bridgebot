 
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents animation which is used
    /// </summary>
    public class AnimationRuntime : ICoroutineable
    {
        public AnimFrame GetFrameAfterAnim()
            => Animation.LastFrame switch
            {
                LastFrameMode.ByProperty => new AnimFrame(Animation.SpriteAfterAnimation),
                LastFrameMode.FrameBeforeAnim => new AnimFrame(SpriteBeforeAnimStart),
                LastFrameMode.LastAnimFrame => Animation.Frames[Animation.Frames.Length - 1],
                _ => null
            };

        private TimeSpan WaitingTime { get; set; }
        public Animation Animation { get; set; }
        private Sprite SpriteBeforeAnimStart { get; }
        public uint Frame
        {
            get => _Frame;
            set
            {
                _Frame = value;
                OnRenderFrameChanged(this, GetAnimFrame());
            }
        }
        private uint _Frame;
        public AnimFrame GetAnimFrame() => Animation.Frames[(int)Frame];
        public float Speed { get; set; } = 1f;
        public bool IsEnded { get; set; } = false;
        public bool Loop { get; set; } = false;


        public event EventHandler<SimpleArgs<AnimFrame>> OnRenderFrameChanged = delegate { };

        public event EventHandler<SimpleArgs<AnimationRuntime>> OnAnimEnd = delegate { };
        public event EventHandler<SimpleArgs<AnimationRuntime>> OnReset = delegate { };

        public AnimationRuntime(Animation animation, Sprite before)
        {
            Animation = animation;
            SpriteBeforeAnimStart = before;
        }
        public TimeSpan GetSecondsByOneFrame()
            => TimeSpan.FromSeconds((1.0 / (Speed * Animation.Speed)) / 30.0);
        public void Update()
        {
            if (WaitingTime == new TimeSpan())
                OnRenderFrameChanged(this, Animation.Frames[0]);
            if (IsEnded == false)
            {
                TimeSpan timeToNextFrame = GetSecondsByOneFrame();
                WaitingTime += TimeOfGame.Actual.DeltaTime;
                while (WaitingTime > timeToNextFrame)
                {
                    WaitingTime -= timeToNextFrame;
                    if (Animation.Frames.Length == Frame + 1)
                    {
                        IsEnded = true;

                        if (Loop)
                            Reset();
                        else
                        {

                            OnRenderFrameChanged(this, new AnimFrame(Animation.SpriteAfterAnimation ?? SpriteBeforeAnimStart));

                            OnAnimEnd(this, this);
                        }

                    }
                    else
                        Frame++;
                }
            }

        }
        public void Reset()
        {
            IsEnded = false;
            Frame = 0;
            WaitingTime = new TimeSpan();
            OnReset(this, this);
        }


        public bool GetIsDone()
        {
            return IsEnded == true;
        }








    }
}
