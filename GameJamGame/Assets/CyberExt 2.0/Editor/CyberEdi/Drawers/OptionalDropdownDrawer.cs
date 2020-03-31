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
    [Drawer(typeof(OptionalDropdownAttribute))]
    public class OptionalDropdownDrawer : IMetaDrawer
    {
        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            EditorGUILayout.BeginHorizontal();
        }
        public void DrawAfter(CyberAttribute cyberAttribute)
        {

            OptionalDropdownAttribute atr = cyberAttribute as OptionalDropdownAttribute;

            var prop = CyberEdit.Current.CurrentProp;
            TheEditor.DrawPropertyAsDropdown(CyberEdit.Current.CurrentProp, CyberEdit.Current.CurrentField, null, null, atr.Values, (i) => prop.SetValue(i),
                   (item, index) => atr.Names[index],drawPrefix:false);
            EditorGUILayout.EndHorizontal();
        }

      
    }
}
