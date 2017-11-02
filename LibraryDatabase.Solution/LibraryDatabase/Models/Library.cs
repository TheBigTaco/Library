using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace LibraryDatabase.Models
{
  public static class Library
  {
    public static void AddAuthorToBook(int authorId, int bookId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id) VALUES (@authorId, @bookId);";

      cmd.Parameters.Add(new MySqlParameter("@authorId", authorId));
      cmd.Parameters.Add(new MySqlParameter("@bookId", bookId));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors_books; DELETE FROM checkouts; DELETE FROM authors; DELETE FROM books; DELETE FROM patrons;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
