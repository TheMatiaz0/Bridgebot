using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver.Unity
{
  
    /// <summary>
    /// Draws enum field with the button style. Nice look without prefix. It isn't so good for enums which a lot of elements.
    /// If you want receive the same effect which flags enum, use <see cref="EnumFlagsAttribute"/> and set the <see cref="Cyberevolver.Unity.EnumMode"/> to the Buttons, instead.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Enum)]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumButtonsAttribute : CyberAttribute
    {
       

        public EnumMode EnumMode { get; }
        public EnumButtonsAttribute(EnumMode enumMode =EnumMode.Buttons)
        {
            EnumMode = enumMode;
        }
    }
}
