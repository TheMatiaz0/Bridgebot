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
    [Drawer(typeof(ShowNonSerializeAttribute))]
    public class ShowNoSerializeDrawer : INonPropertDrawer
    {
        public void DrawNonProperty(CyberAttribute cyberAttribute, FieldInfo field)
        {

            TheEditor.PrepareToRefuseGui(this);
            GUI.enabled = false;
            object value = field.GetValue(CyberEdit.Current.Target);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(field.Name);
            try
            {
                TheEditor.GeneralField(value?.ToString(), value as UnityEngine.Object, field.FieldType);
            }
            catch(ArgumentException)
            {
                Debug.LogWarning("This value is not supportet by DrawNonProperty, use [JustShowString] instead");
                EditorGUILayout.TextField(value.ToString());
            }
            EditorGUILayout.EndHorizontal();
            TheEditor.RefuseGui(this);
            
            
        }
    }
}
