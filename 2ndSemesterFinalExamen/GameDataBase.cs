using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace _2ndSemesterFinalExamen
{

    class GameDataBase
    {
        SqlConnection connection;
        string connectionString;

        public virtual void Initialize()
        {
			// TODO: Add your initialization logic here
			//string dbString = ConfigurationManager.ConnectionStrings["GameDB.Properties.Settings.GameDBConnectionString"].ConnectionString;
			string dbString = "RGF0YSBTb3VyY2U9MzQuODkuMTcyLjE1MztEYXRhYmFzZT1KYWNrVGhlR2hvc3RIdW50ZXI7VXNlcj1zcWxzZXJ2ZXI7UGFzc3dvcmQ9dGVzdDEyMzQ=";
			string dbConnection = DecodeAPI.DecodeBase64(dbString);
			connectionString = dbConnection;

        }
       


        /// <summary>
        /// Adds Player information to DB with Unique ID, Player Tag + current points and level
        /// </summary>
        /// <param name="p">Player p</param>
        public bool AddPlayer(Player p)
		{
            bool cleared = true;
            
                string query = "INSERT INTO Player VALUES(@Tag, @Points, @CurrentLevel);";
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    command.Parameters.AddWithValue("@Tag", p.Tag);
                    command.Parameters.AddWithValue("@Points", p.Points);
                    command.Parameters.AddWithValue("@CurrentLevel", p.CurrentLevel);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    cleared = false;
                }
                if (cleared)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                }

            


        }

        /// <summary>
        /// Updates Players Points current Level by searching DB for current player Tag which is Unique in DB
        /// </summary>
        /// <param name="p">Player p</param>
        public void UpdatePlayer(Player p)
		{
            string query = "UPDATE Player SET Point = @Points, Level = @CurrentLevel  WHERE Tag LIKE @Tag;";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", p.Tag);
                command.Parameters.AddWithValue("@Points", p.Points);
                command.Parameters.AddWithValue("@CurrentLevel", p.CurrentLevel);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


		/// <summary>
		/// Gets Player Data and adds to current CLASS of player 
		/// to be able to LOAD an old save
		/// </summary>
		/// <param name="p"></param>
		public bool GetPlayer(Player p)
		{
			bool passedFailed = false;

			string query = $"SELECT * FROM Player WHERE Tag = '{p.Tag}'";
			using (connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				connection.Open();
				var readTalentData = command.ExecuteReader();

				while (readTalentData.Read())
				{
					p.Tag = readTalentData.GetString(1);
					p.Points = readTalentData.GetInt32(2);
					p.CurrentLevel = readTalentData.GetInt16(3);
					passedFailed = readTalentData.HasRows;
				}
			}
			return passedFailed;


		}


		/// <summary>
		/// On player Creation it instaciates a techTree for that specific Player in DB where all talents = lvl 0
		/// </summary>
		/// <param name="p">Player p</param>
		public void AddPlayerTalentTree(Player p)
		{

            List<int> TalentTreeID = new List<int>();

            string query = $"SELECT ID FROM TalentTree";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                var readTalentData = command.ExecuteReader();
				while (readTalentData.Read())
				{
					TalentTreeID.Add(readTalentData.GetInt32(0));
				}
			}


            List<string> Querys = new List<string>();
            query = "INSERT INTO PlayerTalentTree VALUES";
            int itemCounter = 0;
            foreach (int tID in TalentTreeID)
			{
                itemCounter++;
                if (TalentTreeID.Count > itemCounter)
				{
                    query += $"((SELECT ID FROM Player WHERE Tag LIKE @Tag), {tID}, 0),";
                }else
				{
                    query += $"((SELECT ID FROM Player WHERE Tag LIKE @Tag), {tID}, 0)";
                }
                
                
			}
            query += ";";
           
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
               
                command.Parameters.AddWithValue("@Tag", p.Tag);
                connection.Open();
                command.ExecuteNonQuery();
                
            }
        }
        public void UpdateTalent(Player p, Talent t)
		{
            string query = "UPDATE PlayerTalentTree SET  current_level = @TalentLevel  WHERE Player_id = (SELECT ID FROM Player WHERE Tag LIKE @Tag) AND talent_tree_id = (SELECT ID FROM TalentTree WHERE Tag LIKE @TalentTag);";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", p.Tag);
                command.Parameters.AddWithValue("@TalentTag", t.Tag);
                command.Parameters.AddWithValue("@TalentLevel", t.CurrentLevel);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Creates a List<Talent> where we create a new row with Tag Max level and current level from DB WHERE Player ID = current player TAG
        /// </summary>
        /// <param name="p">Player p</param>
        /// <returns></returns>
        public List<Talent> GetTalents(Player p)
		{
            List<Talent> listTalent = new List<Talent>();
            string query = $"SELECT A.Tag, A.Max_level, B.Current_level, A.description FROM TalentTree A INNER JOIN PlayerTalentTree B ON (A.ID = B.Talent_tree_id) WHERE B.player_id = (SELECT ID FROM Player WHERE Tag LIKE @Tag)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                
                command.Parameters.AddWithValue("@Tag", p.Tag);
                connection.Open();
                var readTalentData = command.ExecuteReader();
                while (readTalentData.Read())
                {
                    listTalent.Add(new Talent(readTalentData.GetString(0), readTalentData.GetInt16(1), readTalentData.GetInt16(2), readTalentData.GetString(3)));
                }
            }
            return listTalent;
		}

        /// <summary>
        /// Creates a List<TalentEdges> To populate edges between talents
        /// </summary>
        /// <param name="p"></param>
        /// <returns>ListOfTalentConnections</returns>
        public List<TalentEdges> GetTalentConnections()
        {
            List<TalentEdges> listEdges = new List<TalentEdges>();
            string query = $"SELECT * FROM TalentConnections";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                connection.Open();
                var readTalentData = command.ExecuteReader();
                while (readTalentData.Read())
                {
                    listEdges.Add(new TalentEdges(readTalentData.GetString(1), readTalentData.GetString(2)));
                }
            }
            return listEdges;
        }

        /// <summary>
        /// Deletes everything from SaveData Table in DB when Save Game is pressed for the current player active
        /// </summary>
        /// <param name="p"></param>
        public void InstanciateGameSession(Player p)
		{

            
            string query = "DELETE FROM SaveData  WHERE player_id = (SELECT ID FROM Player WHERE Tag LIKE @Tag);";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", p.Tag);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Updates Player Data 
        /// Clears DB SaveData table for this player searching for player Tag which is UNIQUE
        /// Querys DB with new list of all enemies and their current position inkluding player
        /// </summary>
        /// <param name="p"></param>
        /// <param name="GameSave"></param>
        public void SaveGameSession(Player p, GameSaveData GameSave)
		{
            UpdatePlayer(p);
            InstanciateGameSession(p);
            int itemCounter = 0;
            string query = "INSERT INTO SaveData VALUES";
            foreach (GameUnit g in GameSave.ListGameUnits)
			{
                itemCounter++;
                if (GameSave.ListGameUnits.Count > itemCounter)
                {
                    query += $"((SELECT ID FROM Player WHERE Tag = @Tag),'{g.Tag}', {g.Health},{g.PosX},{g.PosY}),";
                   
                }
                else
                {
                    query += $"((SELECT ID FROM Player WHERE Tag = @Tag),'{g.Tag}', {g.Health},{g.PosX},{g.PosY})";
                }
            }
            query += ";";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", p.Tag);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    
                }
               
            }

        }

        /// <summary>
        /// Gets DB data for last save for this game under this unique player Tag
        /// </summary>
        /// <param name="p"></param>
        /// <returns>List<GameUnit> Keeps track of player and all mosters last position and hp and which type it was</returns>
        public List<GameUnit> GetSaveGame(Player p)
		{
            List<GameUnit> unitList = new List<GameUnit>();
            string query = $"SELECT Tag, Health, x, y FROM SaveData WHERE player_id = (SELECT ID FROM Player WHERE Tag LIKE @Tag)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", p.Tag);

                connection.Open();
                var readGameData = command.ExecuteReader();

                while (readGameData.Read())
                {
                    unitList.Add(new GameUnit(readGameData.GetString(0), readGameData.GetInt16(1), readGameData.GetInt16(2), readGameData.GetInt16(3)));
                }
            }
            return unitList;
        }

    }
}
