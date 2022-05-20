﻿using System;
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
        private InputComponent userTag;
        private GameStates currentGS = GameStates.PreGame;
        private GameStates previousGS;
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
                PosPlayer = new Vector2(-550, -300),
                Text = "Back"
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

            //LOAD - GAME 
            var userTagInfo = new TextComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(-225, -30),
                Text = "User Tag:"
            };
             userTag = new InputComponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -30),
                startText = "...",
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
                PosPlayer = new Vector2(0, -60),
                Text = "ResumeGame"
            };
            var upgradesButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 0),
                Text = "Talents",
            };

            var quitButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 60),
                Text = "Quit Game"
            };

            resumeGame.Click += ResumePlayingGame;
            upgradesButton.Click += GoToUpgrades;
            quitButton.Click += StopPlayingGame;
            menuGameComponents = new List<Component>()
            {
                resumeGame,
                upgradesButton,
                quitButton,
                backButton,
            };
            //MENU - GAME 

            //UPGRADE

            var shot = new DescriptButton(Game1.Instance.buttonText, Game1.Instance.gameFont, Game1.Instance.shoot, "shoot", new Vector2(-50,-50) )
            {
              
            };

            inGameComponents = new List<Component>()
            {
                shot,
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

        private void PreviousGameState(object sender, System.EventArgs e)
        {
            currentGS = previousGS;
        }
        private void LogInStateChange(object sender, System.EventArgs e)
        {
            previousGS = currentGS;
            currentGS = GameStates.LoadGame;
        }
        private void CreateNewGame(object sender, System.EventArgs e)
        {
            previousGS = Game1.Instance.gameState;
            currentGS = GameStates.NewGame;
        }

        private void LogInToChar(object sender, System.EventArgs e)
        {
            ((Player)Game1.Instance.Player.GetComponent<Player>()).Tag = userTag.CurrentValue;
            bool failedOrPassed = Game1.Instance.GameDB.GetPlayer(((Player)Game1.Instance.Player.GetComponent<Player>()));
            if (failedOrPassed)
            {
                currentGS = GameStates.Menu;
            }
            else
            {
                currentGS = GameStates.PreGame;
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
            GameSaveData gameSave = new GameSaveData();
            gameSave.ListGameUnits = Game1.Instance.GameDB.GetSaveGame(((Player)Game1.Instance.Player.GetComponent<Player>()));
            Game1.Instance.GameDB.SaveGameSession(((Player)Game1.Instance.Player.GetComponent<Player>()), gameSave);
            
            //Game1.Instance.Exit();
        }
    }
}
