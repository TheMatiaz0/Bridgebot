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
    /// Drawes a field, which isn't serializable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowNonSerializeAttribute : CyberAttribute
    {

    }
}
