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
    [Drawer(typeof(SimplyBoxGroupAttribute))]
    public class SimplyBoxGroupDrawer : IGroupDrawer
    {
        public void DrawGroup(IGrouping<string, MemberInfo> groups,string[] usedFolder)
        {
            CyberEdit.Current.DrawBasicGroup(groups,BackgroundMode.HelpBox, usedFolder);
        }
    }
}
