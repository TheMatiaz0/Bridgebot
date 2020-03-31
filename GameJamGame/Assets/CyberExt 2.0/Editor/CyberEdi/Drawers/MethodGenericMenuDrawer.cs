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
    [Drawer(typeof(MethodGenericMenuAttribute))]
    public class MethodGenericMenuDrawer : IMetaDrawer
    {
        public void DrawBefore(CyberAttribute cyberAttributer)
        {
            EditorGUILayout.BeginHorizontal();
        }
        public void DrawAfter(CyberAttribute cyberAttributer)
        {

            MethodGenericMenuAttribute atr = cyberAttributer as MethodGenericMenuAttribute;
            Type finalTarget = CyberEdit.Current.GetFinalTargetType();
            UnityEngine.Object target = CyberEdit.Current.Target;
            if (GUILayout.Button("☰",GUILayout.Width(20)))
            {
                GenericMenu genericMenu = new GenericMenu();
                foreach (var item in atr.Methods) genericMenu.AddItem(new GUIContent(item), false, (object arg)
                       => finalTarget.GetMethod(arg.ToString(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Invoke(target, null), item);
                genericMenu.ShowAsContext();
            }
            EditorGUILayout.EndHorizontal();
        }

      
    }
}
