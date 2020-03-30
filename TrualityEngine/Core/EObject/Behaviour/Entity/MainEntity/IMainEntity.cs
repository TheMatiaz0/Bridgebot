using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Non generic replacement for <see cref="MainEntity{T}"/>
    /// </summary>
    interface IMainEntity
    {
        Entity SetToMain();
        Entity Main { get; }
        bool IsMain { get; }
       
    }
}
