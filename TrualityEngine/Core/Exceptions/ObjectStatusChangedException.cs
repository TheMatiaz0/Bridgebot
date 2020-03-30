using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CA1032
#pragma warning disable CA2229
namespace TrualityEngine.Core
{
    /// <summary>
    /// Abstract for the ObjectIsKilledException and the ObjectIsDeactivatedException
    /// </summary>
    [Serializable]
    public abstract class ObjectStatusChangedException:Exception
    {
        /// <summary>
        /// Object which own deactivating/killing throwing this exception
        /// </summary>
        public IKillable EObject { get; }
        public ObjectStatusChangedException(IKillable eObject)
        {
            EObject = eObject;
        }
    }
}
