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

        public int levelnm;

        public Talent thisTalent { get; set; }

        public string Text { get; set; }

        //Methods

        public Buttoncomponent(Texture2D textureHere, SpriteFont fontHere)
        {
            texture = textureHere;
            font = fontHere;
            PenColour = Color.Black;

        }

        //Button med linjer konstruktor
        public Buttoncomponent(Texture2D texturHere, Vector2 line1, Vector2 line2, Vector2 line3, Talent aTalent, SpriteFont fontHere)
        {
            texture = texturHere;
            endOne = line1;
            endTwo = line2;
            endThree = line3;
            thisTalent = aTalent;
            font = fontHere;
            PenColour = Color.Black;
            

        }

        public override void Draw( SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (isHovering)
                colour = Color.Gray;


            if (child != null)
            {
                if (endOne != new Vector2(0,0))
                {
                    Vector2 end  = new Vector2(Game1.Instance.buttonsWorldPosition.X + endOne.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + endOne.Y - texture.Height / 2);

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

                if (endTwo != new Vector2(0,0))
                {
                    Vector2 end = new Vector2(Game1.Instance.buttonsWorldPosition.X + endTwo.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + endTwo.Y - texture.Height / 2);

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
                if (endThree != new Vector2(0,0))
                {
                    Vector2 end = new Vector2(Game1.Instance.buttonsWorldPosition.X + endThree.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + endThree.Y - texture.Height / 2);

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

            if (thisTalent != null && thisTalent.Locked == true)
            {
                spriteBatch.Draw(texture, position, Color.Gray);
            }

            if (thisTalent != null && thisTalent.Locked == false)
            {
                spriteBatch.Draw(texture, position, colour);
            }

            if (thisTalent == null)
            {
                spriteBatch.Draw(texture, position, colour);
            }

            //spriteBatch.Draw(texture, position, colour);

            //talent level box
            if (thisTalent != null && thisTalent.MaxLevel > 1)
            {
                spriteBatch.Draw(Game1.Instance.LevelBox, position, colour);
                spriteBatch.DrawString(font, " "+levelnm, position, PenColour);
            }

            //tekst
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (position.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (position.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

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
            if (position != new Vector2(Game1.Instance.buttonsWorldPosition.X + PosPlayer.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + PosPlayer.Y - texture.Height / 2))
            {
                position = new Vector2(Game1.Instance.buttonsWorldPosition.X + PosPlayer.X - texture.Width / 2, Game1.Instance.buttonsWorldPosition.Y + PosPlayer.Y - texture.Height / 2);
                Rectangle = new Rectangle((int)PosPlayer.X + (int)Game1.Instance.ViewWVH.X - texture.Width / 2, (int)PosPlayer.Y + (int)Game1.Instance.ViewWVH.Y - texture.Height / 2, texture.Width, texture.Height);
            }

            if (thisTalent != null)
            {
                levelnm = thisTalent.CurrentLevel;
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
