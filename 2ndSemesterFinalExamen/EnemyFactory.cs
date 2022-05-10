using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace _2ndSemesterFinalExamen
{
    class EnemyFactory
{
    public static Enemy[] skullEnemies = new Enemy[250];
    public static Enemy[] monEnemies = new Enemy[250];
    public static Enemy[] ghostEnemies = new Enemy[250];

    public int timer = 0;

    static Random rNum = new Random();

    Thread skullThread, monThread, ghostThread;

    public EnemyFactory(Texture2D skullText, Texture2D monText, Texture2D ghostText)
    {
            for (int i = 0; i < skullEnemies.Length; i++)
            {
                skullEnemies[i] = new Enemy(skullText,150);
            }
            for (int i = 0; i < monEnemies.Length; i++)
            {
                monEnemies[i] = new Enemy(monText, 170);
            }
            for (int i = 0; i < ghostEnemies.Length; i++)
            {
                ghostEnemies[i] = new Enemy(ghostText, 200);
            }

            skullThread = new Thread(new ThreadStart(SkullSpawn));
            skullThread.IsBackground = true;
            skullThread.Start();

            monThread = new Thread(new ThreadStart(MonSpawn));
            monThread.IsBackground = true;
            monThread.Start();

            ghostThread = new Thread(new ThreadStart(GhostSpawn));
            ghostThread.IsBackground = true;
            ghostThread.Start();


    }
    
        public void SkullSpawn()
        {

        }

        public void MonSpawn()
        {

        }

        public void GhostSpawn()
        {

        }
}
}
