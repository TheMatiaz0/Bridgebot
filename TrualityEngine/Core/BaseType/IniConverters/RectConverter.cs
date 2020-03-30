using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    [CustomIniConverter(typeof(Rect))]
    public class RectConverter : IIniConverter
    {
        public object Refuse(string iniCode)
        {
            var elements=  IniManager.SplitedFromCollection(iniCode).Select(int.Parse).ToArray();
            return new Rect(elements[0], elements[1], elements[2], elements[3]);
        }
    }
}
