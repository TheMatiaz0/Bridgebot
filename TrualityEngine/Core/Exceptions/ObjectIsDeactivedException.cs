using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable CA1032
#pragma warning disable CA2229

namespace TrualityEngine.Core
{
    /// <summary>
    /// This object will throw , if something try do on deactivate object, what is illegal on deactivated object.
    /// </summary>
    [Serializable]
    public class ObjectIsDeactivedException:ObjectStatusChangedException
    {
        public ObjectIsDeactivedException(EObject eObject) : base(eObject)
        {

        }
    }
}
