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
    // private Book NameOfTheWind = new Book("Name Of The Wind");
    // private Book WiseMansFear = new Book("Wise Mans Fear");

    public void Dispose()
    {
      Library.ClearAll();
    }
    public LibraryTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
  }
}
