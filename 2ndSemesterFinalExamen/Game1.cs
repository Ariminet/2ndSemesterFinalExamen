using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

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
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private GameStates gameState;

		private GameDataBase GameDB = new GameDataBase();

		Texture2D skull, mon, ghost;

		private EnemyFactory enemyFactory;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			GameDB.Initialize();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			skull = Content.Load<Texture2D>("");
			mon = Content.Load<Texture2D>("");
			ghost = Content.Load<Texture2D>("");

			enemyFactory = new EnemyFactory(skull, mon, ghost);

			// TODO: use this.Content to load your game content here
			//GameDB.CreateTableDB();
			//GameDB.InsertDB();
		}

		

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

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

			// TODO: Add your drawing code here

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



	}

}
