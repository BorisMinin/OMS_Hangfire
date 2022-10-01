using Microsoft.EntityFrameworkCore;
using OMS.Data.Model.Entities;

namespace OMS.Data.Access.Maps.EntityMaps
{
    public class OrderMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(x => x.OrderId);
                
            builder.Entity<Order>()
                .Property(od => od.OrderDate)
                .HasColumnType("datetime");

            builder.Entity<Order>()
                .Property(rd => rd.RequiredDate)
                .HasColumnType("datetime");

            builder.Entity<Order>()
                .Property(od => od.OrderDate)
                .HasColumnType("datetime");

            builder.Entity<Order>()
                .Property(sd => sd.ShippedDate)
                .HasColumnType("datetime");

            builder.Entity<Order>()
                .Property(f => f.Freight)
                .HasColumnType("money");

            builder.Entity<Order>()
                .Property(sn => sn.ShipName)
                .HasColumnType("nvarchar(40)");

            builder.Entity<Order>()
                .Property(sa => sa.ShipAddress)
                .HasColumnType("nvarchar(60)");

            builder.Entity<Order>()
                .Property(sc => sc.ShipCity)
                .HasColumnType("nvarchar(15)");

            builder.Entity<Order>()
                .Property(sr => sr.ShipRegion)
                .HasColumnType("nvarchar(15)");

            builder.Entity<Order>()
                .Property(spc => spc.ShipPostalCode)
                .HasColumnType("nvarchar(10)");

            builder.Entity<Order>()
                .Property(sc => sc.ShipCountry)
                .HasColumnType("nvarchar(15)");

        }
    }
}