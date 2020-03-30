using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Tag system works similar to enum flags. 
    /// Implements <see cref="IEnumerable{string}"/> 
    /// </summary>
    public readonly struct Tags:IEnumerable<string>,IEquatable<Tags>
    {

        public static readonly Tags Empty=new Tags();
        private SortedSet<string> AllTags { get;   }
        public Tags(string tag)
        {
            AllTags = new SortedSet<string>() { tag };
        }
        public Tags(params string[] tag )
        {
            AllTags =new  SortedSet<string>(tag);
        }

        public IReadOnlyCollection<string> GetAll()//I cannot overload main constructor, beacuse it is struct
        {
            if (AllTags == null)
                return new string[0];
            return AllTags.ToArray();

        }
        /// <summary>
        /// If this tag contains flag from the parameter, then method will return true
        /// </summary>     
        /// <returns></returns>
        public bool Has(string tag)
            => GetAll().Any(item => item == tag);
        /// <summary>
        /// If this tag contains flag from the parameter, then method will return true
        /// </summary>     
        /// <returns></returns>
        public bool Has(Tags other)
        {
            foreach (string element in other)
                if (this.Has(element) == false)
                    return false;
            return true;
        }
        /// <summary>
        /// Return new Tag that equals this flag + flag's parametr
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Tags Combine(Tags tag)
        {
            return new Tags(this.GetAll().Union( tag.GetAll()).ToArray());
        }
        public override string ToString()
        {
            if (GetAll().Count == 0)
                return "Empty";
            StringBuilder builder = new StringBuilder("{");
            foreach(string element in GetAll())
            {
                builder.Append($"{element}, ");
            }
            builder.Remove(builder.Length - 2, 2);
            builder.Append("}");
            return builder.ToString();
        }

      
        public static bool operator ==(Tags a, Tags b)
            => a.GetAll().SequenceEqual(b);
        public static bool operator !=(Tags a, Tags b)
            => !(a == b);
        public static Tags operator +(Tags a, Tags b)
            => a.Combine(b);
        public override bool Equals(object obj)
        {
            if(obj is Tags==false)
                return false;
            return(Tags)obj == this;
        }
        public override int GetHashCode()
        {
            return GetAll().Sum(item => item.GetHashCode());
        }
        public IEnumerator<string> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }
        public static implicit operator Tags(string value)
            =>new Tags(value);
    

        
       
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(Tags other)
        {
            return this == other;
        }
    }

}
