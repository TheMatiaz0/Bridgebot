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
    [Drawer(typeof(HashSetArrayAttribute))]
    public class HashSetAttributeDrawer : IArrayModiferDrawer
    {
        public void DrawAfterAll(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
            object[] goodValues = CyberEdit.Current.CurrentProp.ToEnumerable().Select(item => item.GetJustValue())
                  .Distinct().ToArray();
            if (goodValues.Length != prop.arraySize)
            {
                EditorGUILayout.BeginHorizontal();
                TheEditor.HelpBox("Two or more identical keys", MessageType.Warning, new GUIStyle() { fixedHeight=20});
                if(GUILayout.Button("Fix"))
                    prop.SetArray(goodValues);

                EditorGUILayout.EndHorizontal();
            }
             

           
            
        }

        public void DrawAfterSize(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
          
        }

        public void DrawBeforeSize(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
           
        }
    }
}
