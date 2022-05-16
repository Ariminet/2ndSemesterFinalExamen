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
        private int speed = 1000;
        public int radius = 18;
        private Dir direction;
        private bool collided = false;

       


        public Projectile(Dir playerDir, Vector2 newPos)
        {
            position = newPos;
            direction = playerDir;
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
            }

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Game1.Instance.ball, new Vector2(position.X , position.Y ), Color.White);
        }
    }
}
