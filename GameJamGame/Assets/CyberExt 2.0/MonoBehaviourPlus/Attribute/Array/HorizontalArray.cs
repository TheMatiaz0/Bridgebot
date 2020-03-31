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
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class HorizontalArrayAttribute : CyberAttribute
    {
        public float MinWith { get; }
        public int ElementAtOneWide { get; }
        public HorizontalArrayAttribute( int elementAtOneWide, float minWidth)
        {
            
            ElementAtOneWide = elementAtOneWide;
            MinWith = minWidth;
        }

        public HorizontalArrayAttribute(int elementAtOneWide)
            : this(elementAtOneWide, 1.0f / elementAtOneWide * 60) { }
      
        public HorizontalArrayAttribute()
            : this(6, 10) { }
      

       

    }
}
