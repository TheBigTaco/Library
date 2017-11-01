using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;

namespace Library.Models.Tests
{
  [TestClass]
  public class BookTests : IDisposable
  {
    private Book NameOfTheWind = new Book("Name Of The Wind");
    private Book WiseMansFear = new Book("Wise Mans Fear");

    public void Dispose()
    {
      Book.ClearAll();
    }
    public BookTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    [TestMethod]
    public void Equals_EqualsOveride_True()
    {
      Book NameOfTheWind2 = new Book("Name Of The Wind");
      Assert.AreEqual(NameOfTheWind, NameOfTheWind2);
    }
    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_EmptyList()
    {
      int result = Book.GetAll().Count;

      Assert.AreEqual(0,result);
    }
    [TestMethod]
    public void Save_SaveBookToDatabase_Book()
    {
      NameOfTheWind.Save();
      Book result = Book.GetAll()[0];

      Assert.AreEqual(NameOfTheWind, result);
    }
    [TestMethod]
    public void Find_FindBookInDatabase_Book()
    {
      NameOfTheWind.Save();
      WiseMansFear.Save();

      Book result = Book.Find(NameOfTheWind.Id);

      Assert.AreEqual(NameOfTheWind, result);
    }
  }
}
