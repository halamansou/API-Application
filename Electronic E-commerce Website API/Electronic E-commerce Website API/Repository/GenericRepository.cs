using Electronic_E_commerce_Website_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Electronic_E_commerce_Website_API.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        ECommerceApiContext db;

        public GenericRepository(ECommerceApiContext db)
        {
            this.db = db;
        }

        public List<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public TEntity GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity) {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }


        public void Delete(int id) {
            TEntity obj = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(obj);
        }

        public void Save() {
            db.SaveChanges();
        }



        public IQueryable<TEntity> Get(Func<TEntity, bool> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
    }
}
