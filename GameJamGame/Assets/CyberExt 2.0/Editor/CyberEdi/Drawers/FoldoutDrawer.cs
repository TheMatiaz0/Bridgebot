using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using Cyberevolver.Unity;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(FoldoutGroupAttribute))]
    public class FoldoutDrawer : IGroupDrawer
    {
     

        public void DrawGroup(IGrouping<string, MemberInfo> group ,string[] usedFolder )
        {
            bool clearZone = CyberEdit.Current.DeepWay.Count == 0 && usedFolder.Length ==1;
            bool res;
            bool before = CyberEdit.Current.GetExpand(group.Key);
            if (clearZone)
            {
                res= EditorGUILayout.BeginFoldoutHeaderGroup(before, group.Key);
              
            }
            else
            {
              res=  EditorGUILayout.Foldout(before, group.Key);
            }
            CyberEdit.Current.SetExpand(group.Key,res
              );
          
            
            if (CyberEdit.Current.GetExpand(group.Key))
            {


                TheEditor.DrawBeforeGroup<CustomBackgrounGroupDrawer>(group.Key);
                EditorGUILayout.BeginVertical();

                EditorGUI.indentLevel++;

                CyberEdit.Current.DrawBasicGroup(group, BackgroundMode.None,drawPrefix:false,usedGroup: usedFolder);
                EditorGUI.indentLevel--;
           
           
                TheEditor.DrawAfteGroup<CustomBackgrounGroupDrawer>(group.Key);
                EditorGUILayout.EndVertical();
            }
           
            if (clearZone)
                EditorGUILayout.EndFoldoutHeaderGroup();

        }

       
    }
}
