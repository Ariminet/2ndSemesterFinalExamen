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
        public static List<Projectile> projectiles = new List<Projectile>();

        //private Vector2 position;
        private int speed = 1000;
        public int radius = 18;
        //private Dir direction;
        private bool collided = false;

       


        public Projectile(Vector2 newPos, Dir newDir)
        {
            gameObject.transform.Position = newPos;
            gameObject.transform.direction = newDir;
        }

        public Vector2 Position
        {
            get
            {
                return gameObject.transform.Position;
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

            switch (gameObject.transform.direction)
            {
                case Dir.Down:
                    gameObject.transform.Position += new Vector2(gameObject.transform.Position.X, speed * dt);
                    break;
                case Dir.Up:
                    gameObject.transform.Position -= new Vector2(gameObject.transform.Position.X, speed * dt);
                    break;
                case Dir.Left:
                    gameObject.transform.Position -= new Vector2(speed * dt, gameObject.transform.Position.Y);
                    break;
                case Dir.Right:
                    gameObject.transform.Position += new Vector2(speed * dt, gameObject.transform.Position.Y);
                    break;
            }

        }
    }
}
