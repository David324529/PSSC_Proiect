using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
        }

        // Tabelele din baza de date
        public DbSet<ProductDto> Products { get; set; }
        public DbSet<CustomerDto> Customers { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<OrderItemDto> OrderItems { get; set; }
        public DbSet<InvoiceDto> Invoices { get; set; }
        public DbSet<ShipmentDto> Shipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabela Products
            modelBuilder.Entity<ProductDto>()
                .ToTable("Products")
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<ProductDto>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); 

            // Tabela Customers
            modelBuilder.Entity<CustomerDto>()
                .ToTable("Customers")
                .HasKey(c => c.CustomerId);

            // Tabela Orders
            modelBuilder.Entity<OrderDto>()
                .ToTable("Orders")
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<OrderDto>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<OrderDto>()
                .HasOne<CustomerDto>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tabela OrderItems
            modelBuilder.Entity<OrderItemDto>()
                .ToTable("OrderItems")
                .HasKey(oi => oi.OrderItemId);

            modelBuilder.Entity<OrderItemDto>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<OrderItemDto>()
                .HasOne<OrderDto>()
                .WithMany()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItemDto>()
                .HasOne<ProductDto>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            //  Tabela Invoices
            modelBuilder.Entity<InvoiceDto>()
                .ToTable("Invoices")
                .HasKey(i => i.InvoiceId);

            modelBuilder.Entity<InvoiceDto>()
                .Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<InvoiceDto>()
                .HasOne<OrderDto>()
                .WithMany()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tabela Shipments
            modelBuilder.Entity<ShipmentDto>()
                .ToTable("Shipments")
                .HasKey(s => s.ShipmentId);

            modelBuilder.Entity<ShipmentDto>()
                .HasOne<OrderDto>()
                .WithMany()
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
