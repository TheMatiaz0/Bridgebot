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
    /// Makes a window to set that array. It not supported a lot of other array modifer.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class WindowArrayAttribute : CyberAttribute
    {

    }
}
