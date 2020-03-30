using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Direction))]
    public class DirectionConverter : IIniConverter
    {
        public object Refuse(string iniCode)
        {

            if (IniManager.CanBeCollection(iniCode) == false)
                return ((SimpleDirection)Enum.Parse(typeof(SimpleDirection), iniCode)).ToDirection();
            else
                return (Direction)(Vect2)new Vect2Converter().Refuse(iniCode);
        }
    }
}
