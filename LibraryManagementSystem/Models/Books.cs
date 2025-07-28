using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
	public class Books
	{
		
	   public required int Id { get; set; }

        public required string Name { get; set; }

		public string AuthorName { get; set; }

		public string Publication { get; set; }

        public string Quantity { get; set; }



    }
}

