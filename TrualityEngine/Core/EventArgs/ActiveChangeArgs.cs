using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class ActiveChangeArgs : BaseActivatedObjectArgs
    {

        public bool Value { get; }
        public ActiveChangeArgs(ActivatedObject activatedObject,bool value) : base(activatedObject)
        {
            Value = value;
        }
    }
}
