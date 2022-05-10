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
    }
}
