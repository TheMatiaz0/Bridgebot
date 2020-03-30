using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class CollisonArgs : BaseColliderEntityArg
    {
        public ColliderEntity A { get; }
        public ColliderEntity B { get; }
      
        public CollisonArgs(ColliderEntity a, ColliderEntity b) :base(a)
        {
            A = a;
            B = b;
           
            
        }
    }
}
