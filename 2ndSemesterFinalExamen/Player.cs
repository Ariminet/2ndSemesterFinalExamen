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
        private int tmpLevel;
        //public Vector2 position = new Vector2(500, 300);
        private int speed = 300;
        public bool PlayerSet = false;
        public int Health { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public int Damage { get; set; } = 50;
        private float reloadSpeed = 1.5f;
        private float reloadTimer = 0.0f;
        private bool reload = false;
        private float projSize = 1.0f;
        private int projSpeed = 1000;
        private bool piercingShot = false;
        private bool explosiveShot = false;
        private int explosiveCount = 3;
        private float explosiveTimer = 0.0f;
        private bool explode = false;
        private bool explodeUsed = false;
        private bool sprayShot = false;
        private float sprayShotTimer = 0.0f;
        private float sprayShotCount = 3;
        private bool shield = false;
        private bool shieldUsed = false;
        private bool dash = false;
        private bool dashUsed = false;
        private float dashTimer = 0.0f;
        public List<Ability> Abilities = new List<Ability>(); 
        public int magazine { get; private set; } = 3;
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
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            reloadTimer += dt;
            dashTimer += dt;
            explosiveTimer += dt;
            sprayShotTimer += dt;
            if (Health <= 0)
			{
                dead = true;
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.RemoveRange(0, ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Count);

            }
            if(CurrentLevel != tmpLevel || dead)
			{
                explodeUsed = false;
                shieldUsed = false;
                sprayShotCount = 3;
                tmpLevel = CurrentLevel;
            }
			foreach (Ability a in Abilities)
			{
                if(a.Tag == "Bigger Projectiles" && projSize != (float)(a.Level + 0.50))
				{
                    projSize = (float)(a.Level+ 0.50); 
				}

                if (a.Tag == "Faster Shots" && projSpeed != 1000 + 50 * a.Level)
                {
                    projSpeed = 1000 + 50 * a.Level;
                }
                if (a.Tag == "Quick Reload" && reloadSpeed != 1.5f - (a.Level * 0.25f))
                {
                    reloadSpeed = 1.5f - (a.Level * 0.25f);
                }
                if (a.Tag == "Speed" && speed != 300 + (a.Level * 15))
                {
                    speed = 300 + (a.Level * 15);
                }
                if (a.Tag == "Piercing Shot" && !piercingShot )
                {
                    piercingShot = true;
                }
                if (a.Tag == "Damage" && Damage != 50 + 25*a.Level)
                {
                    Damage = 50 + 25 * a.Level;
                }
                if (a.Tag == "Explode" && !explode )
                {
                    explode = true;
                }
                if (a.Tag == "Bullet Shield" && !shield)
                {
                    shield = true;
                }
                if (a.Tag == "Dash" && !dash)
                {
                    dash = true;
                }
                if (a.Tag == "Explosive Shot" && !explosiveShot)
                {
                    explosiveShot = true;
                }
                if (a.Tag == "Spray  Shot" && !sprayShot)
                {
                    sprayShot = true;
                }
            }
            if(kState.IsKeyDown(Keys.R) && kStateOld.IsKeyUp(Keys.R))
			{
                reload = true;
                reloadTimer = 0.0f;
			}


            if (kState.IsKeyDown(Keys.M) && kStateOld.IsKeyUp(Keys.M) && !explodeUsed && explode)
            {
                explodeUsed = true;
                foreach (GameObject enemy in Game1.Instance.enemyFactory.skullEnemies)
                {
                    int sum = 500 + ((Enemy)enemy.GetComponent<Enemy>()).radius;

                    if (Vector2.Distance(gameObject.transform.Position, enemy.transform.Position) < sum)
                    {
                        ((Enemy)enemy.GetComponent<Enemy>()).Health -= 100000;
                    }
                }
                foreach (GameObject enemy in Game1.Instance.enemyFactory.monEnemies)
                {
                    int sum = 500 + ((Enemy)enemy.GetComponent<Enemy>()).radius;

                    if (Vector2.Distance(gameObject.transform.Position, enemy.transform.Position) < sum)
                    {
                        ((Enemy)enemy.GetComponent<Enemy>()).Health -= 100000;
                    }
                }
                foreach (GameObject enemy in Game1.Instance.enemyFactory.ghostEnemies)
                {
                    int sum = 500 + ((Enemy)enemy.GetComponent<Enemy>()).radius;

                    if (Vector2.Distance(gameObject.transform.Position, enemy.transform.Position) < sum)
                    {
                        ((Enemy)enemy.GetComponent<Enemy>()).Health -= 100000;
                    }
                }
            }

            if (kState.IsKeyDown(Keys.N) && kStateOld.IsKeyUp(Keys.N) && !shieldUsed && shield)
            {
                shieldUsed = true;
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(0, 100)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(0, -100)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(-50, 50)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(50, 50)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(100, 0)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(-100, 0)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(50, -50)));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.Shield, gameObject.transform.Position, 1.0f, 0, false, new Vector2(-50, -50)));
            }
            if (kState.IsKeyDown(Keys.C) && kStateOld.IsKeyUp(Keys.C) && sprayShotCount > 0 && sprayShot)
            {
                sprayShotCount--;
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.StrafeLeft, gameObject.transform.Position, projSize, projSpeed, piercingShot, gameObject.transform.direction));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(gameObject.transform.direction, gameObject.transform.Position, projSize, projSpeed, piercingShot));
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(Dir.StrafeRight, gameObject.transform.Position, projSize, projSpeed, piercingShot, gameObject.transform.direction));
                sprayShotTimer = 0.0f;
            }
            if (kState.IsKeyDown(Keys.B) && kStateOld.IsKeyUp(Keys.B) && !dashUsed && dash)
            {
                dashUsed = true;
                dashTimer = 0.0f;
                switch (gameObject.transform.direction)
                {
                    case Dir.Down:
                        if (gameObject.transform.Position.Y < 1250)
                        {
                            gameObject.transform.Position += new Vector2(0, 500);
                            if(gameObject.transform.Position.Y > 1250)
							{
                                gameObject.transform.Position = new Vector2(gameObject.transform.Position.X, 1250);
                            }
                        }
                        break;
                    case Dir.Up:
                        if (gameObject.transform.Position.Y > 200)
                        {
                            gameObject.transform.Position -= new Vector2(0, 500);
                            if (gameObject.transform.Position.Y < 200)
                            {
                                gameObject.transform.Position = new Vector2(gameObject.transform.Position.X, 200);
                            }
                        }
                        break;
                    case Dir.Left:
                        if (gameObject.transform.Position.X > 225)
                        {
                            gameObject.transform.Position -= new Vector2(500, 0);
                            if (gameObject.transform.Position.X < 225)
                            {
                                gameObject.transform.Position = new Vector2(225,gameObject.transform.Position.Y);
                            }
                        }
                        break;
                    case Dir.Right:
                        if (gameObject.transform.Position.X < 1275)
                        {
                            gameObject.transform.Position += new Vector2(500, 0);
                            if (gameObject.transform.Position.X > 1275)
                            {
                                gameObject.transform.Position = new Vector2(1275, gameObject.transform.Position.Y);
                            }
                        }
                        break;
                }
            }

            if (kState.IsKeyDown(Keys.V) && kStateOld.IsKeyUp(Keys.V) && explosiveShot && explosiveCount > 0)
            {
                explosiveCount--;
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(gameObject.transform.direction ,gameObject.transform.Position, projSize, projSpeed,false, explosiveShot));
                explosiveTimer = 0.0f;
            }
            if (explosiveCount <= 0 && explosiveTimer >= 60.0f)
            {
                explosiveCount = 3;
            }
            if (sprayShotCount <= 0 && sprayShotTimer >= 25.0f)
            {
                sprayShotCount = 3;
            }
            if (dashUsed && dashTimer >= 5.0f)
			{
                dashUsed = false;
            }
            if (reloadTimer >= reloadSpeed && reload)
			{
                magazine = 3;
                reload = false;
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

            ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.Position = new Vector2(gameObject.transform.Position.X , gameObject.transform.Position.Y );
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
            if ( kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space) && reloadTimer > reloadSpeed)
            {

                    magazine--;
                ((Projectile)gameObject.GetComponent<Projectile>()).projectiles.Add(new Projectile(gameObject.transform.direction ,gameObject.transform.Position, projSize, projSpeed,piercingShot));
            if(magazine <= 0)
					{
                        reload = true;
                        reloadTimer = 0.0f;
                    }
                }

            kStateOld = kState;
           }
        }
    }
}
