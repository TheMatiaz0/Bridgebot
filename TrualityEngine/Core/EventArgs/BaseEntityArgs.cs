using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class BaseEntityArgs:BaseEntityCallerArgs
    {
        public override EntityCaller AsEntityCaller => AsEntity;
        public  virtual Entity AsEntity { get; }
        public BaseEntityArgs(Entity entity):base(null)
        {
            AsEntity = entity;
        }

    }
}
