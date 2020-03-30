 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public static class PrefabCollection
    {


        public static Button GetClassicButton(string text, Color spriteColor, Color textColor)
            => GetClassicButton<Button>(text, spriteColor, textColor);
        public static T GetClassicButton<T>(string text,Color spriteColor, Color textColor)
            where T:Button,new()
        {
            SpriteEntity spriteEntity = new SpriteEntity()
            {
                Sprite = Sprite.ButtonSprite
               
            };
            spriteEntity.RenderColor = spriteColor;
            TextEntity child = new TextEntity()
            {
                RenderColor = textColor
            };
            child.Text = text;
            child.Layer = 1;
            child.PrivateScale = new Vect2(30, 30); 
            spriteEntity.AddChild(child);            
            spriteEntity.PrivateScale = new Vect2(200, 70);
            T button= spriteEntity.ComponentManager.Add<T>();
            button.TextEntity = child;
            return button;


        }


    }
}
