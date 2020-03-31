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
    [Drawer(typeof(HorizontalGroupAttribute))]
    public class HorizontalGroupDrawer : IGroupDrawer
    {
        public void DrawGroup(IGrouping<string, MemberInfo> groups,string[] usedFolder)
        {
            BackgroundMode mode = BackgroundMode.None;
            EditorGUILayout.BeginHorizontal();
            CyberEdit.Current.PushHorizontalStack();
            if (TheEditor.DrawBeforeGroup<CustomBackgrounGroupDrawer>(groups.Key) != null)
                mode = BackgroundMode.None;

        
         
            CyberEdit.Current.DrawBasicGroup(groups, mode,usedGroup:usedFolder,drawPrefix: false);
         
            TheEditor.DrawAfteGroup<CustomBackgrounGroupDrawer>(groups.Key);
            EditorGUILayout.EndHorizontal();
            CyberEdit.Current.PopHorizontalStack();
        }
    }
}
