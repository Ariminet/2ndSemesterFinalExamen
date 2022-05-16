﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace _2ndSemesterFinalExamen
{
    class EnemyFactory
    {
       

        public GameObject[] skullEnemies = new GameObject[250];
        public GameObject[] monEnemies = new GameObject[250];
        public GameObject[] ghostEnemies = new GameObject[250];
        public double timer = 2D;
        public double spawnTimer = 2D;
        public double totalTime = 0;


        static Random rNum = new Random();

        Thread skullThread, monThread, ghostThread;

        private static EnemyFactory instance;

        public static EnemyFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyFactory();
                }
                return instance;
            }
        }

        private EnemyFactory()
        {
            timer = spawnTimer;
            for (int i = 0; i < skullEnemies.Length; i++)
            {
                GameObject tmp = new GameObject();
                tmp.AddComponent(new Enemy(150));
                tmp.AddComponent(new SpriteAnimation(Game1.Instance.skull, 4, 8));
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Game1.Instance.skull, 10, 8);
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0];
                skullEnemies[i] = tmp;
            }
            for (int i = 0; i < monEnemies.Length; i++)
            {
                GameObject tmp = new GameObject();
                tmp.AddComponent(new Enemy(150));
                tmp.AddComponent(new SpriteAnimation(Game1.Instance.mon, 4, 8));
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Game1.Instance.mon, 10, 8);
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0];
                monEnemies[i] = tmp;
            }
            for (int i = 0; i < ghostEnemies.Length; i++)
            {
                GameObject tmp = new GameObject();
                tmp.AddComponent(new Enemy(150));
                tmp.AddComponent(new SpriteAnimation(Game1.Instance.ghost, 4, 8));
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Game1.Instance.ghost, 10, 8);
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0];
                ghostEnemies[i] = tmp;
            }

            skullThread = new Thread(new ThreadStart(SkullSpawn));
            skullThread.IsBackground = true;
            skullThread.Start();

            //monThread = new Thread(new ThreadStart(MonSpawn));
            //monThread.IsBackground = true;
            //monThread.Start();

            //ghostThread = new Thread(new ThreadStart(GhostSpawn));
            //ghostThread.IsBackground = true;
            //ghostThread.Start();


        }

        public void EnemyPaused()
		{
           totalTime = 0;
           timer = 2D;
           spawnTimer = 2D;
        }
        public void SkullSpawn()
        {
           
           while (Game1.Instance.gameState == GameStates.InGame)
            {


                if (timer <= 0)
                {
                    timer = spawnTimer;
                    int side = rNum.Next(4);

                    switch (side)
                    {
                        case 0:
                            foreach (GameObject e in skullEnemies)
                            {
                                if (((Enemy)e.GetComponent<Enemy>()).Dead)
                                {
                                    e.transform.isMoving = true;
                                    e.transform.Position = new Vector2(-500, rNum.Next(-500, 2000));
                                    
                                    ((Enemy)e.GetComponent<Enemy>()).Dead = false;
                                    break;
                                }
                                
                            }
                            break;
                        case 1:
                            foreach (GameObject e in skullEnemies)
                            {
                                if (((Enemy)e.GetComponent<Enemy>()).Dead)
                                {
                                    e.transform.isMoving = true;
                                    e.transform.Position = new Vector2(-500, rNum.Next(2000, 2000));
                                    ((Enemy)e.GetComponent<Enemy>()).Dead = false;
                                    break;

                                }

                            }
                            break;
                        case 2:
                            foreach (GameObject e in skullEnemies)
                            {
                                if (((Enemy)e.GetComponent<Enemy>()).Dead)
                                {
                                    e.transform.isMoving = true;
                                    e.transform.Position = (new Vector2(rNum.Next(-500, 2000), -500));
                                    ((Enemy)e.GetComponent<Enemy>()).Dead = false;
                                    break;

                                }

                            }
                            break;
                        case 3:
                            foreach (GameObject e in skullEnemies)
                            {
                                if (((Enemy)e.GetComponent<Enemy>()).Dead)
                                {
                                    e.transform.isMoving = true;
                                    e.transform.Position = (new Vector2(rNum.Next(-500, 2000), 2000));
                                    ((Enemy)e.GetComponent<Enemy>()).Dead = false;
                                    break;
                                }
                            }
                            break;
                    }
                    
                    
                }

                if (spawnTimer > 0.5)
                {
                    spawnTimer -= 0.1;
                }

            }
        }
    }
       
}
