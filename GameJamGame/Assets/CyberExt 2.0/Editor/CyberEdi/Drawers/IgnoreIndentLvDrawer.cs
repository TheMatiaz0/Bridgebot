﻿using System;
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
    [Drawer(typeof(IgnoreIndentLvAttribute))]
    public class IgnoreIndentLvDrawer : IMetaDrawer
    {
       private int indent;
        public void DrawBefore(CyberAttribute atr)
        {
            indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel -= indent;

        }
        public void DrawAfter(CyberAttribute atr)
        {
            EditorGUI.indentLevel += indent;
        }
    }
}
