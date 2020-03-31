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
    [Drawer(typeof(SeparatorAttribute))]
    public class SeparatorDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            SeparatorAttribute attribute = cyberAttribute as SeparatorAttribute;
            EditorGUILayout.BeginHorizontal();

            var content = new GUIContent(attribute.Label);
            float width = EditorStyles.label.CalcSize(content).x + 2;
            if (string.IsNullOrEmpty(attribute.Label) == false)
            {
                DrawMinBox();
                EditorGUILayout.LabelField(attribute.Label, attribute.ApplyStyle( new GUIStyle("label")), GUILayout.Width(width));
                DrawMinBox();
                
            }
            else
                DrawMinBox();
            EditorGUILayout.EndHorizontal();

            void DrawMinBox()
            {

                var style = new GUIStyle("groupBox");
              
               
                EditorGUILayout.BeginVertical();              
                GUILayout.Box(GUIContent.none,style,GUILayout.Height(attribute.Height/(float)UISize.Default),GUILayout.MaxWidth(int.MaxValue));
                EditorGUILayout.EndVertical();
              
                
            }
        }
        public void DrawAfter(CyberAttribute atr)
        {

        }
    }
}
