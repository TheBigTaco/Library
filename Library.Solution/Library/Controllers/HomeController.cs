using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Controllers
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
