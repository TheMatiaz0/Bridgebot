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
    
   
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,
        AllowMultiple =true)]
    public class ExtendetFoldoutGroupAttribute : GroupAttribute
    {
        public Equaler Equaler { get; }
        public object Value { get; }

        public ExtendetFoldoutGroupAttribute(string folder, Equaler equaler,object value)
            :base(folder)
        {
            Equaler = equaler;
            Value = value;
        }
        public ExtendetFoldoutGroupAttribute(string folder, object value)
            : this(folder, Equaler.Equal, value) { }
        public ExtendetFoldoutGroupAttribute(string folder)
            : this(folder, Equaler.Always, true) { }
        
    }
}
