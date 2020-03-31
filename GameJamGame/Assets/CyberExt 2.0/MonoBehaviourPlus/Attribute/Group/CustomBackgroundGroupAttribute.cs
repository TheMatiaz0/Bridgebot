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
    /// Attach this to a class to change default background for a group.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct,AllowMultiple =true)]
    public class CustomBackgroundGroupAttribute : GroupModiferAttribute
    {
        public CustomBackgroundGroupAttribute( string folder, BackgroundMode backgroundMode) :base(folder)
        {
            BackgroundMode = backgroundMode;
   
          
        }
        

        public BackgroundMode BackgroundMode { get; }
       

    }
}
