using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary >
    /// Element that can be added to <see cref="TrualityEngine.Core.Entity"/>.
    /// Inheritance by <see cref="TrualityEngine.Core.Entity"/>  usually is better solution than use <see cref="Component{TEntityRequirer}"/>
    /// </summary>
    public class Component< TEntityRequirer>:BaseComponent
        where TEntityRequirer:class
    {
        public Component(bool multipleLock=false) : base(multipleLock, typeof(TEntityRequirer))
        {
        }

        public new TEntityRequirer Entity => (TEntityRequirer)(object)base.Entity;

        

    }
}
