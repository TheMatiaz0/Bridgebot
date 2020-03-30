using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class MouseCollisionArgs : BaseColliderEntityArg
    {
        public State State { get; }
        public MouseButton MouseButton { get; }
        public MouseCollisionArgs(ColliderEntity entity,State state, MouseButton mouseButton):base(entity)
        {
            State = state;
            MouseButton = mouseButton;
        }

        
       
    }
}
