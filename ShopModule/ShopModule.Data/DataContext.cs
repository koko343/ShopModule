using ShopModule.Core.Entities;
using ShopModule.Core.Interfaces;
using System.Data.Entity;

namespace ShopModule.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base("Connection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products").HasKey(q => q.Id);
            modelBuilder.Entity<Category>().ToTable("Categories").HasKey(q => q.Id);
            modelBuilder.Entity<Picture>().ToTable("Pictures").HasKey(q => q.Id);
        }
    }
}
