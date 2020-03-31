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
    [Drawer(typeof(ExtendetFoldoutGroupAttribute))]
    public class ExtendetFoldoutDrawer : IGroupDrawer
    {

       

        public void DrawGroup(IGrouping<string, MemberInfo> groupToDraw,string[] usedFolder)
        {

       
            FieldInfo first = groupToDraw.First() as FieldInfo;
            SerializedProperty firstProp = CyberEdit.Current.GetPropByName(first.Name);

            bool any = (TheEditor.DrawBeforeGroup<CustomBackgrounGroupDrawer>(groupToDraw.Key))!= null;
            BackgroundMode mode = any ? BackgroundMode.None :( BackgroundMode.Box);
            TheEditor.BeginVertical(mode);
            CyberEdit.Current.DrawProperty(first);
            Rect rect= EditorGUILayout.GetControlRect(false,0);

            rect = new Rect(rect.x, rect.y - 20, rect.width,20);

            bool[] good =
                (from item in groupToDraw.Skip(1)
                 let attribute = item.GetCustomAttribute<ExtendetFoldoutGroupAttribute>()
                 select TheEditor.CheckEquals(firstProp, attribute.Value, attribute.Equaler)).ToArray();
           
            if (good.Contains(true))
            {
                bool isFoldout = firstProp.isExpanded = EditorGUI.Foldout(rect, firstProp.isExpanded, new string(' ', 100), true);

                if (isFoldout)
                {
                    EditorGUI.indentLevel++;
                    foreach (MemberInfo item in groupToDraw.Skip(1))
                    {
                        ExtendetFoldoutGroupAttribute currentElementAttribute = item.GetCustomAttribute<ExtendetFoldoutGroupAttribute>();
                        if (TheEditor.CheckEquals(firstProp, currentElementAttribute.Value, currentElementAttribute.Equaler))
                            CyberEdit.Current.DrawMember(item);
                    }
                    EditorGUI.indentLevel--;
                }
              
                TheEditor.DrawAfteGroup<CustomBackgrounGroupDrawer>(groupToDraw.Key);
            }
            TheEditor.EndVertical(mode);



        }
    }
}
