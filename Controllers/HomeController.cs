using BooksShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

namespace BooksShop.Controllers
{
  
 
    public class HomeController : Controller
    {
            [Authorize]
            public IActionResult Index()
            {
                if (User.Identity.IsAuthenticated) 
                {

                    return Content(User.Identity.Name);


                }
                
                return Content("Не аутентифицирован");  
            }
    }
 
}
