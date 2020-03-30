using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class BaseColliderEntityArg:BaseEntityArgs
    {
        public override Entity AsEntity => AsColliderEntity;
        public virtual ColliderEntity AsColliderEntity { get; }
        public BaseColliderEntityArg(ColliderEntity entity):base(null)
        {
            AsColliderEntity = entity;
        }
    }
}
