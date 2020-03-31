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
    /// If some elemenent have the same folder name, they'll be draw in the same foldout. Can be nested, like all groups.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method,AllowMultiple =true)]
    public class FoldoutGroupAttribute : GroupAttribute
    {
      
        
        public FoldoutGroupAttribute(string folder) : base(folder)
        {

        }
    }
}
