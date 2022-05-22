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
        public string CurrentValue;

        private KeyboardState oldKState;

        private Vector2 Position;
        public Vector2 PosPlayer { get; set; }
        public Rectangle Rectangle
        {
            get;set;
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


            CurrentValue = CurrentText;
            


            spriteBatch.Draw(texture, Rectangle, colour);

            if (!active)
            {
                if (string.IsNullOrEmpty(CurrentText))
                {
                    var x = (Position.X + (Rectangle.Width / 2)) - (font.MeasureString(startText).X / 2);
                    var y = (Position.Y + (Rectangle.Height / 2)) - (font.MeasureString(startText).Y / 2);

                    spriteBatch.DrawString(font, startText, new Vector2(x, y), Color.Black);
                }
                
            }

            if (!string.IsNullOrEmpty(CurrentText))
            {
                var x = (Position.X + (Rectangle.Width / 2)) - (font.MeasureString(CurrentText).X / 2);
                var y = (Position.Y + (Rectangle.Height / 2)) - (font.MeasureString(CurrentText).Y / 2);

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


                if (currentKeyboard.IsKeyDown(Keys.Back) && oldKState.IsKeyUp(Keys.Back)  )
                {
                        //tmpBool = false;
                        RemoveText();
                        //NumberOfLetters--; 
                }

                if (tmpTx.Length > 0)
                {
                    string tmpstring = Convert.ToString(tmpTx[0]);

                    tmpText = tmpTx[0].ToString();

                    

                    if (currentKeyboard.IsKeyDown(currentKeyboard.GetPressedKeys()[0]) && oldKState.IsKeyUp(currentKeyboard.GetPressedKeys()[0]) && !currentKeyboard.IsKeyDown(Keys.Back) && tmpText.Any(c => char.IsLetter(c))&& CurrentText.Length <= 50 )
                    {
                        if(tmpText.Length <= 1)
						{
                            //tmpBool = false;
                            tmpText = tmpText.TrimStart('D');
                            AddMoreText(tmpText);
                        }
                       
                        
                        

                        //NumberOfLetters++;
                        
                    }



                }
                oldKState = currentKeyboard;
                //if (!tmpBool)
                //{
                //    timer++;
                //    if (timer > 8)
                //    {
                //        tmpBool = true;
                //        timer = 0;
                //    }

                //}
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
			if (list.Count > 0)
			{
				list.RemoveAt(list.Count - 1);
		}

		string output = new string(list.ToArray());

            CurrentText = output;
            
            
        }
    }
}
