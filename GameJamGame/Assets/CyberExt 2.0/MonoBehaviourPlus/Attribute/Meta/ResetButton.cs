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
    /// Addes a reset button, which resets value.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.NonGeneric)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ResetButtonAttribute : CyberAttribute
    {
      

        public object Value { get; }
        public ResetButtonAttribute(object value)
        {
            Value = value;
        }

    }
}
