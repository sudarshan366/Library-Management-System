using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
  
    public class BookController : Controller
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        // READ - List all books
        [HttpGet]
        public IActionResult Books()
        {
            var books = _context.Books.OrderBy(b => b.Id).ToList();
           
            return View(books);
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            if (id != null)
            {
                var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);
                return View(bookInDb);
            }

            return View();
        }
        
        public IActionResult CreateOrEdit(Books book)
        {
            if (book.Id == 0)
            {
                _context.Books.Add(book);

            }
            else

            {
                _context.Books.Update(book);
            }



            _context.SaveChanges();

            return RedirectToAction("Books");
        }

        public IActionResult DeleteBook(int id)
        {

            var books = _context.Books.SingleOrDefault(books => books.Id == id);
            _context.Books.Remove(books);
            return RedirectToAction("Books");
        }


    }
}