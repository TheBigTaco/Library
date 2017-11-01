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
