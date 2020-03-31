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
    public enum CyberObjectMode
    {
        Expandable,
        InOneLine,
        AlwaysExpanded,
        JustLikeNormalFields,
        HeaderAndFields,
        BoxGroup
      
    }
    /// <summary>
    /// Serializable struct/class which have it will be draw using the cyber inspector. 
    /// It possible to change a drawing style, for example like a BoxGroup.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
    public class ShowCyberInspectorAttribute : CyberAttribute
    {
        

        public CyberObjectMode CyberObjectMode { get; }
        public BackgroundMode BackgroundMode { get; }
        public bool NameIn { get; }
        public bool HidePrefix { get; set; }
        public ShowCyberInspectorAttribute(
            CyberObjectMode cyberObjectMode = CyberObjectMode.Expandable,
            BackgroundMode backgroundMode = BackgroundMode.None,
            bool nameIn=true)
        {
            NameIn = nameIn;
            CyberObjectMode = cyberObjectMode;
            BackgroundMode = backgroundMode;
        }




    }
}
