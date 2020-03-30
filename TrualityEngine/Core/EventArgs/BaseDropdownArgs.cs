using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using TrualityEngine.Core;
namespace TrualityEngine.Core
{
    public class BaseDropdownArgs<T> : BaseEntityArgs
    {
        public override Entity AsEntity => AsDropdown;
        public BaseDropdownArgs(Dropdown<T> drop) : base(null)
        {
            AsDropdown = drop;
        }

        public virtual Dropdown<T> AsDropdown { get; }
    }

}
