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
    [Drawer(typeof(EndAttribute))]
    public class EndDrawer : IMetaDrawer
    {
        public  void DrawAfter(CyberAttribute atr)
        {
           
        }

        public  void DrawBefore(CyberAttribute atr)
        {

            TheEditor.Pop();

        }
    }
}
