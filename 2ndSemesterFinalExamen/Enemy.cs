using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2ndSemesterFinalExamen
{
    class Enemy
{
        private Vector2 pos;
        private int speed;
        public SpriteAnimation anim;
        public int radius = 30;

        bool dead = true;

        public Enemy(Texture2D text, int spe) //skabellsen af fjende typen
        {
            this.speed = spe;
            anim = new SpriteAnimation(text, 10, 6);
        }
        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public void Draw (GameTime gameTime, Vector2 playPos, bool PlayerDead)
        {
            anim.Position = new Vector2(pos.X - 48, pos.Y - 66);
            anim.AnimUpdate(gameTime);

            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(!PlayerDead)
            {
                Vector2 moveDir = playPos - pos;
                moveDir.Normalize();
                pos += moveDir * (speed * Game1.dt);
            }

            
        }
    }
}
