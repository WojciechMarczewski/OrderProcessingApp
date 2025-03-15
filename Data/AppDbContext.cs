using Microsoft.EntityFrameworkCore;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().OwnsOne(o => o.Product);
            modelBuilder.Entity<Order>().OwnsOne(o => o.OrderAmount, amount =>
            {
                amount.Property(a => a.Value);
                amount.HasOne(a => a.Currency).WithMany();
            });
            modelBuilder.Entity<Order>().OwnsOne(o => o.Address);
            modelBuilder.Entity<Order>().HasMany(o => o.OrderStatusHistory).WithOne();
            modelBuilder.Entity<OrderStatusChange>().Property(osc => osc.Status);
            modelBuilder.Entity<OrderStatusChange>().Property(osc => osc.TimeStamp);
        }
    }
}
