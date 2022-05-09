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
			connectionString = ConfigurationManager.ConnectionStrings["GameDB.Properties.Settings.GameDBConnectionString"].ConnectionString;

		}

        //public void CreateTableDB()
        //{
        //    string query = "CREATE TABLE IF NOT EXISTS TestTwo (ID INTEGER PRIMARY KEY, Name NVARCHAR(50));";
        //    using (connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        connection.Open();
        //        //command.Parameters.AddWithValue("@FirstName", "LKFDJKGLDF");
        //        //command.Parameters.AddWithValue("@LastName", "DSFGSDF");
        //        command.ExecuteNonQuery();

        //    }

        //}

        //public void InsertDB()
        //{
        //    string query = "INSERT INTO TestThree VALUES(@Name)";
        //    using (connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@Name", "LKFDJKGLDF");
        //        command.ExecuteNonQuery();

        //    }

        //}

    }
}
