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
    /// Just like the <see cref="DropdownAttribute"/> but the normal field is drawing too.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OptionalDropdownAttribute : DropdownAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">Dropdown options.</param>
        public OptionalDropdownAttribute(object[] values) : base(values)
        {
        }
        /// <summary>
        /// Names and values have to have the same lenght.
        /// Names declare what user seese, values declare what they are actually.
        /// </summary>
        /// <param name="names">What user sees</param>
        /// <param name="values">What are actually</param>
        /// <param name="showAsName"></param>

        public OptionalDropdownAttribute(string[] names, object[] values, bool showAsName = true) : base(names, values, showAsName)
        {
        }
    }
}
