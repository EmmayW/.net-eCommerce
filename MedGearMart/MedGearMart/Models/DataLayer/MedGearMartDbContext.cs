
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MedGearMart.Models.DomainModel;
using System.Diagnostics;
using MedGearMart.Models.DataLayer.SeedData;

namespace MedGearMart.Models.DataLayer
{
    public class MedGearMartDbContext : IdentityDbContext<AppUser>
    {
        public MedGearMartDbContext(DbContextOptions<MedGearMartDbContext> options) : base(options)
        {
        }
        public DbSet<Gear> Gears { get; set; }
       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gear>()
           .HasOne(t => t.Category)
           .WithMany(a => a.Gears)
           .HasForeignKey(t => t.CategoryId)
           .OnDelete(DeleteBehavior.Restrict);

         

            modelBuilder.Entity<OrderItem>()
           .HasOne(t => t.Order)
           .WithMany()
           .HasForeignKey(t => t.OrderId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product) // OrderItem has one Gear
                .WithMany() // Gear can be associated with many OrderItems
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);





            base.OnModelCreating(modelBuilder);

            
            CategorySeedData.Initialize(modelBuilder);
            GearSeedData.Initialize(modelBuilder);
        }



    }
}
