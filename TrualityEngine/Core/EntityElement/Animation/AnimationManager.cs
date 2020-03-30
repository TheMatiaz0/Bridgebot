using Microsoft.Xna.Framework;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Connected with <see cref="TrualityEngine.Core.Entity"/>. Use this to play any animation
    /// </summary>
    public class AnimationManager : IEntityManager<SpriteEntity>
    {


        /// <summary>
        /// Actual-runned animation.
        /// If any <see cref="AnimationRuntime"/> is not playing, it will return null
        /// </summary>
        public AnimationRuntime Current { get; private set; }
        /// <summary>
        /// Actual-connected <see cref="Entity"/>
        /// </summary>
        public SpriteEntity Entity { get; }
        public bool IsActive { get; set; }
    

        public AnimationManager(SpriteEntity entity)
        {
            Entity = entity;
        }
        /// <summary>
        /// Plays animation. If other <see cref="AnimationRuntime"/> is playing, older <see cref="AnimationRuntime"/> will be stopped
        /// </summary>       
        public AnimationRuntime Play(Animation animation, float speed = 1f, bool loop = false)
        {
            if (Current != null)
                Stop();


            Current = animation.CreateRuntime(Entity.Sprite);
            Current.OnRenderFrameChanged += Current_OnFrameChanged;
            Current.Loop = loop;
            Current.Speed = speed;
            return Current;

        }

        private void Current_OnFrameChanged(object sender, SimpleArgs<AnimFrame> e)
        {
            Entity.Sprite = e.Value.Sprite;
            e.Value.Action?.Invoke(e, sender as AnimationRuntime, Entity);
        }
        /// <summary>
        /// Stops current <see cref="AnimationRuntime"/>
        /// </summary>
        public void Stop()
        {
            if (Current != null)
            {

                Current.OnRenderFrameChanged -= Current_OnFrameChanged;
                AnimFrame frame = Current.GetFrameAfterAnim();
                Entity.Sprite = frame.Sprite;
                if (Current.Animation.RemberLastProperty)
                {
                    if (frame.Color != null)
                        Entity.RenderColor = Color.Lerp(Entity.RenderColor, (Color)frame.Color, frame.Ammount);
                    Entity.PrivateScale *= frame.Scale;
                    Entity.RenderOffset += frame.Offset;
                    Entity.RenderRotation += frame.Rotate;
                    if (frame.Alpha != null)
                        Entity.RenderColor = new Color(Entity.RenderColor, (float)frame.Alpha);
                }
                Current = null;

            }

        }
        internal void Update()
        {
            if(IsActive)
            {
                if (Current != null)
                {
                    Current.Update();
                    if (Current.IsEnded)
                        Stop();
                }
            }
           


        }
    }
}
