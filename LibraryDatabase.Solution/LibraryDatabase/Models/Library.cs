using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace LibraryDatabase.Models
{
  public static class Library
  {
    public const double CheckoutLength = 14;
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

    public static void Checkout(int bookId, int patronId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE books SET available_amount = available_amount - 1 WHERE available_amount > 0; INSERT INTO checkouts (book_id, patron_id, due_date, returned) VALUES (@bookId, @patronId, @dueDate, false);";
      cmd.Parameters.Add(new MySqlParameter("@bookId", bookId));
      cmd.Parameters.Add(new MySqlParameter("@patronId", patronId));
      cmd.Parameters.Add(new MySqlParameter("@dueDate", DateTime.Now.AddDays(CheckoutLength).ToString("yyyy-MM-dd")));
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Book> GetOverDueBooks()
    {
      List<Book> output = new List<Book>{};
      string todaysDate = DateTime.Now.ToString("yyyy-MM-dd");
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM checkouts JOIN books ON(checkouts.book_id = books.id) WHERE returned = false AND checkouts.due_date < @currentDate;";
      cmd.Parameters.Add(new MySqlParameter("@currentDate", todaysDate));
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
