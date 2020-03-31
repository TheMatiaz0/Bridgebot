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
    [Drawer(typeof(HideWhenAttribute))]
    public class HideWhenDrawer : IShowWhenDrawer
    {
        public bool CanDraw(CyberAttribute cyberAttrribute)
        {
            HideWhenAttribute attr = cyberAttrribute as HideWhenAttribute;
            return !TheEditor.CheckEquals(attr);
        }
    }
}
