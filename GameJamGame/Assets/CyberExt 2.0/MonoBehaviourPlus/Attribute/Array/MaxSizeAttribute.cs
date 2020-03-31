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
    /// Declares max size, for a array.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class MaxSizeAttribute : CyberAttribute
    {
      

        public uint Max { get; }
        public MaxSizeAttribute(uint max)
        {
            Max = max;
        }
    }
}
