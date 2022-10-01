using Microsoft.EntityFrameworkCore;
using OMS.Data.Access.Helpers;
using OMS.Data.Model.Entities;

namespace OMS.Data.Access.DAL
{
    public class OMSDbContext : DbContext
    {
        public OMSDbContext(DbContextOptions<OMSDbContext> options) : base(options)
        {
            //Database.EnsureCreated(); 
        }
        // Представление набора сущностей
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mappings = MappingsHelper.GetMainMappings();

            foreach (var mapping in mappings)
            {
                mapping.Visit(modelBuilder);
            }
        }
    }
}