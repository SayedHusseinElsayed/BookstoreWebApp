using Bookstore.Models.Repo;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class BookDBRepo:IBookstoreRepo<Book>
    {
        BookstoreDBContext db;
        public BookDBRepo(BookstoreDBContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity); 
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(book => book.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int Id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();

        }

        public List<Book> Search(string term)
        {
            var results = db.Books.Include(a => a.Author).Where(b => b.Title.Contains(term) || b.Description.Contains(term) || b.Author.Fullname.Contains(term)).ToList();
            return results;

        }
    }
}
