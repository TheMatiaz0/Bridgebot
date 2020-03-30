using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
    public class ValueInEntityChangedArgs<T> : BaseEntityArgs
    {
        public T Value { get; }
        public ValueInEntityChangedArgs(Entity entity,T value) : base(entity)
        {
            Value = value;
        }
    }
}
