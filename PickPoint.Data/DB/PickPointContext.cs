using Microsoft.EntityFrameworkCore;

using PickPoint.Data.Entities;
using PickPoint.Data.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PickPoint.Data.DB
{
    public class PickPointContext : DbContext
    {
        public PickPointContext(DbContextOptions<PickPointContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
               .HasKey(e => e.Id);
            modelBuilder.Entity<Order>()
                .Property(e => e.Items)
                //.HasConversion(v=> string.Join(" | ", v), v => v.Split(" | ", StringSplitOptions.RemoveEmptyEntries).ToArray());
                .HasConversion(v => JsonSerializer.Serialize(v, null), v => JsonSerializer.Deserialize<IEnumerable<string>>(v, null).ToArray());
            
            modelBuilder.Entity<Postamate>().HasData(FakeDataCreator.GetPostamates());

            modelBuilder.Entity<Order>().HasData(FakeDataCreator.GetOrders());
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Postamate> Postamates { get; set; }
    }
}
