using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class BaseBehaviourArgs:BaseActivatedObjectArgs
    {
        public override ActivatedObject AsActivatedObject => AsBaseBehaviour;
        public virtual Behaviour AsBaseBehaviour { get;}
        public BaseBehaviourArgs(Behaviour behaviour):base(null)
        {
            AsBaseBehaviour = behaviour;
        }
    }
}
