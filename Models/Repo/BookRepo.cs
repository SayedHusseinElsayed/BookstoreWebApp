using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repo
{
    public class BookRepo : IBookstoreRepo<Book>
    {
        List<Book> books;
        public BookRepo()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id=1,Title="C# programming", Description="No Description", Author = new Author{ Id=2, Fullname="Sayed"}
                },
                new Book
                {
                    Id=2,Title="python Programming", Description="No Description", Author = new Author{Id=3, Fullname = "Ahmed" }
                },
                new Book
                {
                    Id=3,Title="SQL SERVER", Description="No Description", Author = new Author{ Id=1, Fullname = "Alaa"}
                },

            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id)+1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(book => book.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(b => b.Title.Contains(term)).ToList();
        }

        public void Update(int Id, Book entity)
        {
            var book = Find(Id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
            book.imageUrl = entity.imageUrl;

        }
    }
}
