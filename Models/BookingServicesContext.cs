using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Models
{
    public class BookingServicesContext : DbContext
    {
        public BookingServicesContext() { }
        public BookingServicesContext(DbContextOptions<BookingServicesContext> options) : base(options) { }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);     
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ServiceType).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Content).HasColumnType("ntext");
                entity.Property(e => e.Rate).HasColumnType("int");
                entity.Property(e => e.CreatedAt).HasMaxLength(20);
            });
        }
    }
}
