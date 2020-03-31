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
    /// Attach to a dictionary to change something in, for example, make it as Reorderable List. 
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.None,typeof(BaseSerializeDictionary))]
    [AttributeUsage(AttributeTargets.Field
        ,AllowMultiple =false)]
    public class DictioniarySettingsAttribute : Attribute
    {
        public DictioniarySettingsAttribute(bool tryDoReordable, bool expandable)
        {
            TryDoReordable = tryDoReordable;
            Exandable = expandable;
        }

        public bool TryDoReordable { get; }
        public bool Exandable { get; }
       

    }
}
