using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class CameraWillRenderChangeArgs : BaseEntityArgs
    {
        public bool Value { get; }
        public CameraWillRenderChangeArgs(Entity entity,bool value) : base(entity)
        {
            Value = value;
            
        }

    }
}
