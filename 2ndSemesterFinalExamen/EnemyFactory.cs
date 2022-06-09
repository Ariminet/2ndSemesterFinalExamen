using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace _2ndSemesterFinalExamen
{
    enum FactoryStates
	{
        InProgess,
        Paused,
	}
    class EnemyFactory
    {
       

        public GameObject[] skullEnemies = new GameObject[250];
        public GameObject[] monEnemies = new GameObject[250];
        public GameObject[] ghostEnemies = new GameObject[250];
        //public double timer = 2D;
        //public double spawnTimer = 2D;
        //public double spawnTimerMax = 2;
        //public double totalTime = 0;
        public static double timer = 2D;
        public static double maxTime = 2D;
        public static double totalTime = 0;
        private int currentSpawned = 0;
        private int basicSpawns = 5;
        public int maxSpawns = 5;
        //private bool enemiesAlive = true;
        public int enemyKills = 0;
        private bool nextLevelState = false;

        static Random rNum = new Random();

        //Thread skullThread, monThread, ghostThread;
        Thread spawnThread;

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
            timer = 0;
            for (int i = 0; i < skullEnemies.Length; i++)
            {
                GameObject tmp = new GameObject();
                tmp.AddComponent(new Enemy());
                ((Enemy)tmp.GetComponent<Enemy>()).Points = 10;
                tmp.AddComponent(new SpriteAnimation(Game1.Instance.skull, 4, 8));
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Game1.Instance.skull, 10, 8);
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0];
                skullEnemies[i] = tmp;
            }
            for (int i = 0; i < monEnemies.Length; i++)
            {
                GameObject tmp = new GameObject();
                tmp.AddComponent(new Enemy());
                ((Enemy)tmp.GetComponent<Enemy>()).Points = 15;
                tmp.AddComponent(new SpriteAnimation(Game1.Instance.mon, 4, 8));
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Game1.Instance.mon, 10, 8);
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0];
                monEnemies[i] = tmp;
            }
            for (int i = 0; i < ghostEnemies.Length; i++)
            {
                GameObject tmp = new GameObject();
                tmp.AddComponent(new Enemy());
                ((Enemy)tmp.GetComponent<Enemy>()).Points = 5;
                tmp.AddComponent(new SpriteAnimation(Game1.Instance.ghost, 4, 8));
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Game1.Instance.ghost, 10, 8);
                ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)tmp.GetComponent<SpriteAnimation>()).animations[0];
                ghostEnemies[i] = tmp;
            }

            spawnThread = new Thread(new ThreadStart(Spawner));
            spawnThread.IsBackground = true;
            spawnThread.Start();

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
            maxTime = 2D;
           //spawnTimer = 2D;
        }
        public void ResetEnemies()
		{
			foreach (GameObject e in skullEnemies)
			{
				if (!((Enemy)e.GetComponent<Enemy>()).Dead)
				{
                    ((Enemy)e.GetComponent<Enemy>()).Dead = true;
                    e.transform.Position = new Vector2(-5000, -5000);
                }
			}

            foreach (GameObject e in ghostEnemies)
            {
                if (!((Enemy)e.GetComponent<Enemy>()).Dead)
                {
                    ((Enemy)e.GetComponent<Enemy>()).Dead = true;
                    e.transform.Position = new Vector2(-5000, -5000);
                }
            }

            foreach (GameObject e in monEnemies)
            {
                if (!((Enemy)e.GetComponent<Enemy>()).Dead)
                {
                    ((Enemy)e.GetComponent<Enemy>()).Dead = true;
                    e.transform.Position = new Vector2(-5000, -5000);
                }
            }
        }

        public void SkullSpawner()
		{
            //timer = spawnTimer;
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
                            e.transform.Position = new Vector2(-500, rNum.Next(-500, 2000));
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

        public void GhostSpawner()
		{
            
                //timer = spawnTimer;
                int side = rNum.Next(4);

                switch (side)
                {
                    case 0:
                        foreach (GameObject e in ghostEnemies)
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
                        foreach (GameObject e in ghostEnemies)
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
                    case 2:
                        foreach (GameObject e in ghostEnemies)
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
                        foreach (GameObject e in ghostEnemies)
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

        public void MonSpawner()
		{
            //timer = spawnTimer;
            int side = rNum.Next(4);

            switch (side)
            {
                case 0:
                    foreach (GameObject e in monEnemies)
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
                    foreach (GameObject e in monEnemies)
                    {
                        if (((Enemy)e.GetComponent<Enemy>()).Dead)
                        {
                            e.transform.isMoving = true;
                            e.transform.Position = new Vector2(2000, rNum.Next(-500, 2000));
                            ((Enemy)e.GetComponent<Enemy>()).Dead = false;
                            break;

                        }

                    }
                    break;
                case 2:
                    foreach (GameObject e in monEnemies)
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
                    foreach (GameObject e in monEnemies)
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

        public void SpeedBoostEnemies()
        {
            foreach (GameObject e in skullEnemies)
            {
                
                    ((Enemy)e.GetComponent<Enemy>()).baseSpeed += 6;
                    ((Enemy)e.GetComponent<Enemy>()).speed = ((Enemy)e.GetComponent<Enemy>()).baseSpeed;
                
            }

            foreach (GameObject e in ghostEnemies)
            {
                
                    ((Enemy)e.GetComponent<Enemy>()).baseSpeed += 15 ;
                    ((Enemy)e.GetComponent<Enemy>()).speed = ((Enemy)e.GetComponent<Enemy>()).baseSpeed;
                
            }

            foreach (GameObject e in monEnemies)
            {
                
                    ((Enemy)e.GetComponent<Enemy>()).baseSpeed += 4;
                    ((Enemy)e.GetComponent<Enemy>()).speed = ((Enemy)e.GetComponent<Enemy>()).baseSpeed;
                
            }
        }

        public void HealthBoostEnemies()
        {
            foreach (GameObject e in skullEnemies)
            {
                    ((Enemy)e.GetComponent<Enemy>()).maxHealth += 5 ;
            }

            foreach (GameObject e in ghostEnemies)
            {
                    ((Enemy)e.GetComponent<Enemy>()).maxHealth += 1 ;
            }

            foreach (GameObject e in monEnemies)
            {
                    ((Enemy)e.GetComponent<Enemy>()).maxHealth += 10 ;
            }
        }

  
        public void SetFactoryStats()
		{
            int timesHandle = 0;
            if(Game1.Instance.currentLevel >= 10)
			{
                timesHandle = Game1.Instance.currentLevel % 10;
            }
			for (int i = 0; i <= timesHandle; i++)
			{
                //basicSpawns += 5;
                SpeedBoostEnemies();
                HealthBoostEnemies();
                //spawnTimerMax -= 0.2;
            }
            maxSpawns += (Game1.Instance.currentLevel / 10) * 5;
            
			foreach (GameUnit unit in Game1.Instance.GameSave.ListGameUnits)
			{
				switch (unit.Tag)
				{
                    case "Ghost":
                        foreach (GameObject e in ghostEnemies)
                        {
                            if (e.transform.Position.X == 0 || e.transform.Position.Y == -5000 )
                            {
                                ((Enemy)e.GetComponent<Enemy>()).maxHealth = unit.Health;
                                ((Enemy)e.GetComponent<Enemy>()).Health = ((Enemy)e.GetComponent<Enemy>()).maxHealth;
                                e.transform.Position = new Vector2(unit.PosX, unit.PosY);
                                break;

                            }

                        }
                        break;
                    case "Skull":
                        foreach (GameObject e in skullEnemies)
                        {
                            if (e.transform.Position.X == 0 || e.transform.Position.Y == -5000)  
                            {
                                ((Enemy)e.GetComponent<Enemy>()).maxHealth = unit.Health;
                                ((Enemy)e.GetComponent<Enemy>()).Health = ((Enemy)e.GetComponent<Enemy>()).maxHealth;
                                e.transform.Position = new Vector2(unit.PosX, unit.PosY);
                                break;
                            }

                        }
                        break;
                    case "Monster":
						foreach (GameObject e in monEnemies)
						{
                            if (e.transform.Position.X == 0 || e.transform.Position.Y == -5000) 
                            {
                                ((Enemy)e.GetComponent<Enemy>()).maxHealth = unit.Health;
                                ((Enemy)e.GetComponent<Enemy>()).Health = ((Enemy)e.GetComponent<Enemy>()).maxHealth;
                                e.transform.Position = new Vector2(unit.PosX, unit.PosY);
                                break;
                            }
                            
						}
                        break;
                    case "Player":
                        Game1.Instance.Player.transform.Position = new Vector2(unit.PosX, unit.PosY);
                        ((Player)Game1.Instance.Player.GetComponent<Player>()).LoadPlayer(unit.Health);
                        break;
                    default:
						break;
				}
                Game1.Instance.camera.Position = Game1.Instance.Player.transform.Position;

            }
        }
        public void Spawner()
        {
           
            
            
            
         
            while (true)
            {
                if (nextLevelState && Game1.Instance.currentLevel >= 10 &&  10 % Game1.Instance.currentLevel == 0)
                {
                    //basicSpawns += 5;
                    //SpeedBoostEnemies();
                    //HealthBoostEnemies();
                    //spawnTimerMax -= 0.5;
                    //spawnTimerMax = 2;
                    //maxSpawns = Game1.Instance.currentLevel * 2 + basicSpawns;
                    maxSpawns += Game1.Instance.currentLevel * 5 / 10;
                    nextLevelState = false;

                }

                //maxSpawns = Game1.Instance.currentLevel * 2 + basicSpawns;



                //if (currentSpawned >= maxSpawns && !enemiesAlive && Game1.Instance.gameState == GameStates.InGame)
                if (enemyKills >= maxSpawns  && Game1.Instance.gameState == GameStates.InGame)
                {
                    Game1.Instance.menuNavigator.currentGS = GameStates.NextLevel;
                    Game1.Instance.gameState = GameStates.NextLevel;
                    ((Player)Game1.Instance.Player.GetComponent<Player>()).CurrentLevel++;
                    SpeedBoostEnemies();
                    HealthBoostEnemies();
                    currentSpawned = 0;
                    nextLevelState = true;
                    enemyKills = 0;
                    //spawnTimerMax -= 0.2;

                }

                if (Game1.Instance.gameState != GameStates.InGame)
                {
                    totalTime = 0;
                    timer = 2D;
                    maxTime = 2D;
                    //spawnTimer = 2D;
                }

                if (Game1.Instance.gameState == GameStates.InGame)
                {

                    if (currentSpawned < maxSpawns) { 
                    if (timer <= 0)
                    {
                        int monType = rNum.Next(3);
                        switch (monType)
                        {
                            case 0:
                                SkullSpawner();
                                    currentSpawned++;
                                break;
                            case 1:
                                GhostSpawner();
                                    currentSpawned++;
                                break;
                            case 2:
                                MonSpawner();
                                    currentSpawned++;
                                break;
                            default:
                                break;
                        }

                            timer = maxTime;

                            if (maxTime > 1)
                            {
                                maxTime -= 0.1;
                            }

                        }

                        
                    }

                }
            }
        }
    }
       
}
