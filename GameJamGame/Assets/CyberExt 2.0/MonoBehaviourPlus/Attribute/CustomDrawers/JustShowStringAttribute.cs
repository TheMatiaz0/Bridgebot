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
    /// Just showing result of <see cref="object.ToString"/>. It can be attach to field or method which aren't returning <see cref="void"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class JustShowStringAttribute : CyberAttribute
    {
        
    }
}
