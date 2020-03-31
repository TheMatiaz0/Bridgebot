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
    [Drawer(typeof(EndAfterAttribute))]
    public class EndAfterDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttribute atr)
        {


        }
        public void DrawAfter(CyberAttribute atr)
        {
            TheEditor.Pop();
        }
    }
}
