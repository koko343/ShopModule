using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ShopModule.Core.Interfaces
{
    public interface IDataContext
    {
        /// Gets the database.
        Database Database { get; }

        /// Gets the configuration.
        DbContextConfiguration Configuration { get; }

        /// Gets the change tracker.
        DbChangeTracker ChangeTracker { get; }

        /// Saves the changes.
        int SaveChanges();

        /// Entries the specified entity.
        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        /// Sets this instance.
        DbSet<T> Set<T>() where T : class;
    }
}
