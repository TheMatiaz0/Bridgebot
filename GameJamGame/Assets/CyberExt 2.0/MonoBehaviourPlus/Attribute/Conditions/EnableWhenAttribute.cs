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
    /// When ShowWhen is false, the GUI is disable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method
        ,AllowMultiple =true)]
    public class EnableWhenAttribute : CondtionsAttribute
    {
        public EnableWhenAttribute(string serializedProp) : base(serializedProp)
        {
        }

        public EnableWhenAttribute(string serializedProp, object value) : base(serializedProp, value)
        {
        }

        public EnableWhenAttribute(string serializedProp, Equaler equaler, object value) : base(serializedProp, equaler, value)
        {
        }
    }
}
