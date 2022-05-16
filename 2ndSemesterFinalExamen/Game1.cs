﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Comora;

enum GameStates
{
	PreGame,
	LoadGame,
	NewGame,
	Menu,
	InGame,
	Upgrades,
	Pause
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

		private GameDataBase GameDB = new GameDataBase();
		public SpriteFont gameFont;
		public  Texture2D skull, mon, ghost,ball, background,buttonText;
		public List<GameObject> gameObjects { get; private set; } = new List<GameObject>();

		public EnemyFactory enemyFactory;
		public MenuNavigator menuNavigator;

		

		public GameObject Player = new GameObject();
		Camera camera;
		private static Game1 instance;

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

			

			this.camera = new Camera(_graphics.GraphicsDevice);
			this.camera.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
			GameDB.Initialize();
			IniciateGameComponents();

			foreach (GameObject gO in gameObjects)
			{
				gO.Awake();
			}
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			background = Content.Load<Texture2D>("./assets/images/background");
			gameFont = Content.Load<SpriteFont>("assets/font/gameFont");
			ball = Content.Load<Texture2D>("assets/images/ball");
			skull = Content.Load<Texture2D>("assets/Enemy/skull");
			mon = Content.Load<Texture2D>("assets/Enemy/skull");
			ghost = Content.Load<Texture2D>("assets/Enemy/skull");
			buttonText = Content.Load<Texture2D>("assets/Buttons/knap");

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
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			menuNavigator.Update(gameTime);


			switch (gameState)
			{
				case GameStates.PreGame:
					PreGame();
					enemyFactory.EnemyPaused();
					break;
				case GameStates.Menu:
					GameMenu();
					enemyFactory.EnemyPaused();
					break;
				case GameStates.InGame:
					InGameUpdate(gameTime);
					break;
				case GameStates.Upgrades:
					UpgradesMenu();
					enemyFactory.EnemyPaused();
					break;
				case GameStates.Pause:
					PauseGame();
					enemyFactory.EnemyPaused();
					break;
				default:
					break;
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin(this.camera);
			
			_spriteBatch.Draw(background, new Vector2(-450, -450), Color.White);

			// TODO: Add your drawing code here
			menuNavigator.Draw(_spriteBatch);

			switch (gameState)
			{
				case GameStates.PreGame:
					DrawPreGame();
					break;
				case GameStates.Menu:
					DrawGameMenu();
					
					break;
				case GameStates.InGame:
					InGameDraw(_spriteBatch);
					break;
				case GameStates.Upgrades:
					DrawUpgradesMenu();
					break;
				case GameStates.Pause:
					DrawPauseGame();
					break;
				default:
					break;
			}

			_spriteBatch.End();
			base.Draw(gameTime);
		}

	

		public void PreGame()
		{

		}
		public void DrawPreGame()
		{

		}
		public void GameMenu()
		{

		}
		public void DrawGameMenu()
		{

		}
		public void InGameUpdate(GameTime gameTime) 
		{
			dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			enemyFactory.timer -= dt;



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
					((Player)Player.GetComponent<Player>()).dead = true;
				}
			}

			foreach (GameObject e in enemyFactory.monEnemies)
			{
				((Enemy)e.GetComponent<Enemy>()).Update(gameTime);
				int sum = 32 + ((Enemy)e.GetComponent<Enemy>()).radius;

				if (Vector2.Distance(Player.transform.Position, e.transform.Position) < sum)
				{
					((Player)Player.GetComponent<Player>()).dead = true;
				}
			}

			foreach (GameObject e in enemyFactory.ghostEnemies)
			{
				((Enemy)e.GetComponent<Enemy>()).Update(gameTime);
				int sum = 32 + ((Enemy)e.GetComponent<Enemy>()).radius;

				if (Vector2.Distance(Player.transform.Position, e.transform.Position) < sum)
				{
					((Player)Player.GetComponent<Player>()).dead = true;
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
							((Enemy)enemy.GetComponent<Enemy>()).Dead = true;
							enemy.transform.Position = new Vector2(-5000, -5000);
						}
					}
					foreach (GameObject enemy in enemyFactory.monEnemies)
					{
						int sum = proj.radius + ((Enemy)enemy.GetComponent<Enemy>()).radius;

						if (Vector2.Distance(proj.Position, enemy.transform.Position) < sum)
						{
							proj.Collided = true;
							((Enemy)enemy.GetComponent<Enemy>()).Dead = true;
							enemy.transform.Position = new Vector2(-5000, -5000);
						}
					}
					foreach (GameObject enemy in enemyFactory.ghostEnemies)
					{
						int sum = proj.radius + ((Enemy)enemy.GetComponent<Enemy>()).radius;

						if (Vector2.Distance(proj.Position, enemy.transform.Position) < sum)
						{
							proj.Collided = true;
							((Enemy)enemy.GetComponent<Enemy>()).Dead = true;
							enemy.transform.Position = new Vector2(-5000, -5000);
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

		}
		public void UpgradesMenu() 
		{

		}
		public void DrawUpgradesMenu()
		{

		}
		public void PauseGame()
		{

		}
		public void DrawPauseGame()
		{

		}

		public void IniciateGameComponents()
		{
			//GameObject Player = new GameObject();

			

			Player.AddComponent(new Player());
			Player.transform.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 48, _graphics.PreferredBackBufferHeight / 2 - 48);
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
