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
    /// If ShowWhen is true, this element'll be hided.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class HideWhenAttribute : CondtionsAttribute
    {
        public HideWhenAttribute(string serializedProp) : base(serializedProp)
        {
        }

        public HideWhenAttribute(string serializedProp, object value) : base(serializedProp, value)
        {
        }

        public HideWhenAttribute(string serializedProp, Equaler equaler, object value) : base(serializedProp, equaler, value)
        {
        }
    }
}
