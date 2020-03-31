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
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(CustomGuiAttribute))]
    public class GuiDrawer : IStyleDrawer
    {
        public void ChangeGuiStyle(CyberAttribute attrribute, ref GUIStyle style,ref string customName)
        {
            var fatr = (attrribute as CustomGuiAttribute);
            style = fatr.ApplyStyle(style);
            customName = fatr.Label ?? customName;
        }

       
    }
}
