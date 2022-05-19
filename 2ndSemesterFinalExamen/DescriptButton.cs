using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2ndSemesterFinalExamen 
{
    class DescriptButton : Component
{
       TextComponent textComp;
       Buttoncomponent buttonComp;

        

        public DescriptButton(Texture2D textSprite, SpriteFont font, Texture2D buttonSprite, string text, Vector2 Pos)
        {
            textComp = new TextComponent(textSprite, font);
            buttonComp = new Buttoncomponent(buttonSprite, font);

            textComp.Text = text;

            textComp.PosPlayer = Pos;
            buttonComp.PosPlayer = new Vector2 (Pos.X -50, Pos.Y+50);

            buttonComp.child = textComp;
            
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            buttonComp.Draw(spriteBatch);

            if (textComp.active)
            {
                textComp.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            buttonComp.Update(gameTime);
            textComp.Update(gameTime);
            
        }
    }
}
