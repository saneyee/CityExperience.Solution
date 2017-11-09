using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CityExperiences.Models
{
  public class City
  {
    private int _id;
    private string _name;
    private string _link;

    public City(string name, string link, int Id = 0)
    {
      _name = name;
      _link = link;
      _id = Id;
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public string GetPhotoLink()
    {
      return _link;
    }

    public int GetId()
    {
      return _id;
    }

    public static City Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int cityId = 0;
      string cityName = "";
      string cityPhoto = "";

      while (rdr.Read())
      {
        cityId = rdr.GetInt32(0);
        cityName = rdr.GetString(1);
        cityPhoto = rdr.GetString(2);

      }

      City newCity= new City(cityName, cityPhoto, cityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCity;
    }

    public static City FindId(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE name = @thisName;";

      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@thisName";
      searchName.Value = name.ToLower();
      cmd.Parameters.Add(searchName);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int cityId = 0;
      string cityName = "";
      string cityPhoto = "";

      while (rdr.Read())
      {
        cityId = rdr.GetInt32(0);
        cityName = rdr.GetString(1);
        cityPhoto = rdr.GetString(2);

      }

      City newCity= new City(cityName, cityPhoto, cityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCity;
    }

    public static List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities ORDER BY id DESC LIMIT 4;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string cityName = rdr.GetString(1);
        string cityLink = rdr.GetString(2);
        City newCity = new City(cityName, cityLink, cityId);
        allCities.Add(newCity);
      }
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }

      return allCities;
    }

    public List<Experience> GetCityExperiences()
    {
      List<Experience> allExperiences = new List<Experience> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT experiences.* FROM cities JOIN experiences ON (cities.id = experiences.location_id) WHERE cities.name = @searchName;";

      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@searchName";
      searchName.Value = this._name;
      cmd.Parameters.Add(searchName);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int experiencesId = 0;
      int experiencesLocationId = 0;
      int experiencesUserId = 0;
      string experiencesTitle = "";
      string experiencesDescription = "";
      string experiencesPhotoLink = "";
      int experiencesPrice = 0;

      while(rdr.Read())
      {
        experiencesId = rdr.GetInt32(0);
        experiencesLocationId = rdr.GetInt32(1);
        experiencesUserId = rdr.GetInt32(2);
        experiencesTitle = rdr.GetString(3);
        experiencesDescription = rdr.GetString(4);
        experiencesPhotoLink = rdr.GetString(5);
        experiencesPrice = rdr.GetInt32(6);

        Experience newExperiences = new Experience(experiencesLocationId, experiencesUserId, experiencesTitle, experiencesDescription, experiencesPhotoLink, experiencesPrice,  experiencesId);
        allExperiences.Add(newExperiences);

      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExperiences;
    }

    public List<Experience> GetCityHosts()
    {
      List<Experience> allExperiences = new List<Experience> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT experiences.* FROM cities JOIN experiences ON (host.id = experiences.user_id) WHERE host.id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this._id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int experiencesId = 0;
      int experiencesLocationId = 0;
      int experiencesUserId = 0;
      string experiencesTitle = "";
      string experiencesDescription = "";
      string experiencesPhotoLink = "";
      int experiencesPrice = 0;



      while(rdr.Read())
      {
        experiencesId = rdr.GetInt32(0);
        experiencesLocationId = rdr.GetInt32(1);
        experiencesUserId = rdr.GetInt32(2);
        experiencesTitle = rdr.GetString(3);
        experiencesDescription = rdr.GetString(4);
        experiencesPhotoLink = rdr.GetString(5);
        experiencesPrice = rdr.GetInt32(6);

        Experience newExperiences = new Experience(experiencesLocationId, experiencesUserId, experiencesTitle, experiencesDescription, experiencesPhotoLink, experiencesPrice,  experiencesId);
        allExperiences.Add(newExperiences);

      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExperiences;
    }

    public void DeleteCity(Experience newExperience)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE name SET name -= 1 where bookId = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = newExperience.GetId();
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
