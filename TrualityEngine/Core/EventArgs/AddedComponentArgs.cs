using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class AddedComponentArgs:BaseEntityArgs
    {

        public BaseComponent Component { get; }

        public AddedComponentArgs(BaseComponent component, Entity entity):base(entity)
        {
            Component = component;
            
        }
    }
}
