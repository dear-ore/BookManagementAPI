using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using BookManagementAPI.DTOs;
using BookManagementAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BooksController(BookDbContext context)
        {
            _context = context;
        }

        //private static List<Book> SomeBooks =
        //    new()
        //    {
        //        new Book { Id = 1, Title = "Things Fall Apart", Author = "Chinua Achebe", Genre = "Fiction", Price = 7000, IsAvailable = true, PublishedDate = new DateOnly (1958, 1, 23)},
        //        new Book { Id = 2, Title = "Fairy tales", Author = "Hans Christian Andersen", Genre = "Fiction", Price = 10000, IsAvailable = true, PublishedDate = new DateOnly (1836, 3, 29)},
        //        new Book { Id = 3, Title = "The Divine Comedy", Author = "Dante Alighieri", Genre = "Comedy", Price = 4000, IsAvailable = true, PublishedDate = new DateOnly (1315, 9, 13)}
        //    };

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var response = _context.Books.Select(book => new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                IsAvailable = book.IsAvailable,
                Price = book.Price, 
                PublishedDate = book.PublishedDate,
            }).ToList();

           return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        { 

            var somebook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (somebook == null)
                return NotFound();
            else
            {
                BookResponseDto response = new BookResponseDto();
                response.Id = somebook.Id;
                response.Title = somebook.Title;
                response.Author = somebook.Author;
                response.Genre = somebook.Genre;
                response.IsAvailable = somebook.IsAvailable;
                response.Price = somebook.Price;
                response.PublishedDate = somebook.PublishedDate;
                return Ok(response);
            }          
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddNewBook([FromBody] CreateBookDto newBook)
        {
            var bookObj = new Book();
            
            bookObj.Title = newBook.Title;
            bookObj.Author = newBook.Author;
            bookObj.Genre = newBook.Genre;
            bookObj.Price = newBook.Price;
            bookObj.IsAvailable = newBook.IsAvailable;
            bookObj.PublishedDate = newBook.PublishedDate;
            _context.Books.Add(bookObj);
            _context.SaveChanges();

            var response = new BookResponseDto();
            response.Id = bookObj.Id;
            response.Title = bookObj.Title;
            response.Author = bookObj.Author;
            response.Genre = bookObj.Genre;
            response.IsAvailable = bookObj.IsAvailable;
            response.Price = bookObj.Price;
            response.PublishedDate = bookObj.PublishedDate;
            
            return CreatedAtAction(nameof(GetBookById), new { id = response.Id}, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditBook(int id, [FromBody] UpdateBookDto bookToEdit)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Author = bookToEdit.Author;
            existingBook.PublishedDate = bookToEdit.PublishedDate;
            existingBook.IsAvailable = bookToEdit.IsAvailable;
            existingBook.Title = bookToEdit.Title;
            existingBook.Genre = bookToEdit.Genre;
            existingBook.Price = bookToEdit.Price;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
  
            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
