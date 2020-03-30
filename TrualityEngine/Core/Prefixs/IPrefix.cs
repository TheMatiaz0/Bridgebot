using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Interface for Prefix class
    /// </summary>
    public interface IPrefix
    {
        IReadOnlyList<Action> GetCreator();
    }
}
