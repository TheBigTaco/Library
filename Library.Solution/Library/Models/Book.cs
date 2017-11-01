using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Library.Models
{
  public class Book
  {
    public int Id {get; private set;}
    public string Title {get;}

    public Book(string title, int id = 0)
    {
      Id = id;
      Title = title;
    }

    public override bool Equals(object newBook)
    {
      if(!(newBook is Book))
      {
        return false;
      }
      else
      {
        Book otherBook = (Book)newBook;
        bool titleEqual = this.Title.Equals(otherBook.Title);
        return (titleEqual);
      }
    }

    public override int GetHashCode()
    {
      return (this.Title.GetHashCode());
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books (title) VALUES (@title)";

      cmd.Parameters.Add(new MySqlParameter("@title", this.Title));

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);
        Book newBook = new Book(bookTitle, bookId);
        allBooks.Add(newBook);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allBooks;
    }

    public static Book Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"SELECT * FROM books WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bookId = 0;
      string bookTitle = "";

      while(rdr.Read())
      {
        bookId = rdr.GetInt32(0);
        bookTitle = rdr.GetString(1);
      }
      Book newBook = new Book(bookTitle, bookId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newBook;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
