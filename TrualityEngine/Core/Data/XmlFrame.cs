using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using TrualityEngine.Core;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents frame of animation in the XML code
    /// </summary>
    
    public class XmlFrame
    {
        [XmlAttribute]
        public string Sprite { get; set; } = "";
        public Color? Color { get; set; } = null;
        public Vect2 Offset { get; set; } = new Vect2(1, 1);
        public Vect2 Scale { get; set; } = new Vect2(1, 1);
    }
}
