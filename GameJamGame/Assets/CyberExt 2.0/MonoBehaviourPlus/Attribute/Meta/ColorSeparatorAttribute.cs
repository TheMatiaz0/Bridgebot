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
    /// Makes nice looking seperator which color.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method|AttributeTargets.Class|AttributeTargets.Struct)]
    public class ColorSeparatorAttribute : ColorAttribute
    {

        public float Height { get; set; } = 3;
        public ColorSeparatorAttribute(string rgb) : base(rgb)
        {
        }

      

        public ColorSeparatorAttribute(AColor color) : base(color)
        {
        }
       
    }
}
