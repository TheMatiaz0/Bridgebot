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
    /// Elements which is in, shall not pass in your string.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.None,typeof(string))]
    [AttributeUsage(AttributeTargets.Field)]
    public class IllegalSymbolsAttribute : CyberAttribute
    {
        

        public string[] Symbols { get; }
        public IllegalSymbolsAttribute(params string[] symbols)
        {
            Symbols = symbols ?? new string[0];
        }
    }
}
