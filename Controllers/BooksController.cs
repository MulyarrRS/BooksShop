using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksShop.Models;


namespace BooksShop.Controllers
{
    public class BooksController : Controller
    {
        private BookContext db;
        public BooksController(BookContext context) 
        {
            db = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
