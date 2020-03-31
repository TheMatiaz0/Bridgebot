using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Cyberevolver.Unity
{
   public enum CalcMode
    { 
        UseContextSettings,
        NoCalc,
        Calc
    }
    
    /// <summary>
    /// Makes debug button. It can be attach to a method, or a field. 
    /// If you are attaching this to field, you have to set <see cref="Method"/> property. 
    /// Set <see cref="InNextLine"/> to true if you want to create button in next line.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Field,
        AllowMultiple =true)]
    public class ButtonAttribute : ColorAttribute
    {
      
        public string Text { get; } 
        public UnityEventCallState WhenCanPress { get; }
        public float Height { get; }
        public string Method { get; set; }
        public bool InNextLine { get; set; }
        public CalcMode CalcMode { get; set; }
        public bool After { get; set; } = true;


        public ButtonAttribute(string text="",AColor color=AColor.None, 
            UnityEventCallState whenCanPress=UnityEventCallState.EditorAndRuntime,
            UISize size=UISize.Default)
            :base(color)
        {

            Height = (int)size;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            WhenCanPress = whenCanPress;
        }
        public ButtonAttribute( UnityEventCallState whenCanPut, string text = "")
            : this(text, AColor.None,whenCanPut) { }
        public ButtonAttribute(UISize size)
            : this("", AColor.None,UnityEventCallState.EditorAndRuntime, size) { }



    }
}
