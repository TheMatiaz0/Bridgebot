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
using System.Reflection;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(JustShowStringAttribute))]
    public class JustShowStringDrawer : INonPropertDrawer, IDirectlyDrawer,IMethodDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttribute attribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            Draw(content, field);
        }

        public void DrawMethod(MethodInfo method, CyberAttribute cyberAttrribute)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(method.Name);
            TheEditor.PrepareToRefuseGui(this);
            GUI.enabled = false;
            EditorGUILayout.TextField( method.Invoke(CyberEdit.Current.Target,null)?.ToString()??"null");
            TheEditor.RefuseGui(this);
            EditorGUILayout.EndHorizontal();
        }

        public void DrawNonProperty(CyberAttribute cyberAttribute, FieldInfo field)
        {
            Draw(new GUIContent(field.Name), field);
        }
        private void Draw(GUIContent content,FieldInfo field)
        {
            TheEditor.PrepareToRefuseGui(this);
            GUI.enabled = false;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(content);
            EditorGUILayout.TextField(field.GetValue(CyberEdit.Current.Target).ToString());
            EditorGUILayout.EndHorizontal();
            TheEditor.RefuseGui(this);
        }
    }
}
