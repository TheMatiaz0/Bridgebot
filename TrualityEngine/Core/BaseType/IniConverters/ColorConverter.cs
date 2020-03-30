using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Color))]
    public class ColorConverter : IIniConverter
    {
        public object Refuse(string iniCode)
        {
            var elements = IniManager.SplitedFromCollection(iniCode).Select(byte.Parse).ToList(); ;
            if (elements.Count < 4)
                elements.Add(255);
            return new Color(elements[0], elements[1], elements[2], elements[3]);
        }
    }
}
