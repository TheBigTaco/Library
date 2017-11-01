using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace LibraryDatabase.Models
{
  public class Book
  {
    public int Id {get; private set;}
    public string Title {get;}

    public Book (string title, int id = 0)
    {
      Id = id;
      Title = title;
    }

    public override bool Equals(object other)
    {
      if (!(other is Book))
      {
        return false;
      }
      else
      {
        Book otherBook = (Book)other;
        return this.Title == otherBook.Title;
      }
    }
    public override int GetHashCode()
    {
      return this.Title.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books (title) VALUES (@title);";
      cmd.Parameters.Add(new MySqlParameter("@title", Title));
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Book> GetAll()
    {
      List<Book> output = new List<Book> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string title = rdr.GetString(1);
        output.Add(new Book(title, id));
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static Book Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books WHERE id = @id;";
      cmd.Parameters.Add(new MySqlParameter("@id", searchId));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string title = "";
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        title = rdr.GetString(1);
      }
      Book output = new Book(title, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }
  }
}
