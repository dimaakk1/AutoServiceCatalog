using AutoServiceCatalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Db
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options) : base(options) { }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartDetail> PartDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PartSupplier> PartSupplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>()
                .HasOne(p => p.PartDetail)
                .WithOne(d => d.Part)
                .HasForeignKey<PartDetail>(d => d.PartId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Parts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<PartSupplier>()
                .HasKey(ps => new { ps.PartId, ps.SupplierId });

            modelBuilder.Entity<PartSupplier>()
                .HasOne(ps => ps.Part)
                .WithMany(p => p.PartSuppliers)
                .HasForeignKey(ps => ps.PartId);

            modelBuilder.Entity<PartSupplier>()
                .HasOne(ps => ps.Supplier)
                .WithMany(s => s.PartSuppliers)
                .HasForeignKey(ps => ps.SupplierId);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Part>()
                .HasIndex(p => p.Name);
        }
    }
}
