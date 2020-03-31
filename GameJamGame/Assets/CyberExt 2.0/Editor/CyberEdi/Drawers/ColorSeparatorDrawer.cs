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
    [Drawer(typeof(ColorSeparatorAttribute))]
    public class ColorSeparatorDrawer : IMetaDrawer,IClassDrawer
    {
        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            ColorSeparatorAttribute colorSeparatorAttribute = cyberAttribute as ColorSeparatorAttribute;
           
         
            EditorGUI.DrawRect(EditorGUI.IndentedRect( EditorGUILayout.GetControlRect(true,colorSeparatorAttribute.Height)), colorSeparatorAttribute.CurColor);
        }
        public void DrawAfter(CyberAttribute cyberAttribute)
        {
           
        }

        public void DrawBeforeClass(CyberAttribute attribute)
        {
            DrawBefore(attribute);
        }
    }
}
