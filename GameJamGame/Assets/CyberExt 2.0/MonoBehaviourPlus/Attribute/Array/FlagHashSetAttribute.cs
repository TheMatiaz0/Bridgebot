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
    /// Just like <see cref="EnumFlagsAttribute"/> but , for arrays. Array have only those value which are select.
    /// </summary>
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class FlagHashSetAttribute : CyberAttribute
    {
     

        public string[] Names { get; }
        public object[] Values { get; }
        public EnumMode Mode { get; }
        public FlagHashSetAttribute(string[] names, object[] values, EnumMode mode=EnumMode.CheckBox)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Mode = mode;
        }
        public FlagHashSetAttribute(object[] values,EnumMode mode=EnumMode.CheckBox)
            : this(values.Select(item => item?.ToString() ?? "null").ToArray(), values, mode)
        { }

    }
}
