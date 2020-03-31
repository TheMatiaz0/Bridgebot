using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Cyberevolver.Unity
{
    [Flags]
    public enum LegalTypeFlags
    {
        None=0,
        Array=1<<0,
        GenericNonArray=1<<1,
        NonGeneric = 1 << 2,
        ObjectReference=1<<3,
        NumberValue=1<<4,
        Enum=1<<5,
     
       

    }
   
    [AttributeUsage(AttributeTargets.Class)]
    public class CyberAttributeUsageAttribute : Attribute
    {
        public LegalTypeFlags Flag { get; }
        public Type[] AdditionalLegalType { get; }
        public CyberAttributeUsageAttribute(LegalTypeFlags legalTypeFlags,params Type[] goodTypes)
        {
            Flag = legalTypeFlags;
            AdditionalLegalType = goodTypes ?? new Type[0];
        }
    }
}
