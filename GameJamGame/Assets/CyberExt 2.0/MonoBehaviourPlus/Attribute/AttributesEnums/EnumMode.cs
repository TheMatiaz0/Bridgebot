using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Cyberevolver.Unity
{
    public enum EnumMode
    {
        Classic,
        Buttons,
        CheckBox,
    }
}

#if UNITY_EDITOR
namespace Cyberevolver.EditorUnity
{
    public static class EnumModeExtension
    {
        public static GUIStyle GetStyle(this EnumMode mode)
        {
            switch (mode)
            {
                case EnumMode.Buttons: return GUI.skin.button;
                case EnumMode.CheckBox: return GUI.skin.toggle;
                case EnumMode.Classic: return GUI.skin.button;
                default: throw new ArgumentException("uknown value");
            }
        }
    }
}
#endif