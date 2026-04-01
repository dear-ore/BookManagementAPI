using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    public class UpdateBookDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }

        [Range(10, 50000)]
        public decimal Price { get; set;  }
        public bool IsAvailable { get; set; }
        public DateOnly PublishedDate { get; set; }
    }
}
