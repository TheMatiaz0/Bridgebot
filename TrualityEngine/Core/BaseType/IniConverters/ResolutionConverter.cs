using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Resolution))]
    public class ResolutionConverter : IIniConverter
    {
        public object Refuse(string iniCode)
        {
            return new Resolution(((Vect2)new Vect2Converter().Refuse(iniCode)));
        }
    }
}
