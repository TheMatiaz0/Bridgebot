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
    /// Default, it makes dropown which you can set few options in your flags. You can set the <see cref="EnumMode"/> to get other flag drawing style. 
    /// For example, just like the <see cref="EnumButtonsAttribute"/>.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Enum)]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumFlagsAttribute : CyberAttribute
    {
        public EnumFlagsAttribute(EnumMode enumFlagOption=EnumMode.Classic)
        {
            this.EnumFlagOption = enumFlagOption;
        }

        public EnumMode EnumFlagOption { get; }
    }
}
