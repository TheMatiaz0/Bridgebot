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
using System.Xml.Linq;

namespace  TrualityEngine.Core
{
    public abstract class UniversalKey
    {
        public abstract Type GetKeyType();
        public abstract int KeyId { get; }
        public abstract byte Player { get; set; }
        public abstract bool IsPressed(Input input);
        public abstract bool IsFirstFrame(Input input);
        public abstract bool IsEndOfPress(Input input);

        public override string ToString()
        {
            return $"{Enum.ToObject(GetKeyType(), KeyId)} P:{Player}";
        }
        public XElement GetXElement(string name)
        {
            return
                 new XElement(name,
                    new XElement(nameof(KeyId), KeyId),
                    new XElement(nameof(Player), Player),
                    new XElement("Type", GetKeyType().AssemblyQualifiedName));
        }
        public static UniversalKey CreateByXElement(XElement element)
        {
            int keyId = int.Parse(element.Element(nameof(KeyId)).Value);
            byte player = byte.Parse(element.Element(nameof(Player)).Value);
            Type type = Type.GetType(element.Element("Type").Value);
            return Create(Enum.ToObject(type,keyId),player);
        }
        public static UniversalKey Create<T>(T key, byte player = 0) => key switch
        {
            UniversalKey u => u,
            GamePadButton b => new UniversalGamePad() { Buttons = b, Player = player },
            Keys k => new UniversalKeyboard() { Key = k },
            MouseButton m=>new UniversalMouseButtons(){ Key=m},
            _ => throw new ArgumentException("Wrong type", nameof(T)),
        };


        private class UniversalMouseButtons : UniversalKey
        {
            public MouseButton Key;
            public override int KeyId => (int)Key;

            public override byte Player
            {
                get => 0;
                set => throw new NotImplementedException();
            }

            public override Type GetKeyType()
            {
                return typeof(MouseButton);
            }

            public override bool IsFirstFrame(Input input)
            {
                return input.IsFirstFrame(Key);
            }

            public override bool IsPressed(Input input)
            {
                return input.IsPressed(Key);
            }
            public override bool IsEndOfPress(Input input)
            {
                return input.IsEndOfPress(Key);
            }
        }


        private class UniversalGamePad : UniversalKey
        {
            public GamePadButton Buttons;
            public override byte Player { get; set; }
            public override int KeyId => (int)Buttons;

            public override Type GetKeyType()
            {
                return typeof(Buttons);
            }

            public override bool IsFirstFrame(Input input)
            {
                return input.IsFirstFrame(Buttons, Player);
            }

            public override bool IsPressed(Input input)
            {
                return input.IsPressed(Buttons, Player);
            }
            public override bool IsEndOfPress(Input input)
            {
                return input.IsEndOfPress(Buttons, Player);
            }

        }
        private class UniversalKeyboard : UniversalKey
        {

            public Keys Key;
            public override int KeyId => (int)Key;

            public override byte Player
            {
                get => 0;
                set => throw new NotImplementedException();
            }


            public override Type GetKeyType()
            {
                return typeof(Keys);
            }
            public override bool IsFirstFrame(Input input)
            {
                return input.IsFirstFrame(Key);
            }

            public override bool IsPressed(Input input)
            {
                return input.IsPressed(Key);
            }
            public override bool IsEndOfPress(Input input)
            {
                return input.IsEndOfPress(Key);
            }

        }

    }
   
   

}
