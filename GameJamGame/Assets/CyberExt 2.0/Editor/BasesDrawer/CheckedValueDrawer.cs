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
namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(CheckedValue))]
    public class CheckedValueDrawer : PropertyDrawer
    {

     
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            position = EditorGUI.PrefixLabel(position, label);
            SerializedProperty current = property.FindPropertyRelative("_Current");
            SerializedProperty min = property.FindPropertyRelative("_Min");
            SerializedProperty max = property.FindPropertyRelative("_Max");

            int minV = min.intValue;
            int maxV = max.intValue;
            position.width -=4;
            float intFieldWidth = position.width* (2f / 9);
            float SliderWidth = position.width * (1f / 3);
          
            void Move()
            {
                position.x += position.width + 1;
            }
            int InitFieldAndMove(int val)
            {
                position.width = intFieldWidth;
                int res= EditorGUI.IntField(position, val);
                Move();
                return res;
            }
            int cur;
            cur = current.intValue;
            minV = InitFieldAndMove(minV);

            position.width = SliderWidth;
            cur = (int)GUI.HorizontalSlider(position,cur, minV, maxV);
            Move();
            
            maxV = InitFieldAndMove(maxV);
            cur = (int)InitFieldAndMove(cur);
     
            if (minV >= maxV)
                minV = maxV- 1;
            min.intValue = minV;
            max.intValue = maxV;
            current.intValue = CheckedValue.CheckedGet(minV, maxV, cur); ;
        }
    }
}
