using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class GameSaveData
{
        public List<GameUnit> ListGameUnits { get; set; } = new List<GameUnit>();

        public GameSaveData()
		{
           

			foreach (GameObject enemy in Game1.Instance.enemyFactory.ghostEnemies)
			{
                ListGameUnits.Add(new GameUnit("Ghost", ((Enemy)enemy.GetComponent<Enemy>()).maxHealth, (int)enemy.transform.Position.X, (int)enemy.transform.Position.Y));

            }
            foreach (GameObject enemy in Game1.Instance.enemyFactory.monEnemies)
            {
                ListGameUnits.Add(new GameUnit("Monster", ((Enemy)enemy.GetComponent<Enemy>()).maxHealth, (int)enemy.transform.Position.X, (int)enemy.transform.Position.Y));

            }
            foreach (GameObject enemy in Game1.Instance.enemyFactory.skullEnemies)
            {
                ListGameUnits.Add(new GameUnit("Skull", ((Enemy)enemy.GetComponent<Enemy>()).maxHealth, (int)enemy.transform.Position.X, (int)enemy.transform.Position.Y));
            }

            ListGameUnits.Add(new GameUnit("Player", ((Player)Game1.Instance.Player.GetComponent<Player>()).MaxHealth, (int)Game1.Instance.Player.transform.Position.X, (int)Game1.Instance.Player.transform.Position.Y));

        }

    }
}
