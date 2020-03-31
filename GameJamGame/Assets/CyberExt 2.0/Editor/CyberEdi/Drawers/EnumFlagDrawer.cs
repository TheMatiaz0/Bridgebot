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
    [Drawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttribute attribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            EnumFlagsAttribute attr = attribute as EnumFlagsAttribute;
            if (property.intValue < 0)
                property.intValue = 0;
            void DrawCheckBox(GUIStyle checkStyle)
            {
                foreach (var item in Enum.GetValues(CyberEdit.Current.CurrentField.FieldType))
                {


                    int val = Convert.ToInt32(item);
                    if (val == 0)
                        continue;
                    bool has = (property.intValue | val) == property.intValue;


                    bool toogleRes = GUILayout.Toggle(has, new GUIContent(item.ToString()), checkStyle);
                    if (toogleRes == false && has == true)
                    {
                        property.intValue ^= val;
                    }
                    else if (toogleRes == true && has == false)
                    {
                        property.intValue |= val;
                    }

                }
            }
            switch (attr.EnumFlagOption)
            {
                case EnumMode.Classic:
                    property.intValue = EditorGUILayout.MaskField(GUIContent.none, property.intValue, property.enumNames);
                    break;
                default:
                    DrawCheckBox(attr.EnumFlagOption.GetStyle());
                    break;
              
            }
           

            EditorGUILayout.EndHorizontal();
        }
       
        public Enum GetEnum(int value,Type type)
        {
            return Enum.GetValues(type).OfType<Enum>().FirstOrDefault(item => Convert.ToInt32(item).Equals(value))??Enum.GetValues(type).OfType<Enum>().First();
        }
    }
}
