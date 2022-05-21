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
        private Buttoncomponent parent;
        private Vector2 Position;
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

            spriteBatch.Draw(texture, Rectangle, Color.White);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Position.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Position.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), Color.Black);
            }
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (Position != Game1.Instance.buttonsWorldPosition + PosPlayer)
            {
                Position = new Vector2(Game1.Instance.buttonsWorldPosition.X + PosPlayer.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + PosPlayer.Y - texture.Height / 2);
            }

        }

    }
}
