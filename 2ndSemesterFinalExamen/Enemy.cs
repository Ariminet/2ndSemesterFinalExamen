using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2ndSemesterFinalExamen
{
    class Enemy : Component
{
        private Vector2 pos;
        private int speed;
        public int radius = 30;
        public int Health { get; set; } = 0;

        bool dead = true;

        public Enemy(int spe) //skabellsen af fjende typen
        {
            this.speed = spe;
            
        }
        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public void Move(GameTime gameTime, Vector2 playPos, bool PlayerDead)
        {

			
            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(!PlayerDead && !dead && Game1.Instance.gameState == GameStates.InGame)
            {
                Vector2 moveDir = playPos - gameObject.transform.Position;
                moveDir.Normalize();
                gameObject.transform.Position += moveDir * (speed * Game1.dt);

            }

            
        }
  //      public void SetToDead()
		//{
		//	if (dead)
		//	{

		//	}
		//}
        public override void Update(GameTime gameTime)
        {
            if (gameObject.transform.isMoving)
            {
            ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.AnimUpdate(gameTime);
            }
        }


    }
}
