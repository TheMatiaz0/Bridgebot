using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public interface IKillable
    {
        bool IsKill { get; }
        bool IsActive { get; }
      
    }
}
