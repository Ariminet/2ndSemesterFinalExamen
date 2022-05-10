using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2ndSemesterFinalExamen
{
    class Player : Component
    {
        public string Tag { get; set; }
        public int Points { get; set; }
        public int CurrentLevel { get; set; }
        public Vector2 position = new Vector2(500, 300);
        private int speed = 300;

        //public Dir direction { get;  set; } = Dir.Down;
        //public bool isMoving { get; private set; } = false;
        public KeyboardState kStateOld { get; private set; } = Keyboard.GetState();
        public bool dead = false;

		//public SpriteAnimation anim;

		//public SpriteAnimation[] animations = new SpriteAnimation[4];

		//public Vector2 Position { get => position; }

		//public void setX(float newX)
		//{
		//	position.X = newX;
		//}
		//public void setY(float newY)
		//{
		//	position.Y = newY;
		//}


		public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            gameObject.transform.isMoving = false;

            if (kState.IsKeyDown(Keys.Right))
            {
                gameObject.transform.direction = Dir.Right;
                gameObject.transform.isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Left))
            {
                gameObject.transform.direction = Dir.Left;
                gameObject.transform.isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Up))
            {
                gameObject.transform.direction = Dir.Up;
                gameObject.transform.isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Down))
            {
                gameObject.transform.direction = Dir.Down;
                gameObject.transform.isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Space)) 
            {
                gameObject.transform.isMoving = false;
            }
            if (dead )
            {
                gameObject.transform.isMoving = false;
            }
			if (gameObject.transform.isMoving)
			{

				switch (gameObject.transform.direction)
				{
					case Dir.Down:
						if (gameObject.transform.Position.Y < 1250)
						{
							gameObject.transform.Position += new Vector2(gameObject.transform.Position.X,speed * dt);
						}
						break;
					case Dir.Up:
						if (gameObject.transform.Position.Y > 200)
						{
							gameObject.transform.Position -= new Vector2(gameObject.transform.Position.X,speed * dt);
						}
						break;
					case Dir.Left:
						if (gameObject.transform.Position.X > 225)
						{
							gameObject.transform.Position -= new Vector2(speed * dt, gameObject.transform.Position.Y);
						}
						break;
					case Dir.Right:
						if (gameObject.transform.Position.X < 1275)
						{
							gameObject.transform.Position += new Vector2(speed * dt, gameObject.transform.Position.Y);
						}
						break;
				}

			}

			// anim = animations[(int)direction];

			// anim.Position = new Vector2(position.X - 48, position.Y - 48);
			// if (kState.IsKeyDown(Keys.Space))
			// {
			//         anim.setFrame(0);
			// }
			//else if (isMoving) {
			//     anim.Update(gameTime);
			// }else
			// {
			//     anim.setFrame(1);
			// }
			if ( kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                Projectile.projectiles.Add(new Projectile(gameObject.transform.Position,gameObject.transform.direction));
            }

            kStateOld = kState;
        }
    }
}
