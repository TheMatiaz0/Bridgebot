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
namespace  Cyberevolver.EditorUnity
{
    [Drawer(typeof(BackgroundAttribute))]
    public class BackgroundDrawer : IMetaDrawer
    {
       
        public  void DrawBefore(CyberAttribute atr)
        {


            TheEditor.PrepareToRefuseGui(this.GetType());
            Color color = ((BackgroundAttribute)atr).CurColor;
            GUI.backgroundColor = (Color)color;
        }
        public  void DrawAfter(CyberAttribute atr)
        {
            TheEditor.TryRefuseGui(this.GetType());
        }
    }
}
