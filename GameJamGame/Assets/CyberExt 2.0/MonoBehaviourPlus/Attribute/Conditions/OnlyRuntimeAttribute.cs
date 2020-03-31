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
    public enum ActionIfNotRuntime
    {
        Disable,
        DontDraw,
    }
    /// <summary>
    /// Disables GUI if game isn't playing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class OnlyRuntimeAttribute : CyberAttribute
    {
     
        public ActionIfNotRuntime ActionIfNotRuntime { get; }
        public OnlyRuntimeAttribute(ActionIfNotRuntime actionIfNotRuntime=ActionIfNotRuntime.Disable)
        {
            ActionIfNotRuntime = actionIfNotRuntime;
        }

    }
}
