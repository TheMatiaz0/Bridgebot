using TrualityEngine.Core;
using System;
using System.Collections;
using System.Collections.Generic;
namespace TrualityEngine.Core
{
    public abstract class CollectionManager<TLimit> : IEnumerable<TLimit>, ICloneable, ICollectionManager<TLimit>
        where TLimit : class
    {
        protected List<TLimit> List { get; set; } = new List<TLimit>();
        public bool CanAddNull { get; }
        public Event<CollectionManagerArgs<TLimit>> OnAddElement { get; protected set; } = Event.Empty;
        public Event<CollectionManagerArgs<TLimit>> OnRemoveElement { get; protected set; } = Event.Empty;
        public IReadOnlyList<TLimit> GetAll() => List;

        public T1 Add<T1>(T1 value)
            where T1 : class, TLimit
        {
            return (Add(val: value)) as T1;
        }
        /// <summary>
        /// Adding new element.
        /// </summary>
        /// <param name="val">element, which will be added </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public TLimit Add(TLimit val)
        {
            if (CanAddNull == false && val is null)
                throw new ArgumentNullException(nameof(val));
            if (CanAdd(val))
            {
                List.Add(val);
                OnAddElement.Invoke(this, new CollectionManagerArgs<TLimit>(CManagerAction.Add, val, this));
                return val;
            }
            return null;
        }
        public bool Remove<T1>(T1 value)
            where T1 : class, TLimit
        {

            if (CanAddNull == false && value is null)
                throw new ArgumentNullException(nameof(value));
            int index;
            if ((index = List.FindIndex((item => item == value))) != -1)
                if (CanRemove(value))
                    if (List.Remove(value))
                    {
                        OnRemoveElement.Invoke(this, new CollectionManagerArgs<TLimit>(CManagerAction.Add, value, this));
                        return true;
                    }

            return false;

        }
        public bool Remove(int index)
        {
            if (CanRemove(List[index]))
                if (List.Remove(List[index]))
                {
                    OnRemoveElement.Invoke(this, new CollectionManagerArgs<TLimit>(CManagerAction.Remove, List[index], this));
                    return true;
                }

            return false;
        }

        public CollectionManager(bool canAddNull)
        {
            CanAddNull = canAddNull;
        }
        protected abstract bool CanAdd(TLimit value);
        protected abstract bool CanRemove(TLimit value);
        CollectionManager<TLimit> Clone()
            => base.MemberwiseClone() as CollectionManager<TLimit>;
        object ICloneable.Clone()
        {
            return Clone();
        }

        TLimit ICollectionManager<TLimit>.Add(TLimit item)
        {
            return Add(item);
        }

        bool ICollectionManager<TLimit>.Remove(TLimit item)
        {
            return Remove(item);
        }

        IReadOnlyList<TLimit> ICollectionManager<TLimit>.Get()
        {
            return List;
        }

        public IEnumerator<TLimit> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
