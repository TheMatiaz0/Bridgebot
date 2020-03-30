using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TrualityEngine.Core
{
    public abstract class GameSettings<T>
        where T:GameSettings<T>,new()
    {



        private static T _Active = new T();
        public static T Active
        {
            get => _Active;
             set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value != _Active )
                    _Active = value;
            }
        }
        public void SaveInConfigFile()
        {
            XmlSerializer<T>.SerializeXml((this as T), nameof(T));
        }
        public static T LoadFromConfigFile()
        {
           return XmlSerializer<T>.LoadXml(nameof(T));
        }
        

    }
}
