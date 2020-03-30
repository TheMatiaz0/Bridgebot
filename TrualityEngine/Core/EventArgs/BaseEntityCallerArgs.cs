using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class BaseEntityCallerArgs:BaseBehaviourArgs
    {
        public override Behaviour AsBaseBehaviour => AsEntityCaller;
        public  virtual EntityCaller AsEntityCaller { get; }
        public BaseEntityCallerArgs(EntityCaller entityCaller):base(null)
        {
            AsEntityCaller = entityCaller;
        }

    }
}
