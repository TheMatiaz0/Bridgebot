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
    [CyberAttributeUsage(LegalTypeFlags.ObjectReference)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ResourcePrefabAttribute : CyberAttribute
    {
        public ResourcePrefabAttribute(string folder)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
        }
        public string Folder { get; }
    }
}
