using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
 
    public class CustomIniConverterAttribute:Attribute
    {
        public CustomIniConverterAttribute(Type current)
        {
            Current = current ?? throw new ArgumentNullException(nameof(current));
        }

        public Type Current { get; }
    }

    public interface IIniConverter
    {
        object Refuse(string iniCode);
     
    }
}
