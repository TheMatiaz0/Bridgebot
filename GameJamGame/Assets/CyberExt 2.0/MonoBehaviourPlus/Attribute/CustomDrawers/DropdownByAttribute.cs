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
    /// Dropdownby is just like dropdown, by values are getting from method. (Have to return thing that is convertable to <see cref="IEnumerable{string}"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownByAttribute : CyberAttribute
    {
        public DropdownByAttribute(string valueGetter)
        {
            ValueGetter = valueGetter ?? throw new ArgumentNullException(nameof(valueGetter));
        }

        public string ValueGetter { get; }
       
     

    }
}
