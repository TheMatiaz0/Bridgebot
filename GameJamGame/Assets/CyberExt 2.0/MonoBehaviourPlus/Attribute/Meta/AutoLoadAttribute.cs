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
    /// Addes button to automaticaly set this field from getComponent and when component is adding.
    /// If you set AngryPutIfNull, it will be automaticaly set if field equals null (EditorOnly).
    /// If you set WithChilder, component can be from children too.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.ObjectReference)]
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoLoadAttribute : CyberAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="withChildren">CIf it's true , component can be from</param>
        /// <param name="angryPutIfNull">If it's true always if field equals null will try get component.</param>
        public AutoLoadAttribute(bool withChildren=false, bool angryPutIfNull=false)
        {
            AngryPutIfNull = angryPutIfNull;
            WithChildren = withChildren;
        }

    
        public bool AngryPutIfNull { get; }
        public bool WithChildren { get; }
        
    }
}
