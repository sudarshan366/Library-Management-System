using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
	public class IssueBook
	{

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Books? Book { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; }


    }
}

