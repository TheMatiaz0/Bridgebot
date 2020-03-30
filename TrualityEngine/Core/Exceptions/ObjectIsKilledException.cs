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
    /// This object will throw , if something try do on killed object
    /// if object is killing when something task is playing, task will throw this exception.
    /// </summary>
    [Serializable]
    public class ObjectIsKilledException:ObjectStatusChangedException
    {
        public ObjectIsKilledException(IKillable eObject):base(eObject)
        {

        }
    }
}
