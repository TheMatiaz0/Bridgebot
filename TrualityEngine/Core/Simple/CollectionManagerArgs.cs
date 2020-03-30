using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class CollectionManagerArgs<T>:EventArgs
        where T:class
    {

        public CollectionManager<T> CollectionManager { get; }
        public T Element { get; }
        public CManagerAction ActionType { get; }
        public CollectionManagerArgs(CManagerAction action,T element,CollectionManager<T> collectionManager)
        {
            Element = element;
            CollectionManager = collectionManager;
            ActionType = action;
        }
    }
}
