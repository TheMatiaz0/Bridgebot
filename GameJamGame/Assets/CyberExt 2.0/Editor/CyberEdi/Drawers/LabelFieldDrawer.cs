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
    [Drawer(typeof(LabelFieldAttribute))]
    public class LabelFieldDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            LabelFieldAttribute attribute = cyberAttribute as LabelFieldAttribute;
            TheEditor.PrepareToRefuseGui(this.GetType());
           


            var rect = EditorGUILayout.GetControlRect();
            if (attribute.IgnoreIndentLv == false)
                rect = EditorGUI.IndentedRect(rect);



            GUI.Label(rect,attribute.Label, attribute.ApplyStyle(new GUIStyle(EditorStyles.label)));
            TheEditor.RefuseGui(this.GetType());

        }
        public void DrawAfter(CyberAttribute cyberAttribute)
        {

        }
    }
}
