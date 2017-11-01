using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LibraryDatabase.Models;

namespace LibraryDatabase.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
