using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class TextComponent : Component
    {
        private SpriteFont font;
        private Texture2D texture;
        public Texture2D talentName;
        
        private Buttoncomponent parent;
        private Vector2 Position;

        public Talent thisTalent;
        public Vector2 PosPlayer { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
           
        }

        public bool active = true;

        public string Text { get; set; }
        public TextComponent(Texture2D textureHere, SpriteFont fontHere)
        {
            texture = textureHere;
            font = fontHere;
        }
        public TextComponent(Texture2D textureHere, SpriteFont fontHere, Buttoncomponent parent)
        {
            texture = textureHere;
            font = fontHere;
            this.parent = parent;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                Position = new Vector2(Game1.Instance.buttonsWorldPosition.X + PosPlayer.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + PosPlayer.Y - texture.Height / 2);

                spriteBatch.Draw(texture, Position, Color.White);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Position.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Position.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), Color.Black);
            }
                if (thisTalent != null)
                {
                    var x = (Position.X + (Rectangle.Width / 2)) - (font.MeasureString(thisTalent.Description).X / 2);
                    var y = (Position.Y + (Rectangle.Height / 2)) - (font.MeasureString(thisTalent.Description).Y / 2);

                    spriteBatch.DrawString(font, thisTalent.Description, new Vector2(x, y), Color.Black);

                    spriteBatch.Draw(talentName, new Vector2 (Position.X, Position.Y - 50), Color.White);

                    spriteBatch.DrawString(font, thisTalent.Tag, new Vector2(Position.X + 10, y - 50), Color.Black);
                }
            }

        }

        public override void Update(GameTime gameTime)
        {
            
            
        }

    }
}
