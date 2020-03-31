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
    /// If some elemenent have the same folder name, they'll be draw in the same group. Can be nested, like all groups.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Class|AttributeTargets.Struct,AllowMultiple =true)]
    public class SimplyBoxGroupAttribute : GroupAttribute
    {
        public SimplyBoxGroupAttribute(string folder) : base(folder)
        {
        }
    }
}
