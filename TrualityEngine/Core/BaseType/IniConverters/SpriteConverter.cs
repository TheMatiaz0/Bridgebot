using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Sprite))]
    public class SpriteConverter : IIniConverter
    {
        public object Refuse(string iniCode)
        {
            return Sprite.Get(iniCode);
        }
    }
}
