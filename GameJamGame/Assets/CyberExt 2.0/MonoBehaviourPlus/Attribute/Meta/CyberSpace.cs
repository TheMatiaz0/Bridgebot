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
    /// Just like <see cref="UnityEngine.Space"/> but it collaborates which other cyber edit attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,AllowMultiple =true)]
    public class CyberSpaceAttribute : CyberAttribute
    {
        public float Power { get; } 
        public CyberSpaceAttribute(float power=4)
        {
            Power = power;
        }

      
    }
}
