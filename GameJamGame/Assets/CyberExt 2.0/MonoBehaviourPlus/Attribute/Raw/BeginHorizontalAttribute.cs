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
    /// Raw version of <see cref="HorizontalGroupAttribute"/>, try use it instead.
    /// <see cref="StartHorizontalAttribute"/> has to be ended by a <see cref="EndAttribute"/> or a <see cref="EndAfterAttribute"/>.
    /// Anything what is between a Start and a End will be just like in a one horizontal group.
    /// It's recommended to use <see cref="HidePrefixAttribute"/> or <see cref="ShortPrefixAttribute"/> on elements between.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method|AttributeTargets.Class|AttributeTargets.Struct
        ,AllowMultiple =true)]
    public class StartHorizontalAttribute : CyberAttribute
    {
      

        public BackgroundMode BackgroundMode { get; }
        public float RightPush { get; set; } = 0;
        public string Name { get; }
        /// <summary>
        /// </summary>
        /// <param name="name">Optional prefix.</param>
        /// <param name="mode">Optional background.</param>
        public StartHorizontalAttribute(string name = null, BackgroundMode mode = BackgroundMode.None)
        {
            BackgroundMode = mode;
            Name = name;
        }
        /// <summary>
        /// </summary>
        /// <param name="mode">Optional background.</param>
        /// <param name="name">Optional prefix.</param>
        public StartHorizontalAttribute(BackgroundMode mode,string name=null)
            : this(name, mode) { }

    }
}
