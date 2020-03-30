using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using TrualityEngine.Core;
namespace TrualityEngine.Core
{
    /// <summary>
    /// You should regard this as alias from <see cref="Asset{T}"/> when T is <see cref="Texture2D"/>.
    /// </summary>
    public  class Sprite 
    {
        public readonly static Sprite ClearSprite =SpriteCreator.MakeRectangle(new Point(1, 1), Color.White);
        public readonly static Sprite ButtonSprite
            = SpriteCreator.MakeGradientY(new Point(100, 100), new Gradient(new Color(200,200,200), new Color(180, 180, 180)));
        private readonly Asset<Texture2D> asset;
        public Texture2D Value => asset.Value;
        public Sprite(Asset<Texture2D> asset)
        {
            this.asset=asset;
        }
        public static Sprite CreateAnonymous(Texture2D val)
        {
            return Asset<Texture2D>.CreateAnonymous(val);
        }
        public static Sprite Get(string name)
        {
            return Asset<Texture2D>.Get(name);
        }
        public static Sprite Add(Texture2D val, string name)
        {
            return Asset<Texture2D>.Add(val, name);
        }
      
        public static implicit operator Sprite (Asset<Texture2D> asset)
        {
            return new Sprite(asset);
        }
        public static implicit operator Asset<Texture2D>(Sprite sprite)
        {
            return sprite.asset;
        }
        
    }
    

}

