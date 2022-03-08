using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required (ErrorMessage = "Please provide proper book name")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please provide correct author name")]
		public string AuthorName { get; set; }
		[Required(ErrorMessage = "Please select genre")]
		public Genre Genre { get; set; }
		public byte GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		[Required(ErrorMessage = "Please provide data")]
		[Display(Name="Release Date")]
		public DateTime ReleaseDate { get; set; }
		[Display(Name="Number in Stock")]
		[Range(1, 20, ErrorMessage = "Number of stock must be bettwen 1 and 20")]
		[Required(ErrorMessage = "Please provide a number of books")]
		public int NumberInStock { get; set; }
		public int NumberAvailable { get; set; }
	}

}
