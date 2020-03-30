 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class ParticleSettings:ICloneable
    {
        public ParticleSettings(TimeSpan lifeTime, Sprite sprite, float speed, Color color, Vect2 size)
        {
            LifeTime = lifeTime;
            Sprite = sprite ?? Sprite.ClearSprite;
            Speed = speed;
            Color = color;
            Size = size;
           
        }
        public ParticleSettings():this(default,default,default,default,default)
        {

        }

        public TimeSpan LifeTime { get; set; } = TimeSpan.FromSeconds(2f);
      
        public Sprite Sprite { get; set; }
       
        public float Speed { get; set; } = 30;
        public Color Color { get; set; } = Color.White;
        public Vect2 Size { get; set; } = new Vect2(1, 1);
        public ParticleSettings Clone()
        {
            return (ParticleSettings)this.MemberwiseClone();
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

    }
}
