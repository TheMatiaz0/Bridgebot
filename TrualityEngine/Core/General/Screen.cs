using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{

    /// <summary>
    /// Main information about screen
    /// </summary>
    public  class Screen:GameSettings<Screen>
    {
        
      
        /// <summary>
        /// Screen resolution
        /// </summary>
        public Resolution Resolution
        {
            get => new Resolution(GameHeart.Actual?.BaseGame?.GraphicsDevice.Viewport.Width ?? 700, GameHeart.Actual?.BaseGame?.GraphicsDevice.Viewport.Height ?? 600);
            set
            {
                SetResolution(value);
            }
        }

        public bool IsFullScreen
        {
            get => GameHeart.Actual.BaseGame.Graphics.IsFullScreen;
            set
            {
                GameHeart.Actual.BaseGame.Graphics.IsFullScreen = true;
                GameHeart.Actual.BaseGame.Graphics.ApplyChanges();
            }
        }



        public bool CanResize
        {
            get => GameHeart.Actual.BaseGame.Window.AllowUserResizing;
            set
            {
                GameHeart.Actual.BaseGame.Window.AllowUserResizing = value;
            }
        }
        /// <summary>
        /// Virtual resolution. It is used for base representation of position. If game window resolution is other than this, window will scale.
        /// If camera has base scale and it is centred left screen edge position will be -Virtual.X/2, and right will be +Virtual.X/2.
        /// </summary>
        public static Resolution Virtual { get; set; } = new Resolution(800, 480);

        /// <summary>
        /// Virtual scale is equal to ActualResoultion/VirtualResoultion;
        /// </summary>
        public static  Vect2 VirtualScale => Active.Resolution / Virtual;
        /// <summary>
        /// Setting screen resolution. Changing does not affect to item position dependence.
        /// </summary>
        /// <param name="res"></param>
        private  void SetResolution(Resolution res)
        {
            GameHeart.Actual.BaseGame.Graphics.PreferredBackBufferWidth = (int)res.Width;
            GameHeart.Actual.BaseGame.Graphics.PreferredBackBufferHeight = (int)res.Height;
            GameHeart.Actual.BaseGame.Graphics.ApplyChanges();
            
        }

        
            

    }
}
