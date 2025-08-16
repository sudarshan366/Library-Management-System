using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public class IssueBookController : Controller
    {
        private readonly LibraryContext _context;

        public IssueBookController(LibraryContext context)
        {
            _context = context;
        }

        // GET: IssueBook
        public async Task<IActionResult> IssueBook()
        {
            var libraryContext = _context.IssueBooks.Include(i => i.Book).Include(i => i.Student).ToList(); 
            return View(libraryContext);


        }

        // GET: IssueBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueBook = await _context.IssueBooks
                .Include(i => i.Book)
                .Include(i => i.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueBook == null)
            {
                return NotFound();
            }

            return View(issueBook);
        }

        // GET: IssueBook/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: IssueBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,StudentId,IssueDate,ReturnDate,IsReturned")] IssueBook issueBook)
        {
            if (ModelState.IsValid)
            {
                // ✅ Force UTC on IssueDate and ReturnDate
                issueBook.IssueDate = DateTime.SpecifyKind(issueBook.IssueDate, DateTimeKind.Utc);
                if (issueBook.ReturnDate.HasValue)
                {
                    issueBook.ReturnDate = DateTime.SpecifyKind(issueBook.ReturnDate.Value, DateTimeKind.Utc);
                }

                _context.Add(issueBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IssueBook));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", issueBook.BookId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", issueBook.StudentId);
            return View(issueBook);
        }

        // GET: IssueBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueBook = await _context.IssueBooks.FindAsync(id);
            if (issueBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", issueBook.BookId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", issueBook.StudentId);
            return View(issueBook);
        }

        // POST: IssueBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,StudentId,IssueDate,ReturnDate,IsReturned")] IssueBook issueBook)
        {
            if (id != issueBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ✅ Force UTC here as well
                    issueBook.IssueDate = DateTime.SpecifyKind(issueBook.IssueDate, DateTimeKind.Utc);
                    if (issueBook.ReturnDate.HasValue)
                    {
                        issueBook.ReturnDate = DateTime.SpecifyKind(issueBook.ReturnDate.Value, DateTimeKind.Utc);
                    }

                    _context.Update(issueBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueBookExists(issueBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IssueBook)); 
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", issueBook.BookId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", issueBook.StudentId);
            return View(issueBook);
        }

        // GET: IssueBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueBook = await _context.IssueBooks
                .Include(i => i.Book)
                .Include(i => i.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueBook == null)
            {
                return NotFound();
            }

            return View(issueBook);
        }

        // POST: IssueBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueBook = await _context.IssueBooks.FindAsync(id);
            if (issueBook != null)
            {
                _context.IssueBooks.Remove(issueBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IssueBook));
        }

        private bool IssueBookExists(int id)
        {
            return _context.IssueBooks.Any(e => e.Id == id);
        }
    }
}
