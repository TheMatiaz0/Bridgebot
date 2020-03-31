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
using UnityEditorInternal;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(SortingLayerAttribute))]
    public class SortingLayerDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttribute atrribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(content);
            string[] all= GetAllSortingLayers();
           
            string val = property.stringValue;
            if(all.Any(item=>item!=property.stringValue)==false)
            {
               val = all[0];
            }
            if(EditorGUILayout.DropdownButton(new GUIContent(val),FocusType.Passive))
            {
                GenericMenu generic = new GenericMenu();
                foreach (string item in all)
                {
                    generic.AddItem(new GUIContent(item), false,
                        (data) =>
                        {
                            property.stringValue = data.ToString();
                            property.serializedObject.ApplyModifiedProperties();
                        }, item);
                }
                generic.ShowAsContext();
            }
            property.stringValue = val;
            
            EditorGUILayout.EndHorizontal();
        }
        private string[] GetAllSortingLayers()
        {
            Type internalEditorUtilityType = typeof(InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            return (string[])sortingLayersProperty.GetValue(null, new object[0]);
        }
    }
}
