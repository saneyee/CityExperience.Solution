using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CityExperiences.Models
{
  public class Tag
  {
    private int _id;
    private string _tagName;


    public Tag(string tagName, int id = 0)
    {
      _id = id;
      _tagName = tagName.ToLower();
    }

    public override bool Equals(System.Object otherTag)
    {
      if (!(otherTag is Tag))
      {
        return false;
      }
      else
      {
        Tag newTag = (Tag) otherTag;
        return this.GetId().Equals(newTag.GetId());
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetTagName()
    {
      return _tagName;
    }

    public int GetId()
    {
      return _id;
    }

    public bool IsNewTag()
    {
      bool IsNewTag = true;
      List<Tag> allTags = new List<Tag> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int TagId = rdr.GetInt32(0);
        string TagName = rdr.GetString(1);
        Tag newTag = new Tag(TagName, TagId);
        allTags.Add(newTag);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      foreach (var tag in allTags)
      {
        if(tag.GetTagName() == _tagName)
        {
          IsNewTag = false;
        }
      }
      return IsNewTag;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO tags (name) VALUES (@tagName);";

      MySqlParameter tagName = new MySqlParameter();
      tagName.ParameterName = "@tagName";
      tagName.Value = this._tagName;
      cmd.Parameters.Add(tagName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Tag> GetAll()
    {
      List<Tag> allTags = new List<Tag> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int tagId = rdr.GetInt32(0);
        string tagName = rdr.GetString(1);
        Tag newTag = new Tag(tagName, tagId);
        allTags.Add(newTag);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTags;
    }

    public static Tag Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int tagId = 0;
      string tagName = "";

      while (rdr.Read())
      {
        tagId = rdr.GetInt32(0);
        tagName = rdr.GetString(1);
      }

      Tag newTag= new Tag(tagName, tagId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newTag;
    }

    public static Tag FindId(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags WHERE name = @thisName;";

      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@thisName";
      searchName.Value = name;
      cmd.Parameters.Add(searchName);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int tagId = 0;
      string tagName = "";

      while (rdr.Read())
      {
        tagId = rdr.GetInt32(0);
        tagName = rdr.GetString(1);
      }

      Tag newTag= new Tag(tagName, tagId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newTag;
    }

    public Tag FindTag()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags WHERE name = (@searchName);";

      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@searchName";
      searchName.Value = _tagName;
      cmd.Parameters.Add(searchName);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int TagId = 0;
      string TagName = "";

      while(rdr.Read())
      {
        TagId = rdr.GetInt32(0);
        TagName = rdr.GetString(1);
      }
      Tag foundTag = new Tag(TagName, TagId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundTag;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tags;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void UpdateTagName(string newTagName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE tags SET name = @newTagName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter tagName = new MySqlParameter();
      tagName.ParameterName = "@newTagName";
      tagName.Value = newTagName;
      cmd.Parameters.Add(tagName);

      cmd.ExecuteNonQuery();
      _tagName = newTagName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void DeleteTag()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tags WHERE id = @TagId; DELETE FROM experiences_tags WHERE tag_id = @TagId;";

      MySqlParameter tagIdParameter = new MySqlParameter();
      tagIdParameter.ParameterName = "@TagId";
      tagIdParameter.Value = this.GetId();
      cmd.Parameters.Add(tagIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    // public void AddExperience(Experience newExperience)
    // {
    //     MySqlConnection conn = DB.Connection();
    //     conn.Open();
    //     var cmd = conn.CreateCommand() as MySqlCommand;
    //     cmd.CommandText = @"INSERT INTO experiences_tags (experience_id, tag_id) VALUES (@ExperienceId, @TagId);";
    //
    //     MySqlParameter experience_id = new MySqlParameter();
    //     experience_id.ParameterName = "@ExperienceId";
    //     experience_id.Value = newExperience.GetId();
    //     cmd.Parameters.Add(experience_id);
    //
    //     MySqlParameter tag_id = new MySqlParameter();
    //     tag_id.ParameterName = "@TagId";
    //     tag_id.Value = _id;
    //     cmd.Parameters.Add(tag_id);
    //
    //     cmd.ExecuteNonQuery();
    //     conn.Close();
    //     if (conn != null)
    //     {
    //         conn.Dispose();
    //     }
    // }
    //
    public List<Experience> GetTagExperiences()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT experiences.* FROM tags
      JOIN experiences_tags ON (tags.id = experiences_tags.tag_id)
      JOIN experiences ON (experiences_tags.experience_id = experiences.id) WHERE tags.name = @TagName;";

      MySqlParameter tagname = new MySqlParameter();
      tagname.ParameterName = "@TagName";
      tagname.Value = _tagName;
      cmd.Parameters.Add(tagname);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Experience> experiences = new List<Experience>{};

      while(rdr.Read())
      {
        int experienceId = rdr.GetInt32(0);
        int locationId = rdr.GetInt32(1);
        int userId = rdr.GetInt32(2);
        string title = rdr.GetString(3);
        string description = rdr.GetString(4);
        string photo = rdr.GetString(5);
        int price = rdr.GetInt32(6);
        Experience newExperience = new Experience(locationId, userId, title, description, photo, price, experienceId);
        experiences.Add(newExperience);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return experiences;
    }
  }
}
