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
    [Drawer(typeof(ConstSizeAttribute))]
    public class ConstSizeDrawer : IArrayModiferDrawer
    {
        private bool befor;
       
       

        public void DrawAfterSize(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
            var attribute = cyberAttrribute as ConstSizeAttribute;
            if (prop.arraySize != attribute.Size)
                prop.arraySize = attribute.Size;
            GUI.enabled = befor;
            

        }
        public void DrawBeforeSize(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
            befor = GUI.enabled;
            GUI.enabled = false;
        }
        public void DrawAfterAll(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {

        }

    }
}
