using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // получаем обьект из БД
        public async Task<IActionResult> Index()
        {
            return View(await db.Books.ToListAsync());

        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Book book) 
        {
            db.Books.Add(book); // добавляем в БД книгу
            await db.SaveChangesAsync(); // сохраняем книгу
            return RedirectToAction("Index");
        
        }
    }
}
