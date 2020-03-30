using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public interface IAsset<out T>
        where T:class
    {
       public string Name { get; }
        public T Value { get; }
    }
}
