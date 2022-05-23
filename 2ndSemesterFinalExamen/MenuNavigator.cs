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
        public List<Component> preGameComponents, loadGameComponents, newGameComponents, inGameComponents, menuGameComponents, pauseGameComponents, upgradeGameComponents, gameOverComponents;
        private InputComponent userTag;
        private InputComponent newUserTag;
        public GameStates currentGS = GameStates.PreGame;
        private GameStates previousGS;
        private bool failedOrPassed = true;
        private TextComponent IncorrectData, GameOverText;
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
            var backButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(-525, -325),
                Text = "Back"
            };

            IncorrectData = new TextComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -150),
                Text = ""
            };

            GameOverText = new TextComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 0),
                Text = "Game Over..."
            };
            backButton.Click += PreviousGameState;
            //PRE - GAME 
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
            //PRE - GAME 

            //NEW - GAME 
            var newUserInfo = new TextComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -90),
                Text = "Player Tag:"
            };
            newUserTag = new InputComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -30),
                startText = "......",
            };

            var CreateChar = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 30),
                Text = "Create Character"
            };

            CreateChar.Click += CreateCharacter;

            newGameComponents = new List<Component>()
            {
                newUserInfo,
                newUserTag,
                CreateChar,
                backButton,
                
            };
            //NEW - GAME 

            //LOAD - GAME 
            var userTagInfo = new TextComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -90),
                Text = "User Tag:"
            };
             userTag = new InputComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -30),
                startText = "......",
            };

            var SendLogin = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 30),
                Text = "Login In"
            };

            SendLogin.Click += LogInToChar;

            loadGameComponents = new List<Component>()
            {
                userTagInfo,
                userTag,
                SendLogin,
                backButton,
                
            };
            //LOAD - GAME 

            //MENU - GAME 
            var resumeGame = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -90),
                Text = "ResumeGame"
            };
            var upgradesButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -30),
                Text = "Talents",
            };

            var quitButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 30),
                Text = "Save Game"
            };

            resumeGame.Click += ResumePlayingGame;
            upgradesButton.Click += GoToUpgrades;
            quitButton.Click += StopPlayingGame;
            menuGameComponents = new List<Component>()
            {
                resumeGame,
                upgradesButton,
                quitButton,
                
            };
            //MENU - GAME 

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
            if(Game1.Instance.gameState != currentGS)
			{
                Game1.Instance.gameState = currentGS;
            }
            switch (Game1.Instance.gameState)
			{
				case GameStates.PreGame:
                    foreach (var component in preGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.LoadGame:
                    if (!failedOrPassed)
                    {
                        IncorrectData.Text = "Tag does not exsist";
                        IncorrectData.Update(gameTime);

                    }
                    foreach (var component in loadGameComponents)
                    {
                        component.Update(gameTime);
                    }
                    break;
				case GameStates.NewGame:
                    foreach (var component in newGameComponents)
                    {
                        if (!failedOrPassed)
                        {
                            IncorrectData.Text = "Tag is already in use";
                            IncorrectData.Update(gameTime);

                        }
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
                case GameStates.GameOver:
                    GameOverText.Update(gameTime);
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
                    if (!failedOrPassed)
                    {
                        IncorrectData.Draw(_spriteBatch);

                    }
                    foreach (var component in loadGameComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.NewGame:
                    if (!failedOrPassed)
                    {
                        IncorrectData.Draw(_spriteBatch);

                        }
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
                case GameStates.GameOver:
                    GameOverText.Draw(_spriteBatch);
                    break;
                default:
                    break;
            }
        }

        private void PreviousGameState(object sender, System.EventArgs e)
        {
            currentGS = previousGS;
            failedOrPassed = true;
        }
        private void LogInStateChange(object sender, System.EventArgs e)
        {
            previousGS = currentGS;
            currentGS = GameStates.LoadGame;
        }
        private void CreateNewGame(object sender, System.EventArgs e)
        {
            previousGS = currentGS;
            currentGS = GameStates.NewGame;
        }
        private void CreateCharacter(object sender, System.EventArgs e)
        {
            ((Player)Game1.Instance.Player.GetComponent<Player>()).Tag = newUserTag.CurrentValue;
                 failedOrPassed = Game1.Instance.GameDB.AddPlayer(((Player)Game1.Instance.Player.GetComponent<Player>()));
                if (failedOrPassed)
                {
                    currentGS = GameStates.Menu;
                }
                else
                {
                    currentGS = GameStates.NewGame;
                }
			if (failedOrPassed)
			{
                Game1.Instance.GameDB.AddPlayerTalentTree(((Player)Game1.Instance.Player.GetComponent<Player>()));
            }
           


        }
        private void LogInToChar(object sender, System.EventArgs e)
        {
            ((Player)Game1.Instance.Player.GetComponent<Player>()).Tag = userTag.CurrentValue;
             failedOrPassed = Game1.Instance.GameDB.GetPlayer(((Player)Game1.Instance.Player.GetComponent<Player>()));

            if (failedOrPassed)
            {
                Game1.Instance.GameSave = new GameSaveData();
                Game1.Instance.GameSave.ListGameUnits = Game1.Instance.GameDB.GetSaveGame(((Player)Game1.Instance.Player.GetComponent<Player>()));
                currentGS = GameStates.Menu;
            }
            else
            {
                currentGS = GameStates.LoadGame;
            }
        }


        private void ResumePlayingGame(object sender, System.EventArgs e)
		{
            currentGS = GameStates.InGame;
		}
        private void GoToUpgrades(object sender, System.EventArgs e)
        {
            currentGS = GameStates.Upgrades;
        }
        private void StopPlayingGame(object sender, System.EventArgs e)
        {
            Game1.Instance.GameSave = new GameSaveData();
            //gameSave.ListGameUnits = Game1.Instance.GameDB.GetSaveGame(((Player)Game1.Instance.Player.GetComponent<Player>()));
            Game1.Instance.GameDB.SaveGameSession(((Player)Game1.Instance.Player.GetComponent<Player>()), Game1.Instance.GameSave);
            
            //Game1.Instance.Exit();
        }
    }
}
