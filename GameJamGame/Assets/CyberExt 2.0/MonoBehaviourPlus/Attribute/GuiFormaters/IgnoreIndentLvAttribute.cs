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
    /// Element which it is attached ignores the indent lv.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class IgnoreIndentLvAttribute : CyberAttribute
    {

    }
}
