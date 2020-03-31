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
    /// Makes dropdown instead classic fielder. 
    /// Names which user seese can be other than actually values. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownAttribute : CyberAttribute
    {
        public object[] Values { get; }
        public string[] Names { get; }
        public bool ShowAsName { get; }

        /// <summary>
        /// Names and values have to have the same lenght.
        /// Names declare what user seese, values declare what they are actually.
        /// </summary>
        /// <param name="names">What user sees</param>
        /// <param name="values">What are actually</param>
        /// <param name="showAsName"></param>
        public DropdownAttribute( string[] names, object[] values,bool showAsName=true)
        {
            Values = values ?? new object[0];
            Names = names;
            ShowAsName = showAsName;
        }

        /// <summary>
        /// Values will be show on a dropdown.
        /// </summary>
        /// <param name="values">Dropdown options.</param>
        public DropdownAttribute( object[] values)
            : this(values.Select(item=>item.ToString()).ToArray(), values,false) { }
        

        
    }
}
