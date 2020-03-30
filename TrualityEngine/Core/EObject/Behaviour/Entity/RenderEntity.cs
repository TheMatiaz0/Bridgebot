using Microsoft.Xna.Framework;
using System;

namespace TrualityEngine.Core
{

    /// <summary>
    /// Entity that can render on the screen
    /// </summary>
    public abstract class RenderEntity : ColliderEntity, IComparable, IComparable<RenderEntity>
    {

        /// <summary>
        /// RenderOffset does not affect children, and collider
        /// </summary>
        public Vect2 RenderOffset { get; set; }
        /// <summary>
        /// Rotation does not affects children
        /// Warning! Collider will not a rotate
        /// </summary>
        public Rotation RenderRotation { get; set; }
        public virtual Color RenderColor { get; set; }
        public bool IsRendering { get; set; } = true;
        private Vect2 _PrivateScale = new Vect2(10, 10);
        public long Layer { get; set; }
        public Vect2 PrivateScale
        {
            get => _PrivateScale;
            set
            {
                if (value != _PrivateScale)
                {
                    _PrivateScale = value;
                    OnScaleChanged.Invoke(this, new ValueInEntityChangedArgs<Vect2>(this, value));
                }

            }
        }
        public RenderEntity()
        {
            RenderColor = Color.White;

        }
        public virtual Collider GetDrawCollisonInfo()
            => GetCollision();
        protected virtual Color GetRenderColor()
            => RenderColor;
     
        internal void Draw(FixedBatch batch)
        {
            if (IsRendering == false)
                return;
            Behaviour.Call(ComponentManager.GetAll(), item => item.IfDraw(batch));
            IfDraw(batch);
        }
      public void RenderLookAt(Vect2 pos)
        {
            this.RenderRotation= CalculateLookAt(pos);
        }
        public int CompareTo(RenderEntity other)
        {
            if (other == null)
                return 1;
            else
            {
                if (other.Layer == this.Layer)
                    return 0;
                else return (other.Layer > this.Layer) ? -1 : 1;
            }
        }
        public int CompareTo(object obj)
        {
            if (obj is RenderEntity == false)
                return 0;
            return CompareTo(obj as RenderEntity);
        }

       
        public void SetAlpha(float alpha)
        {
            this.RenderColor = new Color(this.RenderColor.R, this.RenderColor.G, this.RenderColor.B, alpha);
        }
        public void SetAlpha(byte alpha)
        {
            this.RenderColor = new Color(this.RenderColor.R,this.RenderColor.G,this.RenderColor.B,alpha);
        }


        protected override Rotation GetSpecialRotate()
        {
            return base.GetSpecialRotate() + RenderRotation;
        }
        protected override Vect2 GetSpecialScale()
        {
            return base.GetSpecialScale() * PrivateScale;
        }
        protected override Vect2 GetSpecialOffset()
        {
            return base.GetSpecialOffset() + RenderOffset;
        }

       
    }
}
