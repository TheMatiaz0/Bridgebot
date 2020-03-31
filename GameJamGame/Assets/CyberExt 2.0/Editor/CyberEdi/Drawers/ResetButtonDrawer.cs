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
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(ResetButtonAttribute))]
    public class ResetButtonDrawer : IMetaDrawer
    {

        
        public void DrawBefore(CyberAttribute cyberAttribute)
        {

            EditorGUILayout.BeginHorizontal();
          
        }
        public void DrawAfter(CyberAttribute cyberAttribute)
        {
            ResetButtonAttribute attribute = cyberAttribute as ResetButtonAttribute;
            if (GUILayout.Button("↺",GUILayout.Width(20)))
            {
                CyberEdit.Current.CurrentProp.SetValue(attribute.Value);
            }
               
            EditorGUILayout.EndHorizontal();
        }
    }
}
