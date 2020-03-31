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
    /// Makes the GUI disable forever.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class ReadOnlyAttribute : CyberAttribute
    {

    }
}
