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
    /// Raw attribute. Use to end something that need a end. It'll be invoke before drawing a field.
    /// If you want to end after, use <see cref="EndAfterAttribute"/> instead.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,
        AllowMultiple =true)]
    public class EndAttribute : CyberAttribute
    {

    }
}

