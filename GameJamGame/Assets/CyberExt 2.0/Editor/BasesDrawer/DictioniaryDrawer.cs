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
using UnityEditorInternal;

namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(BaseSerializeDictionary),true)]
    public class DictioniaryDrawer : PropertyDrawer
    {
        private ReorderableList reorderable;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }

      
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
       
            DictioniarySettingsAttribute attribute = CyberEdit.Current.CurrentField?.GetCustomAttributes(true).OfType<DictioniarySettingsAttribute>().FirstOrDefault();
            attribute = attribute ?? new DictioniarySettingsAttribute(false, true);
            
            SerializedProperty keys = property.FindPropertyRelative("keys");
            SerializedProperty values = property.FindPropertyRelative("values");
            bool isAdv = (values.arraySize > 0 && values.GetArrayElementAtIndex(0).propertyType == SerializedPropertyType.Generic);
            bool doReordable = ((attribute?.TryDoReordable ) == false) || (values.arraySize > 0 && values.GetArrayElementAtIndex(0).propertyType == SerializedPropertyType.Generic);
            EditorGUILayout.BeginVertical();
            if (doReordable==false&&reorderable==null)
            {
                reorderable = new ReorderableList(CyberEdit.Current.SerializedObject, keys, true, true, true, true)
                {
                    drawHeaderCallback =
                    (Rect rect) =>
                    {
                        EditorGUI.LabelField(rect, property.displayName);
                    },

                    drawElementCallback =   
                   (Rect rect, int i, bool isActive, bool isFocused) =>
                   {
                       rect.width /= 2;
                       EditorGUI.PropertyField(rect, keys.GetArrayElementAtIndex(i), GUIContent.none);
                       rect.x += rect.width;
                       EditorGUI.PropertyField(rect, values.GetArrayElementAtIndex(i), GUIContent.none);
                   }
                };

            }
              
            ChangeSize(keys.arraySize);
            void ChangeSize(int newSize)
            {
                keys.arraySize = newSize;
                values.arraySize = newSize;
            }
           

            IEnumerable<(SerializedProperty key, SerializedProperty value)> connected = keys.ToEnumerable().Zip(values.ToEnumerable(), (key, value) => (key, value));          
            bool IsExpand() => ((attribute?.Exandable) == false || (property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true)));

            int index = 0;
            bool? isExpand = null;
            if (doReordable &&(isExpand= IsExpand())==true)
            {

                EditorGUI.indentLevel++;
                object[] keyValues = keys.ToEnumerable().Select(element => element.GetJustValue()).ToArray();
                if ((keyValues.Distinct().Count() == keyValues.Length) == false)
                {
                    TheEditor.HelpBox("Key duplicate", MessageType.Warning, new GUIStyle() { fixedHeight = 30 });

                }


                foreach ((SerializedProperty key, SerializedProperty value) in connected)
                {
                    index++;
                    if (isAdv)
                        EditorGUILayout.BeginVertical("GroupBox");
                    else
                        EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    if (isAdv)
                        EditorGUILayout.BeginHorizontal("HelpBox");
                  
                    EditorGUILayout.PropertyField(key, (isAdv)?new GUIContent($"Key:"):GUIContent.none);

                    TheEditor.PrepareToRefuseGui(this);
                    GUI.color = Color.red;
                    if (isAdv)
                        EditorGUILayout.EndHorizontal();
                    TheEditor.RefuseGui(this);
                    EditorGUILayout.EndHorizontal();
                    if (isAdv)
                        EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(value, (isAdv) ? new GUIContent($"Value:"): GUIContent.none);
                    if (isAdv)
                    {
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndVertical();
                    }
                    else
                        EditorGUILayout.EndHorizontal();




                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("");
                TheEditor.PrepareToRefuseGui(this);
                if (GUILayout.Button("+", GUILayout.MinWidth(10), GUILayout.MaxWidth(30)))
                {
                    ChangeSize(keys.arraySize + 1);
                }
           
                if (GUILayout.Button("-", GUILayout.MinWidth(10), GUILayout.MaxWidth(30)))
                {
                    ChangeSize(keys.arraySize - 1);
                }
                TheEditor.RefuseGui(this);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                
                EditorGUI.indentLevel--;

            }
            else if(doReordable==false||(isExpand??IsExpand()))
            {
             
                reorderable.DoLayoutList();
                
            }
            EditorGUILayout.EndHorizontal();
        }


    }
}
