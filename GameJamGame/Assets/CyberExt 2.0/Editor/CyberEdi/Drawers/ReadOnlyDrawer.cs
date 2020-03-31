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
    [Drawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : IMetaDrawer
    {

     
        public void DrawBefore(CyberAttribute atr)
        {

            
            GUI.enabled = false;
           

        }
        public void DrawAfter(CyberAttribute atr)
        {
           
        }
    }
}
