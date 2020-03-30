using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Rotation))]
    public class RotationConverter : IIniConverter
    {
        public object Refuse(string iniCode)
        {
            return new Rotation( float.Parse(iniCode));
        }
    }
}
