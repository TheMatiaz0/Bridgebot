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
    /// Raw attribute. Use to end something that need end.It'll be invoke after drawing a field.
    /// If you want to end before, use <see cref="EndAttribute"/> instead.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,AllowMultiple =true)]
    public class EndAfterAttribute : CyberAttribute
    {

    }
}
