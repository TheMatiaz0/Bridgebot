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
    [Drawer(typeof(StartVerticalAttribute))]
    public class StartVerticalDrawer : IAlwaysDrawer,IEnderDrawer,IClassDrawer
    {
      

        public void DrawAfter(CyberAttribute cyberAttribute)
        {
            
        }


        public void DrawBefore(CyberAttribute cyberAttribute)
        {
            bool before;
            before = GUI.enabled;
            GUI.enabled = true;
            StartVerticalAttribute attribute = cyberAttribute as StartVerticalAttribute;
          
            TheEditor.BeginVertical(attribute.BackgroundMode);
           
            

            GUI.enabled = before;
        }

        public void DrawBeforeClass(CyberAttribute attribute)
        {
            DrawBefore(attribute);
        }

        public void DrawEnd(CyberAttribute cyberAttribute)
        {
            StartVerticalAttribute attribute = cyberAttribute as StartVerticalAttribute;
            TheEditor.EndVertical(attribute.BackgroundMode);
           
        }

      
    }
}
