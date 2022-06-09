using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2ndSemesterFinalExamen
{
    class Projectile : Component
    {
        public List<Projectile> projectiles = new List<Projectile>();

        private Vector2 position;
        public int speed = 1000;
        public int radius = 18;
        private Dir direction;
        private bool collided = false;
        private float Size;
        public bool PiercingShot;
        public bool explosiveShot = false;
        public int Damage { get; set; } = 25;
        private Vector2 posPlayer;
        public Dir parentDir;

       


        public Projectile(Dir playerDir, Vector2 newPos, float Size, int speed, bool PiercingShot)
        {
            position = newPos;
            direction = playerDir;
            this.Size = Size;
            this.speed = speed;
            float tmpRadius = (float)radius * Size;
            radius = (int)tmpRadius;
            this.PiercingShot = PiercingShot;
            explosiveShot = false;
        }
        public Projectile(Dir playerDir, Vector2 newPos, float Size, int speed, bool PiercingShot, bool explosiveShot)
        {
            position = newPos;
            direction = playerDir;
            this.Size = Size;
            this.speed = speed;
            float tmpRadius = (float)radius * Size;
            radius = (int)tmpRadius;
            this.PiercingShot = PiercingShot;
            this.explosiveShot = explosiveShot;

        }


        public Projectile(Dir playerDir, Vector2 newPos, float Size, int speed, bool PiercingShot,Vector2 posPlayer)
        {
            position = newPos;
            direction = playerDir;
            this.Size = Size;
            this.speed = speed;
            float tmpRadius = (float)radius * Size;
            radius = (int)tmpRadius;
            this.PiercingShot = PiercingShot;
            this.posPlayer = posPlayer;
        }

        public Projectile(Dir playerDir, Vector2 newPos, float Size, int speed, bool PiercingShot, Dir parentDir)
        {
            position = newPos;
            direction = playerDir;
            this.Size = Size;
            this.speed = speed;
            float tmpRadius = (float)radius * Size;
            radius = (int)tmpRadius;
            this.PiercingShot = PiercingShot;
            this.parentDir = parentDir;
        }



        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (direction)
            {
                case Dir.Down:
                    position += new Vector2(0, speed * Game1.dt);
                    break;
                case Dir.Up:
                    position -= new Vector2(0, speed * Game1.dt);
                    break;
                case Dir.Left:
                    position -= new Vector2(speed * Game1.dt, 0);
                    break;
                case Dir.Right:
                    position += new Vector2(speed * Game1.dt, 0);
                    break;
                case Dir.Shield:
                    position = new Vector2(Game1.Instance.Player.transform.Position.X + posPlayer.X, Game1.Instance.Player.transform.Position.Y + +posPlayer.Y);
                    break;
                case Dir.StrafeLeft:
                    if(parentDir == Dir.Left)
					{
                        position.X -= speed * Game1.dt;
                        position.Y += speed * Game1.dt;
                    }
                    if(parentDir == Dir.Down)
					{
                        position += new Vector2(speed * Game1.dt, speed * Game1.dt);
                    }
                    if (parentDir == Dir.Right )
                    {
                        position.X += speed * Game1.dt;
                        position.Y -= speed * Game1.dt;
                    }
                    if ( parentDir == Dir.Up)
					{
                        position -= new Vector2(speed * Game1.dt, speed * Game1.dt);
                    }
                        break;
                case Dir.StrafeRight:
                    if (parentDir == Dir.Left )
                    {
                        position -= new Vector2(speed * Game1.dt, speed * Game1.dt);
                    }
                    if(parentDir == Dir.Down)
					{
                        position.X -= speed * Game1.dt;
                        position.Y += speed * Game1.dt;
                    }
                    if (parentDir == Dir.Right)
                    {
                        position += new Vector2(speed * Game1.dt, speed * Game1.dt);
                    }
                    if ( parentDir == Dir.Up)
                    {
                        position.X += speed * Game1.dt;
                        position.Y -= speed * Game1.dt;
                    }
                    
                    break;

            }

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
                _spriteBatch.Draw(Game1.Instance.ball, new Vector2(position.X, position.Y), null, Color.White, 0.0f, new Vector2(0, 0), Size, SpriteEffects.None, 1.0f);
            
        }
    }
}
