
using System;
using MySql.Data.MySqlClient;
using CityExperiences;

namespace CityExperiences.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
