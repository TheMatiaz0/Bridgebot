﻿using Cyberevolver.Unity;
using System;
using UnityEditor;

namespace Cyberevolver.EditorUnity
{
    public static class LegalTypeFlagsExtension
    {
        public static bool IsGoodWith(this LegalTypeFlags flag,Type[] goodTypes ,SerializedProperty prop)
        {



           
            if(goodTypes!=null)
            {
                foreach (var item in goodTypes)
                    if (TheReflection.Is(CyberEdit.Current.CurrentField.FieldType, item))
                        return true;
            }

            if (flag.HasFlag(LegalTypeFlags.Array) && prop.isArray)
                return true;
            if (flag.HasFlag(LegalTypeFlags.GenericNonArray) && prop.propertyType == SerializedPropertyType.Generic && prop.isArray == false)
                return true;
            if (flag.HasFlag(LegalTypeFlags.NonGeneric) && prop.propertyType != SerializedPropertyType.Generic)
                return true;
            if (flag.HasFlag(LegalTypeFlags.ObjectReference) && prop.propertyType == SerializedPropertyType.ObjectReference)
                return true;  
            if (flag.HasFlag(LegalTypeFlags.NumberValue) && prop.GetFieldType().IsNumber())
                return true;
            if (flag.HasFlag(LegalTypeFlags.Enum) && prop.propertyType == SerializedPropertyType.Enum)
                return true;
            return false;

        }
    }
}
