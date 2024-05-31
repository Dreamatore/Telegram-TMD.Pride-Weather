using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_TMD.Pride
{
    internal static class DataBase
    {
        const string connectionString = "Data Source=mydatabase.db;Version=3;";
        public static void CreateTable()
        {       
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            using var command = new SQLiteCommand(connection);

            command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Users (
        UserId INTEGER PRIMARY KEY,
        City TEXT             
    )";

            command.ExecuteNonQuery();
        }
        public static string ReadCity(long id)
        {
            try
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                using var command = new SQLiteCommand(connection);
                command.CommandText = $"SELECT * FROM Users WHERE UserId = {id}";
                using var reader = command.ExecuteReader();
                reader.Read();
               
                return reader.GetString(1);               
            }

            catch (Exception ex) { return ""; }

            
        }
        public static void UpdateCity(long userId, string city)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            using var command = new SQLiteCommand(connection);

            command.CommandText = @$"INSERT INTO Users(UserId,City) VALUES({userId}, '{city}')
  ON CONFLICT(UserId) DO UPDATE SET City = excluded.City;";

            command.ExecuteNonQuery();
            
          
        }
    }
}
