using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class BookAuthorViewModel
    {
        public int BookID { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Desciption { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }

    }
}
