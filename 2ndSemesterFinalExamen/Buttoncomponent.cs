using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class Buttoncomponent : Component
    {
        //Fields
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;

        //Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        private Vector2 Position;
        public Vector2 PosPlayer { get; set; }
        public Rectangle Rectangle;
        
        

        public TextComponent child;
        public string Text { get; set; }

        //Methods

        public Buttoncomponent(Texture2D textureHere, SpriteFont fontHere)
        {
            texture = textureHere;
            font = fontHere;
            PenColour = Color.Black;

        }
        public override void Draw( SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(texture, Position, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Position.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Position.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColour);
            }


        }
       
        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
            //var mouseRectangle = new Rectangle((int)Game1.Instance.mouseWorldPosition.X - (int)Game1.Instance.camera.Position.X, (int)Game1.Instance.mouseWorldPosition.Y - (int)Game1.Instance.camera.Position.Y, 1, 1);
            var mouseRectangle = new Rectangle((int)currentMouse.X, (int)currentMouse.Y, 1, 1);


            isHovering = false;

            if (child != null)
            {
                child.active = false;
            }
            if (Position != Game1.Instance.buttonsWorldPosition + PosPlayer)
            {
                Position = new Vector2(Game1.Instance.buttonsWorldPosition.X + PosPlayer.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + PosPlayer.Y - texture.Height / 2);
                Rectangle = new Rectangle((int)PosPlayer.X + (int)Game1.Instance.ViewWVH.X - texture.Width / 2, (int)PosPlayer.Y + (int)Game1.Instance.ViewWVH.Y - texture.Height / 2, texture.Width, texture.Height);
            }

            
            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());

                }
                if (child != null)
                {
                    child.active = true;
                }
            }
        }
    }
}
