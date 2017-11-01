using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDatabase.Models;

namespace LibraryDatabase.Models.Tests
{
  [TestClass]
  public class AuthorTests : IDisposable
  {
    private Author bill = new Author("Bill Billson");
    private Author otherBill = new Author("Bill Billson");
    private Author jim = new Author("Jim Jimson");
    private Author patrick = new Author("Patrick Rothejaksl");

    public void Dispose()
    {
      Library.ClearAll();
    }
    public AuthorTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    [TestMethod]
    public void Equals_AreTheSame_True()
    {
      Assert.AreEqual(bill, otherBill);
    }
    [TestMethod]
    public void Save_AddToDatabase_EntryAdded()
    {
      patrick.Save();

      int result = Author.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_FindsInDatabase_EntryFound()
    {
      patrick.Save();
      bill.Save();
      otherBill.Save();

      Author result = Author.Find(bill.Id);

      Assert.AreEqual(bill, result);
    }
  }
}
