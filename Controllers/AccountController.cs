using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BooksShop.ViewModels; 
using BooksShop.Models;




namespace BooksShop.Controllers
{
    public class AccountController : Controller
    {
        private UserContext dataBase;
        public AccountController(UserContext context) 
        {
            dataBase = context;
        
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // добавление пользователя
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel modelLog)
        {
            if (ModelState.IsValid)
            {
                User user = await dataBase.Users.FirstOrDefaultAsync(u => u.Email == modelLog.Email && u.Password == modelLog.Password);
                if (user != null)
                {
                    await Authenticate(modelLog.Email); 

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(modelLog);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        // добавляем пользователя в бд
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel modelLog)
        {
            if (ModelState.IsValid)
            {
                User user = await dataBase.Users.FirstOrDefaultAsync(u => u.Email == modelLog.Email);
                if (user == null)
                {

                    dataBase.Users.Add(new User { Email = modelLog.Email, Password = modelLog.Password });
                    await dataBase.SaveChangesAsync();

                    await Authenticate(modelLog.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(modelLog);
        }
        // создание куки пользователя
        private async Task Authenticate(string userName)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        // Выход из сайта
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

 
