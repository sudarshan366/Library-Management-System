using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace LibraryManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly LibraryContext _context;

        public StudentController(LibraryContext context)
        {
            _context = context;
        }

        
        [HttpGet()]
        public IActionResult Student()
        {
            var students = _context.Student.OrderBy(b => b.Id).ToList();
            return View(students);
            
        }
        [HttpGet]
        public IActionResult CreateEdit(int? id)
        {
            if (id != null)
            {
                var studentInDB = _context.Student.SingleOrDefault(b => b.Id == id);
                return View(studentInDB);
            }

            return View();
        }
            [HttpPost]
        public IActionResult CreateEdit(Student student)
        {
            

            if (student.Id == 0)
            {
                _context.Student.Add(student);
            }
            else
            {
                _context.Student.Update(student);
            }

            _context.SaveChanges();

            return RedirectToAction("Student");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _context.Student.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Student.Find(id);
            if (student != null)
            {
                _context.Student.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction("Student");
        }

    }
}
      


