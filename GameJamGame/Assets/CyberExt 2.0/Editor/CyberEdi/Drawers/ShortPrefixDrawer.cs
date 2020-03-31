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
    [Drawer(typeof(ShortPrefixAttribute))]
    public class ShortPrefixDrawer : IPrefixDrawer
    {
        public bool DrawPrefix(GUIContent content, GUIStyle style, CyberAttribute cyberAttrribute)
        {
            var attribute = cyberAttrribute as ShortPrefixAttribute;
            EditorGUILayout.LabelField(content,GUILayout.Width(attribute.Width));
            return true;
        }
    }
}
