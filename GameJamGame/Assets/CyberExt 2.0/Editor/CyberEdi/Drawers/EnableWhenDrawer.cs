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
    [Drawer(typeof(EnableWhenAttribute))]
    public class EnableWhenDrawer : IMetaDrawer
    {

     
        public void DrawBefore(CyberAttribute atr)
        {
          
            var fatr = (atr as EnableWhenAttribute);
            GUI.enabled = TheEditor.CheckEquals(fatr);

        }
        public void DrawAfter(CyberAttribute atr)
        {
            
        }
    }
}
