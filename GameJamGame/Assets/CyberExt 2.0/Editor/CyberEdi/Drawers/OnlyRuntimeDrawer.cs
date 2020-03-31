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
    [Drawer(typeof(OnlyRuntimeAttribute))]
    public class OnlyRuntimeDrawer : IShowWhenDrawer, IMetaDrawer
    {

        public void DrawBefore(CyberAttribute cyberAttrribute)
        {
            OnlyRuntimeAttribute atr = cyberAttrribute as OnlyRuntimeAttribute;
            if (Application.isPlaying == false)
                GUI.enabled = false;

        }
        public void DrawAfter(CyberAttribute atr)
        {

        }

        public bool CanDraw(CyberAttribute cyberAttrribute)
        {
            OnlyRuntimeAttribute atr = cyberAttrribute as OnlyRuntimeAttribute;
            return atr.ActionIfNotRuntime == ActionIfNotRuntime.Disable || Application.isPlaying;
        }
    }
}
