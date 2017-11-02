using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace LibraryDatabase.Models
{
  public class Patron
  {
    public int Id {get; private set;}
    public string Name {get;}

    public Patron (string name, int id = 0)
    {
      Id = id;
      Name = name;
    }

    public override bool Equals(object other)
    {
      if (!(other is Patron))
      {
        return false;
      }
      else
      {
        Patron otherPatron = (Patron)other;
        return this.Name == otherPatron.Name;
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO patrons (name) VALUES (@name);";
      cmd.Parameters.Add(new MySqlParameter("@name", Name));
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Patron> GetAll()
    {
      List<Patron> output = new List<Patron> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patrons;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        output.Add(new Patron(name, id));
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static Patron Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patrons WHERE id = @id;";
      cmd.Parameters.Add(new MySqlParameter("@id", searchId));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string name = "";
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Patron output = new Patron(name, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static List<Book> GetCheckedOutBooks(int patronId)
    {
      List<Book> output = new List<Book>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM checkouts JOIN books ON (checkouts.book_id = books.id) WHERE checkouts.patron_id = @patronId;";
      cmd.Parameters.Add(new MySqlParameter("@patronId", patronId));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);
        Book newBook = new Book(bookTitle, bookId);
        output.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }
  }
}
