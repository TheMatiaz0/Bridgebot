using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrualityEngine.Core
{
    public class SingleTileset
    {
        public string Texture { get; set; }
        public Sprite GetAsset()
        {
            if (Texture == null || Texture == String.Empty)
                return null;
            else
                return Sprite.Get(Texture);
        }
        public Point OneCellSize { get; set; } 
        public IEnumerable<Sprite> GetAll()
        {
            if (Texture == null)
                throw new ArgumentNullException(nameof(Texture));
            return TilesetLoader.ToSinglesTextures(Sprite.Get(Texture).Value, OneCellSize);
        }
       
    }
}
