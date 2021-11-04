using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksShop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public string AutorBook { get; set; }
        public int YearPublication { get; set; }
    }
}
