using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using UnityEditor;
using System.Collections;
namespace  Cyberevolver.EditorUnity
{
    [Drawer(typeof(StartHorizontalAttribute))]
    public class StartHorizontalDrawer : IAlwaysDrawer,IEnderDrawer,IClassDrawer
    {
        public void DrawBeforeClass(CyberAttribute attribute)
        {
            DrawBefore(attribute);
        }

        public void DrawAfter(CyberAttribute cyberAttribute)
        {
            
        }

        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            bool before;
            before = GUI.enabled;
            GUI.enabled = true;
            var attribute = cyberAttribute as StartHorizontalAttribute;
            EditorGUILayout.BeginHorizontal();
            
         
            if (attribute.Name != null)
                EditorGUILayout.PrefixLabel(new GUIContent((attribute.Name)),new GUIStyle(),new GUIStyle() { fixedWidth=20});
            if (attribute.RightPush != 0)
                GUILayout.Label("", GUILayout.Width(attribute.RightPush));
            TheEditor.BeginHorizontal(attribute.BackgroundMode);
            CyberEdit.Current.PushHorizontalStack();
            GUI.enabled = before;
        }

        public void DrawEnd(CyberAttribute cyberAttribute)
        {
            var attribute = cyberAttribute as StartHorizontalAttribute;
            TheEditor.EndHorizontal(attribute.BackgroundMode);
            EditorGUILayout.EndHorizontal();

            CyberEdit.Current.PopHorizontalStack();
        }
    }
}
