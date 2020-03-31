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
    [Drawer(typeof(BoxGroupAttribute))]
    public class BoxGroupDrawer : IGroupDrawer
    {

       
        public void DrawGroup(IGrouping<string, MemberInfo> groups,string[] usedGroup)
        {




            TheEditor.DrawBoxHeader(new GUIContent(groups.Key));
       
            CyberEdit.Current.DrawBasicGroup(groups,BackgroundMode.HelpBox, usedGroup, drawPrefix:false);
      
            TheEditor.DrawAfteGroup<CustomBackgrounGroupDrawer>(groups.Key);
         
        }
       
    }
}
