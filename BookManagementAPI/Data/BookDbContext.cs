using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookManagementAPI.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
    }

    //public class BookDbContext : DbContext
    //{
    //    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
    //    public DbSet<Book> Books { get; set; }
    //}
}



    

    