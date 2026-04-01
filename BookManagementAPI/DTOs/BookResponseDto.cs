using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public DateOnly PublishedDate { get; set; }
    }
}
