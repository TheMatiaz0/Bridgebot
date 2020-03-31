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
    /// Addes generic menu close to the method/field.
    /// Generic menu elements invoke methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,AllowMultiple =true)]
    public class MethodGenericMenuAttribute : CyberAttribute
    {
        public MethodGenericMenuAttribute(params string[] methods)
        {
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
        }

        public string[] Methods { get; }
    }
}
