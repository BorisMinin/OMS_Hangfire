using Microsoft.EntityFrameworkCore;
using OMS.Data.Model.Entities;

namespace OMS.Data.Access.Maps.EntityMaps
{
    public class CategoryMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .ToTable("Categories")
                .Property(cn => cn.CategoryName)
                .HasColumnType("nvarchar(15)");

            builder.Entity<Category>()
                .Property(d => d.Description)
                .HasColumnType("ntext");
        }
    }
}