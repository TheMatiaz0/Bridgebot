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
    /// Drawes array as reordable list. It don't work with all types of variables.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ReordableListAttribute : CyberAttribute
    {

    }
}
