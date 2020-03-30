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
namespace  TrualityEngine.Core
{
    public class ChangeCurrentDropdownEventArgs<T> : BaseDropdownArgs<T>
    {


        public DropdownElement<T> LastElement { get; }
        public DropdownElement<T> NewElement { get; }
        public ChangeCurrentDropdownEventArgs(Dropdown<T> dropdown, DropdownElement<T> last, DropdownElement<T> @new) : base(dropdown)
        {

            LastElement = last;
            NewElement = @new;


        }

    }

}
