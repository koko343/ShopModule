using ShopModule.Core.Entities.Base;
using System.Linq;

namespace ShopModule.Core.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : EntityBase
    {
            /// Get query for all entities
            IQueryable<T> GetAll();

            /// Add entity to data base
            T Add(T entity);

            /// Delete entity from data base
            void Delete(T entity);

            /// Edit entity in data base
            void Edit(T entity);

            /// Seve changes
            void SaveChanges();

            /// Get entity by id
            T GetById(long id);

            /// Update entity in data base
            void Update(T sentity);
        }
}
