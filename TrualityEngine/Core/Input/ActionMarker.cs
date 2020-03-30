using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using TrualityEngine.Core;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;

namespace TrualityEngine.Core
{
    public class ActionMarker:IXmlSerializable
    {
        private static XmlSerializer xmlSerializer;
        public Input Input { get; private set; }
        private Dictionary<string, List<UniversalKey>> keys = new Dictionary<string, List<UniversalKey>>();
        public ActionMarker(Input input)
        {
            Input = input ?? throw new NullReferenceException();
        }
        public ActionMarker()
        {
            Input = Input.Actual;
        }
        private void Check(string action)
        {
            if (keys.ContainsKey(action) == false)
                keys.Add(action, new List<UniversalKey>());

        }
        private void P_Add<T>(string action, T key,byte player=0)
        {
            Check(action);
            keys[action].Add(UniversalKey.Create(key,player));
        }
        private void P_AddRange<T>(string action, IEnumerable<T> keys,byte player=0)
        {
            foreach (var item in keys)
                P_Add(action, item,player);
        }
        public void Add(string action, Keys key) => P_Add(action, key);
        public void Add(string action, GamePadButton button, byte player) => P_Add(action, button, player);
        public void Add(string action, MouseButton button) => P_Add(action, button);
        public void Add(string action, UniversalKey universalKey) => P_Add(action, universalKey);
        public void AddRange(string action, params Keys[] keys) => P_AddRange(action, keys);
        public void AddRange(string action, byte player, params GamePadButton[] buttons) => P_AddRange(action, buttons,player);
        public void AddRange(string action, params MouseButton[] buttons) => P_Add(action, buttons);


        public void Apply(ActionMarker other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Object.ReferenceEquals(this,other))
            {
                throw new ArgumentException("Other cannot be current object itself", nameof(other));
            }
            foreach(var item in other.keys)
            {
                this.keys[item.Key] = item.Value;
            }
        }
        /// <summary>
        /// Removes marked action.
        /// </summary>
        /// <param name="action"></param>
        public void Clear(string action)
        {
            keys.Remove(action);
        }
       
        public bool IsPressed(string action)
        {
            return TryCheck(action).Any(item => item.IsPressed(Input));
 
        }
        public bool IsFirstFrame(string action)
        {
            return TryCheck(action).Any(item => item.IsFirstFrame(Input));
        }
        public bool IsEndOfPress(string action)
        {
            return TryCheck(action).Any(item => item.IsEndOfPress(Input));
        }
        private void ThrowActionWasntMarked(string action)
        {
            throw new ArgumentException($"Action {action} wasn't marked");
        }
        private List<UniversalKey> TryCheck(string action)
        {
            if (keys.TryGetValue(action, out List<UniversalKey> result))
            {
                return result;
            }
            ThrowActionWasntMarked(action);
            return null;//it won't happen never , but it have to be here
        }
   
        public void Serialize(string file)
        {
            xmlSerializer ??= new XmlSerializer(typeof(ActionMarker));
            file = FixExtension(file);
            using StreamWriter writer = new StreamWriter(file);
            xmlSerializer.Serialize(writer, this);

        }
        private string FixExtension(string file)
        {
            if (string.IsNullOrEmpty(System.IO.Path.GetExtension(file)))
            {
                file += ".xml";
            }
            return file;
        }
        public void Deserialize(string file)
        {
            xmlSerializer ??= new XmlSerializer(typeof(ActionMarker));
            file = FixExtension(file);
            using StreamReader reader = new StreamReader(file);
            ActionMarker marker= (ActionMarker)xmlSerializer.Deserialize(reader);
            this.Apply(marker);

        }
        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

         void IXmlSerializable.ReadXml(XmlReader reader)
        {
            XElement xElement = new XElement(XElement.Load(reader));
            foreach(var item in xElement.Element("Keys").Elements())
            {
                foreach(var key in item.Elements())
                {
                    this.Add(item.Name.ToString(), UniversalKey.CreateByXElement(key));
                }
            }
               

        }

         void IXmlSerializable.WriteXml(XmlWriter writer)
        {

            XElement xmlCode =
                new XElement("Keys");
          
            foreach (var item in keys)
            {
                XElement cur;
                  xmlCode.Add(cur=new XElement(item.Key));
                int i = 0;
                foreach (var element in item.Value)
                {
                    cur.Add(element.GetXElement($"_{i}"));
                    i++;
                }
                   
                
            }
            xmlCode.WriteTo(writer);
          
        }
    }
}
