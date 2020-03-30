using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
   
    public enum PairElement
    {
        First = 0,
        Second = 1
    }
    public static class PairElementExtension
    {
        public static PairElement Reverse(this PairElement element)
        {
            return (element == PairElement.First) ? PairElement.Second : PairElement.First;
        }
    }


    /// <summary>
    /// Generic collection for 2 value.It implement IEnumerator and classic value getting ("first","second") 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class Pair<T1, T2> : IEnumerable<object>
       
    {
        public void Swap()
        {
            T1 temp = First;
            First = (T1)(object)Second;
            Second = (T2)(object)temp;
        }
        protected const string OutOfAgumenText = "Value must be in range 0-1";


        public T1 First
        {
            get => _First;
            set
            {
                _First = value;
                OnFirstValueChanged(this, value);
            }

        }
        private T1 _First;

        public T2 Second
        {
            get => _Second;
            set
            {
                _Second = value;
                OnSecondValueChanged(this, value);
            }
        }
        private T2 _Second;


        public event EventHandler<SimpleArgs<T1>> OnFirstValueChanged = delegate { };
        public event EventHandler<SimpleArgs<T2>> OnSecondValueChanged = delegate { };


        public Pair(T1 first = default, T2 second = default)
        {
            First = first;
            Second = second;
        }

        public Pair(T2 b, T1 a = default) : this(a, b) { }
        public object this[int index]
        {
            get
            {
                return index switch
                {
                    0 => First,
                    1 => Second,
                    _ => throw new ArgumentOutOfRangeException(OutOfAgumenText),
                };
            }
            set
            {
                switch (index)
                {
                    case 0: First = (T1)value; break;
                    case 1: Second = (T2)value; break;
                }
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            yield return First;
            yield return Second;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public class Comparer : IComparer<Pair<T1, T2>>
        {
            public PairElement Element { get; }
            public Comparer(PairElement byElement)
            {
                Element = byElement;
            }

            public int Compare(Pair<T1, T2> x, Pair<T1, T2> y)
            {

                return ((x[(int)Element])as IComparable).CompareTo(x[(int)Element]);
            }

        }
    }

    public class Pair<T> : Pair<T, T>, IEnumerable<T>    
    {
        public Pair(T a = default, T b = default) : base(first: a, second: b) { }
        public new T this[int index]
        {
            get => (T)base[index];
            set => base[index] = value;
        }

        public new IEnumerator<T> GetEnumerator()
        {
            yield return First;
            yield return Second;
        }
    }

}
