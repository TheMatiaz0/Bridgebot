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
    /// They'll show as normal fields instead array.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ArrayAsFieldsAttribute : CyberAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="names">Names of fields</param>
        public ArrayAsFieldsAttribute(params string[] names)
        {
            Names = names;
        }

        public string[] Names { get; }

    }
}
