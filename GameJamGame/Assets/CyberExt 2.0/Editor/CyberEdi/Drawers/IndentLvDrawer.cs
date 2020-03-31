using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using UnityEditor;
using System.Collections;
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(IndentLvAttribute))]
    public class IndentLvDrawer : IAlwaysDrawer
    {
        public void DrawAfter(CyberAttribute atr)
        {
            IndentLvAttribute indentLvAttribute = atr as IndentLvAttribute;
            switch(indentLvAttribute.Mode)
            {
                case IndentMode.After:
                    Draw(indentLvAttribute);break;
                case IndentMode.OneShot:
                    EditorGUI.indentLevel -= (atr as IndentLvAttribute).Quantity;break;

            }
             
        }
        public void DrawBefore(CyberAttribute atr)
        {
            IndentLvAttribute indentLvAttribute = atr as IndentLvAttribute;
            if (indentLvAttribute.Mode!=IndentMode.After)
                Draw(indentLvAttribute);
        }
        private void Draw(IndentLvAttribute atr)
        {
            EditorGUI.indentLevel += atr.Quantity;
        }
    }
}

