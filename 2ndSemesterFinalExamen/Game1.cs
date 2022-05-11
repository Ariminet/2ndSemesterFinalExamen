using Microsoft.Xna.Framework;
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

		private GameStates gameState;

		private GameDataBase GameDB = new GameDataBase();
		SpriteFont gameFont;
		Texture2D skull, mon, ghost;
		public List<GameObject> gameObjects { get; private set; } = new List<GameObject>();
		private EnemyFactory enemyFactory;
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
			gameFont = Content.Load<SpriteFont>("assets/font/gameFont");
			//skull = Content.Load<Texture2D>("");
			//mon = Content.Load<Texture2D>("");
			//ghost = Content.Load<Texture2D>("");

			//enemyFactory = new EnemyFactory(skull, mon, ghost);


			//enemyFactory.SkullSpawn();
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
			dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

			
			
			foreach (GameObject gO in gameObjects)
			{
				gO.Update(gameTime);
				if(((Player)gO.GetComponent<Player>()) != null)
				{
					this.camera.Position = gO.transform.Position + new Vector2(48,48);
					this.camera.Update(gameTime);
				}
			}

			switch (gameState)
			{
				case GameStates.PreGame:
					PreGame();
					break;
				case GameStates.Menu:
					GameMenu();
					break;
				case GameStates.InGame:
					InGame();
					break;
				case GameStates.Upgrades:
					UpgradesMenu();
					break;
				case GameStates.Pause:
					PauseGame();
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
			// TODO: Add your drawing code here
			foreach (GameObject gO in gameObjects)
			{
				//gO.Draw(_spriteBatch);
				//if(((Player)gO.GetComponent<Player>())!= null)
				//{
				((SpriteAnimation)gO.GetComponent<SpriteAnimation>()).anim.Draw(_spriteBatch, gO.transform.Position);
				//}
				_spriteBatch.DrawString(gameFont, "Current Position: " + gO.transform.Position, new Vector2(500, 500), Color.White);
			
			}
			_spriteBatch.End();
			base.Draw(gameTime);
		}

	

		public void PreGame()
		{

		}
		public void GameMenu()
		{

		}
		public void InGame() 
		{
		
		}
		public void UpgradesMenu() 
		{

		}
		public void PauseGame()
		{

		}

		public void IniciateGameComponents()
		{
			GameObject Player = new GameObject();
			Player.AddComponent(new Player());
			Player.AddComponent(new SpriteAnimation(Content.Load<Texture2D>("assets/Player/player"),1,8));
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[0] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkDown"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[1] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkUp"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[2] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkLeft"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[3] = new SpriteAnimation(Content.Load<Texture2D>("assets/Player/walkRight"), 4, 8);
			((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).anim = ((SpriteAnimation)Player.GetComponent<SpriteAnimation>()).animations[0];
			Player.transform.Position = new Vector2(_graphics.PreferredBackBufferWidth/2 - 48 , _graphics.PreferredBackBufferHeight / 2 - 48);
			gameObjects.Add(Player);
			
		}



	}

}
