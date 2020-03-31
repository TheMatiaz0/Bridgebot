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
    public enum IndentMode
    {
        Normal,
        After,
        OneShot
    }
    /// <summary>
    /// Setes the indent lv. Value can be negative. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class IndentLvAttribute : CyberAttribute
    {


        public int Quantity { get; }
      public IndentMode Mode { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity">Can be negative</param>
        /// <param name="mode"></param>
        public IndentLvAttribute(int quantity,IndentMode mode=IndentMode.Normal)
        {
            Quantity = quantity;
            Mode = mode;
        }
    }
}

