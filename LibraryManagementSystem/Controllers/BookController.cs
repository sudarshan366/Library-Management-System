using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Books(string searchString)
        {
            var books = from b in _context.Books
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b =>
            EF.Functions.ILike(b.Name, $"%{searchString}%") ||
            EF.Functions.ILike(b.AuthorName, $"%{searchString}%"));
            }

            return View(await books.ToListAsync());
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

        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            var books = _context.Books.FirstOrDefault(b => b.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBookconfirmed(int id)
        {
            var books = _context.Books.Find(id);
            if (books != null)
            {
                _context.Books.Remove(books);
                _context.SaveChanges();
            }

            return RedirectToAction("Books");
        }


    }
}