using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repo
{
    public class AuthorDBRepo :IBookstoreRepo<Author>
    {
        BookstoreDBContext db;
        public AuthorDBRepo(BookstoreDBContext _db)
        {
         db= _db;
        }
        public void Add(Author entity)
        {
            db.Author.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Author.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Author.SingleOrDefault(author => author.Id == id);
            return author;
        }

        public IList<Author> list()
        {
            return db.Author.ToList();
        }

        public List<Author> Search(string term)
        {
            return db.Author.Where(a => a.Fullname.Contains(term)).ToList();
        }

        public void Update(int Id, Author entity)
        {
            db.Update(entity);
            db.SaveChanges();

        }


    }
}

