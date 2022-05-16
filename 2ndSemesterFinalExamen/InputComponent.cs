using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;


namespace _2ndSemesterFinalExamen
{
    class InputComponent : Component
    {
        private MouseState currentMouse;
        private MouseState previousMouse;
        private KeyboardState currentKeyboard;
        private KeyboardState previousKeyboard;
        private SpriteFont font;
        private Texture2D texture;
        private bool isHovering;
        private int max = 10;
        private bool active = false;
        private string tmpText;
        private int timer = 0;

        public string startText { get; set; }

        private int NumberOfLetters = 0;

        private bool tmpBool = true;

        private string buttonTitle;

        public string CurrentText;
        public int CurrentValue;

        private Vector2 Position;
        public Vector2 PosPlayer { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }
            
        public InputComponent (Texture2D textureHere, SpriteFont fontHere)
        {
            texture = textureHere;
            font = fontHere;
           
     
        }
        public override void Draw ( SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if(isHovering || active)
            {
                colour = Color.Gray;
            }

            if (Int32.TryParse(CurrentText, out int CurrenValue))
            {
                CurrentValue = Int32.Parse(CurrentText);
            }


            spriteBatch.Draw(texture, Rectangle, colour);

            if (!active)
            {
                if (string.IsNullOrEmpty(CurrentText))
                {
                    var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(startText).X / 2);
                    var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(startText).Y / 2);

                    spriteBatch.DrawString(font, startText, new Vector2(x, y), Color.Black);
                }
                
            }

            if (!string.IsNullOrEmpty(CurrentText))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(CurrentText).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(CurrentText).Y / 2);

                spriteBatch.DrawString(font, CurrentText, new Vector2(x, y), Color.Black);
            }      
        }

        public override void Update(GameTime gametime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            currentKeyboard = Keyboard.GetState();


            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;
            Position = Game1.Instance.Player.transform.Position + PosPlayer;
            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    active = true;
                    
                    CurrentText = "";
                    NumberOfLetters = 0;
                }
            }
            if(!mouseRectangle.Intersects(Rectangle) && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                active = false;
  

            }

            if (active)
            {
                
                var tmpTx = currentKeyboard.GetPressedKeys();


                if (currentKeyboard.IsKeyDown(Keys.Back) && NumberOfLetters > 0 && tmpBool)
                {
                        tmpBool = false;
                        RemoveText();
                        NumberOfLetters--; 
                }

                if (tmpTx.Length > 0)
                {
                    string tmpstring = Convert.ToString(tmpTx[0]);

                    tmpText = tmpTx[0].ToString();



                    if (tmpBool && tmpText.Any(c=>char.IsDigit(c)) && NumberOfLetters<10)
                    {

                        tmpText = tmpText.TrimStart('D');
                        AddMoreText(tmpText);
                        
                        tmpBool = false;

                        NumberOfLetters++;
                        
                    }



                }
                    if (!tmpBool)
                    {
                        timer++; 
                        if (timer > 8)
                        {
                            tmpBool = true;
                            timer = 0;
                        }

                    }
            }
        }

        public void AddMoreText(string Text)
        {
           
                CurrentText +=  Text;
            
        }

        public void RemoveText()
        {
            List<char> list = new List<char>();
            for (int i = 0; i < CurrentText.Length; i++)
            {
                list.Add(CurrentText[i]);
            }
            if(NumberOfLetters > 0)
			{
                list.RemoveAt(NumberOfLetters - 1);
            }

            string output = new string(list.ToArray());

            CurrentText = output;
            
            
        }
    }
}
