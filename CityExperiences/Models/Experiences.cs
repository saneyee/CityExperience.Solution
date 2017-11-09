using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CityExperiences.Models
{
  public class Experience
  {
    private int _id;
    private int _location_id;
    private int _user_id;
    private string _title;
    private string _description;
    private string _photo_link;
    private int _price;

    public Experience(int locationId, int userId, string title, string description, string photoLink, int price, int id = 0)
    {
      _id = id;
      _location_id = locationId;
      _user_id = userId;
      _title = title;
      _description = description;
      _photo_link = photoLink;
      _price = price;
    }

    public override bool Equals(System.Object otherExperience)
    {
      if(!(otherExperience is Experience))
      {
        return false;
      }
      else
      {
        Experience newExperience = (Experience) otherExperience;
        bool idEquality = this.GetId() == newExperience.GetId();
        bool location_idEquality = this.GetLocationId() == newExperience.GetLocationId();
        bool user_idEquality = this.GetUserId() == newExperience.GetUserId();
        bool titleEquality = this.GetTitle() == newExperience.GetTitle();
        bool descriptionEquality = this.GetDescription() == newExperience.GetDescription();
        bool photo_linkEquality = this.GetPhotoLink() == newExperience.GetPhotoLink();
        bool priceEquality = this.GetPrice() == newExperience.GetPrice();
        return(idEquality && location_idEquality && user_idEquality && titleEquality && descriptionEquality && photo_linkEquality && priceEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetTitle().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public int GetLocationId()
    {
      return _location_id;
    }

    public int GetUserId()
    {
      return _user_id;
    }

    public string GetTitle()
    {
      return _title;
    }

    public string GetDescription()
    {
      return _description;
    }

    public string GetPhotoLink()
    {
      return _photo_link;
    }

    public int GetPrice()
    {
      return _price;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO experiences (location_id, user_id, title, description, photo_link, price) VALUES (@locationId, @userId, @title, @description, @photoLink, @price);";

      MySqlParameter locationId = new MySqlParameter();
      locationId.ParameterName = "@locationId";
      locationId.Value = this._location_id;
      cmd.Parameters.Add(locationId);

      MySqlParameter userId = new MySqlParameter();
      userId.ParameterName = "@userId";
      userId.Value = this._user_id;
      cmd.Parameters.Add(userId);

      MySqlParameter title = new MySqlParameter();
      title.ParameterName = "@title";
      title.Value = this._title;
      cmd.Parameters.Add(title);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter photoLink = new MySqlParameter();
      photoLink.ParameterName = "@photoLink";
      photoLink.Value = this._photo_link;
      cmd.Parameters.Add(photoLink);

      MySqlParameter price = new MySqlParameter();
      price.ParameterName = "@price";
      price.Value = this._price;
      cmd.Parameters.Add(price);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Experience Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM experiences WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int experienceId = 0;
      int experienceLocationId = 0;
      int experienceUserId = 0;
      string experienceTitle = "";
      string experienceDescription = "";
      string experiencePhotoLink = "";
      int experiencePrice = 0;

      while(rdr.Read())
      {
        experienceId = rdr.GetInt32(0);
        experienceLocationId = rdr.GetInt32(1);
        experienceUserId = rdr.GetInt32(2);
        experienceTitle = rdr.GetString(3);
        experienceDescription = rdr.GetString(4);
        experiencePhotoLink = rdr.GetString(5);
        experiencePrice = rdr.GetInt32(6);
      }

      Experience newExperience = new Experience(experienceLocationId, experienceUserId, experienceTitle, experienceDescription, experiencePhotoLink, experiencePrice, experienceId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newExperience;
    }

    public static List<Experience> GetAll()
    {
      List<Experience> allExperiences = new List<Experience> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM experiences ORDER BY id DESC LIMIT 4;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int experienceId = rdr.GetInt32(0);
        int experienceLocationId = rdr.GetInt32(1);
        int experienceUserId = rdr.GetInt32(2);
        string experienceTitle = rdr.GetString(3);
        string experienceDescription = rdr.GetString(4);
        string experiencePhotoLink = rdr.GetString(5);
        int experiencePrice = rdr.GetInt32(6);

        Experience newExperience = new Experience(experienceLocationId, experienceUserId, experienceTitle, experienceDescription, experiencePhotoLink, experiencePrice, experienceId);
        allExperiences.Add(newExperience);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExperiences;
    }

    public string GetHostName()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT name FROM users WHERE users.id = @userId;";

      MySqlParameter userId = new MySqlParameter();
      userId.ParameterName = "@userId";
      userId.Value = this._user_id;
      cmd.Parameters.Add(userId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hostName = "";

      while(rdr.Read())
      {
        hostName = rdr.GetString(0);
      }
      string name = hostName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return name;
    }

    public string GetHostPhone()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT phone FROM users WHERE users.id = @userId;";

      MySqlParameter userId = new MySqlParameter();
      userId.ParameterName = "@userId";
      userId.Value = this._user_id;
      cmd.Parameters.Add(userId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hostPhone = "";

      while(rdr.Read())
      {
        hostPhone = rdr.GetString(0);
      }
      string phone = hostPhone;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return phone;
    }

    public string GetHostEmail()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT email FROM users WHERE users.id = @userId;";

      MySqlParameter userId = new MySqlParameter();
      userId.ParameterName = "@userId";
      userId.Value = this._user_id;
      cmd.Parameters.Add(userId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hostEmail = "";

      while(rdr.Read())
      {
        hostEmail = rdr.GetString(0);
      }
      string email = hostEmail;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return email;
    }

    public string GetCityName()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT name FROM cities WHERE cities.id = @locationId;";

      MySqlParameter locationId = new MySqlParameter();
      locationId.ParameterName = "@locationId";
      locationId.Value = this._location_id;
      cmd.Parameters.Add(locationId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string cityName = "";

      while(rdr.Read())
      {
        cityName = rdr.GetString(0);
      }
      string name = cityName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return name;

    }

    public void UpdateDescription(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE experiences SET description = @newDescription WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _description = newDescription;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdatePrice(int newPrice)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE experiences SET price = @newPrice WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter price = new MySqlParameter();
      price.ParameterName = "@newPrice";
      price.Value = newPrice;
      cmd.Parameters.Add(price);

      cmd.ExecuteNonQuery();
      _price = newPrice;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdatePhoto(string newPhoto)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE experiences SET photo_link = @newPhoto WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter photo = new MySqlParameter();
      photo.ParameterName = "@newPhoto";
      photo.Value = newPhoto;
      cmd.Parameters.Add(photo);

      cmd.ExecuteNonQuery();
      _photo_link = newPhoto;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void AddTag(Tag newTag)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO experiences_tags (experience_id, tag_id) VALUES (@experienceId, @tagId);";

      MySqlParameter experienceId = new MySqlParameter();
      experienceId.ParameterName = "@experienceId";
      experienceId.Value = this._id;
      cmd.Parameters.Add(experienceId);

      MySqlParameter tagId = new MySqlParameter();
      tagId.ParameterName = "@tagId";
      tagId.Value = newTag.GetId();
      cmd.Parameters.Add(tagId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Tag> GetTags()
    {
      List<Tag> experiencesTags = new List<Tag> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT tags.* FROM experiences JOIN experiences_tags ON (experiences.id = experiences_tags.experience_id) JOIN tags ON (experiences_tags.tag_id = tags.id) WHERE experiences.id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int tagsId = rdr.GetInt32(0);
        string tagsName = rdr.GetString(1);
        Tag newTag = new Tag(tagsName, tagsId);
        experiencesTags.Add(newTag);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return experiencesTags;
    }


    public void DeleteExperience()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand("DELETE FROM experiences WHERE id = @ExperienceId; DELETE FROM bookings WHERE experience_id = @ExperienceId; DELETE FROM experiences_tags WHERE experience_id = @ExperienceId;", conn);
      MySqlParameter ExperienceIdParameter = new MySqlParameter();
      ExperienceIdParameter.ParameterName = "@ExperienceId";
      ExperienceIdParameter.Value = this.GetId();

      cmd.Parameters.Add(ExperienceIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
