using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    /// <summary>
    /// If ShowWhen is false, element'll be hided.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,AllowMultiple =true)]
    public class ShowWhenAttribute : CondtionsAttribute
    {
        public ShowWhenAttribute(string serializedProp) : base(serializedProp)
        {
        }

        public ShowWhenAttribute(string serializedProp, object value) : base(serializedProp, value)
        {
        }

        public ShowWhenAttribute(string serializedProp, Equaler equaler, object value) : base(serializedProp, equaler, value)
        {
        }
    }


}
