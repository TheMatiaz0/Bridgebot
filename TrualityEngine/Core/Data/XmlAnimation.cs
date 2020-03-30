using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrualityEngine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents animation in the XML code
    /// </summary>
    
    public class XmlAnimation
    {
        public XmlFrame[] Frames { get; set; }
        public float Speed { get; set; } = 1f;
        public Animation ToAnim()
            => new Animation(Frames.Select(item => new AnimFrame(Sprite.Get(item.Sprite), item.Scale, item.Color, item.Offset)), Speed, null);

    }
}
