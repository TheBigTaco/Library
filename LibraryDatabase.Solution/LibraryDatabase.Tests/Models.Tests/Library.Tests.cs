using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDatabase.Models;

namespace LibraryDatabase.Models.Tests
{
  [TestClass]
  public class LibraryTests : IDisposable
  {
    private Book NameOfTheWind = new Book("Name Of The Wind");
    private Book WiseMansFear = new Book("Wise Mans Fear");
    private Author patrick = new Author("Patrick Rothfusalsdkjf;aksdhfj");
    private Author dr = new Author("Doctor SUESsSsSsSs");
    private Patron me = new Patron("Patreon");

    public void Dispose()
    {
      Library.ClearAll();
    }
    public LibraryTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    [TestMethod]
    public void AddAuthorToBook_AddsAuthorToBook_AuthorAddedToBook()
    {
      NameOfTheWind.Save();
      patrick.Save();
      Library.AddAuthorToBook(patrick.Id, NameOfTheWind.Id);
      List<Author> result = Book.GetAuthors(NameOfTheWind.Id);
      List<Author> testList = new List<Author>{patrick};
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void AddAuthorToBook_AddsAuthorsToBook_AuthorAddedToBook()
    {
      NameOfTheWind.Save();
      patrick.Save();
      dr.Save();
      Library.AddAuthorToBook(patrick.Id, NameOfTheWind.Id);
      Library.AddAuthorToBook(dr.Id, NameOfTheWind.Id);
      List<Author> result = Book.GetAuthors(NameOfTheWind.Id);
      List<Author> testList = new List<Author>{patrick, dr};
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void AddAuthorToBook_AddsBooksToAuthor_AuthorAddedToBook()
    {
      NameOfTheWind.Save();
      patrick.Save();
      WiseMansFear.Save();
      Library.AddAuthorToBook(patrick.Id, NameOfTheWind.Id);
      Library.AddAuthorToBook(patrick.Id, WiseMansFear.Id);
      List<Book> result = Author.GetBooks(patrick.Id);
      List<Book> testList = new List<Book>{NameOfTheWind, WiseMansFear};
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Checkout_CheckoutBookToPatron_CheckedOutBook()
    {
      NameOfTheWind.Save();
      me.Save();
      Library.Checkout(NameOfTheWind.Id, me.Id);
      List<Book> result = Patron.GetCheckedOutBooks(me.Id);
      List<Book> testList = new List<Book>{NameOfTheWind};

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
