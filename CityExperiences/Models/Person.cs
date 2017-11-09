using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CityExperiences.Models
{
  public class Person
  {
    private int _id;
    private string _name;
    private string _dateOfBirth;
    private string _country;
    private string _email;
    private string _password;
    private string _phone;

    public Person(string name, string dateOfBirth, string country, string email, string phone, string password,  int id = 0)
    {
      _id = id;
      _name = name;
      _dateOfBirth = dateOfBirth;
      _country = country;
      _email = email;
      _password = password;
      _phone = phone;
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDateOfBirth()
    {
      return _dateOfBirth;
    }

    public string GetCountry()
    {
      return _country;
    }

    public string GetEmail()
    {
      return _email;
    }

    public string GetPassword()
    {
      return _password;
    }

    public string GetPhone()
    {
      return _phone;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO users (name, date_of_birth, country, email, phone, password) VALUES (@name, @dateOfBirth, @country, @email, @phone, @password);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter dateOfBirth = new MySqlParameter();
      dateOfBirth.ParameterName = "@dateOfBirth";
      dateOfBirth.Value = this._dateOfBirth;
      cmd.Parameters.Add(dateOfBirth);

      MySqlParameter country = new MySqlParameter();
      country.ParameterName = "@country";
      country.Value = this._country;
      cmd.Parameters.Add(country);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@email";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      MySqlParameter password = new MySqlParameter();
      password.ParameterName = "@password";
      password.Value = this._password;
      cmd.Parameters.Add(password);

      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@phone";
      phone.Value = this._phone;
      cmd.Parameters.Add(phone);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Person> GetAll()
    {
      List<Person> allPersons = new List<Person> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM users;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int userId = rdr.GetInt32(0);
        string userName = rdr.GetString(1);
        string userDateOfBirth = rdr.GetString(2);
        string userCountry = rdr.GetString(3);
        string userEmail = rdr.GetString(4);
        string userPassword = rdr.GetString(6);
        string userPhone = rdr.GetString(5);

        Person newPerson = new Person(userName, userDateOfBirth, userCountry, userEmail,  userPhone, userPassword, userId);
        allPersons.Add(newPerson);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPersons;
    }


    public static Person Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM users WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int userId = 0;
      string userName = "";
      string userDateOfBirth = "";
      string userCountry = "";
      string userEmail = "";
      string userPassword = "";
      string userPhone = "";

      while(rdr.Read())
      {
        userId = rdr.GetInt32(0);
        userName = rdr.GetString(1);
        userDateOfBirth = rdr.GetString(2);
        userCountry = rdr.GetString(3);
        userEmail = rdr.GetString(4);
        userPassword = rdr.GetString(6);
        userPhone = rdr.GetString(5);
      }
      Person newPerson = new Person(userName, userDateOfBirth, userCountry, userEmail,  userPhone, userPassword, userId);
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return newPerson;
    }

    public List<Experience> GetPersonListings()
    {
      List<Experience> allPersonListings = new List<Experience> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT experiences.* FROM users
      JOIN experiences ON (users.id = experiences.user_id) WHERE users.id = @SearchId;";

      MySqlParameter SearchId = new MySqlParameter();
      SearchId.ParameterName = "@SearchId";
      SearchId.Value = this._id;
      cmd.Parameters.Add(SearchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;


      while(rdr.Read())
      {
        int experienceId = rdr.GetInt32(0);
        int experienceLocationId = rdr.GetInt32(1);
        int experiencePersonId = rdr.GetInt32(2);
        string experienceTitle = rdr.GetString(3);
        string experienceDescription = rdr.GetString(4);
        string experiencsPhotoLink = rdr.GetString(5);
        int experiencePrice = rdr.GetInt32(6);

        Experience newExperience = new Experience(experienceLocationId, experiencePersonId, experienceTitle, experienceDescription, experiencsPhotoLink, experiencePrice, experienceId);
        allPersonListings.Add(newExperience);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPersonListings;
    }

    public List<Experience> GetPersonBookings()
    {
      List<Experience> allPersonBookings = new List<Experience> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT experiences.* FROM users JOIN bookings ON (users.id = bookings.user_id) JOIN experiences ON (bookings.experience_id = experiences.id) WHERE users.id = @searchId;";

      MySqlParameter SearchId = new MySqlParameter();
      SearchId.ParameterName = "@searchId";
      SearchId.Value = this._id;
      cmd.Parameters.Add(SearchId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int experienceId = rdr.GetInt32(0);
        int experienceLocationId = rdr.GetInt32(1);
        int experiencePersonId = rdr.GetInt32(2);
        string experienceTitle = rdr.GetString(3);
        string experienceDescription = rdr.GetString(4);
        string experiencsPhotoLink = rdr.GetString(5);
        int experiencePrice = rdr.GetInt32(6);

        Experience newExperience = new Experience(experienceLocationId, experiencePersonId, experienceTitle, experienceDescription, experiencsPhotoLink, experiencePrice, experienceId);
        allPersonBookings.Add(newExperience);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPersonBookings;
    }

    //Experiences and bookings

    public void DeletePerson()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"DELETE FROM users WHERE id = @userId; DELETE FROM experiences WHERE id = @userId; DELETE FROM bookings WHERE id = @userId;";


      MySqlParameter PersonIdParameter = new MySqlParameter();
      PersonIdParameter.ParameterName = "@userId";
      PersonIdParameter.Value = this.GetId();

      cmd.Parameters.Add(PersonIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

  }
}
