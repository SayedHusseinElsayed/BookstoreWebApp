using Microsoft.EntityFrameworkCore;
namespace Bookstore.Models
{
    public class BookstoreDBContext: DbContext
    {
        public BookstoreDBContext(DbContextOptions<BookstoreDBContext> options):base(options) 
        {

        }
        public DbSet<Book> Books { get; set;}
        public DbSet<Author> Author { get; set;}    
       
    }
}
