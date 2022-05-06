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



        public void readDataDB()
        {
            string query = "INSERT INTO Test VALUES(@FirstName, @LastName)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@FirstName", "PalleMikkelsen");
                command.Parameters.AddWithValue("@LastName", "mikkelsen");
                command.ExecuteNonQuery();

            }

        }

    }
}
