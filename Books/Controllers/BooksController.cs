using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookData _bookData;

        public BooksController(BookData bookData)
        {
            _bookData = bookData;
        }

        public IActionResult Book(int id)
        {
            var book = _bookData.GetBook(id);
            return View(book);
        }

        [HttpGet]
        public IActionResult NewBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewBook(Book book)
        {
            _bookData.CreateBook(book);
            return Redirect("/home/index");
        }
    }
}
