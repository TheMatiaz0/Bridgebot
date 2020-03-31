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
    [Drawer(typeof(FlagHashSetAttribute))]
    public class FlagHashSetDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttribute attribute, GUIContent content, GUIStyle style, FieldInfo field)
        {


           if( property.isExpanded= EditorGUILayout.Foldout(property.isExpanded,content,true))
            {

                EditorGUI.indentLevel++;
                FlagHashSetAttribute attr = attribute as FlagHashSetAttribute;

                var ar = property.ToEnumerable().ToArray();
                var ar2 = ar.Select(item => item.GetJustValue()).ToArray();
                HashSet<object> elements = new HashSet<object>(ar2);


               foreach(var item in elements.ToArray())
                {
                    if (attr.Values.Any(v => v.Equals(item))==false)
                        elements.Remove(item);
                }

                GUIStyle btStyle = attr.Mode.GetStyle();
             
                int index = 0;
                foreach (var item in attr.Names)
                {
                    bool val = elements.Contains(attr.Values[index]);
                    if (EditorGUILayout.Toggle(item, val, btStyle))
                    {
                        if (val == false)
                        {
                            elements.Add(attr.Values[index]);
                        }
                    }
                    else if (val)
                    {
                        elements.Remove(attr.Values[index]);
                    }
                    index++;
                }
                property.arraySize = elements.Count;
                object[] array = elements.ToArray();
                for (int x = 0; x < property.arraySize; x++)
                {
                    property.GetArrayElementAtIndex(x).SetValue(array[x]);

                }
                EditorGUI.indentLevel--;
            }

           

         
        }
    }
}
