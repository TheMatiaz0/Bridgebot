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
    [Drawer(typeof(HelpBoxWhenAttribute))]
    public class HelpBoxWhenDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttribute atr)
        {
            HelpBoxWhenAttribute helpBoxWhen = atr as HelpBoxWhenAttribute;

            if (TheEditor.CheckEquals(CyberEdit.Current.GetPropByName(helpBoxWhen.SerializedProperty), helpBoxWhen.Value, helpBoxWhen.Equaler))
                TheEditor.HelpBox(helpBoxWhen.Text, helpBoxWhen.MessageType
                    , new GUIStyle() { fixedHeight = helpBoxWhen.Height, fixedWidth = helpBoxWhen.Width });

        }
        public void DrawAfter(CyberAttribute atr)
        {

        }
    }
}
