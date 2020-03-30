using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrualityEngine.Core
{
    /// <summary>
    /// If it is alive, it will render text
    /// </summary>
    public class TextEntity : RenderEntity
    {

        public Asset<SpriteFont> Font { get; set; }
        public float? LimitBlock { get; set; }


        /// <summary>
        /// How text will be render
        /// </summary>
        public TextRenderOptions TextRenderOptions { get; set; }
        public Event<EntityTextEventArgs> OnTextChanged { get; private set; } = Event.Empty;

        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                OnTextChanged.Invoke(this, new EntityTextEventArgs(this));
                _RenderText = GetFinalText();

            }
        }
        private string _Text;
        private string _RenderText;




        public TextEntity(string text = "Text",
            TextRenderOptions textRenderOptions = TextRenderOptions.Center, Color? color = null, bool notLoadFont = false)
        {
            if (notLoadFont == false)
                Font = Asset<SpriteFont>.Get("Arial");
            OnTextChanged.Value += IfTextChanged;
            RenderColor = color ?? Color.Red;

            Text = text;

            TextRenderOptions = textRenderOptions;



        }
        public TextEntity() : this(text: "")
        {

        }
        public override Collider GetCollision()
        {

            return Collider.Create(new NeverColliding());
        }
        public override Collider GetDrawCollisonInfo()
        {
            return Collider.Create(new AlwaysColliding());
        }
        protected internal override void IfDraw(FixedBatch batch)
        {
            base.IfDraw(batch);
            
            if (String.IsNullOrEmpty(Text) == false && Font != null)
                batch.DrawString(Font.Value, _RenderText, Pos, GetRenderColor(), FullScale, TextRenderOptions);


        }
        private string GetFinalText()
        {
            string finalText = Text;
            if (LimitBlock != null&&String.IsNullOrEmpty(finalText)==false)
            {
                
                string[] splited = finalText.Split();
                StringBuilder bulider = new StringBuilder();
                float xPower = 0;
                foreach(var item in splited)
                {
                    xPower += Font.Value.MeasureString($"{item}").X;
                    if (xPower > LimitBlock)
                    {
                        bulider.Append($"\n{item} ");
                        xPower = 0;
                    }
                    else
                    {
                        bulider.Append($"{item} ");
                    }
                }
                finalText = bulider.ToString();
                


            }
            return finalText;
        }
        protected virtual void IfTextChanged(object sender, EntityTextEventArgs textChanged) { }



        public class EntityTextEventArgs : EventArgs, ITextableArg
        {
            public TextEntity Entity { get; }

            public string Text => Entity.Text;

            public EntityTextEventArgs(TextEntity entity)
            {
                Entity = entity;
            }
        }

    }
}
