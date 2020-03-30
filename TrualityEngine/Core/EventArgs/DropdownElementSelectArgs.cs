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
    public class DropdownElementSelectArgs<T> : BaseDropdownArgs<T>
    {
        public DropdownElement<T> Element { get; }
        public DropdownElementSelectArgs(Dropdown<T> dropdown, DropdownElement<T> element) : base(dropdown)
        {
            Element = element;
        }

    }

}
