using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repo
{
    public class AuthorRepo : IBookstoreRepo<Author>
    {
        IList<Author> authors;

        public AuthorRepo()
        {
            authors = new List<Author>()
            {
                new Author() {Id=1, Fullname="Sayed"},
                new Author() {Id=2, Fullname="Khaled"},
                new Author() {Id=3, Fullname="Ghada"},
            };
        }
        public void Add(Author entity)
        {
            entity.Id= authors.Max(a => a.Id)+1;
           authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author= authors.SingleOrDefault(author => author .Id == id);   
            return author;
        }

        public IList<Author> list()
        {
           return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.Fullname.Contains(term)).ToList();

        }

        public void Update(int Id, Author entity)
        {
            var author = Find(Id);
            author.Fullname = entity.Fullname;

        }
    }
}
