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
namespace TrualityEngine.Core
{
    public class CheckBox : SpriteEntity
    {
        public static readonly Sprite Box = SpriteCreator.AddBorder(SpriteCreator.MakeRectangle(new Point(100, 100), new Color(Color.White, 0)), Color.Red, 10);
        public Event<ValueInEntityChangedArgs<bool>> OnValueChanged { get; private set; } = Event.Empty;
        private bool _CheckBoxValue;
        public bool CheckBoxValue
        {
            get => _CheckBoxValue;
            set
            {
                if (_CheckBoxValue != value)
                {
                    _CheckBoxValue = value;
                    IfCheckBoxValueChange();
                    OnValueChanged.Invoke(this, new ValueInEntityChangedArgs<bool>(this, value));

                }

            }
        }
        private SpriteEntity filledChild;
        public CheckBox()
        {
            Sprite = Box;
            filledChild = new SpriteEntity();
            filledChild.Sprite = Sprite.ClearSprite;
            filledChild.RenderColor = Color.Yellow;
            this.OnScaleChanged.Value += WhenOnScaleChanged;
            WhenOnScaleChanged(this, new ValueInEntityChangedArgs<Vect2>(this, this.Scale));

            filledChild.SetParent(this);
            CheckBoxValue = false;
        }

        public void SimulateChange()
        {
            for (int x = 0; x < 2; x++)
                CheckBoxValue = !CheckBoxValue;
        }
        private void WhenOnScaleChanged(object sender, ValueInEntityChangedArgs<Vect2> e)
        {
            filledChild.PrivateScale = this.FullScale - (new Vect2(10) * 2f / new Vect2(this.Sprite.Value.Width, this.Sprite.Value.Height) * this.FullScale);
        }

        protected virtual void IfCheckBoxValueChange()
        {
            filledChild.IsRendering = CheckBoxValue;
        }

        protected override void IfMouseSingleClick(MouseButton mouseButton)
        {
            base.IfMouseSingleClick(mouseButton);
            if (mouseButton == MouseButton.Left)
                CheckBoxValue = !CheckBoxValue;


        }


    }

}

