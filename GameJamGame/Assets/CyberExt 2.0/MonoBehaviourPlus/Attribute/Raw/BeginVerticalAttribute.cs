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
    /// Raw version of <see cref="StartVerticalAttribute"/>, try use it instead.
    /// <see cref="StartVerticalAttribute"/> has to be ended by a <see cref="EndAttribute"/> or a <see cref="EndAfterAttribute"/>.
    /// Anything what is between a Start and a End will be just like in a one VerticalGroup group.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method|AttributeTargets.Class|AttributeTargets.Struct,
        AllowMultiple =true)]
    public class StartVerticalAttribute : CyberAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="backgroundMode">Optional background</param>
        public StartVerticalAttribute(BackgroundMode backgroundMode=BackgroundMode.None)
        {
            BackgroundMode = backgroundMode;
        }

        public BackgroundMode BackgroundMode { get; }
    }
}

