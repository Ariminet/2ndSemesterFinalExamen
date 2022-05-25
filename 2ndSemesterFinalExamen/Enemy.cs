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
        public int speed { get; set; }
        public int baseSpeed { get; set; } = 150;
        public int radius = 30;
        public int Health { get; set; } = 50;
        public int maxHealth = 50;
        public int Damage { get; set; } = 5;
        bool dead = true;

        public Enemy() //skabellsen af fjende typen
        {
            this.speed = baseSpeed;
            Health = maxHealth;

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
            if (Health <= 0)
            {
                dead = true;
                //Game1.Instance.enemyFactory.CheckIfEnemiesAreDead();
                gameObject.transform.Position = new Vector2(-2000, -2000);
                Health = maxHealth;
                Game1.Instance.enemyFactory.enemyKills++;
            }
            if (gameObject.transform.isMoving)
            {
            ((SpriteAnimation)gameObject.GetComponent<SpriteAnimation>()).anim.AnimUpdate(gameTime);
            }
        }


    }
}
