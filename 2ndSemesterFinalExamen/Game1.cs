using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;
using Comora;

enum GameStates
{
	PreGame,
	LoadGame,
	NewGame,
	Menu,
	InGame,
	Upgrades,
	Pause,
	GameOver,
	NextLevel,
}

enum Dir
{
	Down,
	Up,
	Left,
	Right
}
namespace _2ndSemesterFinalExamen
{
	 class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public  GameStates gameState = GameStates.PreGame;

		public GameDataBase GameDB = new GameDataBase();
		public SpriteFont gameFont;
    
		public  Texture2D gameOverButtonTexture,gameOverTexture, skull, mon, ghost,ball, background,buttonText, shoot, fasterShots, quickReload, speed, sprayShot, biggerProjectiles, piercingShot, dash, bulletShield, explosiveShot, explode, damage, line, LevelBox;

		public List<GameObject> gameObjects { get; private set; } = new List<GameObject>();

		public EnemyFactory enemyFactory;
		public MenuNavigator menuNavigator;
		public int currentLevel { get; set; } = 1;
		public float angleOfLine;
		public GameSaveData GameSave;

		public Vector2 mouseScreenPosition = Vector2.Zero;
		public Vector2 mouseWorldPosition = Vector2.Zero;

		public Vector2 buttonsScreenPosition = Vector2.Zero;
		public Vector2 buttonsWorldPosition = Vector2.Zero;
		public Vector2 ViewWVH;

		public GameObject Player = new GameObject();
		public Camera camera;
		private static Game1 instance;

		public bool talenTreeCreated = false;

		public static Game1 Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Game1();
				}
				return instance;
			}
		}

		


		public static float dt { get; private set; }
		private Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 720;
			_graphics.ApplyChanges();

			ViewWVH = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);




			this.camera = new Camera(_graphics.GraphicsDevice);
			this.camera.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
			GameDB.Initialize();
			IniciateGameComponents();
			foreach (GameObject gO in gameObjects)
			{
				gO.Awake();
			}

			line = new Texture2D(_graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			line.SetData(new[] { Color.White });

			base.Initialize();
		}
		


		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			background = Content.Load<Texture2D>("./assets/images/background");
			gameFont = Content.Load<SpriteFont>("assets/font/gameFont");
			ball = Content.Load<Texture2D>("assets/images/ball");
			skull = Content.Load<Texture2D>("assets/Enemy/skull");
			mon = Content.Load<Texture2D>("assets/Enemy/monster");
			ghost = Content.Load<Texture2D>("assets/Enemy/Ghost");
			buttonText = Content.Load<Texture2D>("assets/Buttons/knap");
			gameOverTexture = Content.Load<Texture2D>("assets/Buttons/jack_the_ghost_hunter");
			gameOverButtonTexture = Content.Load<Texture2D>("assets/Buttons/gameover");

			//Talent icons
			shoot = Content.Load<Texture2D>("assets/Icons/Shoot");
			fasterShots = Content.Load<Texture2D>("assets/Icons/faster shooting");
			quickReload = Content.Load<Texture2D>("assets/Icons/quick reload");
			speed = Content.Load<Texture2D>("assets/Icons/Speed");
			sprayShot = Content.Load<Texture2D>("assets/Icons/Spray Shot");
			damage = Content.Load<Texture2D>("assets/Icons/damage");
			bulletShield = Content.Load<Texture2D>("assets/Icons/bullet shield");
			biggerProjectiles = Content.Load<Texture2D>("assets/Icons/bigger projectiles");
			piercingShot = Content.Load<Texture2D>("assets/Icons/piercing shot");
			dash = Content.Load<Texture2D>("assets/Icons/dash");
			explosiveShot = Content.Load<Texture2D>("assets/Icons/explosive shot");
			explode = Content.Load<Texture2D>("assets/Icons/explosion");

			LevelBox = Content.Load<Texture2D>("assets/Buttons/talentCount");

			enemyFactory = EnemyFactory.Instance;
			menuNavigator = MenuNavigator.Instance;

			//enemyFactory.MonSpawn();
			//enemyFactory.GhostSpawn();

			// TODO: use this.Content to load your game content here
			//GameDB.CreateTableDB();
			//GameDB.InsertDB();

			foreach (GameObject gO in gameObjects)
			{
				gO.Start();
			}
		}

		

		protected override void Update(GameTime gameTime)
		{
			mouseScreenPosition = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
			buttonsWorldPosition = this.camera.Position;
			camera.ToWorld(ref mouseScreenPosition, out mouseWorldPosition);
			camera.ToScreen(ref buttonsWorldPosition, out buttonsScreenPosition);
			if (gameState == GameStates.InGame && Keyboard.GetState().IsKeyDown(Keys.Escape)) 
			{
				menuNavigator.currentGS = GameStates.Menu;
				gameState = menuNavigator.currentGS;
			}

			//	Exit();

			// TODO: Add your update logic here
			



		    if (gameState != GameStates.InGame)
			{
				menuNavigator.Update(gameTime);
			}
			else if (gameState == GameStates.InGame)
			{
				InGameUpdate(gameTime);
			}
			
		
			if (((Player)Player.GetComponent<Player>()).dead)
			{
				gameState = GameStates.GameOver;
				if(Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Enter))
				{
					enemyFactory.ResetEnemies();
					GameDB.UpdatePlayer(((Player)Player.GetComponent<Player>()));
					menuNavigator.currentGS = GameStates.Menu;
					gameState = menuNavigator.currentGS;
					((Player)Player.GetComponent<Player>()).Health = ((Player)Player.GetComponent<Player>()).MaxHealth;
					((Player)Player.GetComponent<Player>()).dead = false;
					enemyFactory.enemyKills = 0;
					Player.transform.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

				}
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin(this.camera);
			_spriteBatch.Draw(background, new Vector2(-450, -450), Color.White);

			//_spriteBatch.DrawString(gameFont, "MouseWorld X, Y: " + Game1.Instance.mouseWorldPosition, Player.transform.Position - new Vector2(300, 300), Color.White);
			_spriteBatch.DrawString(gameFont, "Level: " + currentLevel, Player.transform.Position - new Vector2(300, 300), Color.White);
			_spriteBatch.DrawString(gameFont, "Kills: " + enemyFactory.enemyKills + "/" + enemyFactory.maxSpawns, Player.transform.Position - new Vector2(300, 250), Color.White);
			_spriteBatch.DrawString(gameFont, "Current GameState: " + gameState, Player.transform.Position - new Vector2(300, 200), Color.White);

			//_spriteBatch.DrawString(gameFont, "ButtonsScreenPos X, Y: " + Game1.Instance.buttonsScreenPosition, Player.transform.Position - new Vector2(300, 250), Color.White);
			//_spriteBatch.DrawString(gameFont, "ButtonsWorldPos X, Y: " + Game1.Instance.buttonsWorldPosition, Player.transform.Position - new Vector2(300, 200), Color.White);
			//_spriteBatch.DrawString(gameFont, "MouseScreen X, Y: " + Mouse.GetState().Position, Player.transform.Position - new Vector2(300, 150), Color.White);
			//_spriteBatch.DrawString(gameFont, "CameraPos X, Y: " + this.camera.Position, Player.transform.Position - new Vector2(300, 150), Color.White);
			//_spriteBatch.DrawString(gameFont, "PlayerPos X, Y: " + Player.transform.Position, Player.transform.Position - new Vector2(300, 100), Color.White);


			// TODO: Add your drawing code here
			if (gameState != GameStates.PreGame)
			{
				InGameDraw(_spriteBatch);

			}

			if (gameState != GameStates.InGame)
			{
				menuNavigator.Draw(_spriteBatch);
			}

			
			
			
					

			_spriteBatch.End();
			base.Draw(gameTime);
		}

	

		
		
		
		public void InGameUpdate(GameTime gameTime) 
		{
			dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			EnemyFactory.timer -= dt;
			EnemyFactory.totalTime += dt;



			Player.Update(gameTime);
			this.camera.Position = Player.transform.Position + new Vector2(48, 48);
			this.camera.Update(gameTime);
			if (((Projectile)Player.GetComponent<Projectile>()) != null)
			{
				foreach (Projectile p in ((Projectile)Player.GetComponent<Projectile>()).projectiles)
				{
					p.Update(gameTime);
				}

			}

			foreach (GameObject e in enemyFactory.skullEnemies)
			{
				((Enemy)e.GetComponent<Enemy>()).Update(gameTime);
				((Enemy)e.GetComponent<Enemy>()).Move(gameTime, Player.transform.Position, ((Player)Player.GetComponent<Player>()).dead);
				int sum = 32 + ((Enemy)e.GetComponent<Enemy>()).radius;

				if (Vector2.Distance(Player.transform.Position, e.transform.Position) < sum)
				{
					//((Player)Player.GetComponent<Player>()).dead = true;
					((Player)Player.GetComponent<Player>()).Health -= ((Enemy)e.GetComponent<Enemy>()).Damage;
				}
			}

			foreach (GameObject e in enemyFactory.monEnemies)
			{
				((Enemy)e.GetComponent<Enemy>()).Update(gameTime);
				((Enemy)e.GetComponent<Enemy>()).Move(gameTime, Player.transform.Position, ((Player)Player.GetComponent<Player>()).dead);
				int sum = 32 + ((Enemy)e.GetComponent<Enemy>()).radius;

				if (Vector2.Distance(Player.transform.Position, e.transform.Position) < sum)
				{
					//((Player)Player.GetComponent<Player>()).dead = true;
					((Player)Player.GetComponent<Player>()).Health -= ((Enemy)e.GetComponent<Enemy>()).Damage;
				}
			}

			foreach (GameObject e in enemyFactory.ghostEnemies)
			{
				((Enemy)e.GetComponent<Enemy>()).Update(gameTime);
				((Enemy)e.GetComponent<Enemy>()).Move(gameTime, Player.transform.Position, ((Player)Player.GetComponent<Player>()).dead);
				int sum = 32 + ((Enemy)e.GetComponent<Enemy>()).radius;

				if (Vector2.Distance(Player.transform.Position, e.transform.Position) < sum)
				{
					//((Player)Player.GetComponent<Player>()).dead = true;
					((Player)Player.GetComponent<Player>()).Health -= ((Enemy)e.GetComponent<Enemy>()).Damage;
				}
			}



			if (((Projectile)Player.GetComponent<Projectile>()).projectiles.Count > 0)
			{

				foreach (Projectile proj in ((Projectile)Player.GetComponent<Projectile>()).projectiles)
				{

					foreach (GameObject enemy in enemyFactory.skullEnemies)
					{
						int sum = proj.radius + ((Enemy)enemy.GetComponent<Enemy>()).radius;

						if (Vector2.Distance(proj.Position, enemy.transform.Position) < sum)
						{
							proj.Collided = true;
							((Enemy)enemy.GetComponent<Enemy>()).Health -= ((Player)Player.GetComponent<Player>()).Damage;
							
							//((Enemy)enemy.GetComponent<Enemy>()).Dead = true;
							//enemy.transform.Position = new Vector2(-5000, -5000);
						}
					}
					foreach (GameObject enemy in enemyFactory.monEnemies)
					{
						int sum = proj.radius + ((Enemy)enemy.GetComponent<Enemy>()).radius;

						if (Vector2.Distance(proj.Position, enemy.transform.Position) < sum)
						{
							proj.Collided = true;
							((Enemy)enemy.GetComponent<Enemy>()).Health -= ((Player)Player.GetComponent<Player>()).Damage;
							//((Enemy)enemy.GetComponent<Enemy>()).Dead = true;
							//enemy.transform.Position = new Vector2(-5000, -5000);
						}
					}
					foreach (GameObject enemy in enemyFactory.ghostEnemies)
					{
						int sum = proj.radius + ((Enemy)enemy.GetComponent<Enemy>()).radius;

						if (Vector2.Distance(proj.Position, enemy.transform.Position) < sum)
						{
							proj.Collided = true;
							((Enemy)enemy.GetComponent<Enemy>()).Health -= ((Player)Player.GetComponent<Player>()).Damage;
							//((Enemy)enemy.GetComponent<Enemy>()).Dead = true;
							//enemy.transform.Position = new Vector2(-5000, -5000);
						}
					}
				}
			}


			((Projectile)Player.GetComponent<Projectile>()).projectiles.RemoveAll(p => p.Collided);
		}
		public void InGameDraw(SpriteBatch _spriteBatch)
		{
			//foreach (GameObject gO in gameObjects)
			//{
			//	if (((Projectile)gO.GetComponent<Projectile>()) != null)
			//	{

			//	}
			//	//gO.Draw(_spriteBatch);
			//	if (((Player)gO.GetComponent<Player>()) != null)
			//	{
			//		foreach (Projectile p in ((Projectile)gO.GetComponent<Projectile>()).projectiles)
			//		{
			//			p.Draw(_spriteBatch);
			//		}
			//	}
			//	((SpriteAnimation)gO.GetComponent<SpriteAnimation>()).anim.Draw(_spriteBatch, gO.transform.Position);
			//	//}

			//}


			if (((Player)Player.GetComponent<Player>()) != null)
			{
				if (!((Player)Player.GetComponent<Player>()).dead)
				{
					((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).anim.Draw(_spriteBatch, Player.transform.Position);
				}
				foreach (Projectile p in ((Projectile)Player.GetComponent<Projectile>()).projectiles)
				{
					p.Draw(_spriteBatch);
				}
			}

			foreach (GameObject e in enemyFactory.skullEnemies)
			{
				if (!((Enemy)e.GetComponent<Enemy>()).Dead)
				{
					((SpriteAnimation)e.GetComponent<SpriteAnimation>()).anim.Draw(_spriteBatch, e.transform.Position);
				}
			}
			foreach (GameObject e in enemyFactory.monEnemies)
			{
				if (!((Enemy)e.GetComponent<Enemy>()).Dead)
				{
					((SpriteAnimation)e.GetComponent<SpriteAnimation>()).anim.Draw(_spriteBatch, e.transform.Position);
				}
			}
			foreach (GameObject e in enemyFactory.ghostEnemies)
			{
				if (!((Enemy)e.GetComponent<Enemy>()).Dead)
				{
					((SpriteAnimation)e.GetComponent<SpriteAnimation>()).anim.Draw(_spriteBatch, e.transform.Position);
				}
			}

		}
		public void IniciateGameComponents()
		{
			//GameObject Player = new GameObject();

			

			Player.AddComponent(new Player());
			Player.transform.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
			Player.AddComponent(new Projectile(Player.transform.direction,Player.transform.Position));
			Player.AddComponent(new SpriteAnimation(Content.Load<Texture2D>("assets/Player/player"),1,8));
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkDown"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[1] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkUp"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[2] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkLeft"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[3] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkRight"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[0];
			
			gameObjects.Add(Player);

			
		}



	}

}
