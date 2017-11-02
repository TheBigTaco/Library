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
    public int TotalAmount {get; private set;}
    public int AvailableAmount {get; private set;}

    public Book (string title, int id = 0, int total = 0, int available = 0)
    {
      Id = id;
      Title = title;
      TotalAmount = total;
      AvailableAmount = available;
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
      cmd.CommandText = @"INSERT INTO books (title, total_amount, available_amount) VALUES (@title, 1, 1);";
      cmd.Parameters.Add(new MySqlParameter("@title", Title));
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      TotalAmount = 1;
      AvailableAmount = 1;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddBook()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE books SET total_amount = total_amount + 1; UPDATE books SET available_amount = available_amount + 1;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void RemoveBook(bool checkedOut = false)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE books SET total_amount = total_amount - 1 WHERE total_amount > 0;";
      if(checkedOut == false)
      {
        cmd.CommandText += @"UPDATE books SET available_amount = available_amount - 1 WHERE available_amount > 0;";
      }
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void Checkout(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE books SET available_amount = available_amount - 1 WHERE available_amount > 0;";
      cmd.ExecuteNonQuery();
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
      int total = 0;
      int available = 0;

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        title = rdr.GetString(1);
        total = rdr.GetInt32(2);
        available = rdr.GetInt32(3);
      }
      Book output = new Book(title, id, total, available);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }
    public static List<Author> GetAuthors(int bookId)
    {
      List<Author> output = new List<Author>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT authors.* FROM authors JOIN authors_books ON (authors.id = authors_books.author_id) WHERE book_id = @bookId;";
      cmd.Parameters.Add(new MySqlParameter("@bookId", bookId));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string authorName = rdr.GetString(1);
        int authorId = rdr.GetInt32(0);
        Author newAuthor = new Author(authorName, authorId);
        output.Add(newAuthor);
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
