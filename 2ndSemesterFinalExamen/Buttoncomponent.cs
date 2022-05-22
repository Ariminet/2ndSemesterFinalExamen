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

        private Vector2 position;
        
        public Vector2 PosPlayer { get; set; }
        public Rectangle Rectangle;

        private Vector2 endOne;
        private Vector2 endTwo;
        private Vector2 endThree;
        
        

        public TextComponent child;
        public string Text { get; set; }

        //Methods

        public Buttoncomponent(Texture2D textureHere, SpriteFont fontHere)
        {
            texture = textureHere;
            font = fontHere;
            PenColour = Color.Black;

        }

        //Button med linjer konstruktor
        public Buttoncomponent(Texture2D texturHere, Vector2 line1, Vector2 line2, Vector2 line3)
        {
            texture = texturHere;
            endOne = line1;
            endTwo = line2;
            endThree = line3;
        }

        public override void Draw( SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (isHovering)
                colour = Color.Gray;

            if (child != null)
            {
                if (endOne != new Vector2())
                {
                    Vector2 end = Game1.Instance.Player.transform.Position + endOne;

                    Vector2 edge = end - position;
                float angle = (float)Math.Atan2(edge.Y, edge.X);

                spriteBatch.Draw(Game1.Instance.line,
                    new Rectangle(
                        (int)position.X + texture.Width / 2,
                        (int)position.Y + texture.Height / 2,
                        (int)edge.Length()*2,
                        1),
                    null,
                    Color.White,
                    angle,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0);

                }
                if (endTwo != new Vector2())
                {
                    Vector2 end = Game1.Instance.Player.transform.Position + endTwo;

                    Vector2 edge = end - position;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);

                    spriteBatch.Draw(Game1.Instance.line,
                        new Rectangle(
                            (int)position.X + texture.Width / 2,
                            (int)position.Y + texture.Height / 2,
                            (int)edge.Length(),
                            1),
                        null,
                        Color.White,
                        angle,
                        new Vector2(0, 0),
                        SpriteEffects.None,
                        0);
                }
                if (endThree != new Vector2())
                {
                    Vector2 end = Game1.Instance.Player.transform.Position + endThree;

                    Vector2 edge = end - position;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);

                    spriteBatch.Draw(Game1.Instance.line,
                        new Rectangle(
                            (int)position.X + texture.Width / 2,
                            (int)position.Y + texture.Height / 2,
                            (int)edge.Length(),
                            1),
                        null,
                        Color.White,
                        angle,
                        new Vector2(0, 0),
                        SpriteEffects.None,
                        0);
                }
            }

            spriteBatch.Draw(texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColour);
            }



        }
       
        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);
            
            isHovering = false;

            if (child != null)
            {
                child.active = false;
            }

            position = Game1.Instance.Player.transform.Position + PosPlayer;

            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

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
