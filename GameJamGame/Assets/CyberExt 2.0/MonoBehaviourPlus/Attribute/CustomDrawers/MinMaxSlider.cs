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
    /// Slider with min and max value. Attach this to <see cref="Vector2"/>,<see cref="Vector2Int"/> or <see cref="Range"/>.
    /// </summary>
  [CyberAttributeUsage(LegalTypeFlags.None,typeof(Vector2),typeof(Vector2Int), typeof(Range))]
    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxSliderAttribute : CyberAttribute
    {
      

        public float Min { get; }
        public float Max { get; }
        public MinMaxSliderAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
        public MinMaxSliderAttribute(int min, int max)
            : this((float)min, max) { }
    }
}
