using ShopModule.Core.Entities.Base;
using ShopModule.Core.Interfaces;
using ShopModule.Core.Interfaces.Repositories.Base;
using System.Data.Entity;
using System.Linq;

namespace ShopModule.Data.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        /// Entity framework db context
        protected readonly IDataContext dataContext;

        /// Gets the current set.
        private DbSet<T> CurrentSet
        {
            get
            {
                return this.dataContext.Set<T>();
            }
        }

        /// Initializes a new instance of the Repository class.
        public Repository(IDataContext context)
        {
            this.dataContext = context;
        }

        /// Get query for all entities
        public IQueryable<T> GetAll()
        {
            return this.CurrentSet.AsQueryable();
        }

        /// Add entity to data base
        public T Add(T entity)
        {
            this.CurrentSet.Add(entity);

            return entity;
        }

        /// Edit entity in data base
        public void Edit(T entity)
        {
            this.dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// Delete entity from data base
        public void Delete(T entity)
        {
            this.CurrentSet.Remove(entity);
        }

        public void SaveChanges()
        {
            this.dataContext.SaveChanges();
        }

        /// Get entity by id
        public T GetById(long id)
        {
            return this.CurrentSet.Find(id);
        }

        /// Update entity in data base
        public void Update(T entity)
        {
            this.dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
