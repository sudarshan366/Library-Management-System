using System;
using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models
{
	public class Student
	{
        public int Id { get; set; }

        public required string Name { get; set; }

        public string Department { get; set; }

        public string Semester { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }
    }
}

