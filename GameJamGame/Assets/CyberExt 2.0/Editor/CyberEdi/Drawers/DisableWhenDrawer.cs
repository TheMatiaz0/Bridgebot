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
    [Drawer(typeof(DisableWhenAttribute))]
    public class DisableWhenDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            var attr = (cyberAttribute as DisableWhenAttribute);
            GUI.enabled = !TheEditor.CheckEquals(attr);

        }
        public void DrawAfter(CyberAttribute atr)
        {

        }
    }
}
