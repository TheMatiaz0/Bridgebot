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
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method
        ,AllowMultiple =true)]
    public class BoxGroupAttribute : GroupAttribute
    {
        public BoxGroupAttribute(string folder) : base(folder)
        {
        }
    }
}
