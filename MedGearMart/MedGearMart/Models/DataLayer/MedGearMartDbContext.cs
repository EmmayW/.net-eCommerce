
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MedGearMart.Models.DomainModel;
using System.Diagnostics;

namespace MedGearMart.Models.DataLayer
{
    public class MedGearMartDbContext : IdentityDbContext<AppUser>
    {
        public MedGearMartDbContext(DbContextOptions<MedGearMartDbContext> options) : base(options)
        {
        }
        public DbSet<Gear> Gears { get; set; }
        public DbSet<Cart> Carts { get; set; }
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

            modelBuilder.Entity<Cart>()
             .HasOne(c => c.Product) // Cart has one Gear
             .WithMany() // Gear does not need to know about multiple Carts
             .HasForeignKey(c => c.ProductId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
            .HasOne(c => c.User) 
            .WithMany(a=>a.Carts) 
            .HasForeignKey(c => c.UserId)
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
        }



    }
}
