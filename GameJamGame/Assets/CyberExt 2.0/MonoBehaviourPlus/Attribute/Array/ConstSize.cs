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
    /// Const size for a array.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ConstSizeAttribute : CyberAttribute
    {
        public ConstSizeAttribute(int  size)
        {
            if(size<0)
            {
                throw new ArgumentException("size should be greater than 0", nameof(size));
            }
            Size = size;
        }

        public int Size { get; }
    }
}
