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
        


        public DescriptButton(Texture2D textSprite, SpriteFont font, Texture2D buttonSprite, Vector2 Pos, Vector2 line1, Vector2 line2, Vector2 line3, Talent aTalent)
        {
            textComp = new TextComponent(textSprite, font);

            buttonComp = new Buttoncomponent(buttonSprite, line1, line2, line3, aTalent, font);

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
