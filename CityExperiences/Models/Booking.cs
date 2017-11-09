using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CityExperiences.Models
{
  public class Booking
  {
    private int _id;
    private int _userId;
    private int _experienceId;

    public Booking(int userId, int experienceId, int id = 0)
    {
      _id = id;
      _userId = userId;
      _experienceId = experienceId;
    }

    public override bool Equals(System.Object otherBooking)
    {
      if (!(otherBooking is Booking))
      {
        return false;
      }
      else
      {
        Booking newBooking = (Booking) otherBooking;
        return this.GetId().Equals(newBooking.GetId());
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public int GetUserId()
    {
      return _userId;
    }

    public int GetExperienceId()
    {
      return _experienceId;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bookings (user_id, experience_id) VALUES (@UserId, @ExperienceId);";

      MySqlParameter UserId = new MySqlParameter();
      UserId.ParameterName = "@UserId";
      UserId.Value = this._userId;
      cmd.Parameters.Add(UserId);

      MySqlParameter ExperienceId = new MySqlParameter();
      ExperienceId.ParameterName = "@ExperienceId";
      ExperienceId.Value = this._experienceId;
      cmd.Parameters.Add(ExperienceId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public string GetGuestName()
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT name FROM users WHERE users.id = @UserId;";

      MySqlParameter userId = new MySqlParameter();
      userId.ParameterName = "@Userid";
      userId.Value = this._userId;
      cmd.Parameters.Add(userId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string guestName = "";

      while(rdr.Read())
      {
        guestName = rdr.GetString(0);
      }
      string gname = guestName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return gname;
    }

    public string GetCity()
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT cities.name FROM bookings
      JOIN experiences ON (bookings.experience_id = experiences.id)
      JOIN cities ON (experiences.location_id = cities.id) WHERE bookings.id = @BookingId;";


      MySqlParameter bookingId = new MySqlParameter();
      bookingId.ParameterName = "@BookingId";
      bookingId.Value = this._id;
      cmd.Parameters.Add(bookingId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string cityName = "";

      while(rdr.Read())
      {
        cityName = rdr.GetString(1);
      }
      string cname = cityName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return cname;
    }

    public string GetExperienceTitle()
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT title FROM experiences WHERE experiences.id = @ExperienceId;";

      MySqlParameter experienceId = new MySqlParameter();
      experienceId.ParameterName = "@ExperienceId";
      experienceId.Value = this._experienceId;
      cmd.Parameters.Add(experienceId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string guestExperience = "";

      while(rdr.Read())
      {
        guestExperience = rdr.GetString(0);
      }
      string gexperience = guestExperience;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return gexperience;
    }

    public string GetHostName()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT users.name FROM bookings
      JOIN experiences ON (bookings.experience_id = experiences.id)
      JOIN users ON (experiences.user_id = users.id) WHERE bookings.id = @BookingId;";


      MySqlParameter bookingId = new MySqlParameter();
      bookingId.ParameterName = "@BookingId";
      bookingId.Value = this._id;
      cmd.Parameters.Add(bookingId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hostName = "";

      while(rdr.Read())
      {
        hostName = rdr.GetString(0);
      }
      string hostname = hostName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return hostname;
    }
    public string GetHostEmail()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT users.email FROM bookings
      JOIN experiences ON (bookings.experience_id = experiences.id)
      JOIN users ON (experiences.user_id = users.id) WHERE bookings.id = @BookingId;";


      MySqlParameter bookingId = new MySqlParameter();
      bookingId.ParameterName = "@BookingId";
      bookingId.Value = this._id;
      cmd.Parameters.Add(bookingId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hostEmail = "";

      while(rdr.Read())
      {
        hostEmail = rdr.GetString(4);
      }
      string hostemail = hostEmail;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return hostemail;
    }
    public string GetHostPhone()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT users.phone FROM bookings
      JOIN experiences ON (bookings.experience_id = experiences.id)
      JOIN users ON (experiences.user_id = users.id) WHERE bookings.id = @BookingId;";


      MySqlParameter bookingId = new MySqlParameter();
      bookingId.ParameterName = "@BookingId";
      bookingId.Value = this._id;
      cmd.Parameters.Add(bookingId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string hostPhone = "";

      while(rdr.Read())
      {
        hostPhone = rdr.GetString(5);
      }
      string hostphone = hostPhone;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return hostphone;
    }

    public static List<Booking> GetAll()
    {
      List<Booking> allBookings = new List<Booking> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bookings;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bookingId = rdr.GetInt32(0);
        int userId = rdr.GetInt32(1);
        int experienceId = rdr.GetInt32(2);

        Booking newBooking = new Booking(userId, experienceId, bookingId);
        allBookings.Add(newBooking);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBookings;
    }

    public static Booking Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bookings WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int bookingId = 0;
      int userId = 0;
      int experienceId = 0;

      while (rdr.Read())
      {
         bookingId = rdr.GetInt32(0);
         userId = rdr.GetInt32(1);
         experienceId = rdr.GetInt32(2);
      }

      Booking newBooking= new Booking(userId, experienceId, bookingId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newBooking;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bookings;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //
    // public void UpdateBookingName(string newBookingName)
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"UPDATE tags SET name = @newBookingName WHERE id = @searchId;";
    //
    //   MySqlParameter searchId = new MySqlParameter();
    //   searchId.ParameterName = "@searchId";
    //   searchId.Value = _id;
    //   cmd.Parameters.Add(searchId);
    //
    //   MySqlParameter tagName = new MySqlParameter();
    //   tagName.ParameterName = "@newBookingName";
    //   tagName.Value = newBookingName;
    //   cmd.Parameters.Add(tagName);
    //
    //   cmd.ExecuteNonQuery();
    //   _tagName = newBookingName;
    //
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
    //
    public void DeleteBooking()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bookings WHERE id = @BookingId;";

      MySqlParameter bookingIdParameter = new MySqlParameter();
      bookingIdParameter.ParameterName = "@BookingId";
      bookingIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bookingIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
    //
    // // public void AddExperience(Experience newExperience)
    // // {
    // //     MySqlConnection conn = DB.Connection();
    // //     conn.Open();
    // //     var cmd = conn.CreateCommand() as MySqlCommand;
    // //     cmd.CommandText = @"INSERT INTO experiences_tags (experience_id, tag_id) VALUES (@ExperienceId, @BookingId);";
    // //
    // //     MySqlParameter experience_id = new MySqlParameter();
    // //     experience_id.ParameterName = "@ExperienceId";
    // //     experience_id.Value = newExperience.GetId();
    // //     cmd.Parameters.Add(experience_id);
    // //
    // //     MySqlParameter tag_id = new MySqlParameter();
    // //     tag_id.ParameterName = "@BookingId";
    // //     tag_id.Value = _id;
    // //     cmd.Parameters.Add(tag_id);
    // //
    // //     cmd.ExecuteNonQuery();
    // //     conn.Close();
    // //     if (conn != null)
    // //     {
    // //         conn.Dispose();
    // //     }
    // // }
    // //
    // public List<Experience> GetBookingExperiences()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT experiences.title FROM experiences WHERE experiences.id = @ExperienceId;";
    //
    //   MySqlParameter experienceId = new MySqlParameter();
    //   experienceId.ParameterName = "@ExperienceId";
    //   experienceId.Value = _experienceId;
    //   cmd.Parameters.Add(experienceId);
    //
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   List<Experience> experiences = new List<Experience>{};
    //
    //   while(rdr.Read())
    //   {
    //     int experienceId = rdr.GetInt32(0);
    //     int locationId = rdr.GetInt32(1);
    //     int experienceId = rdr.GetInt32(2);
    //     string title = rdr.GetString(3);
    //     string description = rdr.GetString(4);
    //     string photo = rdr.GetString(5);
    //     int price = rdr.GetInt32(6);
    //     Experience newExperience = new Experience(locationId, userId, title, description, photo, price, experienceId);
    //     experiences.Add(newExperience);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return experiences;
    // }
  }
}
