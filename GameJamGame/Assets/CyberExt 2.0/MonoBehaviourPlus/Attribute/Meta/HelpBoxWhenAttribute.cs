using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver.Unity
{
    /// <summary>
    /// If ShowWhen is true, it'll draw box with the information.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method, AllowMultiple = true)]
    public class HelpBoxWhenAttribute : HelpBoxAttribute
    {
        public string SerializedProperty { get; }
        public Equaler Equaler { get; }
        public object Value { get; }

        public HelpBoxWhenAttribute(string property, Equaler equaler, object value, string text, MessageType messageType = MessageType.Info, UISize size = UISize.Default) : base(text, messageType, size)
        {
            SerializedProperty = property;
            Equaler = equaler;
            Value = value;
        }
        public HelpBoxWhenAttribute(string property, Equaler equaler, object value, string text, MessageType messageType = MessageType.Info, float size = 0) : base(text, messageType, size)
        {
            SerializedProperty = property;
            Equaler = equaler;
            Value = value;
        }
        public HelpBoxWhenAttribute(string property, string text, MessageType messageType, UISize size=UISize.Default)
            : this(property, Equaler.Equal, true, text, messageType, size) { }
        public HelpBoxWhenAttribute(string property, string text, MessageType messageType, float size=0)
            : this(property, Equaler.Equal, true, text, messageType, size) { }
    }

      
}
