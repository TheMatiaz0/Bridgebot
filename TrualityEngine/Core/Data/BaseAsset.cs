using TrualityEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{

 
    public class BaseAsset
    {
        private static readonly Dictionary<string, BaseAsset> dir = new Dictionary<string, BaseAsset>();
        public object Value { get; }
        public string Name { get; }
        protected BaseAsset(object val, string name)
        {
            Value = val;
            Name = name;
        }

        public static BaseAsset Get(string name)
        {
            if (dir.ContainsKey(name))
                return dir[name];
            else
                return Add(GameHeart.Actual.BaseGame.Content.Load<object>(name), name);
        }
        protected static BaseAsset Add(object val, string name)

        {


            if (dir.ContainsKey(name) == false)
                dir.Add(name, (new BaseAsset(val, name)));
            else
                return null;
            return dir[name];

        }
    }
}
