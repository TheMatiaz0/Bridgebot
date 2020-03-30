using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents all managers connected with entity
    /// </summary>
    public interface IEntityManager<out T>:IActivable
        where T:Entity
    {      
        T Entity { get; }
    }
}
