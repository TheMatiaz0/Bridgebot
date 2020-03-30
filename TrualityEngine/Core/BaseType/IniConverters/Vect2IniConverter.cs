using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Vect2))]
    public class Vect2Converter : IIniConverter
    {
        public object Refuse(string iniCode)
        {
            var elements = IniManager.SplitedFromCollection(iniCode).Select(float.Parse).ToArray();
            return new Vect2( elements[0], elements[1]);
        }
    }
}
