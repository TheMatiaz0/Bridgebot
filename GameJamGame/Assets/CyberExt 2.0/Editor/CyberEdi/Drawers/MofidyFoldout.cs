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
    [Drawer(typeof(CustomBackgroundGroupAttribute))]
    public class CustomBackgrounGroupDrawer : IGroupModifer
    {
        public void BeforeGroup(CyberAttribute cyberAttribute)
        {
            CustomBackgroundGroupAttribute attribute = cyberAttribute as CustomBackgroundGroupAttribute;
            TheEditor.BeginHorizontal(attribute.BackgroundMode);   
        }
        public void AfterGroup(CyberAttribute cyberAttribute)
        {
            CustomBackgroundGroupAttribute attribute = cyberAttribute as CustomBackgroundGroupAttribute;
            TheEditor.EndHorizontal(attribute.BackgroundMode);
        }

        
    }
}
