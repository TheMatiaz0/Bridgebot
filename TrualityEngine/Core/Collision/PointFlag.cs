using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Use this flags to declare which point collision should be ignore
    /// </summary>
    [Flags]
    public enum PointFlags
    {
        None = 0,
        Mouse = 1 << 0
    }
}
