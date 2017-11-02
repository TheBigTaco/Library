using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDatabase.Models;

namespace LibraryDatabase.Models.Tests
{
  [TestClass]
  public class PatronTests : IDisposable
  {
    private Patron bill = new Patron("Bill Billson");
    private Patron otherBill = new Patron("Bill Billson");
    private Patron jim = new Patron("Jim Jimson");
    private Patron patrick = new Patron("Patrick Rothejaksl");
    private Book book = new Book ("book");

    public void Dispose()
    {
      Library.ClearAll();
    }
    public PatronTests()
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

      int result = Patron.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_FindsInDatabase_EntryFound()
    {
      patrick.Save();
      bill.Save();
      otherBill.Save();

      Patron result = Patron.Find(bill.Id);

      Assert.AreEqual(bill, result);
    }
    [TestMethod]
    public void GetDueDate_GetBookDueDate_DateTime()
    {
      patrick.Save();
      book.Save();

      Library.Checkout(book.Id, patrick.Id);
      string result = Patron.GetDueDate(book.Id, patrick.Id).ToString("yyyy-MM-dd");
      string test = DateTime.Now.AddDays(Library.CheckoutLength).ToString("yyyy-MM-dd");

      Assert.AreEqual(test, result);
    }
  }
}
