using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(10, 50000)]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateOnly PublishedDate { get; set; }
    }
}
