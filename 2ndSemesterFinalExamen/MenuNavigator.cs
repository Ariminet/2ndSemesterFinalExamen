using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2ndSemesterFinalExamen
{
    class MenuNavigator
    {
        private static MenuNavigator instance;
        public List<Component> preGameComponents, loadGameComponents, newGameComponents, inGameComponents, menuGameComponents, pauseGameComponents, upgradeGameComponents;

        public static MenuNavigator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuNavigator();
                }
                return instance;
            }
        }

        private MenuNavigator()
        {
            var logIn = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -30),
                Text = "Login via. Tag"
            };

            var createNewGame = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 30),
                Text = "Create New Game"
            };

            logIn.Click += LogInStateChange;
            createNewGame.Click += CreateNewGame;

            preGameComponents = new List<Component>()
            {
                logIn,
                createNewGame,
            };


            //UPGRADE

            var shot = new DescriptButton(Game1.Instance.buttonText, Game1.Instance.gameFont, Game1.Instance.shoot, "shoot", new Vector2(75, -250), new Vector2 (-30, -150),new Vector2(), new Vector2())
            {};
            var fasterShots = new DescriptButton(Game1.Instance.buttonText, Game1.Instance.gameFont, Game1.Instance.fasterShots, "faster shooting", new Vector2(-30, -150), new Vector2(), new Vector2(), new Vector2())
            {};



            inGameComponents = new List<Component>()
            {
                shot,
                fasterShots,
            };

        }

        public  void Update(GameTime gameTime)
		{
			switch (Game1.Instance.gameState)
			{
				case GameStates.PreGame:
                    foreach (var component in preGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.LoadGame:
                    foreach (var component in loadGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.NewGame:
                    foreach (var component in newGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.Menu:
                    foreach (var component in menuGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.InGame:
                    foreach (var component in inGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.Upgrades:
                    foreach (var component in upgradeGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.Pause:
                    foreach (var component in pauseGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				default:
					break;
			}
		}
        
        public void Draw(SpriteBatch _spriteBatch)
		{
            switch (Game1.Instance.gameState)
            {
                case GameStates.PreGame:
                    foreach (var component in preGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.LoadGame:
                    foreach (var component in loadGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.NewGame:
                    foreach (var component in newGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.Menu:
                    foreach (var component in menuGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                     break;
                case GameStates.InGame:
                    foreach (var component in inGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.Upgrades:
                    foreach (var component in upgradeGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.Pause:
                    foreach (var component in pauseGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                default:
                    break;
            }
        }

        private void LogInStateChange(object sender, System.EventArgs e)
        {
            Game1.Instance.gameState = GameStates.LoadGame;
        }
        private void CreateNewGame(object sender, System.EventArgs e)
        {
            Game1.Instance.gameState = GameStates.NewGame;
        }
    }
}
