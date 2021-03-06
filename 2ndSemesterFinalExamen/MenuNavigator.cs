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
        public List<Component> preGameComponents, loadGameComponents, newGameComponents, inGameComponents, menuGameComponents, pauseGameComponents, upgradeGameComponents, gameOverComponents, nextLevelComponents;
        private InputComponent userTag;
        private InputComponent newUserTag;
        public GameStates currentGS = GameStates.PreGame;
        private GameStates previousGS;
        private bool failedOrPassed = true;
        private TextComponent IncorrectData, GameOverText, GameOverTextUnder;
        private Buttoncomponent quitButton;

        private TalentTree talentTree;
        private bool madeTalent = false;


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

            GameOverText = new TextComponent(Game1.Instance.gameOverTexture, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -100),
                Text = ""

            };
            GameOverTextUnder = new TextComponent(Game1.Instance.gameOverButtonTexture, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 150),
                Text = ""

            };
            backButton.Click += PreviousGameState;

            quitButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 90),
                Text = "Quit Game"
            };
            quitButton.Click += QuitPlayGame;

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
                Text = "Resume Game"
            };
            var upgradesButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -30),
                Text = "Talents",
            };

            var saveButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, 30),
                Text = "Save Game"
            };

            resumeGame.Click += ResumePlayingGame;
            upgradesButton.Click += GoToUpgrades;
            saveButton.Click += SaveTheGame;

            menuGameComponents = new List<Component>()
            {
                resumeGame,
                upgradesButton,
                saveButton,
                quitButton,

            };
            //MENU - GAME 

            //INGAME
            var pointCounter = new TextComponent(Game1.Instance.point, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(500, -300),
                Text = "string",

            };

            inGameComponents = new List<Component>()
            {
                pointCounter,
            };


            //GAMEOVER - GAME 
            gameOverComponents = new List<Component>()
            {
                GameOverText,
                GameOverTextUnder,
                //quitButton,

            };
            //GAMEOVER - GAME 


            //NEXT LEVEL - GAME 
            var nextLevelButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
            {
                PosPlayer = new Vector2(0, -90),
                Text = "Next Level"
            };

            nextLevelButton.Click += ResumePlayingGame;

            nextLevelComponents = new List<Component>()
            {
                nextLevelButton,
                upgradesButton,
                saveButton,
                quitButton,

            };
            //NEXTLEVEL - GAME 

        }


        public void Update(GameTime gameTime)
        {
            if (Game1.Instance.talenTreeCreated && !madeTalent)
            {
                var u = 20;

                var shot = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.shoot, new Vector2(0, -250 + u), new Vector2(-90, -150 + u), new Vector2(0, -150 + u), new Vector2(90, -150 + u), TalentTree.Instance.graph.Talents[9])
                { };
                var fasterShots = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.fasterShots, new Vector2(-90, -150 + u), new Vector2(-160, -50 + u), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[6])
                { };
                var quickReload = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.quickReload, new Vector2(0, -150 + u), new Vector2(-50, -50 + u), new Vector2(50, -50 + u), new Vector2(0, 0), TalentTree.Instance.graph.Talents[8])
                { };
                var speed = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.speed, new Vector2(90, -150 + u), new Vector2(150, -50 + u), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[10])
                { };
                var sprayShot = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.sprayShot, new Vector2(-160, -50 + u), new Vector2(-120, 50 + u), new Vector2(-220, 50 + u), new Vector2(0, 0), TalentTree.Instance.graph.Talents[11])
                { };
                var biggerProjectilles = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.biggerProjectiles, new Vector2(-50, -50 + u), new Vector2(0, 50 + u), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[0])
                { };
                var piercingShot = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.piercingShot, new Vector2(50, -50 + u), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[7])
                { };
                var Dash = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.dash, new Vector2(150, -50 + u), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[3])
                { };
                var BulletShield = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.bulletShield, new Vector2(-120, 50 + u), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[1])
                { };
                var ExplosiveShots = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.explosiveShot, new Vector2(0, 50 + u), new Vector2(0, 150 + u), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[5])
                { };
                var Explode = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.explode, new Vector2(0, 150 + u), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[4])
                { };
                var Damage = new DescriptButton(Game1.Instance.talentBox, Game1.Instance.talentName, Game1.Instance.gameFont, Game1.Instance.damage, new Vector2(-220, 50 + u), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), TalentTree.Instance.graph.Talents[2])
                { };

                var backButton = new Buttoncomponent(Game1.Instance.buttonText, Game1.Instance.gameFont)
                {
                    PosPlayer = new Vector2(-525, -325),
                    Text = "Back"
                };


                upgradeGameComponents = new List<Component>()
            {
                shot,
                fasterShots,
                quickReload,
                speed,
                sprayShot,
                biggerProjectilles,
                piercingShot,
                Dash,
                BulletShield,
                ExplosiveShots,
                Explode,
                Damage,
                backButton,

            };
                fasterShots.Click += TalentClick;
                quickReload.Click += TalentClick;
                speed.Click += TalentClick;
                sprayShot.Click += TalentClick;
                biggerProjectilles.Click += TalentClick;
                piercingShot.Click += TalentClick;
                Dash.Click += TalentClick;
                BulletShield.Click += TalentClick;
                ExplosiveShots.Click += TalentClick;
                Explode.Click += TalentClick;
                Damage.Click += TalentClick;

                backButton.Click += PreviousGameState;

                madeTalent = true;
            }

            if (Game1.Instance.gameState != currentGS)
            {
                Game1.Instance.gameState = currentGS;
                if(Game1.Instance.gameState != GameStates.PreGame && Game1.Instance.gameState != GameStates.LoadGame  && Game1.Instance.gameState != GameStates.NewGame)
				{
                    Game1.Instance.GameDB.UpdatePlayer(((Player)Game1.Instance.Player.GetComponent<Player>()));
                }
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

                    if (talentTree == null)
                    {
                        talentTree = TalentTree.Instance;
                        TalentTree.Instance.GraphFill();

                        TalentTree.Instance.graph.Talents[9].Locked = false;
                    }


                    quitButton.PosPlayer = new Vector2(0, 90);
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
                    //GameOverText.PosPlayer = new Vector2(0, -45);
                    //quitButton.PosPlayer = new Vector2(-50, 45);
                    foreach (var component in gameOverComponents)
                    {
                        component.Update(gameTime);
                    }

                    break;
                case GameStates.NextLevel:
                    if (((Player)Game1.Instance.Player.GetComponent<Player>()).CurrentLevel != Game1.Instance.currentLevel)
                    {
                        Game1.Instance.currentLevel = ((Player)Game1.Instance.Player.GetComponent<Player>()).CurrentLevel;
                    }
                    foreach (var component in nextLevelComponents)
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
                    foreach (var component in gameOverComponents)
                    {
                        component.Draw(_spriteBatch);
                    }
                    break;
                case GameStates.NextLevel:
                    foreach (var component in nextLevelComponents)
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
                Game1.Instance.currentLevel = ((Player)Game1.Instance.Player.GetComponent<Player>()).CurrentLevel;
                Game1.Instance.enemyFactory.SetFactoryStats();
				
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
            previousGS = currentGS;
            currentGS = GameStates.Upgrades;
        }
        private void SaveTheGame(object sender, System.EventArgs e)
        {
            Game1.Instance.GameSave = new GameSaveData();
            //gameSave.ListGameUnits = Game1.Instance.GameDB.GetSaveGame(((Player)Game1.Instance.Player.GetComponent<Player>()));
            Game1.Instance.GameDB.SaveGameSession(((Player)Game1.Instance.Player.GetComponent<Player>()), Game1.Instance.GameSave);
            

            //Game1.Instance.Exit();
        }
        private void QuitPlayGame(object sender, System.EventArgs e)
        {

            currentGS = GameStates.PreGame;
            talentTree = null;
            Game1.Instance.talenTreeCreated = false;
            madeTalent = false;

            ((Player)Game1.Instance.Player.GetComponent<Player>()).Abilities.RemoveRange(0, ((Player)Game1.Instance.Player.GetComponent<Player>()).Abilities.Count);
             
            //Game1.Instance.Exit();
        }

        //talent events


        private void TalentClick(object sender, System.EventArgs e)
        {
            int index = TalentTree.Instance.graph.Talents.IndexOf((sender as Buttoncomponent).thisTalent);

                if (TalentTree.Instance.graph.Talents[index].CurrentLevel < TalentTree.Instance.graph.Talents[index].MaxLevel && TalentTree.Instance.graph.Talents[index].Locked == false && Game1.Instance.points >= 10)
                {
                if(((Player)Game1.Instance.Player.GetComponent<Player>()).Points >= 10)
				{
                    ((Player)Game1.Instance.Player.GetComponent<Player>()).Points -= 10;
                    TalentTree.Instance.graph.Talents[index].CurrentLevel += 1;
                    Game1.Instance.GameDB.UpdateTalent(((Player)Game1.Instance.Player.GetComponent<Player>()), TalentTree.Instance.graph.Talents[index]);
                    Game1.Instance.GameDB.UpdatePlayer(((Player)Game1.Instance.Player.GetComponent<Player>()));
                    ((Player)Game1.Instance.Player.GetComponent<Player>()).Abilities.Add(new Ability(TalentTree.Instance.graph.Talents[index].Tag, TalentTree.Instance.graph.Talents[index].CurrentLevel));
                }
                   
                }

            if (TalentTree.Instance.graph.Talents[index].Locked == true && Game1.Instance.points >= 10)
            {

                if (TalentTree.Instance.DFS(TalentTree.Instance.graph.Talents[9], TalentTree.Instance.graph.Talents[index]))
                {
                    if (((Player)Game1.Instance.Player.GetComponent<Player>()).Points >= 10)
                    {
                        TalentTree.Instance.graph.Talents[index].Locked = false;
                        ((Player)Game1.Instance.Player.GetComponent<Player>()).Points -= 10;
                        TalentTree.Instance.graph.Talents[index].CurrentLevel += 1;
                        Game1.Instance.GameDB.UpdateTalent(((Player)Game1.Instance.Player.GetComponent<Player>()), TalentTree.Instance.graph.Talents[index]);
                        Game1.Instance.GameDB.UpdatePlayer(((Player)Game1.Instance.Player.GetComponent<Player>()));
                        ((Player)Game1.Instance.Player.GetComponent<Player>()).Abilities.Add(new Ability(TalentTree.Instance.graph.Talents[index].Tag, TalentTree.Instance.graph.Talents[index].CurrentLevel));
                    }
                }
            }


        }


    }
}
