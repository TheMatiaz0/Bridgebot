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
    [Drawer(typeof(CyberSpaceAttribute))]
    public class CyberSpaceDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttribute atr)
        {

            EditorGUILayout.Space((atr as CyberSpaceAttribute).Power);

        }
        public void DrawAfter(CyberAttribute atr)
        {

        }
    }
}
