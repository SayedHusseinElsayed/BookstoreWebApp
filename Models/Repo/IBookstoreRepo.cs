using System.Collections.Generic;

namespace Bookstore.Models.Repo
{
     public interface IBookstoreRepo<TEntity>
    {
        IList<TEntity> list();

        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int Id, TEntity entity);
        void Delete(int id);
        public List<TEntity> Search(string term);
    }
}
