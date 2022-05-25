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
        public int Points { get; set; } = 0;
        public int CurrentLevel { get; set; } = 0;
        //public Vector2 position = new Vector2(500, 300);
        private int speed = 300;

        public int Health { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public int Damage { get; set; } = 125;

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

        public void LoadPlayer(int MaxHealth)
		{
            this.MaxHealth = Health;
            this.Health = MaxHealth;

        }
		public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(Health <= 0)
			{
                dead = true;
			}

            gameObject.transform.isMoving = false;
			if (!dead) { 
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
							gameObject.transform.Position += new Vector2(0, (float)speed * Game1.dt);
                        }
						break;
					case Dir.Up:
						if (gameObject.transform.Position.Y > 200)
						{
							gameObject.transform.Position -= new Vector2( 0, (float)speed * Game1.dt );
						}
						break;
					case Dir.Left:
						if (gameObject.transform.Position.X > 225)
						{
							gameObject.transform.Position -= new Vector2( (float)speed * Game1.dt , 0);
						}
						break;
					case Dir.Right:
						if (gameObject.transform.Position.X < 1275)
						{
							gameObject.transform.Position += new Vector2( (float)speed * Game1.dt ,0);
						}
						break;
				}

			}

            ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).animations[(int)gameObject.transform.direction];

            ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.Position = new Vector2(gameObject.transform.Position.X - 48, gameObject.transform.Position.Y - 48);
            if (kState.IsKeyDown(Keys.Space))
            {
                ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.setFrame(0);
            }
            else if (gameObject.transform.isMoving)
            {
                ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.AnimUpdate(gameTime);
            }
            else
            {
                ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.setFrame(1);
            }
            if ( kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(gameObject.transform.direction ,gameObject.transform.Position));
            }

            kStateOld = kState;
           }
        }
    }
}
