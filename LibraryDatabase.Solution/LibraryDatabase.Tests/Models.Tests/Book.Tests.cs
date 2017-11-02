using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDatabase.Models;

namespace LibraryDatabase.Models.Tests
{
  [TestClass]
  public class BookTests : IDisposable
  {
    private Book namelyBook = new Book("Name of The Wind");
    private Book namelyBookToo = new Book("Name of The Wind");
    private Book windlyBook = new Book("Windly Name the of");
    private Book patricklyBook = new Book("Patrick");
    private Patron me = new Patron ("Patreon");

    public void Dispose()
    {
      Library.ClearAll();
    }
    public BookTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    [TestMethod]
    public void Equals_AreTheSame_True()
    {
      Assert.AreEqual(namelyBook, namelyBookToo);
    }
    [TestMethod]
    public void Save_AddToDatabase_EntryAdded()
    {
      patricklyBook.Save();

      int result = Book.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_FindsInDatabase_EntryFound()
    {
      patricklyBook.Save();
      namelyBook.Save();
      windlyBook.Save();

      Book result = Book.Find(namelyBook.Id);

      Assert.AreEqual(namelyBook, result);
    }
    [TestMethod]
    public void AddBook_AddsSameBookToAmountInLibrary_Int()
    {
      patricklyBook.Save();
      namelyBook.Save();
      patricklyBook.AddBook();
      patricklyBook.AddBook();

      Book result = Book.Find(patricklyBook.Id);

      Assert.AreEqual(3, result.TotalAmount);
    }
    [TestMethod]
    public void RemoveBook_RemovesSameBookFromAmountInLibrary_Int()
    {
      patricklyBook.Save();
      namelyBook.Save();
      patricklyBook.AddBook();
      patricklyBook.AddBook();
      patricklyBook.RemoveBook();
      patricklyBook.RemoveBook();
      patricklyBook.RemoveBook();

      Book result = Book.Find(patricklyBook.Id);

      Assert.AreEqual(0, result.TotalAmount);
    }
    [TestMethod]
    public void RemoveBook_RemovesSameBookNotNegativeFromAmountInLibrary_Int()
    {
      patricklyBook.Save();
      namelyBook.Save();
      patricklyBook.RemoveBook();
      patricklyBook.RemoveBook();
      patricklyBook.RemoveBook();

      Book result = Book.Find(patricklyBook.Id);

      Assert.AreEqual(0, result.TotalAmount);
    }
    [TestMethod]
    public void RemoveBook_RemovesCheckedOutBookFromAmountInLibrary_Int()
    {
      patricklyBook.Save();
      patricklyBook.AddBook();
      patricklyBook.AddBook();
      me.Save();
      Library.Checkout(patricklyBook.Id, me.Id);
      Library.Checkout(patricklyBook.Id, me.Id);
      patricklyBook.RemoveBook(true);

      Book result = Book.Find(patricklyBook.Id);
      Assert.AreEqual(2, result.TotalAmount);
      Assert.AreEqual(1, result.AvailableAmount);
    }
    [TestMethod]
    public void RemoveBook_RemovesCheckedOutBookNotNegativeFromAmountInLibrary_Int()
    {
      patricklyBook.Save();
      patricklyBook.AddBook();
      patricklyBook.AddBook();
      me.Save();
      Library.Checkout(patricklyBook.Id, me.Id);
      Library.Checkout(patricklyBook.Id, me.Id);
      Library.Checkout(patricklyBook.Id, me.Id);
      Library.Checkout(patricklyBook.Id, me.Id);
      patricklyBook.RemoveBook(true);

      Book result = Book.Find(patricklyBook.Id);
      Assert.AreEqual(2, result.TotalAmount);
      Assert.AreEqual(0, result.AvailableAmount);
    }
  }
}
