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
    /// When ShowWhen are true, the GUI is disable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,
        AllowMultiple =true)]
    public class DisableWhenAttribute : CondtionsAttribute
    {
        public DisableWhenAttribute(string serializedProp) : base(serializedProp)
        {
        }

        public DisableWhenAttribute(string serializedProp, object value) : base(serializedProp, value)
        {
        }

        public DisableWhenAttribute(string serializedProp, Equaler equaler, object value) : base(serializedProp, equaler, value)
        {
        }
    }
}
