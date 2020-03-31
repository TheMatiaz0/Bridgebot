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
    /// Invokes method if value is changing by the inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OnValueChangedAttribute : CyberAttribute
    {
      

        public string Call { get; }
        public OnValueChangedAttribute(string call)
        {
            Call = call ?? throw new ArgumentNullException(nameof(call));
        }
    }
}
