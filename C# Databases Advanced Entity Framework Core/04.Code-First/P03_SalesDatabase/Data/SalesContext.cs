using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data
{
    class SalesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigirationOnCustomer(modelBuilder);
            ConfigurationOnProduct(modelBuilder);
            ConfigurationOnSale(modelBuilder);
            ConfigurationOnStore(modelBuilder);
        }

        private void ConfigurationOnStore(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Store>()
                .HasKey(k => k.StoreId);
            modelBuilder
                .Entity<Store>()
                .Property(p => p.Name)
                .HasMaxLength(80)
                .IsUnicode();
            modelBuilder
                .Entity<Store>()
                .HasMany(s => s.Sales)
                .WithOne(st => st.Store)
                .HasForeignKey(k => k.SaleId);
            
        }

        private void ConfigurationOnSale(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Sale>()
                .HasKey(k => k.SaleId);
            modelBuilder
                .Entity<Sale>()
                .Property(p => p.Date)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder
                .Entity<Sale>()
                .HasOne(p => p.Product)
                .WithMany(s => s.Sales)
                .HasForeignKey(k => k.ProductId);
            modelBuilder
                .Entity<Sale>()
                .HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(k => k.CustomerId);
            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Sales)
                .HasForeignKey(k => k.StoreId);

        }

        private void ConfigurationOnProduct(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasKey(k => k.ProductId);
            modelBuilder
                .Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode();
            modelBuilder
                .Entity<Product>()
                .Property(p => p.Quantity);
            modelBuilder
                .Entity<Product>()
                .Property(p => p.Price);
            modelBuilder
                .Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(250)
                .IsUnicode();
            modelBuilder
                .Entity<Product>()
                .HasMany(s => s.Sales)
                .WithOne(p => p.Product)
                .HasForeignKey(k => k.ProductId);

        }

        private void ConfigirationOnCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .HasKey(k => k.CustomerId);
            modelBuilder
                .Entity<Customer>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode();
            modelBuilder
               .Entity<Customer>()
               .Property(p => p.Email)
               .HasMaxLength(80)
               .IsUnicode();
            modelBuilder
               .Entity<Customer>()
               .Property(p => p.CreditCardNumber);
            modelBuilder
                .Entity<Customer>()
                .HasMany(s => s.Sales)
                .WithOne(c => c.Customer)
                .HasForeignKey(k => k.CustomerId);

        }
    }
}
