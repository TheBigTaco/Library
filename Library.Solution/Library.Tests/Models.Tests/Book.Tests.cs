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
      Assert.AreEqual(NameOfTheWind, NameOfTheWind);
    }
  }
}
