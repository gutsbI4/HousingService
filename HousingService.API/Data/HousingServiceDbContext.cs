using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HousingService.API.Models;

namespace HousingService.API.Data
{
    public partial class HousingServiceDbContext : DbContext
    {
        public HousingServiceDbContext()
        {
        }

        public HousingServiceDbContext(DbContextOptions<HousingServiceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartments> Apartments { get; set; } = null!;
        public virtual DbSet<Cities> Cities { get; set; } = null!;
        public virtual DbSet<Houses> Houses { get; set; } = null!;
        public virtual DbSet<Streets> Streets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartments>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.HouseId).HasColumnName("house_id");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("FK_Apartments_Houses");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Houses>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .HasColumnName("number");

                entity.Property(e => e.StreetId).HasColumnName("street_id");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.StreetId)
                    .HasConstraintName("FK_Houses_Streets");
            });

            modelBuilder.Entity<Streets>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Streets_Cities");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
