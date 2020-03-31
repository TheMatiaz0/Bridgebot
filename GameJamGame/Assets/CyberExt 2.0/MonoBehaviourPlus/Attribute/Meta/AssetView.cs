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
    /// It will be able to show you mini editor of field type, if it isn't null.
    /// It have to be a component or a scriptableObject.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.ObjectReference)]
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetViewAttribute : CyberAttribute
    {

    }
}
