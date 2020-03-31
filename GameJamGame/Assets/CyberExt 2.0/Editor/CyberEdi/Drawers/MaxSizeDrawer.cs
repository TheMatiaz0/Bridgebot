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
    [Drawer(typeof(MaxSizeAttribute))]
    public class MaxSizeDrawer : IMetaDrawer,IArrayModiferDrawer
    {

        public void DrawBefore(CyberAttribute atr)
        {


        }
        public void DrawAfter(CyberAttribute cyberAttribute)
        {
            MaxSizeAttribute attribute = cyberAttribute as MaxSizeAttribute;
            if (CyberEdit.Current.CurrentProp == null)
                return;
            if (CyberEdit.Current.CurrentProp.arraySize > attribute.Max)
                CyberEdit.Current.CurrentProp.arraySize = (int)attribute.Max;
        }

        public void DrawAfterSize(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
            
        }

        public void DrawBeforeSize(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
            TheEditor.ShortLabelField(new GUIContent( $"Max:{(cyberAttrribute as MaxSizeAttribute).Max}"), new GUIStyle { fontStyle = FontStyle.Italic });
        }

        public void DrawAfterAll(SerializedProperty prop, CyberAttribute cyberAttrribute)
        {
           
        }
    }
}
