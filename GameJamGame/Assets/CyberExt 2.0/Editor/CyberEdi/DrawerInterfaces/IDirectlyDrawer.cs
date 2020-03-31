using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{
    public interface IDirectlyDrawer:ICyberDrawer
    {
        void DrawDirectly(SerializedProperty property, CyberAttribute attribute,GUIContent content, GUIStyle style,FieldInfo field);
    }
}
