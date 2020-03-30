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
    public class DropdownElement<T>
    {
        public Dropdown<T> Main { get; }
      
        public Button ButtonItself { get; internal set; }

        internal DropdownElement(T value, Action<Dropdown<T>, DropdownElement<T>> onClick, Dropdown<T> main )
        {
            Value = value;
            if(onClick!=null)
            {
                OnSelect.Value += (s, e) => onClick.Invoke(e.AsDropdown, e.Element);
            }
         
            Main = main;
         
        }

        public T Value { get; set; }
        public Event<DropdownElementSelectArgs<T>> OnSelect { get; protected set; } = Event.Empty;
        internal void InvokeOnClick()
        {
            OnSelect.Invoke(this, new DropdownElementSelectArgs<T>(Main, this));
        }
    }

}
