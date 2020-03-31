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
using System.Reflection;
using System.IO;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(ResourcePrefabAttribute))]
    public class ResourcePrefabDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttribute attribute, GUIContent content, GUIStyle style, FieldInfo field)
        {

            ResourcePrefabAttribute attr = attribute as ResourcePrefabAttribute;
            UnityEngine.Object[] elements;
            IEnumerable<UnityEngine.Object> collection = Resources.LoadAll($"{attr.Folder}");
            if (field.FieldType.Is(typeof(Component)))
                collection = from element in collection
                             select (element as GameObject).GetComponent(field.FieldType);
            elements =
                (from item in collection
                 where item != null
                 select item).ToArray();
            TheEditor.DrawPropertyAsDropdownWithFixValue(property, CyberEdit.Current.CurrentField, content, style, elements,
                          (i) => property.objectReferenceValue = i as UnityEngine.Object, property.GetJustValue(), (element, index) => ((UnityEngine.Object)element).name, true);

        }
    }
}
