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
    /// Declares minimal size for a array.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class MinSizeAttribute : CyberAttribute
    {
      

        public uint Min { get; }
        public MinSizeAttribute(uint min)
        {
            Min = min;
        }
    }
}
