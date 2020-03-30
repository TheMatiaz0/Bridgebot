using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    interface  ICollectionManager<T>
    {
        T Add(T item);
        bool Remove(T item);
        IReadOnlyList<T> Get();
    }
}
